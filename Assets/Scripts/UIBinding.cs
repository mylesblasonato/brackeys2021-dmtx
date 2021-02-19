using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBinding : MonoBehaviour
{
    [SerializeField] Data _data;
    [SerializeField] TextMeshProUGUI _text;

    private void Awake()
    {
        _data._event += UpdateText;
    }

    private void UpdateText(float num)
    {
        _text.text = $"Level: {num}";
    }
}
