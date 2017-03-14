using UnityEngine;
using System.Collections;
using System;
using UniRx;
using Saru.Bullets;

namespace Saru.Weapons
{
    public class SimpleBananaShot : WeaponBase
    {
        [SerializeField]
        private GameObject _bulletPrefab = null;

        void Attack()
        {
            var startPosition = WeaponTransform.position;
            var bullet = Instantiate(_bulletPrefab, startPosition, Quaternion.identity) as GameObject;

            bullet.GetComponent<BulletBase>().InitBullet(_attacker);

        }

        void Start()
        {
            OnAttackObservable
                .Where(x => !x)
                .FirstOrDefault()
                .Subscribe(_ =>
                {
                    OnAttackObservable
                    .DistinctUntilChanged()
                    .Where(x => x)
                    .ThrottleFirst(TimeSpan.FromSeconds(0.4f))
                    .Subscribe(__ => Attack());
                });
        }
    }
}

