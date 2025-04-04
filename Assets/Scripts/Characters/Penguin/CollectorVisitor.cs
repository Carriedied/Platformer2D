using Assets.Scripts.Characters.Penguin.Interfaces;

namespace Assets.Scripts.Characters.Penguin
{
    internal class CollectorVisitor : IVisitor
    {
        private WalletPenguin _purse;
        private Health _health;
        private float _countMedicines;

        public CollectorVisitor(WalletPenguin purse, Health health, float countMedicines)
        {
            _purse = purse;
            _health = health;
            _countMedicines = countMedicines;
        }

        public void Visit(Coin coin)
        {
            _purse.AddCoin();
        }

        public void Visit(MedicalKit medicalKit)
        {
            _health.RestoreHealth(_countMedicines);
        }
    }
}
