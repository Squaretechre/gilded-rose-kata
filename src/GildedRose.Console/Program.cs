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
                }

            };
        }

        public Item Item(ShopItem item) => new Dictionary<ShopItem, Item>()
        {
            { ShopItem.DexterityVest, Items[0] },
            { ShopItem.AgedBrie, Items[1] },
            { ShopItem.ElixirOfTheMongoose, Items[2] },
            { ShopItem.Sulfuras, Items[3] },
            { ShopItem.BackstagePasses, Items[4] },
            { ShopItem.ConjuredManaCake, Items[5] }
        }[item];

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                const int maxItemQuality = 50;

                var itemIsNotAgedBrie = item.Name != "Aged Brie";
                var itemIsNotSulfuras = item.Name != "Sulfuras, Hand of Ragnaros";
                var itemIsNotBackstagePasses = item.Name != "Backstage passes to a TAFKAL80ETC concert";

                var currenItemIsNormalItem = itemIsNotSulfuras &&
                                             itemIsNotAgedBrie &&
                                             itemIsNotBackstagePasses;

                var itemIsBackstagePasses = item.Name == "Backstage passes to a TAFKAL80ETC concert";
                var itemIsSulfuras = item.Name == "Sulfuras, Hand of Ragnaros";
                var itemIsAgedBrie = item.Name == "Aged Brie";

                var currentItemIsNotNormalItem = !currenItemIsNormalItem;

                if (item.Quality > 0 && currenItemIsNormalItem)
                {
                    item.Quality = item.Quality - 1;
                }

                if (currentItemIsNotNormalItem && item.Quality < maxItemQuality)
                {
                    item.Quality = item.Quality + 1;
                }

                if (currentItemIsNotNormalItem && itemIsBackstagePasses && item.SellIn < 11 && item.Quality < maxItemQuality)
                {
                    item.Quality = item.Quality + 1;
                }

                if (currentItemIsNotNormalItem && itemIsBackstagePasses && item.SellIn < 6 && item.Quality < maxItemQuality)
                {
                    item.Quality = item.Quality + 1;
                }

                if (itemIsNotSulfuras)
                {
                    item.SellIn = item.SellIn - 1;
                }

                if (item.SellIn < 0 && itemIsNotAgedBrie && item.Quality > 0 && itemIsNotBackstagePasses && itemIsNotSulfuras)
                {
                    item.Quality = item.Quality - 1;
                }

                if (item.SellIn < 0 && itemIsNotAgedBrie && item.Quality > 0 && itemIsBackstagePasses)
                {
                    item.Quality = item.Quality - item.Quality;
                }

                if (item.SellIn < 0 && itemIsAgedBrie && item.Quality < maxItemQuality)
                {
                    item.Quality = item.Quality + 1;
                }
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
