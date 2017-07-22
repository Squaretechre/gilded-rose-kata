namespace GildedRose.Console.Items
{
    internal class ItemConjured : BaseItem
    {
        public override void UpdateQuality()
        {
            SellIn = SellIn - 1;

            if (QualityBelowMaxQuality())
            {
                Quality = Quality - 2;
            }
        }
    }
}