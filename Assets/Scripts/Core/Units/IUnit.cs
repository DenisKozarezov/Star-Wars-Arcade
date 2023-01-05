using Core.Weapons;
using System;

namespace Core.Units
{
    public interface IUnit
    {
        ITransformable Transformable { get; }
        event Action<IUnit> WeaponHit;
        void SetPrimaryWeapon(IWeapon weapon);
        void Hit();
    }
}