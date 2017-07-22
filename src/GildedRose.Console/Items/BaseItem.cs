namespace GildedRose.Console.Items
{
    public abstract class BaseItem : Item
    {
        protected int ItemQuality;
        protected const int MaxItemQuality = 50;

        public new int Quality
        {
            get => CheckQuality();
            set => ItemQuality = value;
        }

        public virtual int CheckQuality()
        {
            if (ItemQuality > MaxItemQuality)
            {
                ItemQuality = MaxItemQuality;
            }
            return ItemQuality;
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