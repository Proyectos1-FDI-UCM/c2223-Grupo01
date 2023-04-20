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
    private float _enemyInitialSpeed; // para el congelado
    [SerializeField] private float _enemyDetectedSpeed = 7f;
    private float _enemyInitialDetectedSpeed; // para el congelado
    private Vector3 _initialPosition;
    private Rigidbody2D _rigidbody;
    private float _knockbackCounter;
    private bool _isflipped; //Si el enemigo ha dado la vuelta
    private bool _canturn;
    [SerializeField] private float _canturnCOUNTER;
    private float _canturnIniCOUNTER;
    private enum Estados { patrullaje, perseguir, regresar };
    private Estados _estado;
    #endregion

    #region getter && setter
    public void SetcknockBackCounter(float knockbackCounter)
    {
        _knockbackCounter = knockbackCounter;
    }
    public float GetEnemySpeed()
    {
        return _enemySpeed;
    }
    public void SetEnemySpeed(float newEsnemySpeed)
    {
        _enemySpeed = newEsnemySpeed;
    }
    public float GetEnemyDetectedSpeed()
    {
        return _enemyDetectedSpeed;
    }
    public void SetEnemyDetectedSpeed(float newEsnemySpeed)
    {
        _enemyDetectedSpeed = newEsnemySpeed;
    }
    public float GetEnemyInitialSpeed()
    {
        return _enemyInitialSpeed;
    }

    public float GetEnemyInitialDetectedSpeed()
    {
        return _enemyInitialDetectedSpeed;
    }
    #endregion

    #region Methods

    private void FlyingPatrol() //M�todo que provoca que el enemigo se mueva de izquierda a derecha
    {
        if (!_isflipped)
        {
            _rigidbody.velocity = (Vector3.right * _enemySpeed);
        }
        else
        {
            _rigidbody.velocity = (Vector3.left * _enemySpeed);
        }
    }
    private void FlyingChase()
    //Provoca que el enemigo empieze a perseguir al jugador.
    {
        Vector2 _enemydirection = _player.transform.position - transform.position;
        _rigidbody.velocity = (_enemydirection.normalized * _enemyDetectedSpeed);
    }

    private void ReturnPosition()
    //Provoca que los enemigos vuelvan a su posici�n inicial.
    {
        _rigidbody.velocity = ((_initialPosition - transform.position).normalized * _enemySpeed);
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        _isflipped = !_isflipped;
    }

    private void UpdateEnemyState(Estados estado) //M�todo que cambia los distintos estados del enemigo
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

                if (Vector3.Distance(_initialPosition, (Vector2)transform.position) < 0.5f)
                {
                    _estado = Estados.patrullaje;
                }
                break;
        }
    }
    #endregion

    #region Collision methods
    private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dar� la vuelta.
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            _enemySpeed = 0.0f;
            _enemyDetectedSpeed = 0.0f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            _enemySpeed = _enemyInitialSpeed;
            _enemyDetectedSpeed = _enemyInitialDetectedSpeed;
        }
    }
    #endregion

    void Start()
    {
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialPosition = transform.position;
        _estado = Estados.patrullaje;
        _isflipped = false;
        _canturn = true;
        _canturnIniCOUNTER = _canturnCOUNTER;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _enemyInitialSpeed = _enemySpeed;
        _enemyInitialDetectedSpeed = _enemyDetectedSpeed;
    }

    void FixedUpdate()
    {
        //Debug.Log(Mathf.Approximately(transform.position.x, _initialPosition.x) && Mathf.Approximately(transform.position.y, _initialPosition.y));
        //Debug.Log(_estado);
        if (!gameObject.GetComponent<EnemyHealth>()._death)
        {
            if (_knockbackCounter <= 0)
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
                _canturnCOUNTER = _canturnIniCOUNTER;
            }
            else
            {
                _canturn = false;
            }
        }
    }
}