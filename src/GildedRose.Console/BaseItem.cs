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

            if (itemIsSulfuras) return;

            SellIn = SellIn - 1;

            UpdateNormalItem(currenItemIsNormalItem);

            if (itemIsAgedBrie && QualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            if (itemIsAgedBrie && SellIn < 0 && QualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            UpdateBackstagePasses(itemIsBackstagePasses);

            if (Quality > MaxItemQuality)
            {
                Quality = MaxItemQuality;
            }
        }

        private void UpdateNormalItem(bool currenItemIsNormalItem)
        {
            if (currenItemIsNormalItem && QualityAboveZero())
            {
                Quality = Quality - 1;
            }

            if (currenItemIsNormalItem && SellIn < 0 && QualityAboveZero())
            {
                Quality = Quality - 1;
            }
        }

        private void UpdateBackstagePasses(bool itemIsBackstagePasses)
        {
            if (itemIsBackstagePasses && SellIn > 10 && QualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            if (itemIsBackstagePasses && SellIn >= 6 && SellIn <= 10 && QualityBelowMaxQuality())
            {
                Quality = Quality + 2;
            }

            if (itemIsBackstagePasses && SellIn >= 0 && SellIn <= 5 && QualityBelowMaxQuality())
            {
                Quality = Quality + 3;
            }

            if (itemIsBackstagePasses && SellIn < 0 && QualityAboveZero())
            {
                Quality = Quality - Quality;
            }
        }

        private bool QualityBelowMaxQuality()
        {
            return Quality < MaxItemQuality;
        }

        private bool QualityAboveZero()
        {
            return Quality > 0;
        }
    }
}