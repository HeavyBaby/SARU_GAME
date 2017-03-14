using UnityEngine;
using System.Collections;
using UniRx;
using DG.Tweening;

namespace Saru.Enemys
{
    public class EnemyAnimator : MonoBehaviour
    {
        void Start()
        {
            var core = GetComponent<EnemyCore>();

            core.OnEnemyDead
                .Subscribe(_ =>
                {
                    var sequence = DOTween.Sequence();

                    sequence.Append(transform.DOMove(new Vector2(transform.position.x + -5.0f, transform.position.y + 7.0f), 1.2f));

                    sequence.Join(transform.DORotate(new Vector3(0, 0, 360f), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(3));

                    sequence.Join(transform.DOScale(0.0f, 1.0f));

                    sequence.Play();

                    Destroy(gameObject, 3.0f);
                });
        }
    }
}

