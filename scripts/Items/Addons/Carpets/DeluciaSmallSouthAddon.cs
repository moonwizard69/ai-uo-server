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

/*   changelog.
 *   9/16/04,Lego
 *           Changed Display Name of deed 
 *
 *
 *
 */
/////////////////////////////////////////////////
//
// Automatically generated by the
// AddonGenerator script by Arya
//
/////////////////////////////////////////////////

namespace Server.Items
{
    public class DeluciaSmallSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new DeluciaSmallSouthAddonDeed();
            }
        }

        public override bool BlocksDoors { get { return false; } }
        public override bool Redeedable { get { return true; } }

        [Constructable]
        public DeluciaSmallSouthAddon()
        {
            AddonComponent ac = null;
            ac = new AddonComponent(2788);
            AddComponent(ac, -1, -1, 0);
            ac = new AddonComponent(2789);
            AddComponent(ac, -1, 1, 0);
            ac = new AddonComponent(2791);
            AddComponent(ac, -1, 0, 0);
            ac = new AddonComponent(2790);
            AddComponent(ac, 2, -1, 0);
            ac = new AddonComponent(2792);
            AddComponent(ac, 0, -1, 0);
            ac = new AddonComponent(2792);
            AddComponent(ac, 1, -1, 0);
            ac = new AddonComponent(2795);
            AddComponent(ac, 0, 0, 0);
            ac = new AddonComponent(2795);
            AddComponent(ac, 1, 0, 0);
            ac = new AddonComponent(2787);
            AddComponent(ac, 2, 1, 0);
            ac = new AddonComponent(2793);
            AddComponent(ac, 2, 0, 0);
            ac = new AddonComponent(2794);
            AddComponent(ac, 0, 1, 0);
            ac = new AddonComponent(2794);
            AddComponent(ac, 1, 1, 0);

        }

        public DeluciaSmallSouthAddon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class DeluciaSmallSouthAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new DeluciaSmallSouthAddon();
            }
        }

        [Constructable]
        public DeluciaSmallSouthAddonDeed()
        {
            Name = "Delucia Small Carpet [South]";
        }

        public DeluciaSmallSouthAddonDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}