using UnityEngine;
using Core.Models;

namespace Core.Weapons
{
    public class BulletGunModel
    {
        public readonly float ReloadTime;
        public readonly BulletGunConfig BulletGunConfig;
        public readonly Transform FirePoint;
        public readonly BulletType BulletType;
        public readonly Cooldown Cooldown;

        public BulletGunModel(float reloadTime, BulletGunConfig config, Transform firePoint, BulletType bulletType)
        {
            ReloadTime = reloadTime;
            BulletGunConfig = config;
            FirePoint = firePoint;
            BulletType = bulletType;
            Cooldown = new Cooldown();
        }
    }
}
