namespace GildedRose.Console
{
    public class BaseItem : Item
    {
        private int _quality;
        private const int MaxItemQuality = 50;
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";

        public new int Quality
        {
            get
            {
                if (_quality > MaxItemQuality && Name != SulfurasHandOfRagnaros)
                {
                    _quality = MaxItemQuality;
                }
                return _quality;
            }
            set => _quality = value;
        }

        public void UpdateQuality()
        {
            var itemIsNotAgedBrie = Name != AgedBrie;
            var itemIsNotSulfuras = Name != SulfurasHandOfRagnaros;
            var itemIsNotBackstagePasses = Name != BackstagePasses;

            var itemIsBackstagePasses = Name == BackstagePasses;
            var itemIsSulfuras = Name == SulfurasHandOfRagnaros;
            var itemIsAgedBrie = Name == AgedBrie;

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