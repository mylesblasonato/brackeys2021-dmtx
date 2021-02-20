using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    [SerializeField] float _willPowerMax = 100;
    [SerializeField] float _willPowerIncrement = 1;
    [SerializeField] float _willPowerModeDuration = 5; // seconds
    public bool _willPowerMode = false;
    [SerializeField] Transform _meter;
    [SerializeField] ParticleSystem _vfx;
    [SerializeField] InputController _inputController;
    float _currentWillPower = 0;

    public Action _onFullWillPower, _onWpModeTick;

    private void Start()
    {
        _inputController._onInputDown += AddWillPower;
    }

    void AddWillPower()
    {
        if (_currentWillPower < _willPowerMax)
        {
            _currentWillPower += _willPowerIncrement;
            _meter.localScale = new Vector3(_currentWillPower / _willPowerMax, _meter.localScale.y, _meter.localScale.z);
        }
        else
        {
            _willPowerMode = true;
            StartCoroutine("WillPowerMode");
            _onFullWillPower?.Invoke();
        }
    }

    IEnumerator WillPowerMode()
    {
        var elapsed = 0f;
        _vfx.Play();
        AudioManager.Instance.ChangeVolume(2, 1);
        while (elapsed < _willPowerModeDuration * 1000)
        {
            _currentWillPower -= _willPowerIncrement;
            _meter.localScale = new Vector3(_currentWillPower / _willPowerMax, _meter.localScale.y, _meter.localScale.z);
            elapsed += 0.1f;
            yield return new WaitForSeconds(0.1f);          
        }
        _currentWillPower = 0;
        _vfx.Stop();
        AudioManager.Instance.ChangeVolume(2, 0.5f);
        _willPowerMode = false;
    }
}
