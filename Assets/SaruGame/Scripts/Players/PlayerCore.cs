using UnityEngine;
using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using Saru.Damages;
using Saru.Items;

namespace Saru.Players
{
    public class PlayerCore : MonoBehaviour, IReceiveable
    {
        public BoolReactiveProperty IsPlayerControllable = new BoolReactiveProperty(true);

        public BoolReactiveProperty IsPlayerAliveable = new BoolReactiveProperty(true);

        private Subject<DamageInfo> _damageSubject = null;

        private Subject<ItemType> _pickItemSubject = null;



        public IObservable<DamageInfo> OnPlayerDamageObservable
        {
            get
            {
                if (_damageSubject == null) _damageSubject = new Subject<DamageInfo>();
                return _damageSubject.AsObservable();
            }
        }

        public IObservable<ItemType> OnPickupObservable
        {
            get
            {
                if (_pickItemSubject == null) _pickItemSubject = new Subject<ItemType>();
                return _pickItemSubject.AsObservable();
            }
        }

        public IObservable<bool> OnPlayerDeadObservable
        {
            get
            {
                return IsPlayerAliveable.Where(x => !x);
            }
        }



        public void ApplyDamage(DamageInfo damage)
        {
            if (_damageSubject != null)
            {
                _damageSubject.OnNext(damage);
            }
        }



        void Start()
        {
            IsPlayerControllable.Value = true;
            IsPlayerAliveable.Value = true;

            /// プレイヤーのダメージ処理
            OnPlayerDamageObservable
                .TakeUntil(OnPlayerDeadObservable)
                .Where(_ => IsPlayerControllable.Value)
                .Subscribe(_ =>
                {
                    IsPlayerAliveable.Value = false;
                });

            /// アイテムの取得処理
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.GetComponent<IItemObject>())
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    if (_pickItemSubject != null)
                    {
                        _pickItemSubject.OnNext(x.ItemType);
                    }
                    x.PickupItem();
                });
        }

        void OnDestroy()
        {
            if(_damageSubject != null) _damageSubject.OnCompleted();
            if(_pickItemSubject != null) _pickItemSubject.OnCompleted();

        }
    }
}