using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager> {

    [SerializeField]
    private Text _scoreText = null;

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

    void Start()
    {
        ScoreReactiveProperty
            .Subscribe(x =>
            {
                _scoreText.text = "Score : " + x.ToString();
            });
    }
}
