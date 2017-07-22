using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class ItemSulfurasShould
    {
        /*
            ✓ "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            
            ✓ Just for clarification, an item can never have its Quality increase above 50, however 
              "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.
         */

        [Fact]
        public void never_decrease_sell_in_when_quality_is_updated()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(1).Build();

            var sulfuras = program.Item(ShopItem.Sulfuras);

            const int expectedSulfurasSellIn = 0;

            Assert.Equal(expectedSulfurasSellIn, sulfuras.SellIn);
        }

        [Fact]
        public void never_decrease_quality_value_when_quality_is_updated()
        {
            var program = new ProgramBuilder().WithUpdatedQuality(100).Build();
            var sulfuras = program.Item(ShopItem.Sulfuras);

            Assert.Equal(80, sulfuras.Quality);
        }
    }
}