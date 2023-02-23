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

    public float KnockbackForce; //Cuánta fuerza tendrá el knockback.
    public float KnockbackCounter; //Cooldown del knockback.
    public float KnockbackTotalTime; //Cuánto durará el knockback.

    public bool KnockFromRight; //Determina desde que posición ha sido golpeado el enemigo (derecha/izquierda).
    #endregion

    #region references
    private GameObject _player; //El jugador
    private EnemyFOV _myEnemyFOV;
    private Rigidbody2D _rigidbody;
    #endregion

    private void OnTriggerEnter2D(Collider2D Other)
    {

        transform.Rotate(0f, 180f, 0f); // Cada vez que colisione con un collider, el enemigo dará la vuelta. 

    }

    void Start()
    {        
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KnockbackCounter <= 0) //Solo se puede mover el enemigo si no está en modo knockback.
        {
            if (!_myEnemyFOV._detected) //Si el enemigo no ha detectado al jugador, este seguirá su patrón normal
            {

                _rigidbody.velocity = (transform.right * _enemySpeed);//El Enemigo se moverá a la derecha con determinada velocidad
            }
            else if (_myEnemyFOV._detected) //Si el enemigo nos detecta. 
            {
                Vector2 Distfromplayer = new Vector2(_player.transform.position.x - transform.position.x, 0f); //la dirección a la que nuestro enemigo se moverá.
                Vector2 velocity = Distfromplayer * _enemySpeed;
                _rigidbody.velocity = velocity;
            }
        }
        else //Si está en modo knockback
        {
            if (KnockFromRight) //Si golpea por la derecha...
            {
                _rigidbody.velocity = new Vector2(-KnockbackForce, KnockbackForce); //"-KnockbackForce" mueve al enemigo para atrás.
                                                                                    //Es el vector de la fuerza que pega el knockback.
            }

            if(!KnockFromRight) //Si golpea por la izquierda...
            {
                _rigidbody.velocity = new Vector2(KnockbackForce, KnockbackForce); //Esta vuelta manda al enemigo a la derecha.
            }

            KnockbackCounter = KnockbackCounter - Time.deltaTime; //Hace que el contador baje.
        }

        
    }
}
