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

/* Scripts/Items/Armor/Chain/ChainLegs.cs
* ChangeLog
*	7/26/05, erlein
*		Automated removal of AoS resistance related function calls. 10 lines removed.
*	9/07/04, Pigpen
*		Add Arctic Storm Leggings variant of this chain piece.
*/

namespace Server.Items
{
    [FlipableAttribute(0x13be, 0x13c3)]
    public class ChainLegs : BaseArmor
    {

        public override int InitMinHits { get { return 45; } }
        public override int InitMaxHits { get { return 60; } }

        // public override int AosStrReq  { get { return 60; } }
        public override int StrReq { get { return 20; } }

        public override int DexReq { get { return 0; } }
        public override int IntReq { get { return 0; } }
        public override int EquipDexBonus { get { return -3; } }

        public override int ArmorBase { get { return 28; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

        [Constructable]
        public ChainLegs()
            : base(0x13BE)
        {
            Weight = 7.0;
        }

        public ChainLegs(Serial serial)
            : base(serial)
        {
        }

        // old name removed, see base class

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

    public class ArcticStormLeggings : BaseArmor
    {

        public override int InitMinHits { get { return 45; } }
        public override int InitMaxHits { get { return 60; } }

        // public override int AosStrReq  { get { return 60; } }
        public override int StrReq { get { return 20; } }

        public override int DexReq { get { return 0; } }
        public override int IntReq { get { return 0; } }
        public override int EquipDexBonus { get { return -3; } }

        public override int ArmorBase { get { return 28; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

        [Constructable]
        public ArcticStormLeggings()
            : base(0x13BE)
        {
            Weight = 7.0;
            Hue = 1364;
            ProtectionLevel = ArmorProtectionLevel.Guarding;
            DurabilityLevel = ArmorDurabilityLevel.Massive;
            if (Utility.RandomDouble() < 0.20)
                Quality = ArmorQuality.Exceptional;
            Name = "Arctic Storm Leggings";
        }

        public ArcticStormLeggings(Serial serial)
            : base(serial)
        {
        }

        public override bool ShowArmorAttributes { get { return false; } }

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