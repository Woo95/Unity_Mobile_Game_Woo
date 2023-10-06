using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    [SerializeField]
    int _score = 0;
    int _previousScore = 0;

    [SerializeField]
    TextMeshProUGUI _scoreText;

    [SerializeField]
    [Range(0,10)]
    int _enemyNumber;

    GameObject _enemyPrefab;

    float _seconds = 0;
    int _timer = 0;

    GameObject _enemyParent;
    // Start is called before the first frame update
    void Start()
    {
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        _enemyParent = GameObject.Find("Enemies");
        for(int i = 0; i < _enemyNumber; i++)
        {

            Instantiate(_enemyPrefab, _enemyParent.transform);
        }

        _timer = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (_score != _previousScore)
            UpdateScoreUI();

/*        _seconds += Time.deltaTime;    // Timer enemy spawn system

        if(_seconds > _timer)
        {
            _seconds = 0;
            Instantiate(_enemyPrefab, _enemyParent.transform);
        }*/

    }

    public void ChangeScore(int scoreChangingAmount)
    {
        _score += scoreChangingAmount;

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        _scoreText.text = "Score: " + _score;

    }


   public void LoadGameScene()
    {
        SceneManager.LoadScene("Main");
    }
}
