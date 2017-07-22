namespace GildedRose.Console.Items
{
    public class ItemSulfuras : BaseItem
    {
        public override void UpdateQuality()
        {
        }

        public override int CheckQuality()
        {
            return ItemQuality;
        }
    }
}