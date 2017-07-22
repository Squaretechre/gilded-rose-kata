namespace GildedRose.Console
{
    public class NormalItem : BaseItem
    {
        public override void UpdateQuality()
        {
            SellIn = SellIn - 1;

            if (QualityAboveZero())
            {
                Quality = Quality - 1;
            }

            if (SellIn < 0 && QualityAboveZero())
            {
                Quality = Quality - 1;
            }
        }
    }

    public class AgedBrie : BaseItem
    {
        public override void UpdateQuality()
        {
            SellIn = SellIn - 1;
            
            if (QualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            if (SellIn < 0 && QualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }
        }
    }

    public class BaseItem : Item
    {
        protected int _quality;
        protected const int MaxItemQuality = 50;
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

        public virtual void UpdateQuality()
        {
            var itemIsBackstagePasses = Name == BackstagePasses;
            var itemIsSulfuras = Name == SulfurasHandOfRagnaros;
            var itemIsAgedBrie = Name == AgedBrie;

            if (itemIsSulfuras) return;

            SellIn = SellIn - 1;

            UpdateBackstagePasses(itemIsBackstagePasses);
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

        public bool QualityBelowMaxQuality()
        {
            return Quality < MaxItemQuality;
        }

        public bool QualityAboveZero()
        {
            return Quality > 0;
        }
    }
}