using System.Collections.Generic;
using Core.Units;

namespace Core
{
    public class EnemyRegistry
    {
        private readonly LinkedList<IEnemy> _enemies = new LinkedList<IEnemy>();

        public int Count => _enemies.Count;

        public void Register(IEnemy enemy)
        {
            if (!_enemies.Contains(enemy)) _enemies.AddLast(enemy);
        }
        public void Unregister(IEnemy enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}