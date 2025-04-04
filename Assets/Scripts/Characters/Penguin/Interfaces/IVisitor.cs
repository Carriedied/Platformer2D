namespace Assets.Scripts.Characters.Penguin.Interfaces
{
    public interface IVisitor
    {
        void Visit(Coin coin);
        void Visit(MedicalKit medicalKit);
    }
}
