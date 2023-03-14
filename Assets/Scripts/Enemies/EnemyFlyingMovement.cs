using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFlyingMovement : MonoBehaviour
{
    #region Parameters & References
    private GameObject _player; 
    private EnemyFOV _myEnemyFOV; 
    [SerializeField] private float _enemySpeed = 5f;
    private Vector2 _initialPosition;
    private int _enemystate;
    private Rigidbody2D _rigidbody;
    private float _knockbackCounter;
    private enum Estados { patrullaje, perseguir, regresar};
    private Estados _estado;
    #endregion

    #region getter && setter
    public void SetcknockBackCounter(float knockbackCounter)
    {
        _knockbackCounter = knockbackCounter;
    }
    #endregion

    #region Methods

    private void FlyingPatrol() //Método que provoca que el enemigo se mueva de izquierda a derecha
    {
        _rigidbody.velocity = (transform.right * _enemySpeed);
    }
    private void FlyingChase()
    //Provoca que el enemigo empieze a perseguir al jugador.
    {
        Vector3 _enemydirection = new Vector3(_player.transform.position.x-transform.position.x, _player.transform.position.y-transform.position.y,0f);
        _rigidbody.velocity = (_enemydirection.normalized*_enemySpeed);
    }

    private void ReturnPosition()
    //Provoca que los enemigos vuelvan a su posición inicial.
    {
        Vector3 _returndirection = new Vector3(_initialPosition.x - transform.position.x, _initialPosition.y - transform.position.y,0f);    
        _rigidbody.velocity = (_returndirection.normalized * _enemySpeed);
    }

    /*private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dará la vuelta.
    {   
        transform.Rotate(0f, 180f, 0f);
    }*/
    private void UpdateEnemyState ( Estados estado) //Método que cambia los distintos estados del enemigo
    {
        switch (estado)
        {
            case Estados.patrullaje:
                if (_myEnemyFOV._detected)
                {
                    _estado = Estados.perseguir;
                }
                break;

            case Estados.perseguir:
                FlyingChase();
                if (!_myEnemyFOV._detected)
                {
                    _estado = Estados.regresar;
                }
                    break;

            case Estados.regresar:
                ReturnPosition();
               
                if (_myEnemyFOV._detected)
                {
                    _estado = Estados.perseguir;
                }

                if (new Vector2(transform.position.x, transform.position.y) == _initialPosition)
                {
                    _estado = Estados.patrullaje;
                }
                break;
        }    
    }
    
    #endregion

    void Start()
    {
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialPosition = transform.position;
        _estado = Estados.patrullaje;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Debug.Log(_myEnemyFOV._detected);
        //Debug.Log(_estados);
        if (!gameObject.GetComponent<EnemyHealth>()._death)
        {
            if(_knockbackCounter <= 0)
            {
                UpdateEnemyState(_estado);
            }
            else
            {
                _knockbackCounter -= Time.deltaTime;
            }
        }
    }
}
