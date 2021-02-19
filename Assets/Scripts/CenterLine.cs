using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            GameManager.Instance.GameOver();
        }

        if (other.CompareTag("enemy"))
        {
            GameManager.Instance.NextLevel();
        }
    }
}
