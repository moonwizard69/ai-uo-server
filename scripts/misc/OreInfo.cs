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

/*
 * Change log for:
 * C:\Program Files\RunUO\Scripts\Misc\OreInfo.cs
 *	11/14/21, Yoar
 *	    Added ML wood types.
 * Adam: 6/19/04
 * changed "Spined" hue from 0x283 ==> 0x406
 * changed "Horned" hue from 0x227 ==> 0x71D
 * changed "Barbed" hue from 0x1C1 ==> 0x715
 */

using System;
using System.Collections;

namespace Server.Items
{
    public enum CraftResource
    {
        None = 0,
        Iron = 1,
        DullCopper,
        ShadowIron,
        Copper,
        Bronze,
        Gold,
        Agapite,
        Verite,
        Valorite,

        RegularLeather = 101,
        SpinedLeather,
        HornedLeather,
        BarbedLeather,

        RedScales = 201,
        YellowScales,
        BlackScales,
        GreenScales,
        WhiteScales,
        BlueScales,

        RegularWood = 301,
        OakWood,
        AshWood,
        YewWood,
        Heartwood,
        Bloodwood,
        Frostwood,

        // custom resources here
        Obsidian = 10001,
        Stahlrim,
        Dragonglass,
    }

    public enum CraftResourceType
    {
        None,
        Metal,
        Leather,
        Scales,
        Wood
    }

    public class CraftAttributeInfo
    {
        private int m_WeaponFireDamage;
        private int m_WeaponColdDamage;
        private int m_WeaponPoisonDamage;
        private int m_WeaponEnergyDamage;
        private int m_WeaponDurability;
        private int m_WeaponLuck;
        private int m_WeaponGoldIncrease;
        private int m_WeaponLowerRequirements;

        private int m_ArmorPhysicalResist;
        private int m_ArmorFireResist;
        private int m_ArmorColdResist;
        private int m_ArmorPoisonResist;
        private int m_ArmorEnergyResist;
        private int m_ArmorDurability;
        private int m_ArmorLuck;
        private int m_ArmorGoldIncrease;
        private int m_ArmorLowerRequirements;

        private int m_RunicMinAttributes;
        private int m_RunicMaxAttributes;
        private int m_RunicMinIntensity;
        private int m_RunicMaxIntensity;

        public int WeaponFireDamage { get { return m_WeaponFireDamage; } set { m_WeaponFireDamage = value; } }
        public int WeaponColdDamage { get { return m_WeaponColdDamage; } set { m_WeaponColdDamage = value; } }
        public int WeaponPoisonDamage { get { return m_WeaponPoisonDamage; } set { m_WeaponPoisonDamage = value; } }
        public int WeaponEnergyDamage { get { return m_WeaponEnergyDamage; } set { m_WeaponEnergyDamage = value; } }
        public int WeaponDurability { get { return m_WeaponDurability; } set { m_WeaponDurability = value; } }
        public int WeaponLuck { get { return m_WeaponLuck; } set { m_WeaponLuck = value; } }
        public int WeaponGoldIncrease { get { return m_WeaponGoldIncrease; } set { m_WeaponGoldIncrease = value; } }
        public int WeaponLowerRequirements { get { return m_WeaponLowerRequirements; } set { m_WeaponLowerRequirements = value; } }

        public int ArmorPhysicalResist { get { return m_ArmorPhysicalResist; } set { m_ArmorPhysicalResist = value; } }
        public int ArmorFireResist { get { return m_ArmorFireResist; } set { m_ArmorFireResist = value; } }
        public int ArmorColdResist { get { return m_ArmorColdResist; } set { m_ArmorColdResist = value; } }
        public int ArmorPoisonResist { get { return m_ArmorPoisonResist; } set { m_ArmorPoisonResist = value; } }
        public int ArmorEnergyResist { get { return m_ArmorEnergyResist; } set { m_ArmorEnergyResist = value; } }
        public int ArmorDurability { get { return m_ArmorDurability; } set { m_ArmorDurability = value; } }
        public int ArmorLuck { get { return m_ArmorLuck; } set { m_ArmorLuck = value; } }
        public int ArmorGoldIncrease { get { return m_ArmorGoldIncrease; } set { m_ArmorGoldIncrease = value; } }
        public int ArmorLowerRequirements { get { return m_ArmorLowerRequirements; } set { m_ArmorLowerRequirements = value; } }

