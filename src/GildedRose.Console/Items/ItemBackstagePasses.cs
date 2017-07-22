namespace GildedRose.Console.Items
{
    public class ItemBackstagePasses : BaseItem
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
}