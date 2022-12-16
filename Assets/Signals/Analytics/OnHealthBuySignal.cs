namespace Signals.Analytics
{
    public class OnHealthBuySignal
    {
        public readonly int HealthValue;
        public readonly int Goldlost;

        public OnHealthBuySignal(int health, int gold)
        {
            HealthValue = health;
            Goldlost = gold;
        }
    }
}