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

/* Scripts/Mobiles/Monsters/Ore Elementals/ValoriteElemental.cs
 * ChangeLog
 *  8/16/06, Rhiannon
 *		Changed speed settings to match SpeedInfo table.
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 9 lines removed.
 *  7/30/04, Adam
 *		Adjust loot: Less ore (was hurting miners), add gold
 *  6/5/04, Pix
 *		Merged in 1.0RC0 code.
 */


using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class ValoriteElemental : BaseCreature
    {
        [Constructable]
        public ValoriteElemental()
            : base(AIType.AI_Melee, FightMode.All | FightMode.Closest, 10, 1, 0.25, 0.5)
        {
            // TODO: Gas attack
            Name = "a valorite elemental";
            Body = 112;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(28);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 38;
        }

        // Auto-dispel is UOR - http://forums.uosecondage.com/viewtopic.php?f=8&t=6901
        public override bool AutoDispel { get { return Core.RuleSets.AutoDispelChance(); } }
        public override int TreasureMapLevel { get { return Core.RuleSets.AllServerRules() ? 1 : 0; } }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature)
            {
                BaseCreature bc = (BaseCreature)from;

                if (bc.Controlled || bc.BardTarget == this)
                    damage = 0; // Immune to pets and provoked creatures
            }
        }

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            reflect = true; // Every spell is reflected back to the caster
        }

        public ValoriteElemental(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            if (Core.RuleSets.AngelIslandRules())
            {
                // 'Spawning == true' blocked in BaseCreature for Core.RuleSets.AngelIslandRules()
                PackGem();
                PackGem();
                PackGem();
                PackGem();

                PackMagicEquipment(1, 3, 0.80, 0.80);
                PackMagicEquipment(1, 3, 0.20, 0.20);
                PackMagicItem(2, 2, 0.90);
                PackMagicItem(2, 2, 0.25);

                // 7/7/21, Adam: add AncientSmithyHammer for player crafted magic items
                if (CoreAI.IsDynamicFeatureSet(CoreAI.FeatureBits.MagicCraftSystem) == true)
                    if (Utility.RandomChance(10))
                        PackItem(new TenjinsHammer(200));

                // adam: Changed from 25-40, to 5-15
                PackItem(new ValoriteOre(Utility.Random(5, 15)));

                // add some gold to make up for the low ore
                PackGold(200, 350);
            }
            else
            {
                if (Core.RuleSets.AllShards)
                {   // http://web.archive.org/web/20020207055120/http://uo.stratics.com/hunters/ore_elementals.shtml
                    //  Magic Weapons, Magic Armor, 25 Large Colored Ore of the Elemental's type, Gems, 600 Gold 
                    //	(The Dull Copper Elemental that spawns naturally in Shame only gives 2 Large Dull Copper Ore and 2-3 Gems)

                    if (Spawning)
                    {
                        PackGold(600);
                    }
                    else
                    {
                        PackMagicEquipment(1, 3);
                        PackItem(new ValoriteOre(25));
                        PackGem(Utility.Random(3, 5));
                        PackGem(1, .1);
                    }
                }
                else
                {
                    if (Spawning)
                        PackItem(new ValoriteOre(2/*oreAmount*/));

                    AddLoot(LootPack.FilthyRich);
                    AddLoot(LootPack.Gems, 4);
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}