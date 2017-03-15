using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

namespace Saru.GameManager
{
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
    {
        private IntReactiveProperty _score = new IntReactiveProperty(0);


        public IReadOnlyReactiveProperty<int> ScoreReactiveProperty
        {
            get
            {
                return _score.ToReadOnlyReactiveProperty();
            }
        }

        public void AddSocre(int score)
        {
            _score.Value += score;
        }

        void Awake()
        {
            _score.Value = 0;
        }
    }
}

