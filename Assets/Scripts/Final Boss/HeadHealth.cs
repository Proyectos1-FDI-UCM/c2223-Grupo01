using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadHealth : MonoBehaviour
{
    [SerializeField] private int _vidaCabeza = 700;
    private int _vidaCabezaInicial;
    [SerializeField] private BossUI _bossUI;

    #region Getters
    public int GetVidaCabeza()
    {
        return _vidaCabeza;
    }

    public int GetVidaCabezaInicial()
    {
        return _vidaCabezaInicial;
    }
    #endregion

    #region methods
    public void TakeDamage(int damage)
    {
        _vidaCabeza -= damage;
        if (_vidaCabeza < 0)
        {
            Die();
        }
        _bossUI.ActualizaVidaCabeza();
    }

    private void Die()
    {
        SpawnsManager.instance.SetfinishedGame(1);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    #endregion
    private void Start()
    {
        _vidaCabezaInicial = _vidaCabeza;
    }
}
