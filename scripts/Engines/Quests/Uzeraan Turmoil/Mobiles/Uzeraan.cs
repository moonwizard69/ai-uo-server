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

/* Scripts\Engines\Quests\Uzeraan Turmoil\Mobiles\Uzeraan.cs
 * ChangeLog:
 *	5/29/21, Adam
 *		Update weapon/armor drop to use new weapon or armor modifiers
 *		Level 3 weapon with a 20% chance at an upgrade.
 */

using Server.Gumps;
using Server.Items;
using Server.Mobiles;


namespace Server.Engines.Quests.Haven
{
    public class Uzeraan : BaseQuester
    {
        [Constructable]
        public Uzeraan()
            : base("the Conjurer")
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Hue = 0x83F3;

            Female = false;
            Body = 0x190;
            Name = "Uzeraan";
        }

        public override void InitOutfit()
        {
            AddItem(new Robe(0x4DD));
            AddItem(new WizardsHat(0x8A5));
            AddItem(new Shoes(0x8A5));

            AddItem(new LongHair(0x455));
            AddItem(new LongBeard(0x455));

            BlackStaff staff = new BlackStaff();
            staff.Movable = false;
            AddItem(staff);
        }

        public override int GetAutoTalkRange(PlayerMobile pm)
        {
            return 3;
        }

        public override bool CanTalkTo(PlayerMobile to)
        {
            return to.Quest is UzeraanTurmoilQuest;
        }

