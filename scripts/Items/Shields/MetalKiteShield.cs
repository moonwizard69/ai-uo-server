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

/* ./Scripts/Items/Shields/MetalKiteShield.cs
 *	ChangeLog :
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 5 lines removed.
*/

namespace Server.Items
{
    public class MetalKiteShield : BaseShield, IDyable
    {

        public override int InitMinHits { get { return 45; } }
        public override int InitMaxHits { get { return 60; } }

        public override int ShieldStrReq { get { return 45; } }

        public override int ShieldDexReq { get { return 0; } }
        public override int ShieldIntReq { get { return 0; } }
        public override int ArmorBase { get { return 16; } }

        [Constructable]
        public MetalKiteShield()
            : base(0x1B74)
        {
            Weight = 7.0;
        }

        public MetalKiteShield(Serial serial)
            : base(serial)
        {
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;

            Hue = sender.DyedHue;

            return true;
        }

        // old name removed, see base class

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 5.0)
                Weight = 7.0;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
}