namespace Core
{
    public interface IState<T> where T : class
    {
        void Enter();
        void Exit();
        void Update();
    }

    public interface IStateMachine<T> where T : class
    {
        T Context { get; }
        IState<T> CurrentState { get; }
        void SwitchState<State>() where State : IState<T>;
    }
}