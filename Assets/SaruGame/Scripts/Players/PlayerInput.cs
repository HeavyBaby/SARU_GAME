using UnityEngine;
using System.Collections;
using System;
using UniRx;
using UniRx.Triggers;

namespace Saru.Players
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private Subject<Vector2> _moveDirectionSubject = new Subject<Vector2>();

        private Subject<bool> _attackButtonSubject = new Subject<bool>();



        public IReadOnlyReactiveProperty<Vector2> MoveDirectionRP
        {
            get
            {
                return _moveDirectionSubject.ToSequentialReadOnlyReactiveProperty();
            }
        }

        public IObservable<bool> OnAttackButtonObservable
        {
            get
            {
                return _attackButtonSubject.AsObservable();
            }
        }


        void Start()
        {
            /// 移動イベントの通知
            this.UpdateAsObservable()
                .Select(_ => new Vector2
                {
                    x = Input.GetAxisRaw("Horizontal"),
                    y = Input.GetAxisRaw("Vertical")
                }.normalized)
                .Subscribe(_moveDirectionSubject);

            /// 攻撃イベントの通知
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Space))
                .Subscribe(_attackButtonSubject);
        }
    }
}

