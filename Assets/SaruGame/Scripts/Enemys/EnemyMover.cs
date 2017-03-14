using UnityEngine;
using System.Collections;
using UniRx;

namespace Saru.Enemys
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 3.0f;

        void Start()
        {
            var core = GetComponent<EnemyCore>();
            var rb2d = GetComponent<Rigidbody2D>();

            rb2d.velocity = Vector2.left * moveSpeed;

            core.OnEnemyDead.Where(x => x).DelayFrame(1).Subscribe(_ => rb2d.velocity = Vector2.zero);
                
        }
    }
}
