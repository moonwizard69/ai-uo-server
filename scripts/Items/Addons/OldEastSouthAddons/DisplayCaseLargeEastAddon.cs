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

/////////////////////////////////////////////////
//
// Automatically generated by the
// AddonGenerator script by Arya
//
/////////////////////////////////////////////////

namespace Server.Items
{
    public class DisplayCaseLargeEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new DisplayCaseLargeEastAddonDeed();
            }
        }

        [Constructable]
        public DisplayCaseLargeEastAddon()
        {
            AddComponent(new AddonComponent(2837), 0, 2, 3);
            AddComponent(new AddonComponent(2720), 0, 2, 6);
            AddComponent(new AddonComponent(2838), -1, 0, 3);
            AddComponent(new AddonComponent(2721), -1, 0, 6);
            AddComponent(new AddonComponent(2831), 0, 0, 3);
            AddComponent(new AddonComponent(2836), 1, 0, 3);
            AddComponent(new AddonComponent(2719), 1, 0, 6);
            AddComponent(new AddonComponent(2836), 1, -1, 3);
            AddComponent(new AddonComponent(2719), 1, -1, 6);
            AddComponent(new AddonComponent(2836), 1, 1, 3);
            AddComponent(new AddonComponent(2719), 1, 1, 6);
            AddComponent(new AddonComponent(2725), -1, 2, 0);
            AddComponent(new AddonComponent(2834), -1, 2, 3);
            AddComponent(new AddonComponent(2725), -1, 2, 6);
            AddComponent(new AddonComponent(2838), -1, 1, 3);
            AddComponent(new AddonComponent(2721), -1, 1, 6);
            AddComponent(new AddonComponent(2831), 0, -1, 3);
            AddComponent(new AddonComponent(2838), -1, -1, 3);
            AddComponent(new AddonComponent(2721), -1, -1, 6);
            AddComponent(new AddonComponent(2723), -1, -2, 0);
            AddComponent(new AddonComponent(2832), -1, -2, 3);
            AddComponent(new AddonComponent(2723), -1, -2, 6);
            AddComponent(new AddonComponent(2839), 0, -2, 3);
            AddComponent(new AddonComponent(2722), 0, -2, 6);
            AddComponent(new AddonComponent(2724), 1, -2, 0);
            AddComponent(new AddonComponent(2835), 1, -2, 3);
            AddComponent(new AddonComponent(2724), 1, -2, 6);
            AddComponent(new AddonComponent(2840), 1, 2, 0);
            AddComponent(new AddonComponent(2833), 1, 2, 3);
            AddComponent(new AddonComponent(2840), 1, 2, 6);
            AddComponent(new AddonComponent(2831), 0, 1, 3);
            AddonComponent ac = null;
            ac = new AddonComponent(2723);
            AddComponent(ac, -1, -2, 0);
            ac = new AddonComponent(2838);
            AddComponent(ac, -1, 1, 3);
            ac = new AddonComponent(2831);
            AddComponent(ac, 0, 1, 3);
            ac = new AddonComponent(2831);
            AddComponent(ac, 0, 0, 3);
            ac = new AddonComponent(2831);
            AddComponent(ac, 0, -1, 3);
            ac = new AddonComponent(2832);
            AddComponent(ac, -1, -2, 3);
            ac = new AddonComponent(2838);
            AddComponent(ac, -1, 0, 3);
            ac = new AddonComponent(2838);
            AddComponent(ac, -1, -1, 3);
            ac = new AddonComponent(2839);
            AddComponent(ac, 0, -2, 3);
            ac = new AddonComponent(2721);
            AddComponent(ac, -1, 1, 6);
            ac = new AddonComponent(2721);
            AddComponent(ac, -1, 0, 6);
            ac = new AddonComponent(2721);
            AddComponent(ac, -1, -1, 6);
            ac = new AddonComponent(2722);
            AddComponent(ac, 0, -2, 6);
            ac = new AddonComponent(2723);
            AddComponent(ac, -1, -2, 6);

        }

        public DisplayCaseLargeEastAddon(Serial serial)
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

    public class DisplayCaseLargeEastAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new DisplayCaseLargeEastAddon();
            }
        }

        [Constructable]
        public DisplayCaseLargeEastAddonDeed()
        {
            Name = "large display case (east)";
        }

        public DisplayCaseLargeEastAddonDeed(Serial serial)
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