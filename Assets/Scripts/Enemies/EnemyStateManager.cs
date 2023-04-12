using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    #region parameters
    private bool _congelado;
    private bool _quemado;
    private bool _ralentizado;

    [SerializeField] private float _tiempoCongelado = 3, _tiempoQuemado = 3, _tiempoRalentizado = 3;
    [SerializeField] private int _danoPorSegQuemado= 5;

    private float _tiempoCongeladoInicial, _tiempoQuemadoInicial, _contadorDeSegundos, _initialTiempoRalentizado;
    private EnemyMovement _enemyMovement;
    private EnemyFlyingMovement _enemyFlyingMovement;
    private EnemyHealth _enemyHealth;

    [SerializeField]
    private Color[] _colores;   //Array de colores del player

    [SerializeField]
    private Renderer _renderC; //Renderiza el color del player
    #endregion

    #region references
    private Animator _myAnimator;
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

    public void SetRalentizado(bool ralentizado)
    {
        _ralentizado = ralentizado;
    }

    public float GetTiempoRalentizadoInicial()
    {
        return _initialTiempoRalentizado;
    }

    public void SetTiempoRalentizado(float tiempoRalentizadoInicial)
    {
        _tiempoRalentizado = tiempoRalentizadoInicial;
    }

    public float GetTiempoCongeladoInicial()
    {
        return _tiempoCongeladoInicial;
    }

    public void SetTiempoCongelado(float tiempoCongeladoInicial)
    {
        _tiempoCongelado = tiempoCongeladoInicial;
    }
    #endregion

    #region methods

    private void Ralentizado()
    {
        _tiempoRalentizado -= Time.deltaTime;
        if (_tiempoRalentizado <= 0)
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
            _ralentizado = !_ralentizado;
            _tiempoRalentizado = _initialTiempoRalentizado;
        }
    }

    private void Congelado()
    {
        if (_ralentizado)
        {
            _tiempoRalentizado = _initialTiempoRalentizado;
            _ralentizado = false;
        }
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
            GetComponent<EnemyHealth>().TakeDamage(_danoPorSegQuemado);
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
        _myAnimator = GetComponent<Animator>();
        _tiempoQuemadoInicial = _tiempoQuemado;
        _contadorDeSegundos = _tiempoQuemado - 1f;
        _initialTiempoRalentizado = _tiempoRalentizado;
    }

    // Update is called once per frame
    void Update()
    {
        _myAnimator.SetBool("_congelado", _congelado);

        if (_congelado)
        {
            Congelado();
        }
        if (_quemado)
        {
            Quemado();
        }
        if(_ralentizado && !_congelado)
        {
            Ralentizado();
        }
        
        //Gesti�n de colores cuando est� ralentizado o quemado
        if (_ralentizado)
        {
            _renderC.material.color = _colores[0];
        }
        else if (_quemado)
        {
            _renderC.material.color = _colores[1];
        }
        else
        {
            _renderC.material.color = _colores[2]; //color original
        }
    }
}
