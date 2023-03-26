using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    #region parameters
    private bool _congelado;
    private bool _quemado;

    [SerializeField] private float _tiempoCongelado = 3f, _tiempoQuemado = 3, _contadorDeSegundos; 
    [SerializeField] private int _dañoPorSegQuemado= 5;

    private float _tiempoCongeladoInicial, _tiempoQuemadoInicial;
    private EnemyMovement _enemyMovement;
    private EnemyFlyingMovement _enemyFlyingMovement;
    private EnemyHealth _enemyHealth;
    #endregion

    #region getters && setters
    public bool GetCongelado()
    {
        return _congelado;
    }

    public void SetCongelado(bool congelado)
    {
        _congelado = congelado;
    }

    public bool GetQuemado()
    {
        return _quemado;
    }

    public void SetQuemado(bool quemado)
    {
        _quemado = quemado;
    }

    public void SetTiempoQuemado(float tiempoQuemado)
    {
        _tiempoQuemado = tiempoQuemado;
    }
    #endregion

    #region methods
    private void Congelado()
    {
        _tiempoCongelado -= Time.deltaTime;
        if (_tiempoCongelado <= 0)
        {
            _enemyHealth.SetNumBalasCongelado(0);
            if (GetComponent<EnemyMovement>() != null)
            {
                _enemyMovement = GetComponent<EnemyMovement>();
                _enemyMovement.SetEnemySpeed(_enemyMovement.GetEnemyInitialSpeed());
                _enemyMovement.SetEnemyDetectionSpeed(_enemyMovement.GetEnemyInitialDetectedSpeed());
            }
            else if (GetComponent<EnemyFlyingMovement>() != null)
            {
                _enemyFlyingMovement = GetComponent<EnemyFlyingMovement>();
                _enemyFlyingMovement.SetEnemySpeed(_enemyFlyingMovement.GetEnemyInitialSpeed());
                _enemyFlyingMovement.SetEnemyDetectedSpeed(_enemyFlyingMovement.GetEnemyInitialDetectedSpeed());
                _enemyFlyingMovement.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            _congelado = !_congelado;
            _tiempoCongelado = _tiempoCongeladoInicial;
        }
    }

    private void Quemado()
    {
        _tiempoQuemado -= Time.deltaTime;
        if(_tiempoQuemado < _contadorDeSegundos)
        {
            GetComponent<EnemyHealth>().TakeDamage(_dañoPorSegQuemado);
            _contadorDeSegundos--;
        }
        if (_tiempoQuemado <= 0)
        {
            _quemado = false;
            _tiempoQuemado = _tiempoQuemadoInicial;
            _contadorDeSegundos = _tiempoQuemado - 1f;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _congelado = false;
        _quemado=false;
        _tiempoCongeladoInicial = _tiempoCongelado;
        _enemyHealth = GetComponent<EnemyHealth>();
        _tiempoQuemadoInicial = _tiempoQuemado;
        _contadorDeSegundos = _tiempoQuemado - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_congelado)
        {
            Congelado();
        }
        if (_quemado)
        {
            Quemado();
        }
    }
}
