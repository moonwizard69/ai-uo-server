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

using System;

namespace Server.Items
{
    public class BloodSpawn : BaseReagent, ICommodity
    {
        string ICommodity.Description
        {
            get
            {
                return string.Format("{0} blood spawn", Amount);
            }
        }

        [Constructable]
        public BloodSpawn()
            : this(1)
        {
        }

        [Constructable]
        public BloodSpawn(int amount)
            : base(0xF7C, amount)
        {
        }

        public BloodSpawn(Serial serial)
            : base(serial)
        {
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new BloodSpawn(), amount);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}