        public int RunicMinAttributes { get { return m_RunicMinAttributes; } set { m_RunicMinAttributes = value; } }
        public int RunicMaxAttributes { get { return m_RunicMaxAttributes; } set { m_RunicMaxAttributes = value; } }
        public int RunicMinIntensity { get { return m_RunicMinIntensity; } set { m_RunicMinIntensity = value; } }
        public int RunicMaxIntensity { get { return m_RunicMaxIntensity; } set { m_RunicMaxIntensity = value; } }

        public CraftAttributeInfo()
        {
        }

        public static readonly CraftAttributeInfo Blank;
        public static readonly CraftAttributeInfo DullCopper, ShadowIron, Copper, Bronze, Golden, Agapite, Verite, Valorite;
        public static readonly CraftAttributeInfo Spined, Horned, Barbed;
        public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales;
        public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood;

        static CraftAttributeInfo()
        {
            Blank = new CraftAttributeInfo();

            CraftAttributeInfo dullCopper = DullCopper = new CraftAttributeInfo();

            dullCopper.ArmorPhysicalResist = 6;
            dullCopper.ArmorDurability = 50;
            dullCopper.ArmorLowerRequirements = 20;
            dullCopper.WeaponDurability = 100;
            dullCopper.WeaponLowerRequirements = 50;
            dullCopper.RunicMinAttributes = 1;
            dullCopper.RunicMaxAttributes = 2;
            dullCopper.RunicMinIntensity = 10;
            dullCopper.RunicMaxIntensity = 35;

            CraftAttributeInfo shadowIron = ShadowIron = new CraftAttributeInfo();

            shadowIron.ArmorPhysicalResist = 2;
            shadowIron.ArmorFireResist = 1;
            shadowIron.ArmorEnergyResist = 5;
            shadowIron.ArmorDurability = 100;
            shadowIron.WeaponColdDamage = 20;
            shadowIron.WeaponDurability = 50;
            shadowIron.RunicMinAttributes = 2;
            shadowIron.RunicMaxAttributes = 2;
            shadowIron.RunicMinIntensity = 20;
            shadowIron.RunicMaxIntensity = 45;

            CraftAttributeInfo copper = Copper = new CraftAttributeInfo();

            copper.ArmorPhysicalResist = 1;
            copper.ArmorFireResist = 1;
            copper.ArmorPoisonResist = 5;
            copper.ArmorEnergyResist = 2;
            copper.WeaponPoisonDamage = 10;
            copper.WeaponEnergyDamage = 20;
            copper.RunicMinAttributes = 2;
            copper.RunicMaxAttributes = 3;
            copper.RunicMinIntensity = 25;
            copper.RunicMaxIntensity = 50;

            CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();

            bronze.ArmorPhysicalResist = 3;
            bronze.ArmorColdResist = 5;
            bronze.ArmorPoisonResist = 1;
            bronze.ArmorEnergyResist = 1;
            bronze.WeaponFireDamage = 40;
            bronze.RunicMinAttributes = 3;
            bronze.RunicMaxAttributes = 3;
            bronze.RunicMinIntensity = 30;
            bronze.RunicMaxIntensity = 65;

            CraftAttributeInfo golden = Golden = new CraftAttributeInfo();

            golden.ArmorPhysicalResist = 1;
            golden.ArmorFireResist = 1;
            golden.ArmorColdResist = 2;
            golden.ArmorEnergyResist = 2;
            golden.ArmorLuck = 40;
            golden.ArmorLowerRequirements = 30;
            golden.WeaponLuck = 40;
            golden.WeaponLowerRequirements = 50;
            golden.RunicMinAttributes = 3;
            golden.RunicMaxAttributes = 4;
            golden.RunicMinIntensity = 35;
            golden.RunicMaxIntensity = 75;

            CraftAttributeInfo agapite = Agapite = new CraftAttributeInfo();

            agapite.ArmorPhysicalResist = 2;
            agapite.ArmorFireResist = 3;
            agapite.ArmorColdResist = 2;
            agapite.ArmorPoisonResist = 2;
            agapite.ArmorEnergyResist = 2;
            agapite.WeaponColdDamage = 30;
            agapite.WeaponEnergyDamage = 20;
            agapite.RunicMinAttributes = 4;
            agapite.RunicMaxAttributes = 4;
            agapite.RunicMinIntensity = 40;
            agapite.RunicMaxIntensity = 80;

            CraftAttributeInfo verite = Verite = new CraftAttributeInfo();

            verite.ArmorPhysicalResist = 3;
            verite.ArmorFireResist = 3;
            verite.ArmorColdResist = 2;
            verite.ArmorPoisonResist = 3;
            verite.ArmorEnergyResist = 1;
            verite.WeaponPoisonDamage = 40;
            verite.WeaponEnergyDamage = 20;
            verite.RunicMinAttributes = 4;
            verite.RunicMaxAttributes = 5;
            verite.RunicMinIntensity = 45;
            verite.RunicMaxIntensity = 90;

            CraftAttributeInfo valorite = Valorite = new CraftAttributeInfo();

            valorite.ArmorPhysicalResist = 4;
            valorite.ArmorColdResist = 3;
            valorite.ArmorPoisonResist = 3;
            valorite.ArmorEnergyResist = 3;
            valorite.ArmorDurability = 50;
            valorite.WeaponFireDamage = 10;
            valorite.WeaponColdDamage = 20;
            valorite.WeaponPoisonDamage = 10;
            valorite.WeaponEnergyDamage = 20;
            valorite.RunicMinAttributes = 5;
            valorite.RunicMaxAttributes = 5;
            valorite.RunicMinIntensity = 50;
            valorite.RunicMaxIntensity = 100;

            CraftAttributeInfo spined = Spined = new CraftAttributeInfo();

            spined.ArmorPhysicalResist = 5;
            spined.ArmorLuck = 40;
            spined.RunicMinAttributes = 1;
            spined.RunicMaxAttributes = 3;
            spined.RunicMinIntensity = 20;
            spined.RunicMaxIntensity = 40;

            CraftAttributeInfo horned = Horned = new CraftAttributeInfo();

            horned.ArmorPhysicalResist = 2;
            horned.ArmorFireResist = 3;
            horned.ArmorColdResist = 2;
            horned.ArmorPoisonResist = 2;
            horned.ArmorEnergyResist = 2;
            horned.RunicMinAttributes = 3;
            horned.RunicMaxAttributes = 4;
            horned.RunicMinIntensity = 30;
            horned.RunicMaxIntensity = 70;

            CraftAttributeInfo barbed = Barbed = new CraftAttributeInfo();

            barbed.ArmorPhysicalResist = 2;
            barbed.ArmorFireResist = 1;
            barbed.ArmorColdResist = 2;
            barbed.ArmorPoisonResist = 3;
            barbed.ArmorEnergyResist = 4;
            barbed.RunicMinAttributes = 4;
            barbed.RunicMaxAttributes = 5;
            barbed.RunicMinIntensity = 40;
            barbed.RunicMaxIntensity = 100;

            CraftAttributeInfo red = RedScales = new CraftAttributeInfo();

            red.ArmorFireResist = 10;
            red.ArmorColdResist = -3;

            CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

            yellow.ArmorPhysicalResist = -3;
            yellow.ArmorLuck = 20;

            CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

            black.ArmorPhysicalResist = 10;
            black.ArmorEnergyResist = -3;

            CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

            green.ArmorFireResist = -3;
            green.ArmorPoisonResist = 10;

            CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

            white.ArmorPhysicalResist = -3;
            white.ArmorColdResist = 10;

            CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

            blue.ArmorPoisonResist = -3;
            blue.ArmorEnergyResist = 10;

            CraftAttributeInfo oak = OakWood = new CraftAttributeInfo();

            oak.ArmorPhysicalResist = 3;
            oak.ArmorFireResist = 3;
            oak.ArmorPoisonResist = 2;
            oak.ArmorEnergyResist = 3;
            oak.ArmorLuck = 40;
            oak.ArmorDurability = 50;
            oak.WeaponLuck = 40;
            oak.WeaponDurability = 50;
            oak.RunicMinAttributes = 1;
            oak.RunicMaxAttributes = 2;
            oak.RunicMinIntensity = 1;
            oak.RunicMaxIntensity = 50;

            CraftAttributeInfo ash = AshWood = new CraftAttributeInfo();

            ash.ArmorPhysicalResist = 2;
            ash.ArmorColdResist = 4;
            ash.ArmorPoisonResist = 1;
            ash.ArmorEnergyResist = 6;
            ash.ArmorLowerRequirements = 20;
            ash.WeaponLowerRequirements = 20;
            ash.RunicMinAttributes = 2;
            ash.RunicMaxAttributes = 3;
            ash.RunicMinIntensity = 35;
            ash.RunicMaxIntensity = 75;

            CraftAttributeInfo yew = YewWood = new CraftAttributeInfo();

            yew.ArmorPhysicalResist = 6;
            yew.ArmorFireResist = 3;
            yew.ArmorColdResist = 3;
            yew.ArmorEnergyResist = 3;
            yew.RunicMinAttributes = 3;
            yew.RunicMaxAttributes = 3;
            yew.RunicMinIntensity = 40;
            yew.RunicMaxIntensity = 90;

            CraftAttributeInfo heart = Heartwood = new CraftAttributeInfo();

            heart.ArmorPhysicalResist = 2;
            heart.ArmorFireResist = 3;
            heart.ArmorColdResist = 2;
            heart.ArmorPoisonResist = 7;
            heart.ArmorEnergyResist = 2;
            heart.RunicMinAttributes = 4;
            heart.RunicMaxAttributes = 4;
            heart.RunicMinIntensity = 50;
            heart.RunicMaxIntensity = 100;

            CraftAttributeInfo blood = Bloodwood = new CraftAttributeInfo();

            blood.ArmorPhysicalResist = 3;
            blood.ArmorFireResist = 8;
            blood.ArmorColdResist = 1;
            blood.ArmorPoisonResist = 3;
            blood.ArmorEnergyResist = 3;
            blood.ArmorFireResist = 3;

            CraftAttributeInfo frost = Frostwood = new CraftAttributeInfo();

            frost.ArmorPhysicalResist = 2;
            frost.ArmorFireResist = 1;
            frost.ArmorColdResist = 8;
            frost.ArmorPoisonResist = 3;
            frost.ArmorEnergyResist = 4;
            frost.WeaponColdDamage = 40;
        }
    }

