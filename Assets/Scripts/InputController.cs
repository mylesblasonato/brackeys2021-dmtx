using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] Animator _animator, _ropeAnimator;
    [SerializeField] GameObject _TugOfWarGroup;
    [SerializeField] ParticleSystem _vfx;
    [SerializeField] MeterController _meterController;

    public Action _onInputDown;

    public void WillPowerMode()
    {
        _onInputDown?.Invoke();
        SetAnimatorBool(true);
        Move(-transform.right, GameManager.Instance._speed * Time.deltaTime);
        _vfx.Play();
        GameManager.Instance._speed += GameManager.Instance._multiplier;
    }

    private void Update()
    {
        if (!GameManager.Instance._gameOver)
        {
            if (!_meterController._willPowerMode)
            {
                if (Input.anyKeyDown)
                {
                    _onInputDown?.Invoke();
                    Move(-transform.right, GameManager.Instance._speed / 10);
                    _vfx.Play();
                    AudioManager.Instance.ChangeVolume(2, 0.5f);
                    GameManager.Instance._speed += GameManager.Instance._multiplier;
                }

                if (Input.anyKey)
                {
                    SetAnimatorBool(true);
                }
                else
                {
                    SetAnimatorBool(false);
                }
            }
            else
            {
                WillPowerMode();
            }

            Move(transform.right, GameManager.Instance._enemySpeed * GameManager.Instance._data._diffMultiplier * Time.deltaTime);
        }
    }

    public void Move(Vector3 dir, float speed)
    {
        _TugOfWarGroup.transform.Translate(dir * speed * Time.fixedDeltaTime);
    }

    public void SetAnimatorBool(bool flag)
    {
        _animator.SetBool("Tug", flag);
        _ropeAnimator.SetBool("Tug", flag);
    }
}
