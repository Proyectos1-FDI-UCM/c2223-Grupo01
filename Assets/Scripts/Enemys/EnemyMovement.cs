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
}