    public class CraftResourceInfo
    {
        private int m_Hue;
        private int m_Number;
        private Article m_Article;
        private string m_Name;
        private CraftAttributeInfo m_AttributeInfo;
        private CraftResource m_Resource;
        private Type[] m_ResourceTypes;

        public int Hue { get { return m_Hue; } }
        public int Number { get { return m_Number; } }
        public Article Article { get { return m_Article; } }
        public string Name { get { return m_Name; } }
        public CraftAttributeInfo AttributeInfo { get { return m_AttributeInfo; } }
        public CraftResource Resource { get { return m_Resource; } }
        public Type[] ResourceTypes { get { return m_ResourceTypes; } }

        public CraftResourceInfo(int hue, int number, Article article, string name, CraftAttributeInfo attributeInfo, CraftResource resource, params Type[] resourceTypes)
        {
            m_Hue = hue;
            m_Number = number;
            m_Article = article;
            m_Name = name;
            m_AttributeInfo = attributeInfo;
            m_Resource = resource;
            m_ResourceTypes = resourceTypes;

            for (int i = 0; i < resourceTypes.Length; ++i)
                CraftResources.RegisterType(resourceTypes[i], resource);
        }
    }

    public class CraftResources
    {
        private static CraftResourceInfo[] m_MetalInfo = new CraftResourceInfo[]
            {
                new CraftResourceInfo( 0x000, 1053109, Article.An,  "Iron",          CraftAttributeInfo.Blank,       CraftResource.Iron,             typeof( IronIngot ),        typeof( IronOre ),          typeof( Granite ) ),
                new CraftResourceInfo( 0x973, 1053108, Article.A,   "Dull Copper",   CraftAttributeInfo.DullCopper,  CraftResource.DullCopper,       typeof( DullCopperIngot ),  typeof( DullCopperOre ),    typeof( DullCopperGranite ) ),
                new CraftResourceInfo( 0x966, 1053107, Article.A,   "Shadow Iron",   CraftAttributeInfo.ShadowIron,  CraftResource.ShadowIron,       typeof( ShadowIronIngot ),  typeof( ShadowIronOre ),    typeof( ShadowIronGranite ) ),
                new CraftResourceInfo( 0x96D, 1053106, Article.A,   "Copper",        CraftAttributeInfo.Copper,      CraftResource.Copper,           typeof( CopperIngot ),      typeof( CopperOre ),        typeof( CopperGranite ) ),
                new CraftResourceInfo( 0x972, 1053105, Article.A,   "Bronze",        CraftAttributeInfo.Bronze,      CraftResource.Bronze,           typeof( BronzeIngot ),      typeof( BronzeOre ),        typeof( BronzeGranite ) ),
                new CraftResourceInfo( 0x8A5, 1053104, Article.A,   "Gold",          CraftAttributeInfo.Golden,      CraftResource.Gold,             typeof( GoldIngot ),        typeof( GoldOre ),          typeof( GoldGranite ) ),
                new CraftResourceInfo( 0x979, 1053103, Article.An,  "Agapite",       CraftAttributeInfo.Agapite,     CraftResource.Agapite,          typeof( AgapiteIngot ),     typeof( AgapiteOre ),       typeof( AgapiteGranite ) ),
                new CraftResourceInfo( 0x89F, 1053102, Article.A,   "Verite",        CraftAttributeInfo.Verite,      CraftResource.Verite,           typeof( VeriteIngot ),      typeof( VeriteOre ),        typeof( VeriteGranite ) ),
                new CraftResourceInfo( 0x8AB, 1053101, Article.A,   "Valorite",      CraftAttributeInfo.Valorite,    CraftResource.Valorite,         typeof( ValoriteIngot ),    typeof( ValoriteOre ),      typeof( ValoriteGranite ) ),
            };

