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
    [Flipable(0x1032, 0x1033)]
    public class SmoothingPlane : BaseTool
    {
        public override CraftSystem CraftSystem { get { return DefCarpentry.CraftSystem; } }

        [Constructable]
        public SmoothingPlane()
            : base(0x1032)
        {
            Weight = 1.0;
        }

        [Constructable]
        public SmoothingPlane(int uses)
            : base(uses, 0x1032)
        {
            Weight = 1.0;
        }

        public SmoothingPlane(Serial serial)
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