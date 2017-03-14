using UnityEngine;
using System;
using System.Collections;
using UniRx;
using Saru.Damages;

namespace Saru.Enemys
{
    public class EnemyCore : MonoBehaviour, IReceiveable
    {
        private Subject<DamageInfo> _enemyDamageSubject = null;

        public BoolReactiveProperty IsEnemyAliveable = new BoolReactiveProperty(true);


        public IObservable<DamageInfo> OnEnemyDamage
        {
            get
            {
                if (_enemyDamageSubject == null) _enemyDamageSubject = new Subject<DamageInfo>();
                return _enemyDamageSubject.AsObservable();
            }
        }

        public IObservable<bool> OnEnemyDead
        {
            get { return IsEnemyAliveable.Where(x => !x); }
        }


        public void ApplyDamage(DamageInfo damage)
        {
            if(_enemyDamageSubject != null)
            {
                _enemyDamageSubject.OnNext(damage);
            }
        }



        void Start()
        {
            IsEnemyAliveable.Value = true;

            OnEnemyDamage
                .TakeUntil(OnEnemyDead)
                .Subscribe(_ =>
                {
                    IsEnemyAliveable.Value = false;
                });
        }

        void OnDestroy()
        {
            _enemyDamageSubject.OnCompleted();
        }
    }
}

