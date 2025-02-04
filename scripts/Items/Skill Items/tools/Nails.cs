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

using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x102E, 0x102F)]
    public class Nails : BaseTool
    {
        public override CraftSystem CraftSystem { get { return DefCarpentry.CraftSystem; } }

        [Constructable]
        public Nails()
            : base(0x102E)
        {
            Weight = 2.0;
        }

        [Constructable]
        public Nails(int uses)
            : base(uses, 0x102C)
        {
            Weight = 2.0;
        }

        public Nails(Serial serial)
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