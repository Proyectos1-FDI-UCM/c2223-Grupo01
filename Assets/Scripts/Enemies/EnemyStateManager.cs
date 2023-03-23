using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    #region parameters
    private bool _congelado;
    [SerializeField] float _tiempoCongelado = 3f;
    private float _tiempoCongeladoInicial;
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _congelado = false;
        _tiempoCongeladoInicial = _tiempoCongelado;
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_congelado)
        {
            _tiempoCongelado -= Time.deltaTime;
            if(_tiempoCongelado <= 0)
            {
                _enemyHealth.SetNumBalasCongelado(0);
                if (GetComponent<EnemyMovement>() != null)
                {
                    _enemyMovement=GetComponent<EnemyMovement>();
                    _enemyMovement.SetEnemySpeed(_enemyMovement.GetEnemyInitialSpeed());
                    _enemyMovement.SetEnemyDetectionSpeed(_enemyMovement.GetEnemyInitialDetectedSpeed());
                }
                else if(GetComponent<EnemyFlyingMovement>() != null)
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
    }
}
