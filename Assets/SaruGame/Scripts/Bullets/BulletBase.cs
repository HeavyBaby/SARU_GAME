using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using Saru.Damages;

namespace Saru.Bullets
{
    public abstract class BulletBase : MonoBehaviour
    {
        [SerializeField]
        protected float _bulletSpeed = 5.0f;

        [SerializeField]
        protected int _bulletPower = 1;

        protected IAttacker _attacker = null;


        public void InitBullet(IAttacker attacker)
        {
            _attacker = attacker;
        }

        protected abstract void HitBullet(GameObject hit);

        protected virtual void OnStart() { }

        protected void Start()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(hit =>
                {
                    if (hit.GetComponent<BulletBase>() != null) return;
                    HitBullet(hit.gameObject);

                });

            OnStart();
        }
    }
}
