using UnityEngine;
using System;
using System.Collections;
using UniRx;
using Saru.Damages;
using Saru.GameManager;

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

        /// <summary>
        /// ダメージを与える。
        /// </summary>
        /// <param name="damage">ダメージの情報</param>
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

            var scoreManager = ScoreManager.Instance;

            /// 敵がダメージを受けたときの処理
            OnEnemyDamage
                .TakeUntil(OnEnemyDead)
                .Subscribe(_ =>
                {
                    IsEnemyAliveable.Value = false;
                });

            /// 敵が死んだときの処理
            OnEnemyDead
                .Subscribe(_ =>
                {
                    scoreManager.AddSocre(10);
                });

            /// 敵のxが-10以下のとき死ぬ処理
            transform.ObserveEveryValueChanged(x => x.position)
                .Select(pos => pos.x)
                .Where(x => x < -10.0f)
                .Subscribe(_ => Destroy(gameObject));

        }

        void OnDestroy()
        {
            if(_enemyDamageSubject != null) _enemyDamageSubject.OnCompleted();

        }
    }
}

