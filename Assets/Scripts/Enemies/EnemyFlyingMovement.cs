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
    private Transform _initialPosition;
    private Rigidbody2D _rigidbody;
    private float _knockbackCounter;
    private bool _isflipped; //Si el enemigo ha dado la vuelta
    private bool _canturn;
    [SerializeField] private float _canturnCOUNTER;
    private float _canturnIniCOUNTER;
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
        {
            _rigidbody.velocity = ((_initialPosition.position - transform.position).normalized * _enemySpeed);
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        _isflipped = !_isflipped;
    }

    private void UpdateEnemyState(Estados estado) //Método que cambia los distintos estados del enemigo
    {
        switch (estado)
        {
            case Estados.patrullaje:
                FlyingPatrol();
                if (_myEnemyFOV.GetDetected())
                {
                    _estado = Estados.perseguir;
                }
                break;

            case Estados.perseguir:
                FlyingChase();
                if (!_myEnemyFOV.GetDetected())
                {
                    _estado = Estados.regresar;
                }
                break;

            case Estados.regresar:
                ReturnPosition();

                if (_myEnemyFOV.GetDetected())
                {
                    _estado = Estados.perseguir;
                }

                if (Vector3.Distance(_initialPosition.position, transform.position) < 0.5f)
                {
                    _estado = Estados.patrullaje;
                }
                break;
        }
    }
    #endregion

    #region Collision methods
    private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dará la vuelta.
    {
        if (Other != GameManager.instance._player)
        {
            Flip();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12 && _canturn || collision.gameObject.GetComponent<EnemyHealth>() && _canturn)
        {
            _canturn = false;
            Flip();

        }
    }
    #endregion

    void Start()
    {
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialPosition.position = transform.position;
        _estado = Estados.patrullaje;
        _rigidbody = GetComponent<Rigidbody2D>();
        _isflipped = false;
        _canturn = true;
        _canturnIniCOUNTER = _canturnCOUNTER;
    }

    void Update()
    {
        //Debug.Log(_myEnemyFOV._detected);
        Debug.Log(_estado);
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

        if (!_canturn)
        {
            _canturnCOUNTER -= Time.deltaTime;
            if (_canturnCOUNTER <= 0)
            {
                _canturn = true;
            }
        }
        else
        {
            _canturnCOUNTER = _canturnIniCOUNTER;
        }
    }
}
