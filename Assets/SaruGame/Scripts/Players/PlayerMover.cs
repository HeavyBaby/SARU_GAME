using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace Saru.Players
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3.0f;

        [SerializeField]
        private Vector2 _moveVector = Vector2.zero;

        void Start()
        {
            var input = GetComponent<IPlayerInput>();
            var core = GetComponent<PlayerCore>();
            var rb2d = GetComponent<Rigidbody2D>();

            /// 移動イベントの受取り
            input.MoveDirectionRP
                .TakeUntil(core.OnPlayerDeadObservable)
                .Where(_ => core.IsPlayerControllable.Value)
                .Subscribe(v =>
                {
                    _moveVector = v * _moveSpeed;
                });

            /// 移動処理
            this.FixedUpdateAsObservable()
                .TakeUntil(core.OnPlayerDeadObservable)
                .Where(_ => core.IsPlayerControllable.Value)
                .Subscribe(_ =>
                {
                    rb2d.velocity = _moveVector;
                    _moveVector = Vector2.zero;

                });

        }
    }
}
