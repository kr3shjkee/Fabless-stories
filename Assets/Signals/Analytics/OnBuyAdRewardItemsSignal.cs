namespace Signals.Analytics
{
    public class OnBuyAdRewardItemsSignal
    {
        public readonly int ItemId;
        public readonly int GoldValue;

        public OnBuyAdRewardItemsSignal(int id, int gold)
        {
            ItemId = id;
            GoldValue = gold;
        }
    }
}