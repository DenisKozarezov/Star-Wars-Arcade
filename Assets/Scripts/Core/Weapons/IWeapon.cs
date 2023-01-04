using System;
using Core.Units;

namespace Core.Weapons
{
    public interface IWeapon
    {
        event Action<IUnit> Hit;
        void Shoot();
    }
}
