//Parte del c�digo fue obtenido del siguiente video https://www.youtube.com/watch?v=lV47ED8h61k
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _enemySpeed = 3f;
    private float _enemyInitialSpeed; //para el congelado
    [SerializeField] private float _enemydetectionSpeed = 5f;
    private float _enemyInitialDetectedSpeed; // para el congelado
    [SerializeField] private float _maxDistanceDetection = 2f;
    private float _initialSpeed;
    private float _distfromplayer; //La distancia entre enemigo y jugadoe
    private bool _isflipped; //Si el enemigo ha dado la vuelta
    private float _knockbackCounter;
    private bool _canturn;
    [SerializeField] private float _canturnCOUNTER;
    private float _canturnIniCOUNTER;

    [SerializeField] private float _driftCOUNTER;
    private float _driftIniCOUNTER;
    #endregion

    #region references
    [SerializeField] private LayerMask _groundLayer;
    private Collider2D _myCollider2D;
    private GameObject _player; 
    private EnemyFOV _myEnemyFOV;
    private Rigidbody2D _rigidbody;
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
    public float GetEnemyDetectionSpeed()
    {
        return _enemydetectionSpeed;
    }
    public void SetEnemyDetectionSpeed(float newEsnemySpeed)
    {
        _enemydetectionSpeed = newEsnemySpeed;
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
    private bool _isgrounded() //Si el enemigo est� en el suelo
    {
            return Physics2D.BoxCast(_myCollider2D.bounds.center, _myCollider2D.bounds.size, 0f, Vector2.down, .05f, _groundLayer);
    }
    private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dar� la vuelta.
    {
        if(Other != GameManager.instance._player && _canturn)
        {
            _canturn = false;
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
    private void Flip ()
    {
        transform.Rotate(0f, 180f, 0f);
        _isflipped = !_isflipped;
    }
       
    #endregion

 
    void Start()
    {   _canturn= true;
        _canturnIniCOUNTER = _canturnCOUNTER;
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialSpeed = _enemySpeed;
        _isflipped=false;
        _myCollider2D = GetComponent<Collider2D>();
        _enemyInitialSpeed = _enemySpeed;
        _enemyInitialDetectedSpeed = _enemydetectionSpeed;
        _driftIniCOUNTER = _driftCOUNTER;
    }

    void Update()
    {
        _distfromplayer = _player.transform.position.x - transform.position.x; 
        //Solo se puede mover el enemigo si no est� en modo knockback.
        //_knockbackCounter = 
        if (_knockbackCounter <= 0) 
        {   
            if (_isgrounded())
            {
                //Si el enemigo no ha detectado al jugador, este seguir� su patr�n normal
                if (!_myEnemyFOV.GetDetected())
                {
                 _rigidbody.velocity = (transform.right * _enemySpeed);
                }
                //Si el enemigo nos detecta. 
                else if (_myEnemyFOV.GetDetected())
                {
                    //la direcci�n a la que nuestro enemigo se mover�.
                    if(Mathf.Abs(_distfromplayer)> _maxDistanceDetection)
                    {
                        if (_distfromplayer > 0 && _isflipped)
                        {
                            _driftCOUNTER -= Time.deltaTime;
                            if (_driftCOUNTER <= 0)
                            {
                                Flip();
                                _driftCOUNTER = _driftIniCOUNTER;
                            }
                        }
                        else if (_distfromplayer < 0 && !_isflipped)
                        {
                            _driftCOUNTER -= Time.deltaTime;
                            if (_driftCOUNTER <= 0)
                            {
                                Flip();
                                _driftCOUNTER = _driftIniCOUNTER;
                            }
                        }
                    }
                    _rigidbody.velocity = (transform.right * _enemydetectionSpeed);

                }
                //El Enemigo se mover� a la derecha con determinada velocidad

               
            }
        }
        else //Si est� en modo knockback
        {
            _knockbackCounter -= Time.deltaTime;
        }

        if (!_canturn)
        {
            _canturnCOUNTER -= Time.deltaTime;
            if (_canturnCOUNTER <= 0)
            {
                _canturn = true;
                _canturnCOUNTER = _canturnIniCOUNTER;
            }
        }
    }
}