namespace Core.Units
{
    public interface IEnemy
    {
        IUnit Target { get; }
        bool IsTaunted { get; }
        void Taunt(IUnit unit);
    }
}