using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;
using Saru.GameManager;

namespace Saru.UI
{
    public class ScoreViewAndPresenter : MonoBehaviour
    {
        void Start()
        {
            var scoreManager = ScoreManager.Instance;
            var scoreText = GetComponent<Text>();

            /// Model -> View
            scoreManager.ScoreReactiveProperty
                .Subscribe(x => scoreText.text = "Score : " + x.ToString());
        }
    }
}

