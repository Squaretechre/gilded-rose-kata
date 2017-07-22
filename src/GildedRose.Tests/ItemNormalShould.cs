using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class ItemNormalShould
    {
        /*
            - All items have a SellIn value which denotes the number of times we have to sell the item
            - All items have a Quality value which denotes how valuable the item is
            - At the end of each day our system lowers both values for every item

            ✓ The Quality of an item is never negative
            ✓ The Quality of an item is never more than 50
            ✓ Once the sell by date has passed, Quality degrades twice as fast
         */

        [Fact]
        public void decrease_quality_by_1_when_sell_in_date_is_greater_than_or_equal_to_zero()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(10).Build();
            var dexterityVest = program.Item(ShopItem.DexterityVest);
            const int expectedQuality = 10;
            const int expectedSellIn = 0;
            Assert.Equal(expectedQuality, dexterityVest.Quality);
            Assert.Equal(expectedSellIn, dexterityVest.SellIn);
        }

        [Fact]
        public void decrease_quality_by_1_when_quality_is_updated()
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
        public void decrease_sell_in_date_by_1_when_quality_is_updated()
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

        [Theory]
        [InlineData(11, 8, -1)]
        [InlineData(12, 6, -2)]
        [InlineData(13, 4, -3)]
        [InlineData(14, 2, -4)]
        [InlineData(15, 0, -5)]
        [InlineData(16, 0, -6)]
        public void decrease_quality_by_2_when_sell_in_below_zero(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();
            var dexterityVest = program.Item(ShopItem.DexterityVest);
            Assert.Equal(expectedQuality, dexterityVest.Quality);
            Assert.Equal(expectedSellIn, dexterityVest.SellIn);
        }

        [Fact]
        public void never_have_a_negative_quality_value()
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
        public void never_have_quality_above_maximum_quality_of_50()
        {
            const int maximumItemQuality = 50;
            const int timesToUpdateQualityBy = maximumItemQuality + 1;
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);

            Assert.Equal(50, agedBrie.Quality);
        }
    }
}