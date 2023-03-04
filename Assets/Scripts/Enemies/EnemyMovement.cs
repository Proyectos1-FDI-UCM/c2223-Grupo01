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
    [SerializeField] private float _enemydetectionSpeed = 3;
    private float _initialSpeed;
    public float KnockbackForce;                        //Cuánta fuerza tendrá el knockback.
    public float KnockbackCounter;                      //Cooldown del knockback.
    public float KnockbackTotalTime;                    //Cuánto durará el knockback.
    public bool KnockFromRight;
    private bool _running;//Determina desde que posición ha sido golpeado el enemigo (derecha/izquierda).
    #endregion

    #region references
    private GameObject _player; 
    private EnemyFOV _myEnemyFOV;
    private Rigidbody2D _rigidbody;
    #endregion

    #region Methods
    private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dará la vuelta.
    {
        transform.Rotate(0f, 180f, 0f);  
    }
    #endregion

    void Start()
    {        
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialSpeed = _enemySpeed;
    }

    void Update()
    {
        //Solo se puede mover el enemigo si no está en modo knockback.
        if (KnockbackCounter <= 0) 
        {
            //Si el enemigo no ha detectado al jugador, este seguirá su patrón normal
            if (!_myEnemyFOV._detected) 
            {
                // la velocidad de patrullaje es la inicial
                _enemySpeed = _initialSpeed;
                _running = false;
            }
            //Si el enemigo nos detecta. 
            else if (_myEnemyFOV._detected && !_running) 
            {
                //la dirección a la que nuestro enemigo se moverá.
                /* Distfromplayer = new Vector2(_player.transform.position.x - transform.position.x, 0f); 
                Vector2 velocity = Distfromplayer * _enemySpeed;
                _rigidbody.velocity = velocity;*/ //esto es lo que hizo jiale, aumenta la velocidad con la distancia al player (mal)
                _enemySpeed += _enemydetectionSpeed;
                _running = true;
            }
            //El Enemigo se moverá a la derecha con determinada velocidad
            _rigidbody.velocity = (transform.right * _enemySpeed);
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
    }
}
