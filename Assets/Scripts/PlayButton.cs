using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayButton : MonoBehaviour
{
    [SerializeField] Data _data;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _startingEnemyDiff = 0.09f;
    [SerializeField] int _startingBGMIndex = 0;

    private void Start()
    {
        _data._hiLevel = PlayerPrefs.GetFloat("TopLevel");
        _data._diffMultiplier = PlayerPrefs.GetFloat("Boss");
        _text.text = $"Top Level: {_data._hiLevel}";

        AudioManager.Instance.PlayBGM(0, 0, _startingBGMIndex);
        AudioManager.Instance.PlayBGM(2, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            AudioManager.Instance.PlayBGM(1, 0, _startingBGMIndex);
            SceneManager.LoadScene(1);
            _data._level = 0;
            _data._diffMultiplier = _startingEnemyDiff;
        }
    }
}
