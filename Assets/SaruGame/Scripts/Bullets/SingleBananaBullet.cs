using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using Saru.Damages;

namespace Saru.Bullets
{
    public class SingleBananaBullet : BulletBase
    {
        private Rigidbody2D _rb2d = null;

        [SerializeField]
        private float _lifeTime = 3.0f;


        protected override void OnStart()
        {
            base.OnStart();

            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.velocity = Vector2.right * _bulletSpeed;

            transform.DORotate(new Vector3(0, 0, -360), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);

            Destroy(gameObject, _lifeTime);

        }

        protected override void HitBullet(GameObject hitTarget)
        {
            /// 攻撃者が自分以外
            var hitAttacker = hitTarget.GetComponent<IAttacker>();
            if (hitAttacker != null && hitAttacker.OwnerId == _attacker.OwnerId) return;

            /// 受け手がいる場合ダメージを与える。
            var receiveable = hitTarget.GetComponent<IReceiveable>();
            if (receiveable == null) return;
            var damageInfo = new DamageInfo(_bulletPower, _attacker);

            receiveable.ApplyDamage(damageInfo);

            Destroy(gameObject);
        }
    }
}

