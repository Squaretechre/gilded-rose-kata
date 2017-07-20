using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class ProgramShould
    {
        /*
            - All items have a SellIn value which denotes the number of days we have to sell the item
            - All items have a Quality value which denotes how valuable the item is
            - At the end of each day our system lowers both values for every item

            - Once the sell by date has passed, Quality degrades twice as fast
            - The Quality of an item is never negative
            - "Aged Brie" actually increases in Quality the older it gets
            - The Quality of an item is never more than 50
            - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            
            - "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; 
                -   Quality increases by 2 when there are 10 days or less 
                -   By 3 when there are 5 days or less
                -   Quality drops to 0 after the concert
            
            - "Conjured" items degrade in Quality twice as fast as normal items


            - Feel free to make any changes to the UpdateQuality method and add any new code as long as 
              everything still works correctly. However, do not alter the Item class or Items property as 
              those belong to the goblin in the corner who will insta-rage and one-shot you as he doesn't 
              believe in shared code ownership (you can make the UpdateQuality method and Items property 
              static if you like, we'll cover for you).

            - Just for clarification, an item can never have its Quality increase above 50, however 
              "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.
        */

        [Fact]
        public void decrease_ordinary_item_quality_by_1_when_quality_is_updated()
        {
            var program = Program.CreateProgram();
            program.UpdateQuality();

            var dexterityVest = program.ItemsDictionary()[ShopItem.DexterityVest];
            var elixirOfTheMongoose = program.ItemsDictionary()[ShopItem.ElixirOfTheMongoose];

            const int expectedMongooseQuality = 6;
            const int expectedDexterityVestQuality = 19;

            Assert.Equal(expectedDexterityVestQuality, dexterityVest.Quality);
            Assert.Equal(expectedMongooseQuality, elixirOfTheMongoose.Quality);
        }

        [Fact]
        public void decrease_ordinary_items_sell_in_date_by_1_when_quality_is_updated()
        {
            var program = Program.CreateProgram();
            program.UpdateQuality();

            var dexterityVest = program.ItemsDictionary()[ShopItem.DexterityVest];
            var elixirOfTheMongoose = program.ItemsDictionary()[ShopItem.ElixirOfTheMongoose];

            const int expectedDexterityVestSellIn = 9;
            const int expectedMongooseSellIn = 4;

            Assert.Equal(expectedDexterityVestSellIn, dexterityVest.SellIn); 
            Assert.Equal(expectedMongooseSellIn, elixirOfTheMongoose.SellIn);
        }
    }
}