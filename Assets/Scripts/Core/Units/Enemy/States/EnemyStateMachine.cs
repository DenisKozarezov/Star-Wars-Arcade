using System.Collections.Generic;
using System.Linq;

namespace Core.Units
{
    public class EnemyStateMachine : IStateMachine<EnemyController>
    {
        private List<IState<EnemyController>> _states;
        private IState<EnemyController> _currentState;
        public EnemyController Context { get; private set; }
        public IState<EnemyController> CurrentState => _currentState;

        public EnemyStateMachine(EnemyController enemy, EnemyModel model)
        {
            Context = enemy;

            _states = new List<IState<EnemyController>>()
            {
                new EnemyPatrolState(this, model),
                new EnemyAttackState(this, model)
            };
            SwitchState<EnemyPatrolState>();
        }
        public void SwitchState<State>() where State : IState<EnemyController>
        {
            _currentState?.Exit();
            _currentState = _states.FirstOrDefault(state => state is State);
            _currentState.Enter();
        }     
    }
}