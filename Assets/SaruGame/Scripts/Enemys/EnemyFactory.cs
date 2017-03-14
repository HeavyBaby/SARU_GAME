using UnityEngine;
using System.Collections;

namespace Saru.Enemys
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPoints = null;

        [SerializeField]
        private GameObject _enemyPrefab = null;


        public IEnumerator CreateEnemy()
        {
            if(_spawnPoints.Length > 1)
            {
                var seed = Random.Range(0, _spawnPoints.Length);
                var enemy = Instantiate(_enemyPrefab);
                enemy.transform.position = _spawnPoints[seed].position;

            }
            yield return null;
        }

        public IEnumerator CreateEnemySequence()
        {
            while(true)
            {
                yield return CreateEnemy();
                yield return new WaitForSeconds(1.0f);
            }
        }

        void Start()
        {
            StartCoroutine(CreateEnemySequence());
        }
    }
}

