using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadHealth : MonoBehaviour
{
    [SerializeField] private float _vidaCabeza = 700;
    private float _vidaCabezaInicial;
    [SerializeField] private BossUI _bossUI;
    [SerializeField] private AudioClip _hurt;

    #region Getters
    public float GetVidaCabeza()
    {
        return _vidaCabeza;
    }

    public float GetVidaCabezaInicial()
    {
        return _vidaCabezaInicial;
    }
    #endregion

    #region methods
    public void TakeDamage(int damage)
    {
        if (GetComponent<FaseFinalJefe>().enabled)
        {
            GetComponent<AudioSource>().PlayOneShot(_hurt);
            _vidaCabeza -= damage;
            if (_vidaCabeza < 0)
            {
                Die();
            }
            _bossUI.ActualizaVidaCabeza();
        }
    }

    private void Die()
    {
        SpawnsManager.instance.SetfinishedGame(1);
        SceneManager.LoadScene(12);
        Destroy(gameObject);
    }
    #endregion
    private void Start()
    {
        _vidaCabezaInicial = _vidaCabeza;
    }
}
