using GildedRose.Console;
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
            ✓ "Aged Brie" actually increases in Quality the older it gets
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
        public void decrease_ordinary_item_quality_by_1_when_sell_in_date_is_greater_than_or_equal_to_zero()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(10).Build();
            var dexterityVest = program.Item(ShopItem.DexterityVest);
            var expectedQuality = 10;
            var expectedSellIn = 0;
            Assert.Equal(expectedQuality, dexterityVest.Quality);
            Assert.Equal(expectedSellIn, dexterityVest.SellIn);
        }

        [Fact]
        public void decrease_ordinary_item_quality_by_1_when_quality_is_updated()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(1).Build();

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
            var program = new ProgramBuilder().WithUpdatedQuality(1).Build();

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
            var program = new ProgramBuilder().WithUpdatedQuality(80).Build();

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
            const int maximumItemQuality = 50;
            const int timesToUpdateQualityBy = maximumItemQuality + 1;
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);
            
            Assert.Equal(agedBrie.Quality, 50);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 0)]
        public void increase_the_quality_of_aged_brie_by_1_when_sell_in_not_negative(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);
            
            Assert.Equal(agedBrie.Quality, expectedQuality);
            Assert.Equal(agedBrie.SellIn, expectedSellIn);
        }

        [Theory]
        [InlineData(3, 4, -1)]
        [InlineData(4, 6, -2)]
        [InlineData(5, 8, -3)]
        [InlineData(10, 18, -8)]
        [InlineData(26, 50, -24)]
        public void increase_the_quality_of_aged_brie_by_2_when_sell_in_negative(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);

            Assert.Equal(agedBrie.Quality, expectedQuality);
            Assert.Equal(agedBrie.SellIn, expectedSellIn);
        }

        [Theory]
        [InlineData(27, 50, -25)]
        [InlineData(50, 50, -48)]
        [InlineData(100, 50, -98)]
        public void never_increase_aged_brie_quality_above_maximum_quality_of_50(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);

            Assert.Equal(agedBrie.Quality, expectedQuality);
            Assert.Equal(agedBrie.SellIn, expectedSellIn);
        }

        [Fact]
        public void never_decrease_sulfuras_item_sell_in_when_quality_is_updated()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(1).Build();

            var sulfuras = program.Item(ShopItem.Sulfuras);

            const int expectedSulfurasSellIn = 0;

            Assert.Equal(expectedSulfurasSellIn, sulfuras.SellIn);
        }

        [Fact]
        public void never_change_quality_of_sulfuras_when_quality_is_updated()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(100).Build(); 
            var sulfuras = program.Item(ShopItem.Sulfuras);

            Assert.Equal(sulfuras.Quality, 80);
        }
    }

    internal class ProgramBuilder
    {
        private readonly Program _program;

        public ProgramBuilder()
        {
            _program = Program.CreateProgram();
        }

        public ProgramBuilder WithUpdatedQuality(int times)
        {
            for (var i = 0; i < times; i++)
            {
                _program.UpdateQuality();
            }
            return this;
        }

        public Program Build() => _program;
    }
}