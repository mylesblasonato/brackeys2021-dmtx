using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] Animator _animator, _ropeAnimator;
    [SerializeField] GameObject _TugOfWarGroup;
    [SerializeField] ParticleSystem _vfx;

    private void Update()
    {
        if (!GameManager.Instance._gameOver)
        {
            if (Input.anyKeyDown)
            {
                Move(-transform.right, GameManager.Instance._speed);
                _vfx.Play();
                AudioManager.Instance.PlaySFX(1, 1, 1);
                GameManager.Instance._speed += GameManager.Instance._multiplier;
            }
            
            if(Input.anyKey)
            {
                SetAnimatorBool(true);
            }
            else
            {
                SetAnimatorBool(false);
            }

            Move(transform.right, GameManager.Instance._enemySpeed * GameManager.Instance._data._diffMultiplier);
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
