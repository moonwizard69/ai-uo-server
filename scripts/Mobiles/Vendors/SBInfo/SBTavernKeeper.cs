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

/* Scripts/Mobiles/Vendors/SBInfo/SBTavernKeeper.cs
 * ChangeLog
 *  4/3/23, Yoar:
 *  Added bowl food and apple pie.
 *  11/8/22, Adam: (Vendor System)
 *  Complete overhaul of the vendor system:
 * - DisplayCache:
 *   Display cache objects are now strongly typed and there is a separate list for each.
 *   I still dislike the fact it is a �Container�, but we can live with that.
 *   Display cache objects are serialized and are deleted on each server restart as the vendors rebuild the list.
 *   Display cache objects are marked as �int map storage� so Cron doesn�t delete them.
 * - ResourcePool:
 *   Properly exclude all ResourcePool interactions when resource pool is not being used. (Buy/Sell now works correctly without ResourcePool.)
 *   Normalize/automate all ResourcePool resources for purchase/sale within a vendor. If a vendor Sells X, he will Buy X. 
 *   At the top of each SB<merchant> there is a list of ResourcePool types. This list is uniformly looped over for creating all ResourcePool buy/sell entries.
 * - Standard Pricing Database
 *   No longer do we hardcode in what we believe the buy/sell price of X is. We now use a Standard Pricing Database for assigning these prices.
 *   I.e., BaseVendor.PlayerPays(typeof(Drums)), or BaseVendor.VendorPays(typeof(Drums))
 *   This database was derived from RunUO 2.6 first and added items not covered from Angel Island 5.
 *   The database was then filtered, checked, sorted and committed. 
 * - Make use of Rule Sets as opposed to hardcoding shard configurations everywhere.
 *   Exampes:
 *   if (Core.UOAI_SVR) => if (Core.RuleSets.AngelIslandRules())
 *   if (Server.Engines.ResourcePool.ResourcePool.Enabled) => if (Core.RuleSets.ResourcePoolRules())
 *   etc. In this way we centrally adjust who sell/buys what when. And using the SPD above, for what price.
 *  04/19/05, Kit
 *	Added Vendor rental contract
 *  11/12/04, Jade
 *      Changed spelling to make housesitter one word.
 *  11/07/2004, Jade
 *      Added new House Sitter deeds to the inventory of items for sale.
 *	4/24/04, mith
 *		Commented all items from SellList so that NPCs don't buy from players.
 */

using Server.Items;
using System.Collections;