        private static CraftResourceInfo[] m_ScaleInfo = new CraftResourceInfo[]
            {
                new CraftResourceInfo( 0x66D, 1053129, Article.A,   "Red Scales",    CraftAttributeInfo.RedScales,       CraftResource.RedScales,        typeof( RedScales ) ),
                new CraftResourceInfo( 0x8A8, 1053130, Article.A,   "Yellow Scales", CraftAttributeInfo.YellowScales,    CraftResource.YellowScales,     typeof( YellowScales ) ),
                new CraftResourceInfo( 0x455, 1053131, Article.A,   "Black Scales",  CraftAttributeInfo.BlackScales,     CraftResource.BlackScales,      typeof( BlackScales ) ),
                new CraftResourceInfo( 0x851, 1053132, Article.A,   "Green Scales",  CraftAttributeInfo.GreenScales,     CraftResource.GreenScales,      typeof( GreenScales ) ),
                new CraftResourceInfo( 0x8FD, 1053133, Article.A,   "White Scales",  CraftAttributeInfo.WhiteScales,     CraftResource.WhiteScales,      typeof( WhiteScales ) ),
                new CraftResourceInfo( 0x8B0, 1053134, Article.A,   "Blue Scales",   CraftAttributeInfo.BlueScales,      CraftResource.BlueScales,       typeof( BlueScales ) )
            };

