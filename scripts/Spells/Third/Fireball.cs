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

/* Scripts/Spells/Base/Spell.cs
	ChangeLog:
    8/29/21, Lib
        moved DamgeDelay to CoreAI
	6/03/06, Kit
		Added damage type define
	5/15/06, Kit
		Added Min/Max damage define
	7/4/04, Pix
		Added caster to SpellHelper.Damage call so corpse wouldn't stay blue to this caster
	6/5/04, Pix
		Merged in 1.0RC0 code.
	4/01/04 changes by smerXee:
		Changed DamageDelay to 0.65
	3/25/04 changes by smerX:
		Added DamageDelay of 0.3 seconds
*/
using Server.Targeting;
using System;

namespace Server.Spells.Third
{
    public class FireballSpell : Spell
    {
        public override int MinDamage { get { return 7; } }
        public override int MaxDamage { get { return 10; } }
        public override SpellDamageType DamageType
        {
            get
            {
                return SpellDamageType.Fire;
            }
        }

        private static SpellInfo m_Info = new SpellInfo(
                "Fireball", "Vas Flam",
                SpellCircle.Third,
                203,
                9041,
                Reagent.BlackPearl
            );

        public FireballSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public override bool DelayedDamage { get { return true; } }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHSequence(m))
            {
                Mobile source = Caster;

                SpellHelper.Turn(source, m);

                SpellHelper.CheckReflect((int)this.Circle, ref source, ref m);

                double damage;

                if (Core.RuleSets.AOSRules())
                {
                    damage = GetNewAosDamage(19, 1, 5);
                }
                else
                {
                    damage = Utility.Random(MinDamage, MaxDamage);

                    if (CheckResisted(m))
                    {
                        damage *= GetResistScaler(0.75);

                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                    }

                    damage *= GetDamageScalar(m);
                }

                source.MovingParticles(m, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160);
                source.PlaySound(Core.RuleSets.AOSRules() ? 0x15E : 0x44B);

                //Pixie: 7/4/04: added caster to this so corpse wouldn't stay blue to this caster
                //Lib: 8/29/21: moved damage delay to CoreAI
                SpellHelper.Damage(TimeSpan.FromSeconds(CoreAI.SpellFireballDmgDelay), m, Caster, damage, 0, 100, 0, 0, 0);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private FireballSpell m_Owner;

            public InternalTarget(FireballSpell owner)
                : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}