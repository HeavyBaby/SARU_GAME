using UnityEngine;
using System.Collections;
using UniRx;
using DG.Tweening;

namespace Saru.Players
{
    public class PlayerAnimator : MonoBehaviour
    {

        void Start()
        {
            var core = GetComponent<PlayerCore>();

            core.OnPlayerDeadObservable
                .Subscribe(_ =>
                {
                    var sequence = DOTween.Sequence();

                    sequence.Append(
                        transform.DOMoveY(8.0f, 0.5f)
                            );

                    sequence.Append(
                        transform.DORotate(new Vector3(180.0f, 0.0f, 0.0f),0.1f,RotateMode.FastBeyond360)
                        );

                    sequence.Append(
                        transform.DOScale(1.5f, 0.1f)
                        );

                    sequence.Append(
                        transform.DOMoveY(-20.0f,1.0f)
                        );

                    sequence.Play();

                });
        }

    }
}