        public override void OnTalk(PlayerMobile player, bool contextMenu)
        {
            QuestSystem qs = player.Quest;

            if (qs is UzeraanTurmoilQuest)
            {
                if (UzeraanTurmoilQuest.HasLostScrollOfPower(player))
                {
                    qs.AddConversation(new LostScrollOfPowerConversation(true));
                }
                else if (UzeraanTurmoilQuest.HasLostFertileDirt(player))
                {
                    qs.AddConversation(new LostFertileDirtConversation(true));
                }
                else if (UzeraanTurmoilQuest.HasLostDaemonBlood(player))
                {
                    qs.AddConversation(new LostDaemonBloodConversation());
                }
                else if (UzeraanTurmoilQuest.HasLostDaemonBone(player))
                {
                    qs.AddConversation(new LostDaemonBoneConversation());
                }
                else
                {
                    if (player.Profession == 2) // magician
                    {
                        Container backpack = player.Backpack;

                        if (backpack == null
                            || backpack.GetAmount(typeof(BlackPearl)) < 30
                            || backpack.GetAmount(typeof(Bloodmoss)) < 30
                            || backpack.GetAmount(typeof(Garlic)) < 30
                            || backpack.GetAmount(typeof(Ginseng)) < 30
                            || backpack.GetAmount(typeof(MandrakeRoot)) < 30
                            || backpack.GetAmount(typeof(Nightshade)) < 30
                            || backpack.GetAmount(typeof(SulfurousAsh)) < 30
                            || backpack.GetAmount(typeof(SpidersSilk)) < 30)
                        {
                            qs.AddConversation(new FewReagentsConversation());
                        }
                    }

                    QuestObjective obj = qs.FindObjective(typeof(FindUzeraanBeginObjective));

                    if (obj != null && !obj.Completed)
                    {
                        obj.Complete();
                    }
                    else
                    {
                        obj = qs.FindObjective(typeof(FindUzeraanFirstTaskObjective));

                        if (obj != null && !obj.Completed)
                        {
                            obj.Complete();
                        }
                        else
                        {
                            obj = qs.FindObjective(typeof(FindUzeraanAboutReportObjective));

                            if (obj != null && !obj.Completed)
                            {
                                Container cont = GetNewContainer();

                                if (player.Profession == 2) // magician
                                {
                                    cont.DropItem(new MarkScroll(5));
                                    cont.DropItem(new RecallScroll(5));
                                    for (int i = 0; i < 5; i++)
                                    {
                                        cont.DropItem(new RecallRune());
                                    }
                                }
                                else
                                {
                                    cont.DropItem(new Gold(300));
                                    for (int i = 0; i < 6; i++)
                                    {
                                        cont.DropItem(new NightSightPotion());
                                        cont.DropItem(new LesserHealPotion());
                                    }
                                }

                                if (!player.PlaceInBackpack(cont))
                                {
                                    cont.Delete();
                                    player.SendLocalizedMessage(1046260); // You need to clear some space in your inventory to continue with the quest.  Come back here when you have more space in your inventory.
                                }
                                else
                                {
                                    obj.Complete();
                                }
                            }
                            else
                            {
                                obj = qs.FindObjective(typeof(ReturnScrollOfPowerObjective));

                                if (obj != null && !obj.Completed)
                                {
                                    FocusTo(player);
                                    SayTo(player, 1049378); // Hand me the scroll, if you have it.
                                }
                                else
                                {
                                    obj = qs.FindObjective(typeof(ReturnFertileDirtObjective));

                                    if (obj != null && !obj.Completed)
                                    {
                                        FocusTo(player);
                                        SayTo(player, 1049381); // Hand me the Fertile Dirt, if you have it.
                                    }
                                    else
                                    {
                                        obj = qs.FindObjective(typeof(ReturnDaemonBloodObjective));

                                        if (obj != null && !obj.Completed)
                                        {
                                            FocusTo(player);
                                            SayTo(player, 1049379); // Hand me the Vial of Blood, if you have it.
                                        }
                                        else
                                        {
                                            obj = qs.FindObjective(typeof(ReturnDaemonBoneObjective));

                                            if (obj != null && !obj.Completed)
                                            {
                                                FocusTo(player);
                                                SayTo(player, 1049380); // Hand me the Daemon Bone, if you have it.
                                            }
                                            else
                                            {
                                                SayTo(player, 1049357); // I have nothing more for you at this time.
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            PlayerMobile player = from as PlayerMobile;

            if (player != null)
            {
                QuestSystem qs = player.Quest;

                if (qs is UzeraanTurmoilQuest)
                {
                    if (dropped is UzeraanTurmoilHorn)
                    {
                        // from Phenos: only for young players. Other players should get a SendMessage( "You must be young to have this item recharged." )
                        UzeraanTurmoilHorn horn = (UzeraanTurmoilHorn)dropped;

                        if (horn.Charges < 10)
                        {
                            SayTo(from, 1049384); // I have recharged the item for you.
                            horn.Charges = 10;
                        }
                        else
                        {
                            SayTo(from, 1049385); // That doesn't need recharging yet.
                        }

                        return false;
                    }

                    if (dropped is SchmendrickScrollOfPower)
                    {
                        QuestObjective obj = qs.FindObjective(typeof(ReturnScrollOfPowerObjective));

                        if (obj != null && !obj.Completed)
                        {
                            Container cont = GetNewContainer();

                            cont.DropItem(new TreasureMap(1, Map.Trammel));
                            cont.DropItem(new Shovel());
                            cont.DropItem(new UzeraanTurmoilHorn());

                            if (!player.PlaceInBackpack(cont))
                            {
                                cont.Delete();
                                player.SendLocalizedMessage(1046260); // You need to clear some space in your inventory to continue with the quest.  Come back here when you have more space in your inventory.
                                return false;
                            }
                            else
                            {
                                dropped.Delete();
                                obj.Complete();
                                return true;
                            }
                        }
                    }
                    else if (dropped is QuestFertileDirt)
                    {
                        QuestObjective obj = qs.FindObjective(typeof(ReturnFertileDirtObjective));

                        if (obj != null && !obj.Completed)
                        {
                            Container cont = GetNewContainer();

                            if (player.Profession == 2) // magician
                            {
                                cont.DropItem(new BlackPearl(20));
                                cont.DropItem(new Bloodmoss(20));
                                cont.DropItem(new Garlic(20));
                                cont.DropItem(new Ginseng(20));
                                cont.DropItem(new MandrakeRoot(20));
                                cont.DropItem(new Nightshade(20));
                                cont.DropItem(new SulfurousAsh(20));
                                cont.DropItem(new SpidersSilk(20));

                                for (int i = 0; i < 3; i++)
                                    cont.DropItem(Loot.RandomScroll(0, 23, SpellbookType.Regular));
                            }
                            else
                            {
                                cont.DropItem(new Gold(300));
                                cont.DropItem(new Bandage(25));

                                for (int i = 0; i < 5; i++)
                                    cont.DropItem(new LesserHealPotion());
                            }

                            if (!player.PlaceInBackpack(cont))
                            {
                                cont.Delete();
                                player.SendLocalizedMessage(1046260); // You need to clear some space in your inventory to continue with the quest.  Come back here when you have more space in your inventory.
                                return false;
                            }
                            else
                            {
                                dropped.Delete();
                                obj.Complete();
                                return true;
                            }
                        }
                    }
                    else if (dropped is QuestDaemonBlood)
                    {
                        QuestObjective obj = qs.FindObjective(typeof(ReturnDaemonBloodObjective));

                        if (obj != null && !obj.Completed)
                        {
                            Item reward;

                            if (player.Profession == 2) // magician
                            {
                                Container cont = GetNewContainer();

                                cont.DropItem(new ExplosionScroll(4));
                                cont.DropItem(new MagicWizardsHat());

                                reward = cont;
                            }
                            else
                            {
                                BaseWeapon weapon;
                                switch (Utility.Random(6))
                                {
                                    case 0: weapon = new Broadsword(); break;
                                    case 1: weapon = new Cutlass(); break;
                                    case 2: weapon = new Katana(); break;
                                    case 3: weapon = new Longsword(); break;
                                    case 4: weapon = new Scimitar(); break;
                                    default: weapon = new VikingSword(); break;
                                }
                                weapon.Slayer = SlayerName.Silver;
                                if (Core.RuleSets.AOSRules())
                                {
                                    BaseRunicTool.ApplyAttributesTo(weapon, 3, 20, 40);
                                }
                                else
                                {

                                    // Adam: 5/29/21
                                    // Update weapon to use new weapon or armor modifiers (Not sure the proper level, but seems to be low (if any))
                                    // https://uoex.net/wiki/Young_Player_quests
                                    // Level 3 weapon with a 20% chance at an upgrade.
                                    //weapon.DamageLevel = (WeaponDamageLevel)BaseCreature.RandomMinMaxScaled(20, 40);
                                    //weapon.AccuracyLevel = (WeaponAccuracyLevel)BaseCreature.RandomMinMaxScaled(20, 40);
                                    //weapon.DurabilityLevel = (WeaponDurabilityLevel)BaseCreature.RandomMinMaxScaled(20, 40);
                                    Loot.ImbueWeaponOrArmor(noThrottle: false, weapon, Loot.ImbueLevel.Level3 /*3*/, 0.2, false);
                                }

                                reward = weapon;
                            }

                            if (!player.PlaceInBackpack(reward))
                            {
                                reward.Delete();
                                player.SendLocalizedMessage(1046260); // You need to clear some space in your inventory to continue with the quest.  Come back here when you have more space in your inventory.
                                return false;
                            }
                            else
                            {
                                dropped.Delete();
                                obj.Complete();
                                return true;
                            }
                        }
                    }
                    else if (dropped is QuestDaemonBone)
                    {
                        QuestObjective obj = qs.FindObjective(typeof(ReturnDaemonBoneObjective));

                        if (obj != null && !obj.Completed)
                        {
                            Container cont = GetNewContainer();
                            cont.DropItem(new BankCheck(2000));
                            cont.DropItem(new EnchantedSextant());

                            if (!player.PlaceInBackpack(cont))
                            {
                                cont.Delete();
                                player.SendLocalizedMessage(1046260); // You need to clear some space in your inventory to continue with the quest.  Come back here when you have more space in your inventory.
                                return false;
                            }
                            else
                            {
                                dropped.Delete();
                                obj.Complete();
                                return true;
                            }
                        }
                    }
                }
            }

            return base.OnDragDrop(from, dropped);
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (m is PlayerMobile && !m.Frozen && !m.Alive && InRange(m, 4) && !InRange(oldLocation, 4) && InLOS(m))
            {
                if (m.Map == null || !Utility.CanFit(m.Map, m.Location, 16, Utility.CanFitFlags.requireSurface))
                {
                    m.SendLocalizedMessage(502391); // Thou can not be resurrected there!
                }
                else
                {
                    Direction = GetDirectionTo(m);

                    m.PlaySound(0x214);
                    m.FixedEffect(0x376A, 10, 16);

                    m.CloseGump(typeof(ResurrectGump));
                    m.SendGump(new ResurrectGump(m, ResurrectMessage.Healer));
                }
            }
        }

        public Uzeraan(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}