/***************************************************************************
 *
 *   RunUO                   : May 1, 2002
 *   portions copyright      : (C) The RunUO Software Team
 *   email                   : info@runuo.com
 *   
 *   Angel Island UO Shard   : March 25, 2004
 *   portions copyright      : (C) 2004-2024 Tomasello Software LLC.
 *   email                   : luke@tomasello.com
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

/* ChangeLog:
 *	07/23/08, weaver
 *		Automated IPooledEnumerable optimizations. 1 loops updated.
 *  12/28/05, Kit
 *		Added day/night light checking to turn off light source causeing lag
	6/5/04, Pix
		Merged in 1.0RC0 code.
*/

using Server.Items;
using Server.Misc;
using Server.Targeting;
using System;
using System.Collections;

namespace Server.Spells.Fourth
{
    public class FireFieldSpell : Spell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Fire Field", "In Flam Grav",
                SpellCircle.Fourth,
                215,
                9041,
                false,
                Reagent.BlackPearl,
                Reagent.SpidersSilk,
                Reagent.SulfurousAsh
            );

        public FireFieldSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override bool CheckCast()
        {

            return base.CheckCast();
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                int dx = Caster.Location.X - p.X;
                int dy = Caster.Location.Y - p.Y;
                int rx = (dx - dy) * 44;
                int ry = (dx + dy) * 44;

                bool eastToWest;

                if (rx >= 0 && ry >= 0)
                {
                    eastToWest = false;
                }
                else if (rx >= 0)
                {
                    eastToWest = true;
                }
                else if (ry >= 0)
                {
                    eastToWest = true;
                }
                else
                {
                    eastToWest = false;
                }

                Effects.PlaySound(p, Caster.Map, 0x20C);

                int itemID = eastToWest ? 0x398C : 0x3996;

                TimeSpan duration;

                if (Core.RuleSets.AOSRules())
                    duration = TimeSpan.FromSeconds((15 + (Caster.Skills.Magery.Fixed / 5)) / 4);
                else
                    duration = TimeSpan.FromSeconds(4.0 + (Caster.Skills[SkillName.Magery].Value * 0.5));

                for (int i = -2; i <= 2; ++i)
                {
                    Point3D loc = new Point3D(eastToWest ? p.X + i : p.X, eastToWest ? p.Y : p.Y + i, p.Z);

                    new InternalItem(itemID, loc, Caster, Caster.Map, duration, i);
                }
            }

            FinishSequence();
        }

        [DispellableField]
        private class InternalItem : Item
        {
            private Timer m_Timer;
            private DateTime m_End;
            private Mobile m_Caster;

            public override bool BlocksFit { get { return true; } }

            public InternalItem(int itemID, Point3D loc, Mobile caster, Map map, TimeSpan duration, int val)
                : base(itemID)
            {
                bool canFit = SpellHelper.AdjustField(ref loc, map, 12, false);

                Visible = false;
                Movable = false;
                Light = LightType.Circle300;

                MoveToWorld(loc, map);

                m_Caster = caster;

                m_End = DateTime.UtcNow + duration;

                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
                m_Timer.Start();
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Timer != null)
                    m_Timer.Stop();
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)1); // version

                writer.Write(m_Caster);
                writer.WriteDeltaTime(m_End);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();

                switch (version)
                {
                    case 1:
                        {
                            m_Caster = reader.ReadMobile();

                            goto case 0;
                        }
                    case 0:
                        {
                            m_End = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, TimeSpan.Zero, true, true);
                            m_Timer.Start();

                            break;
                        }
                }
            }

            public override bool OnMoveOver(Mobile m)
            {
                if (Visible && m_Caster != null && SpellHelper.ValidIndirectTarget(m_Caster, m) && m_Caster.CanBeHarmful(m, false))
                {
                    m_Caster.DoHarmful(m);

                    int damage = 2;

                    if (!Core.RuleSets.AOSRules() && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0, contextObj: new object[2]))
                    {
                        damage = 1;

                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                    }

                    AOS.Damage(m, m_Caster, damage, 0, 100, 0, 0, 0, this);
                    m.PlaySound(0x208);
                }

                return true;
            }

            private class InternalTimer : Timer
            {
                private InternalItem m_Item;
                private bool m_InLOS, m_CanFit;

                private static Queue m_Queue = new Queue();

                public InternalTimer(InternalItem item, TimeSpan delay, bool inLOS, bool canFit)
                    : base(delay, TimeSpan.FromSeconds(1.0))
                {
                    m_Item = item;
                    m_InLOS = inLOS;
                    m_CanFit = canFit;

                    Priority = TimerPriority.FiftyMS;
                }

                protected override void OnTick()
                {
                    if (m_Item.Deleted)
                        return;

                    if (!m_Item.Visible)
                    {
                        if (m_InLOS && m_CanFit)
                            m_Item.Visible = true;
                        else
                            m_Item.Delete();

                        if (!m_Item.Deleted)
                        {
                            int hours, minutes;

                            Server.Items.Clock.GetTime(m_Item.Map, m_Item.X, m_Item.Y, out hours, out minutes);
                            if (hours >= 6 && hours < 22 && m_Item.Light != LightType.Empty && !SpellHelper.IsFeluccaDungeon(m_Item.Map, m_Item.Location)) //its daytime disable light
                                m_Item.Light = LightType.Empty;
                            else
                                m_Item.Light = LightType.Circle300;

                            m_Item.ProcessDelta();
                            Effects.SendLocationParticles(EffectItem.Create(m_Item.Location, m_Item.Map, EffectItem.DefaultDuration), 0x376A, 9, 10, 5029);
                        }
                    }
                    else if (DateTime.UtcNow > m_Item.m_End)
                    {
                        m_Item.Delete();
                        Stop();
                    }
                    else
                    {
                        Map map = m_Item.Map;
                        Mobile caster = m_Item.m_Caster;

                        if (map != null && caster != null)
                        {
                            IPooledEnumerable eable = m_Item.GetMobilesInRange(0);
                            foreach (Mobile m in eable)
                            {
                                if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z && SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false))
                                    m_Queue.Enqueue(m);
                            }
                            eable.Free();

                            while (m_Queue.Count > 0)
                            {
                                Mobile m = (Mobile)m_Queue.Dequeue();

                                caster.DoHarmful(m);

                                int damage = 2;

                                if (!Core.RuleSets.AOSRules() && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0, contextObj: new object[2]))
                                {
                                    damage = 1;

                                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                                }

                                AOS.Damage(m, caster, damage, 0, 100, 0, 0, 0, this);
                                m.PlaySound(0x208);
                            }
                        }
                    }
                }
            }
        }

        private class InternalTarget : Target
        {
            private FireFieldSpell m_Owner;

            public InternalTarget(FireFieldSpell owner)
                : base(12, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IPoint3D)
                    m_Owner.Target((IPoint3D)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}