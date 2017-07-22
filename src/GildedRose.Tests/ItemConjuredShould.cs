using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class ItemConjuredShould
    {
        // - "Conjured" items degrade in Quality twice as fast as normal items

        [Theory]
        [InlineData(1, 4, 2)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 0, 0)]
        public void decrease_by_2_when_quality_updated(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();

            var conjuredManaCake = program.Item(ShopItem.ConjuredManaCake);

            Assert.Equal(expectedQuality, conjuredManaCake.Quality);
            Assert.Equal(expectedSellIn, conjuredManaCake.SellIn);
        }
    }
}