        private static CraftResourceInfo[] m_LeatherInfo = new CraftResourceInfo[]
            {
                new CraftResourceInfo( 0x000, 1049353, Article.A,           "Normal",        CraftAttributeInfo.Blank,       CraftResource.RegularLeather,   typeof( Leather ),          typeof( Hides ) ),
                new CraftResourceInfo( 0x406/*0x283*/, 1049354, Article.A,  "Spined",       CraftAttributeInfo.Spined,      CraftResource.SpinedLeather,    typeof( SpinedLeather ),    typeof( SpinedHides ) ),
                new CraftResourceInfo( 0x71D/*0x227*/, 1049355, Article.A,  "Horned",       CraftAttributeInfo.Horned,      CraftResource.HornedLeather,    typeof( HornedLeather ),    typeof( HornedHides ) ),
                new CraftResourceInfo( 0x715/*0x1C1*/, 1049356, Article.A,  "Barbed",       CraftAttributeInfo.Barbed,      CraftResource.BarbedLeather,    typeof( BarbedLeather ),    typeof( BarbedHides ) )
            };

        private static CraftResourceInfo[] m_AOSLeatherInfo = new CraftResourceInfo[]
            {
                new CraftResourceInfo( 0x000, 1049353, Article.A,   "Normal",        CraftAttributeInfo.Blank,       CraftResource.RegularLeather,   typeof( Leather ),          typeof( Hides ) ),
                new CraftResourceInfo( 0x8AC, 1049354, Article.A,   "Spined",        CraftAttributeInfo.Spined,      CraftResource.SpinedLeather,    typeof( SpinedLeather ),    typeof( SpinedHides ) ),
                new CraftResourceInfo( 0x845, 1049355, Article.A,   "Horned",        CraftAttributeInfo.Horned,      CraftResource.HornedLeather,    typeof( HornedLeather ),    typeof( HornedHides ) ),
                new CraftResourceInfo( 0x851, 1049356, Article.A,   "Barbed",        CraftAttributeInfo.Barbed,      CraftResource.BarbedLeather,    typeof( BarbedLeather ),    typeof( BarbedHides ) ),
            };

