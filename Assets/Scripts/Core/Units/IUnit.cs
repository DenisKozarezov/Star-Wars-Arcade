using System;
using Core.Weapons;

namespace Core.Units
{
    public interface IUnit
    {
        event Action<IUnit> WeaponHit;
        ITransformable Transformable { get; }
        void SetPrimaryWeapon(IWeapon weapon);
        void Hit();
    }
}