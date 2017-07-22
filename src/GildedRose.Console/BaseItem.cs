namespace GildedRose.Console
{
    public abstract class BaseItem : Item
    {
        private int _quality;
        protected const int MaxItemQuality = 50;
        private const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";

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

        public abstract void UpdateQuality();

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