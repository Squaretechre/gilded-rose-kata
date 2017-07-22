namespace GildedRose.Console
{
    public class BaseItem : Item
    {
        protected int _quality;
        protected const int MaxItemQuality = 50;
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
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