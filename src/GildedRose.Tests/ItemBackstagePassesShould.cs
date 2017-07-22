using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class ItemBackstagePassesShould
    {
        /*
            - "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; 
            -   Quality increases by 2 when there are 10 times or less 
            -   By 3 when there are 5 times or less
            -   Quality drops to 0 after the concert
         */

        [Fact]
        public void increase_quality_by_1_when_there_are_more_than_10_days_left()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(4).Build();
            var passes = program.Item(ShopItem.BackstagePasses);

            const int expectedQuality = 24;
            const int expectedSellIn = 11;

            Assert.Equal(expectedQuality, passes.Quality);
            Assert.Equal(expectedSellIn, passes.SellIn);
        }

        [Fact]
        public void increase_quality_by_2_when_sell_in_is_10()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(5).Build();
            var passes = program.Item(ShopItem.BackstagePasses);

            const int expectedQuality = 26;
            const int expectedSellIn = 10;

            Assert.Equal(expectedQuality, passes.Quality);
            Assert.Equal(expectedSellIn, passes.SellIn);
        }

        [Theory]
        [InlineData(6, 28, 9)]
        [InlineData(7, 30, 8)]
        [InlineData(8, 32, 7)]
        [InlineData(9, 34, 6)]
        public void increase_quality_by_2_when_sell_in_is_between_9_and_6(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();
            var passes = program.Item(ShopItem.BackstagePasses);

            Assert.Equal(expectedQuality, passes.Quality);
            Assert.Equal(expectedSellIn, passes.SellIn);
        }

        [Fact]
        public void increase_quality_by_3_when_sell_in_is_5()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(10).Build();
            var passes = program.Item(ShopItem.BackstagePasses);

            const int expectedQuality = 37;
            const int expectedSellIn = 5;

            Assert.Equal(expectedQuality, passes.Quality);
            Assert.Equal(expectedSellIn, passes.SellIn);
        }

        [Theory]
        [InlineData(11, 40, 4)]
        [InlineData(12, 43, 3)]
        [InlineData(13, 46, 2)]
        [InlineData(14, 49, 1)]
        [InlineData(15, 50, 0)]
        public void increase_quality_by_3_when_sell_in_is_between_4_and_0(int timesToUpdateQualityBy, int expectedQuality, int expectedSellIn)
        {
            var program = new ProgramBuilder().WithUpdatedQuality(timesToUpdateQualityBy).Build();
            var passes = program.Item(ShopItem.BackstagePasses);

            Assert.Equal(expectedQuality, passes.Quality);
            Assert.Equal(expectedSellIn, passes.SellIn);
        }

        [Fact]
        public void still_have_quality_value_when_sell_in_is_zero()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(15).Build();
            var passes = program.Item(ShopItem.BackstagePasses);

            const int expectedQuality = 50;
            const int expectedSellIn = 0;

            Assert.Equal(expectedQuality, passes.Quality);
            Assert.Equal(expectedSellIn, passes.SellIn);
        }

        [Fact]
        public void have_quality_of_zero_when_sell_in_is_negative()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(16).Build();
            var passes = program.Item(ShopItem.BackstagePasses);

            const int expectedQuality = 0;
            const int expectedSellIn = -1;

            Assert.Equal(expectedQuality, passes.Quality);
            Assert.Equal(expectedSellIn, passes.SellIn);
        }
    }
}