        private static CraftResourceInfo[] m_WoodInfo = new CraftResourceInfo[]
            {
                new CraftResourceInfo( 0x000, 1011542, Article.A,   "Normal",        CraftAttributeInfo.Blank,       CraftResource.RegularWood,  typeof( Board ),          typeof( Log ) ),
                new CraftResourceInfo( 0x7DA, 1072533, Article.An,  "Oak",           CraftAttributeInfo.OakWood,     CraftResource.OakWood,      typeof( OakBoard ),       typeof( OakLog ) ),
                new CraftResourceInfo( 0x4A7, 1072534, Article.An,  "Ash",           CraftAttributeInfo.AshWood,     CraftResource.AshWood,      typeof( AshBoard ),       typeof( AshLog ) ),
                new CraftResourceInfo( 0x4A8, 1072535, Article.A,   "Yew",           CraftAttributeInfo.YewWood,     CraftResource.YewWood,      typeof( YewBoard ),       typeof( YewLog ) ),
                new CraftResourceInfo( 0x4A9, 1072536, Article.A,   "Heartwood",     CraftAttributeInfo.Heartwood,   CraftResource.Heartwood,    typeof( HeartwoodBoard ), typeof( HeartwoodLog ) ),
                new CraftResourceInfo( 0x4AA/*0x612*/, 1072538, Article.A, "Bloodwood",     CraftAttributeInfo.Bloodwood,   CraftResource.Bloodwood,    typeof( BloodwoodBoard ), typeof( BloodwoodLog ) ),
                new CraftResourceInfo( 0x47F/*0xB8f*/, 1072539, Article.A, "Frostwood",     CraftAttributeInfo.Frostwood,   CraftResource.Frostwood,    typeof( FrostwoodBoard ), typeof( FrostwoodLog ) )
            };

