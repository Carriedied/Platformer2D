namespace Assets.Scripts.Characters.Penguin.Interfaces
{
    internal interface IVisitor
    {
        void Visit(Coin coin);
        void Visit(MedicalKit medicalKit);
    }
}
