using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadHealth : MonoBehaviour
{
    [SerializeField] private int _vidaCabeza = 700;

    #region methods
    public void TakeDamage(int damage)
    {
        _vidaCabeza -= damage;
        if (_vidaCabeza < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SpawnsManager.instance.SetfinishedGame(1);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    #endregion
}
