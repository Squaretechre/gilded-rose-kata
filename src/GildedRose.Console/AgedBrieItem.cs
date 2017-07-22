namespace GildedRose.Console
{
    public class AgedBrieItem : BaseItem
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
}