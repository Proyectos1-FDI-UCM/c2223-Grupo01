using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    #region parameters
    private bool _congelado;
    private bool _quemado;
    private bool _ralentizado;

    private bool _enableCongeladoSFX;
    private bool _enableImpactoQuemadoSFX;

    [SerializeField] private float _tiempoCongelado = 3, _tiempoQuemado = 3, _tiempoRalentizado = 3;
    [SerializeField] private int _danoPorSegQuemado= 5;

    private float _tiempoCongeladoInicial, _tiempoQuemadoInicial, _contadorDeSegundos, _initialTiempoRalentizado;
    private EnemyMovement _enemyMovement;
    private EnemyFlyingMovement _enemyFlyingMovement;
    private EnemyHealth _enemyHealth;
    private EnemyShoot _enemyShoot;

    [SerializeField]
    private Color[] _colores;   //Array de colores del player

    [SerializeField]
    private Renderer _renderC; //Renderiza el color del player

    [SerializeField] private AudioClip _congeladoSFX;
    [SerializeField] private AudioClip _descongeladoSFX;

    [SerializeField] private AudioClip _quemadoSFX;
    [SerializeField] private AudioClip _impactoQuemadoSFX;
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
            else if(GetComponent<EnemyShoot>() != null)
            {
                _enemyShoot = GetComponent<EnemyShoot>();
                _enemyShoot.CooldownReset();
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
            GetComponent<AudioSource>().PlayOneShot(_descongeladoSFX);
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
            else if(GetComponent<EnemyShoot>() != null)
            {
                _enemyShoot = GetComponent<EnemyShoot>();
                _enemyShoot.CooldownReset();
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
            GetComponent<AudioSource>().PlayOneShot(_quemadoSFX);
            GetComponent<EnemyHealth>().TakeDamage(_danoPorSegQuemado);
            _contadorDeSegundos = _contadorDeSegundos - 0.5f;
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
        _quemado =false;
        _enableCongeladoSFX = true;
        _enableImpactoQuemadoSFX = true;
        _tiempoCongeladoInicial = _tiempoCongelado;
        _enemyHealth = GetComponent<EnemyHealth>();
        _myAnimator = GetComponent<Animator>();
        _tiempoQuemadoInicial = _tiempoQuemado;
        _contadorDeSegundos = _tiempoQuemado - 0.5f;
        _initialTiempoRalentizado = _tiempoRalentizado;
    }

    // Update is called once per frame
    void Update()
    {
        _myAnimator.SetBool("_congelado", _congelado);
        _myAnimator.SetBool("_burned", _quemado);

        if (_congelado)
        {
            if (_enableCongeladoSFX)
            {
                GetComponent<AudioSource>().PlayOneShot(_congeladoSFX);
                _enableCongeladoSFX = false;
            }
            Congelado();
        }
        else
        {
            _enableCongeladoSFX = true;
        }

        if (_quemado)
        {
            if (_enableImpactoQuemadoSFX)
            {
                GetComponent<AudioSource>().PlayOneShot(_impactoQuemadoSFX);
                _enableImpactoQuemadoSFX = false;
            }
            Quemado();
        }
        else
        {
            _enableImpactoQuemadoSFX = true;
        }
        if(_ralentizado && !_congelado)
        {
            Ralentizado();
        }
        
        //Gesti�n de colores cuando est� ralentizado o quemado
        if (_ralentizado && !_enemyHealth.GetDañadoC())
        {
            _renderC.material.color = _colores[0];
        }
        else if (_enemyHealth.GetDañadoC() || _ralentizado && _enemyHealth.GetDañadoC() || _enemyHealth.GetDañadoC() && _congelado)
        {
            _renderC.material.color = _colores[1]; //Color de cuando el enemigo es golpeado (feedback visual)
        }
        else
        {
            _renderC.material.color = _colores[2]; //color original
        }
    }
}
