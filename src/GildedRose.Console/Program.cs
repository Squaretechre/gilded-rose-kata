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
            for (var i = 0; i < Items.Count; i++)
            {
                var currentItem = Items[i];

                if (currentItem.Quality > 0 && currentItem.Name != "Sulfuras, Hand of Ragnaros" && currentItem.Name != "Aged Brie" && currentItem.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    currentItem.Quality = currentItem.Quality - 1;
                }
                else
                {
                    if (currentItem.Quality < 50)
                    {
                        currentItem.Quality = currentItem.Quality + 1;
                    }

                    if (currentItem.Name == "Backstage passes to a TAFKAL80ETC concert" && currentItem.SellIn < 11 && currentItem.Quality < 50)
                    {
                        currentItem.Quality = currentItem.Quality + 1;
                    }

                    if (currentItem.Name == "Backstage passes to a TAFKAL80ETC concert" && currentItem.SellIn < 6 && currentItem.Quality < 50)
                    {
                        currentItem.Quality = currentItem.Quality + 1;
                    }
                }

                if (currentItem.Name != "Sulfuras, Hand of Ragnaros")
                {
                    currentItem.SellIn = currentItem.SellIn - 1;
                }

                if (currentItem.SellIn < 0)
                {
                    if (currentItem.Name != "Aged Brie")
                    {
                        if (currentItem.Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (currentItem.Quality > 0)
                            {
                                if (currentItem.Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    currentItem.Quality = currentItem.Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            currentItem.Quality = currentItem.Quality - currentItem.Quality;
                        }
                    }
                    else
                    {
                        if (currentItem.Quality < 50)
                        {
                            currentItem.Quality = currentItem.Quality + 1;
                        }
                    }
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
