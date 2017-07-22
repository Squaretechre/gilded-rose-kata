using System.Collections.Generic;

namespace GildedRose.Console
{
    /*
        - Feel free to make any changes to the UpdateQuality method and add any new code as long as 
          everything still works correctly. However, do not alter the Item class or Items property as 
          those belong to the goblin in the corner who will insta-rage and one-shot you as he doesn't 
          believe in shared code ownership (you can make the UpdateQuality method and Items property 
          static if you like, we'll cover for you).
     */

    public class Program
    {
        IList<Item> Items;
        IList<BaseItem> OtherItems;

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = CreateProgram();

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public static Program CreateProgram()
        {
            return new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                },
                OtherItems = new List<BaseItem>
                {
                    new BaseItem {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new BaseItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new BaseItem {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new BaseItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new BaseItem {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
                    new BaseItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };
        }

        public BaseItem Item(ShopItem item) => new Dictionary<ShopItem, BaseItem>()
        {
            { ShopItem.DexterityVest, OtherItems[0] },
            { ShopItem.AgedBrie, OtherItems[1] },
            { ShopItem.ElixirOfTheMongoose, OtherItems[2] },
            { ShopItem.Sulfuras, OtherItems[3] },
            { ShopItem.BackstagePasses, OtherItems[4] },
            { ShopItem.ConjuredManaCake, OtherItems[5] }
        }[item];

        public void UpdateQuality()
        {
            foreach (var item in OtherItems)
            {
                item.UpdateQuality();
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

    public class BaseItem : Item
    {
        public void UpdateQuality()
        {
            const int maxItemQuality = 50;

            var itemIsNotAgedBrie = Name != "Aged Brie";
            var itemIsNotSulfuras = Name != "Sulfuras, Hand of Ragnaros";
            var itemIsNotBackstagePasses = Name != "Backstage passes to a TAFKAL80ETC concert";

            var itemIsBackstagePasses = Name == "Backstage passes to a TAFKAL80ETC concert";
            var itemIsSulfuras = Name == "Sulfuras, Hand of Ragnaros";
            var itemIsAgedBrie = Name == "Aged Brie";

            var currenItemIsNormalItem = itemIsNotSulfuras &&
                                         itemIsNotAgedBrie &&
                                         itemIsNotBackstagePasses;

            var currentItemIsNotNormalItem = !currenItemIsNormalItem;

            if (itemIsSulfuras) return;

            var itemQualityAboveZero = Quality > 0;
            var itemQualityBelowMaxQuality = Quality < maxItemQuality;
            var itemIsOverMaxQuality = !itemQualityBelowMaxQuality;

            SellIn = SellIn - 1;

            if (currenItemIsNormalItem && itemQualityAboveZero)
            {
                Quality = Quality - 1;
            }

            if (currenItemIsNormalItem && SellIn < 0 && Quality > 0)
            {
                Quality = Quality - 1;
            }

            if (currentItemIsNotNormalItem && itemIsNotBackstagePasses && itemQualityBelowMaxQuality)
            {
                Quality = Quality + 1;
            }

            if (itemIsBackstagePasses && SellIn > 10 && itemQualityBelowMaxQuality)
            {
                Quality = Quality + 1;
            }

            if (itemIsBackstagePasses && SellIn >= 6 && SellIn <= 10 && itemQualityBelowMaxQuality)
            {
                Quality = Quality + 2;
            }

            if (itemIsBackstagePasses && SellIn >= 0 && SellIn <= 5 && itemQualityBelowMaxQuality)
            {
                Quality = Quality + 3;
            }

            if (itemIsBackstagePasses && SellIn < 0 && Quality > 0)
            {
                Quality = Quality - Quality;
            }

            if (itemIsBackstagePasses && itemIsOverMaxQuality)
            {
                Quality = 0;
            }

            if (itemIsAgedBrie && SellIn < 0 && itemQualityBelowMaxQuality)
            {
                Quality = Quality + 1;
            }

            if (Quality > maxItemQuality)
            {
                Quality = maxItemQuality;
            }
        }
    }
}
