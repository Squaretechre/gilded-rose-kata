namespace GildedRose.Console
{
    public class BaseItem : Item
    {
        private const int MaxItemQuality = 50;

        public void UpdateQuality()
        {
            var itemIsNotAgedBrie = Name != "Aged Brie";
            var itemIsNotSulfuras = Name != "Sulfuras, Hand of Ragnaros";
            var itemIsNotBackstagePasses = Name != "Backstage passes to a TAFKAL80ETC concert";

            var itemIsBackstagePasses = Name == "Backstage passes to a TAFKAL80ETC concert";
            var itemIsSulfuras = Name == "Sulfuras, Hand of Ragnaros";
            var itemIsAgedBrie = Name == "Aged Brie";

            var currenItemIsNormalItem = itemIsNotSulfuras &&
                                         itemIsNotAgedBrie &&
                                         itemIsNotBackstagePasses;

            var currentItemIsNotNormalItem = !currenItemIsNormalItem;

            if (itemIsSulfuras) return;

            SellIn = SellIn - 1;

            if (currenItemIsNormalItem && ItemQualityAboveZero())
            {
                Quality = Quality - 1;
            }

            if (currenItemIsNormalItem && SellIn < 0 && Quality > 0)
            {
                Quality = Quality - 1;
            }

            if (currentItemIsNotNormalItem && itemIsNotBackstagePasses && ItemQualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            if (itemIsBackstagePasses && SellIn > 10 && ItemQualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            if (itemIsBackstagePasses && SellIn >= 6 && SellIn <= 10 && ItemQualityBelowMaxQuality())
            {
                Quality = Quality + 2;
            }

            if (itemIsBackstagePasses && SellIn >= 0 && SellIn <= 5 && ItemQualityBelowMaxQuality())
            {
                Quality = Quality + 3;
            }

            if (itemIsBackstagePasses && SellIn < 0 && Quality > 0)
            {
                Quality = Quality - Quality;
            }

            if (itemIsAgedBrie && SellIn < 0 && ItemQualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            if (Quality > MaxItemQuality)
            {
                Quality = MaxItemQuality;
            }
        }

        private bool ItemQualityBelowMaxQuality()
        {
            return Quality < MaxItemQuality;
        }

        private bool ItemQualityAboveZero()
        {
            return Quality > 0;
        }
    }
}