        private static CraftResourceInfo m_ObsidianInfo = new CraftResourceInfo(0x455, 1023977, Article.An, "Obsidian", CraftAttributeInfo.Blank, CraftResource.Obsidian, typeof(Obsidian), typeof(Blackrock));
        private static CraftResourceInfo m_StahlrimInfo = new CraftResourceInfo(0x47F, 0, Article.A, "Stahlrim", CraftAttributeInfo.Blank, CraftResource.Stahlrim, typeof(Stahlrim), typeof(Frostrock));
        private static CraftResourceInfo m_DragonGlassInfo = new CraftResourceInfo(0x451, 0, Article.A, "Dragonglass", CraftAttributeInfo.Blank, CraftResource.Dragonglass, typeof(Dragonglass));

        /// <summary>
        /// Returns true if '<paramref name="resource"/>' is None, Iron, or RegularLeather. False if otherwise.
        /// </summary>
        public static bool IsStandard(CraftResource resource)
        {
            return (resource == CraftResource.None || resource == CraftResource.Iron || resource == CraftResource.RegularLeather);
        }

        private static Hashtable m_TypeTable;

        /// <summary>
        /// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="CraftResources.GetFromType"/>
        /// </summary>
        public static void RegisterType(Type resourceType, CraftResource resource)
        {
            if (m_TypeTable == null)
                m_TypeTable = new Hashtable();

            m_TypeTable[resourceType] = resource;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
        /// </summary>
        public static CraftResource GetFromType(Type resourceType)
        {
            if (m_TypeTable == null)
                return CraftResource.None;

            if (resourceType == null)
                return CraftResource.None;

            object obj = m_TypeTable[resourceType];

            if (!(obj is CraftResource))
                return CraftResource.None;

            return (CraftResource)obj;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
        /// </summary>
        public static CraftResourceInfo GetInfo(CraftResource resource)
        {
            if (resource == CraftResource.Obsidian)
                return m_ObsidianInfo;

            if (resource == CraftResource.Stahlrim)
                return m_StahlrimInfo;

            if (resource == CraftResource.Dragonglass)
                return m_DragonGlassInfo;

            CraftResourceInfo[] list = null;

            switch (GetType(resource))
            {
                case CraftResourceType.Metal: list = m_MetalInfo; break;
                case CraftResourceType.Leather: list = Core.RuleSets.AOSRules() ? m_AOSLeatherInfo : m_LeatherInfo; break;
                case CraftResourceType.Scales: list = m_ScaleInfo; break;
                case CraftResourceType.Wood: list = m_WoodInfo; break;
            }

            if (list != null)
            {
                int index = GetIndex(resource);

                if (index >= 0 && index < list.Length)
                    return list[index];
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
        /// </summary>
        public static CraftResourceType GetType(CraftResource resource)
        {
            if (resource >= CraftResource.Iron && resource <= CraftResource.Valorite)
                return CraftResourceType.Metal;

            if (resource >= CraftResource.RegularLeather && resource <= CraftResource.BarbedLeather)
                return CraftResourceType.Leather;

            if (resource >= CraftResource.RedScales && resource <= CraftResource.BlueScales)
                return CraftResourceType.Scales;

            if (resource >= CraftResource.RegularWood && resource <= CraftResource.Frostwood)
                return CraftResourceType.Wood;

            return CraftResourceType.None;
        }

        /// <summary>
        /// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
        /// </summary>
        public static CraftResource GetStart(CraftResource resource)
        {
            switch (GetType(resource))
            {
                case CraftResourceType.Metal: return CraftResource.Iron;
                case CraftResourceType.Leather: return CraftResource.RegularLeather;
                case CraftResourceType.Scales: return CraftResource.RedScales;
                case CraftResourceType.Wood: return CraftResource.RegularWood;
            }

            return CraftResource.None;
        }

        /// <summary>
        /// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
        /// </summary>
        public static int GetIndex(CraftResource resource)
        {
            CraftResource start = GetStart(resource);

            if (start == CraftResource.None)
                return 0;

            return (int)(resource - start);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetLocalizationNumber(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Number);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetHue(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Hue);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
        /// </summary>
        public static string GetName(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return (info == null ? string.Empty : info.Name);
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>' -or- CraftResource.None if unable to convert.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info)
        {
            if (info.Name.IndexOf("Spined") >= 0)
                return CraftResource.SpinedLeather;
            else if (info.Name.IndexOf("Horned") >= 0)
                return CraftResource.HornedLeather;
            else if (info.Name.IndexOf("Barbed") >= 0)
                return CraftResource.BarbedLeather;
            else if (info.Name.IndexOf("Leather") >= 0)
                return CraftResource.RegularLeather;

            if (info.Level == 0)
                return CraftResource.Iron;
            else if (info.Level == 1)
                return CraftResource.DullCopper;
            else if (info.Level == 2)
                return CraftResource.ShadowIron;
            else if (info.Level == 3)
                return CraftResource.Copper;
            else if (info.Level == 4)
                return CraftResource.Bronze;
            else if (info.Level == 5)
                return CraftResource.Gold;
            else if (info.Level == 6)
                return CraftResource.Agapite;
            else if (info.Level == 7)
                return CraftResource.Verite;
            else if (info.Level == 8)
                return CraftResource.Valorite;

            return CraftResource.None;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>', using '<paramref name="material"/>' to help resolve leather OreInfo instances.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info, ArmorMaterialType material)
        {
            if (material == ArmorMaterialType.Studded || material == ArmorMaterialType.Leather || material == ArmorMaterialType.Spined ||
                material == ArmorMaterialType.Horned || material == ArmorMaterialType.Barbed)
            {
                if (info.Level == 0)
                    return CraftResource.RegularLeather;
                else if (info.Level == 1)
                    return CraftResource.SpinedLeather;
                else if (info.Level == 2)
                    return CraftResource.HornedLeather;
                else if (info.Level == 3)
                    return CraftResource.BarbedLeather;

                return CraftResource.None;
            }

            return GetFromOreInfo(info);
        }
    }

    // NOTE: This class is only for compatability with very old RunUO versions.
    // No changes to it should be required for custom resources.
    public class OreInfo
    {
        public static readonly OreInfo Iron = new OreInfo(0, 0x000, "Iron");
        public static readonly OreInfo DullCopper = new OreInfo(1, 0x973, "Dull Copper");
        public static readonly OreInfo ShadowIron = new OreInfo(2, 0x966, "Shadow Iron");
        public static readonly OreInfo Copper = new OreInfo(3, 0x96D, "Copper");
        public static readonly OreInfo Bronze = new OreInfo(4, 0x972, "Bronze");
        public static readonly OreInfo Gold = new OreInfo(5, 0x8A5, "Gold");
        public static readonly OreInfo Agapite = new OreInfo(6, 0x979, "Agapite");
        public static readonly OreInfo Verite = new OreInfo(7, 0x89F, "Verite");
        public static readonly OreInfo Valorite = new OreInfo(8, 0x8AB, "Valorite");

        private int m_Level;
        private int m_Hue;
        private string m_Name;

        public OreInfo(int level, int hue, string name)
        {
            m_Level = level;
            m_Hue = hue;
            m_Name = name;
        }

        public int Level
        {
            get
            {
                return m_Level;
            }
        }

        public int Hue
        {
            get
            {
                return m_Hue;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }
    }
}