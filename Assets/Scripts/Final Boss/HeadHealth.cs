using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHealth : MonoBehaviour
{
    [SerializeField] private int _vidaManos = 400;

    #region methods
    public void TakeDamage(int damage)
    {
        _vidaManos -= damage;
        if (_vidaManos < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SpawnsManager.instance.SetfinishedGame(1);
        Destroy(gameObject);
    }
    #endregion
}
