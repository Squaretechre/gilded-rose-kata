using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class AgedBrieShould
    {
        // ✓ "Aged Brie" actually increases in Quality the older it gets

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 0)]
        public void increase_quality_by_1_when_sell_in_not_negative(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);

            Assert.Equal(expectedQuality, agedBrie.Quality);
            Assert.Equal(expectedSellIn, agedBrie.SellIn);
        }

        [Theory]
        [InlineData(3, 4, -1)]
        [InlineData(4, 6, -2)]
        [InlineData(5, 8, -3)]
        [InlineData(10, 18, -8)]
        [InlineData(26, 50, -24)]
        public void increase_quality_by_2_when_sell_in_negative(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);

            Assert.Equal(expectedQuality, agedBrie.Quality);
            Assert.Equal(expectedSellIn, agedBrie.SellIn);
        }

        [Theory]
        [InlineData(27, 50, -25)]
        [InlineData(50, 50, -48)]
        [InlineData(100, 50, -98)]
        public void never_increase_quality_above_maximum_quality_of_50(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var agedBrie = program.Item(ShopItem.AgedBrie);

            Assert.Equal(expectedQuality, agedBrie.Quality);
            Assert.Equal(expectedSellIn, agedBrie.SellIn);
        }
    }
}