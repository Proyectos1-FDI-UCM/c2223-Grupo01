//Parte del código fue obtenido del siguiente video https://www.youtube.com/watch?v=lV47ED8h61k
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _enemySpeed = 5f;
    [SerializeField] private float _enemydetectionSpeed = 3f;
    [SerializeField] private float _maxDistanceDetection = 2f;
    private float _initialSpeed;
    public float KnockbackForce;                        //Cuánta fuerza tendrá el knockback.
    public float KnockbackCounter;                      //Cooldown del knockback.
    public float KnockbackTotalTime;                    //Cuánto durará el knockback.
    public bool KnockFromRight;
    private float _distfromplayer; //La distancia entre enemigo y jugadoe
    private bool _isflipped; //Si el enemigo ha dado la vuelta

    private bool _canturn;
    [SerializeField] private float _canturnCOUNTER;
    private float _canturnIniCOUNTER;
    #endregion

    #region references
    [SerializeField] private LayerMask _ground;
    private GameObject _player; 
    private EnemyFOV _myEnemyFOV;
    private Rigidbody2D _rigidbody;
    #endregion

    #region Methods
    private bool _isgrounded() //Si el enemigo está en el suelo
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, 0.5f, _ground);
    }
    private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dará la vuelta.
    {
        if(Other != GameManager.instance._player)
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
      
    }

    void Update()
    {
        _distfromplayer = _player.transform.position.x - transform.position.x; 
        //Solo se puede mover el enemigo si no está en modo knockback.

        if (KnockbackCounter <= 0) 
        {   
            if (_isgrounded())
            {
                //Si el enemigo no ha detectado al jugador, este seguirá su patrón normal
                if (!_myEnemyFOV._detected)
                {
                    // la velocidad de patrullaje es la inicial

                    _enemySpeed = _initialSpeed;

                 _rigidbody.velocity = (transform.right * _enemySpeed);
                }
                //Si el enemigo nos detecta. 
                else if (_myEnemyFOV._detected)
                {
                    //la dirección a la que nuestro enemigo se moverá.
                    if(Mathf.Abs(_distfromplayer)> _maxDistanceDetection)
                      {
                        if (_distfromplayer > 0 && _isflipped)
                            Flip();
                        else if (_distfromplayer < 0 && !_isflipped)
                            Flip();
                       }
                    _rigidbody.velocity = (transform.right * _enemydetectionSpeed);

                }
                //El Enemigo se moverá a la derecha con determinada velocidad

               
            }
        }
        else //Si está en modo knockback
        {
            //Si golpea por la derecha...
            if (KnockFromRight) 
            {
                //"-KnockbackForce" mueve al enemigo para atrás.
                //Es el vector de la fuerza que pega el knockback.
                _rigidbody.velocity = new Vector2(-KnockbackForce, 0); 
            }
            //Si golpea por la izquierda...
            if (!KnockFromRight) 
            {
                //Esta vuelta manda al enemigo a la derecha.
                _rigidbody.velocity = new Vector2(KnockbackForce, 0); 
            }
            //Hace que el contador baje.
            KnockbackCounter = KnockbackCounter - Time.deltaTime; 
        }

        if (!_canturn)
        {
            _canturnCOUNTER -= Time.deltaTime;
            if (_canturnCOUNTER <= 0)
            {
                _canturn = true;
            }
        }
        else _canturnCOUNTER = _canturnIniCOUNTER;
    }
}