namespace Server.Mobiles
{
    public class SBTavernKeeper : SBInfo
    {
        private ArrayList m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBTavernKeeper()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override ArrayList BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : ArrayList
        {
            public InternalBuyInfo()
            {
                Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Ale, BaseVendor.PlayerPays(7), 20, 0x99F, 0));
                Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Wine, BaseVendor.PlayerPays(7), 20, 0x9C7, 0));
                Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Liquor, BaseVendor.PlayerPays(7), 20, 0x99B, 0));
                Add(new BeverageBuyInfo(typeof(Jug), BeverageType.Cider, BaseVendor.PlayerPays(13), 20, 0x9C8, 0));
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Milk, BaseVendor.PlayerPays(7), 20, 0x9F0, 0));
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Ale, BaseVendor.PlayerPays(11), 20, 0x1F95, 0));
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Cider, BaseVendor.PlayerPays(11), 20, 0x1F97, 0));
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Liquor, BaseVendor.PlayerPays(11), 20, 0x1F99, 0));
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Wine, BaseVendor.PlayerPays(11), 20, 0x1F9B, 0));
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Water, BaseVendor.PlayerPays(11), 20, 0x1F9D, 0));
                Add(new GenericBuyInfo(typeof(Pitcher), BaseVendor.PlayerPays(typeof(Pitcher)), 20, 0xFF6, 0));
                Add(new GenericBuyInfo(typeof(BreadLoaf), BaseVendor.PlayerPays(typeof(BreadLoaf)), 10, 0x103B, 0));
                Add(new GenericBuyInfo(typeof(CheeseWheel), BaseVendor.PlayerPays(typeof(CheeseWheel)), 10, 0x97E, 0));
                Add(new GenericBuyInfo(typeof(CookedBird), BaseVendor.PlayerPays(typeof(CookedBird)), 20, 0x9B7, 0));
                Add(new GenericBuyInfo(typeof(LambLeg), BaseVendor.PlayerPays(typeof(LambLeg)), 20, 0x160A, 0));

                Add(new GenericBuyInfo(typeof(WoodenBowlOfCarrots), BaseVendor.PlayerPays(typeof(WoodenBowlOfCarrots)), 20, 0x15F9, 0));
                Add(new GenericBuyInfo(typeof(WoodenBowlOfCorn), BaseVendor.PlayerPays(typeof(WoodenBowlOfCorn)), 20, 0x15FA, 0));
                Add(new GenericBuyInfo(typeof(WoodenBowlOfLettuce), BaseVendor.PlayerPays(typeof(WoodenBowlOfLettuce)), 20, 0x15FB, 0));
                Add(new GenericBuyInfo(typeof(WoodenBowlOfPeas), BaseVendor.PlayerPays(typeof(WoodenBowlOfPeas)), 20, 0x15FC, 0));
                Add(new GenericBuyInfo(typeof(EmptyPewterBowl), BaseVendor.PlayerPays(typeof(EmptyPewterBowl)), 20, 0x15FD, 0));
                Add(new GenericBuyInfo(typeof(PewterBowlOfCorn), BaseVendor.PlayerPays(typeof(PewterBowlOfCorn)), 20, 0x15FE, 0));
                Add(new GenericBuyInfo(typeof(PewterBowlOfLettuce), BaseVendor.PlayerPays(typeof(PewterBowlOfLettuce)), 20, 0x15FF, 0));
                Add(new GenericBuyInfo(typeof(PewterBowlOfPeas), BaseVendor.PlayerPays(typeof(PewterBowlOfPeas)), 20, 0x1600, 0));
                Add(new GenericBuyInfo(typeof(PewterBowlOfPotatos), BaseVendor.PlayerPays(typeof(PewterBowlOfPotatos)), 20, 0x1601, 0));
                Add(new GenericBuyInfo(typeof(WoodenBowlOfStew), BaseVendor.PlayerPays(typeof(WoodenBowlOfStew)), 20, 0x1604, 0));
                Add(new GenericBuyInfo(typeof(WoodenBowlOfTomatoSoup), BaseVendor.PlayerPays(typeof(WoodenBowlOfTomatoSoup)), 20, 0x1606, 0));

                Add(new GenericBuyInfo(typeof(ApplePie), BaseVendor.PlayerPays(typeof(ApplePie)), 20, 0x1041, 0)); //OSI just has Pie, not Apple/Fruit/Meat

                Add(new GenericBuyInfo("1016450", typeof(Chessboard), BaseVendor.PlayerPays(typeof(Chessboard)), 20, 0xFA6, 0));
                Add(new GenericBuyInfo("1016449", typeof(CheckerBoard), BaseVendor.PlayerPays(typeof(CheckerBoard)), 20, 0xFA6, 0));
                Add(new GenericBuyInfo(typeof(Backgammon), BaseVendor.PlayerPays(typeof(Backgammon)), 20, 0xE1C, 0));
                Add(new GenericBuyInfo(typeof(Dices), BaseVendor.PlayerPays(typeof(Dices)), 20, 0xFA7, 0));

                if (Core.RuleSets.AngelIslandRules())
                {
                    // Jade: Add new House Sitter deeds
                    Add(new GenericBuyInfo("a housesitter contract", typeof(HouseSitterDeed), BaseVendor.PlayerPays(typeof(HouseSitterDeed)), 20, 0x14F0, 0));
                    Add(new GenericBuyInfo("1041243", typeof(ContractOfEmployment), BaseVendor.PlayerPays(typeof(ContractOfEmployment)), 20, 0x14F0, 0));
                }

                if (Core.RuleSets.AngelIslandRules() || Core.RuleSets.SiegeStyleRules())
                {
                    Add(new GenericBuyInfo("a housesitter contract", typeof(HouseSitterDeed), BaseVendor.PlayerPays(typeof(HouseSitterDeed)), 20, 0x14F0, 0));
                }

                // not sure about the publish date here - barkeep contracts appeared September 13, 2001, publish 13, so maybe?
                if (Core.RuleSets.AngelIslandRules() || (Core.RuleSets.StandardShardRules() && PublishInfo.Publish >= 13.5))
                    Add(new GenericBuyInfo("vendor rental contract", typeof(VendorRentalContract), BaseVendor.PlayerPays(typeof(VendorRentalContract)), 20, 0x14F0, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                if (Core.RuleSets.ShopkeepersBuyItemsRules())
                {   // cash buyback
                    Add(typeof(WoodenBowlOfCarrots), BaseVendor.VendorPays(typeof(WoodenBowlOfCarrots)));
                    Add(typeof(WoodenBowlOfCorn), BaseVendor.VendorPays(typeof(WoodenBowlOfCorn)));
                    Add(typeof(WoodenBowlOfLettuce), BaseVendor.VendorPays(typeof(WoodenBowlOfLettuce)));
                    Add(typeof(WoodenBowlOfPeas), BaseVendor.VendorPays(typeof(WoodenBowlOfPeas)));
                    Add(typeof(EmptyPewterBowl), BaseVendor.VendorPays(typeof(EmptyPewterBowl)));
                    Add(typeof(PewterBowlOfCorn), BaseVendor.VendorPays(typeof(PewterBowlOfCorn)));
                    Add(typeof(PewterBowlOfLettuce), BaseVendor.VendorPays(typeof(PewterBowlOfLettuce)));
                    Add(typeof(PewterBowlOfPeas), BaseVendor.VendorPays(typeof(PewterBowlOfPeas)));
                    Add(typeof(PewterBowlOfPotatos), BaseVendor.VendorPays(typeof(PewterBowlOfPotatos)));
                    Add(typeof(WoodenBowlOfStew), BaseVendor.VendorPays(typeof(WoodenBowlOfStew)));
                    Add(typeof(WoodenBowlOfTomatoSoup), BaseVendor.VendorPays(typeof(WoodenBowlOfTomatoSoup)));
                    Add(typeof(BeverageBottle), BaseVendor.VendorPays(typeof(BeverageBottle)));
                    Add(typeof(Jug), BaseVendor.VendorPays(typeof(Jug)));
                    Add(typeof(Pitcher), BaseVendor.VendorPays(typeof(Pitcher)));
                    Add(typeof(BreadLoaf), BaseVendor.VendorPays(typeof(BreadLoaf)));
                    Add(typeof(CheeseWheel), BaseVendor.VendorPays(typeof(CheeseWheel)));
                    Add(typeof(CookedBird), BaseVendor.VendorPays(typeof(CookedBird)));
                    Add(typeof(LambLeg), BaseVendor.VendorPays(typeof(LambLeg)));
                    Add(typeof(Chessboard), BaseVendor.VendorPays(typeof(Chessboard)));
                    Add(typeof(CheckerBoard), BaseVendor.VendorPays(typeof(CheckerBoard)));
                    Add(typeof(Backgammon), BaseVendor.VendorPays(typeof(Backgammon)));
                    Add(typeof(Dices), BaseVendor.VendorPays(typeof(Dices)));
                    Add(typeof(ContractOfEmployment), BaseVendor.VendorPays(typeof(ContractOfEmployment)));
                }
            }
        }
    }
}