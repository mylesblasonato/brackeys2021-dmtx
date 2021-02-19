using System;
using UnityEngine;

[CreateAssetMenu]
public class Data : ScriptableObject
{
    public float _hiLevel = 1;
    public float _level = 1;
    public float _diffMultiplier = 1;
    public float _diffIncreaseAmount = 0.02f;

    public Action<float> _event;

    public float ChangeNumber(float num)
    {
        _event?.Invoke(num);
        return num;
    }
}
