using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // VARS
    public Data _data;
    public InputController _controller;
    [SerializeField] int _startingBGMIndex = 1;
    [SerializeField] Animator[] _animators;
    [SerializeField] ParticleSystem _enemyVfx;
    [SerializeField] TextMeshProUGUI _text, _hiLevel, level;
    public float _speed, _enemySpeed, _multiplier, _diffIncreaseAmount;
    public bool _gameOver = false;

    // SINGLETON
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _data._level += _data.ChangeNumber(1);
        _enemySpeed = _data._diffMultiplier;
        _diffIncreaseAmount = _data._diffIncreaseAmount;

        AudioManager.Instance.PlayBGM(0, 0, _startingBGMIndex);
        AudioManager.Instance.PlayBGM(2, 2, 0);

        if (_data._level > _data._hiLevel)
        {
            _data._hiLevel = _data._level;          
        }

        _hiLevel.text = $"Top Level: {_data._hiLevel}";
        level.text = $"Level: {_data._level}";
    }

    public void GameOver()
    {
        GameManager.Instance._gameOver = true;
        _enemyVfx.Stop();
        foreach (Animator anim in _animators) anim.enabled = false;
        _text.text = "You lose sucker!";
        Invoke("ReturnToMenu", 2f);
    }

    public void NextLevel()
    {
        GameManager.Instance._gameOver = true;
        _enemyVfx.Stop();
        foreach (Animator anim in _animators) anim.enabled = false;       
        _text.text = "You win champ!";
        Invoke("UpdateLevel", 2f);
    }

    public void UpdateLevel()
    {
        _enemySpeed += _diffIncreaseAmount;
        _data._diffMultiplier = _enemySpeed;
        AudioManager.Instance.PlayBGM(1, 0, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        PlayerPrefs.SetFloat("TopLevel", _data._hiLevel);
        PlayerPrefs.SetFloat("Boss", _data._diffMultiplier);
        AudioManager.Instance.PlayBGM(1, 0, 0);
        Destroy(AudioManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
