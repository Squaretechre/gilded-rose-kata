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
                    new NormalItem {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new AgedBrieItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new NormalItem {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new BaseItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new BackstagePassesItem {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
                    new NormalItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };
        }

        public BaseItem Item(ShopItem shopItem) => new Dictionary<ShopItem, BaseItem>()
        {
            { ShopItem.DexterityVest, OtherItems[0] },
            { ShopItem.AgedBrie, OtherItems[1] },
            { ShopItem.ElixirOfTheMongoose, OtherItems[2] },
            { ShopItem.Sulfuras, OtherItems[3] },
            { ShopItem.BackstagePasses, OtherItems[4] },
            { ShopItem.ConjuredManaCake, OtherItems[5] }
        }[shopItem];

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
}
