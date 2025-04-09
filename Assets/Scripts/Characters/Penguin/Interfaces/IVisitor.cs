namespace Assets.Scripts.Characters.Penguin.Interfaces
{
    public interface IVisitor
    {
        public void Visit(Coin coin);
        public void Visit(MedicalKit medicalKit);
    }
}
