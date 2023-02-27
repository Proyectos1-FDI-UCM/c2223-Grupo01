//Parte del c�digo fue obtenido del siguiente video https://www.youtube.com/watch?v=lV47ED8h61k
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _enemySpeed = 5f;

    //Cu�nta fuerza tendr� el knockback.
    public float KnockbackForce;
    //Cooldown del knockback.
    public float KnockbackCounter;
    //Cu�nto durar� el knockback.
    public float KnockbackTotalTime;

    //Determina desde que posici�n ha sido golpeado el enemigo (derecha/izquierda).
    public bool KnockFromRight;
    #endregion

    #region references
    //El jugador
    private GameObject _player; 
    private EnemyFOV _myEnemyFOV;
    private Rigidbody2D _rigidbody;
    #endregion

    private void OnTriggerEnter2D(Collider2D Other)
    {
        // Cada vez que colisione con un collider, el enemigo dar� la vuelta.
        transform.Rotate(0f, 180f, 0f);  

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
        //Solo se puede mover el enemigo si no est� en modo knockback.
        if (KnockbackCounter <= 0) 
        {
            //Si el enemigo no ha detectado al jugador, este seguir� su patr�n normal
            if (!_myEnemyFOV._detected) 
            {
                //El Enemigo se mover� a la derecha con determinada velocidad
                _rigidbody.velocity = (transform.right * _enemySpeed);
            }
            //Si el enemigo nos detecta. 
            else if (_myEnemyFOV._detected) 
            {
                //la direcci�n a la que nuestro enemigo se mover�.
                Vector2 Distfromplayer = new Vector2(_player.transform.position.x - transform.position.x, 0f); 
                Vector2 velocity = Distfromplayer * _enemySpeed;
                _rigidbody.velocity = velocity;
            }
        }
        //Si est� en modo knockback
        else
        {
            //Si golpea por la derecha...
            if (KnockFromRight) 
            {
                //"-KnockbackForce" mueve al enemigo para atr�s.
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
