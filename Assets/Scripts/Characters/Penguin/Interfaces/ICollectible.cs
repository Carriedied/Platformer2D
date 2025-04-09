namespace Assets.Scripts.Characters.Penguin.Interfaces
{
    public interface ICollectible
    {
        public void Accept(IVisitor visitor);
    }
}
