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

    [SerializeField] private float _cooldownDamagedColor;

    private float _initialCooldownDamagedColor;

    private bool _damagedC;

    [SerializeField]
    private Color[] _colores;   //Colores del enemigo

    [SerializeField]
    private Renderer _renderC; //Renderiza el color del enemigo

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
            _damagedC = true;
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
        _damagedC = false;
        _initialCooldownDamagedColor = _cooldownDamagedColor;
    }
    private void Update()
    {
        if (_damagedC)
        {
            _renderC.material.color = _colores[1];
            _cooldownDamagedColor -= Time.deltaTime;
            if (_cooldownDamagedColor <= 0)
            {
                _damagedC = false;
            }
        }
        else
        {
            _cooldownDamagedColor = _initialCooldownDamagedColor;
            _renderC.material.color = _colores[0];
        }
    }
}
