using UnityEngine;

namespace Core
{
    public interface IState<T> where T : MonoBehaviour
    {
        void Enter();
        void Exit();
        void Update();
    }

    public interface IStateMachine<T> where T : MonoBehaviour
    {
        T Context { get; }
        IState<T> CurrentState { get; }
        void SwitchState<State>() where State : IState<T>;
    }
}