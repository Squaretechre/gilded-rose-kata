﻿using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class ProgramShould
    {
        /*
            - All items have a SellIn value which denotes the number of times we have to sell the item
            - All items have a Quality value which denotes how valuable the item is
            - At the end of each day our system lowers both values for every item

            ✓ "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            ✓ The Quality of an item is never negative
            ✓ The Quality of an item is never more than 50
            - "Aged Brie" actually increases in Quality the older it gets
            - Once the sell by date has passed, Quality degrades twice as fast
            
            - "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; 
                -   Quality increases by 2 when there are 10 times or less 
                -   By 3 when there are 5 times or less
                -   Quality drops to 0 after the concert
            
            - "Conjured" items degrade in Quality twice as fast as normal items

            ✓ Just for clarification, an item can never have its Quality increase above 50, however 
              "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.

            - Feel free to make any changes to the UpdateQuality method and add any new code as long as 
              everything still works correctly. However, do not alter the Item class or Items property as 
              those belong to the goblin in the corner who will insta-rage and one-shot you as he doesn't 
              believe in shared code ownership (you can make the UpdateQuality method and Items property 
              static if you like, we'll cover for you).
        */

        [Fact]
        public void decrease_ordinary_item_quality_by_1_when_quality_is_updated()
        {
            var program = Program.CreateProgram();
            program.UpdateQuality();

            var dexterityVest = program.Item(ShopItem.DexterityVest);
            var elixirOfTheMongoose = program.Item(ShopItem.ElixirOfTheMongoose);

            const int expectedMongooseQuality = 6;
            const int expectedDexterityVestQuality = 19;

            Assert.Equal(expectedDexterityVestQuality, dexterityVest.Quality);
            Assert.Equal(expectedMongooseQuality, elixirOfTheMongoose.Quality);
        }

        [Fact]
        public void decrease_items_sell_in_date_by_1_when_quality_is_updated()
        {
            var program = Program.CreateProgram();
            program.UpdateQuality();

            var dexterityVest = program.Item(ShopItem.DexterityVest);
            var agedBrie = program.Item(ShopItem.AgedBrie);
            var elixirOfTheMongoose = program.Item(ShopItem.ElixirOfTheMongoose);
            var sulfuras = program.Item(ShopItem.Sulfuras);
            var backstagePasses = program.Item(ShopItem.BackstagePasses);
            var conjuredManaCake = program.Item(ShopItem.ConjuredManaCake);

            const int expectedDexterityVestSellIn = 9;
            const int expectedAgedBrieSellIn = 1;
            const int expectedMongooseSellIn = 4;
            const int expectedSulfurasSellIn = 0;
            const int expectedBackstagePassesSellIn = 14;
            const int expectedConjuredManaCakeSellIn = 2;

            Assert.Equal(expectedDexterityVestSellIn, dexterityVest.SellIn);
            Assert.Equal(expectedAgedBrieSellIn, agedBrie.SellIn);
            Assert.Equal(expectedMongooseSellIn, elixirOfTheMongoose.SellIn);
            Assert.Equal(expectedSulfurasSellIn, sulfuras.SellIn);
            Assert.Equal(expectedBackstagePassesSellIn, backstagePasses.SellIn);
            Assert.Equal(expectedConjuredManaCakeSellIn, conjuredManaCake.SellIn);
        }

        [Fact]
        public void never_reduce_an_items_quality_so_that_it_is_negative()
        {
            var program = Program.CreateProgram();

            const int numberOftimesToUpdateQualityBy = 80;
            for (var i = 0; i < numberOftimesToUpdateQualityBy; i++)
            {
               program.UpdateQuality(); 
            }

            var dexterityVest = program.Item(ShopItem.DexterityVest);
            var agedBrie = program.Item(ShopItem.AgedBrie);
            var elixirOfTheMongoose = program.Item(ShopItem.ElixirOfTheMongoose);
            var sulfuras = program.Item(ShopItem.Sulfuras);
            var backstagePasses = program.Item(ShopItem.BackstagePasses);
            var conjuredManaCake = program.Item(ShopItem.ConjuredManaCake);

            Assert.False(dexterityVest.Quality < 0);
            Assert.False(agedBrie.Quality < 0);
            Assert.False(elixirOfTheMongoose.Quality < 0);
            Assert.False(sulfuras.Quality < 0);
            Assert.False(backstagePasses.Quality < 0);
            Assert.False(conjuredManaCake.Quality < 0);
        }

        [Fact]
        public void not_increase_quality_of_an_item_above_maximum_quality_of_50()
        {
            var program = Program.CreateProgram();

            const int maximumItemQuality = 50;
            const int timesToUpdateQualityBy = maximumItemQuality + 1;
            for (var i = 0; i < timesToUpdateQualityBy; i++)
            {
                program.UpdateQuality();
            }

            var agedBrie = program.Item(ShopItem.AgedBrie);
            
            Assert.Equal(agedBrie.Quality, 50);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 0)]
        public void increase_the_quality_of_aged_brie_by_1_when_sell_in_not_negative(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = Program.CreateProgram();

            for (var i = 0; i < timesToUpdateQualityBy; i++)
            {
                program.UpdateQuality();
            }

            var agedBrie = program.Item(ShopItem.AgedBrie);
            
            Assert.Equal(agedBrie.Quality, expectedQuality);
            Assert.Equal(agedBrie.SellIn, expectedSellIn);
        }

        [Fact]
        public void never_decrease_sulfuras_item_sell_in_when_quality_is_updated()
        {
            var program = Program.CreateProgram();
            program.UpdateQuality();

            var sulfuras = program.Item(ShopItem.Sulfuras);

            const int expectedSulfurasSellIn = 0;

            Assert.Equal(expectedSulfurasSellIn, sulfuras.SellIn);
        }

        [Fact]
        public void never_change_quality_of_sulfuras_when_quality_is_updated()
        {
            var program = Program.CreateProgram();
            var sulfuras = program.Item(ShopItem.Sulfuras);

            const int timesToUpdateQualityBy = 100;
            for (var i = 0; i < timesToUpdateQualityBy; i++)
            {
                program.UpdateQuality();
            }

            Assert.Equal(sulfuras.Quality, 80);
        }
    }
}