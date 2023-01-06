using System;
using Core.Weapons;

namespace Core.Units
{
    public interface IUnit
    {
        event Action<IUnit> WeaponHit;
        void SetPrimaryWeapon(IWeapon weapon);
        void Hit();
    }
}