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
}