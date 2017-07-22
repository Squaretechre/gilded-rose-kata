namespace GildedRose.Console
{
    public class BackstagePassesItem : BaseItem
    {
        public override void UpdateQuality()
        {
            SellIn = SellIn - 1;

            if (SellIn > 10 && QualityBelowMaxQuality())
            {
                Quality = Quality + 1;
            }

            if (SellIn >= 6 && SellIn <= 10 && QualityBelowMaxQuality())
            {
                Quality = Quality + 2;
            }

            if (SellIn >= 0 && SellIn <= 5 && QualityBelowMaxQuality())
            {
                Quality = Quality + 3;
            }

            if (SellIn < 0 && QualityAboveZero())
            {
                Quality = Quality - Quality;
            }
        }
    }

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
            var itemIsSulfuras = Name == SulfurasHandOfRagnaros;

            if (itemIsSulfuras) return;
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