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
    #endregion

    #region Methods

    private void FlyingPatrol() //Método que provoca que el enemigo se mueva de izquierda a derecha
    {
        transform.Translate(Vector3.right * Time.deltaTime * _enemySpeed);
    }
    private void FlyingChase()
    //Provoca que el enemigo empieze a perseguir al jugador.
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemySpeed * Time.deltaTime);
    }

    private void ReturnPosition()
    //Provoca que los enemigos vuelvan a su posición inicial.
    {
        transform.position = Vector2.MoveTowards(transform.position, _initialPosition, _enemySpeed*Time.deltaTime);
    }

    /*private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dará la vuelta.
    {   
        transform.Rotate(0f, 180f, 0f);
    }*/
    private void ChangeEnemyState ( int estado) //Método que cambia los distintos estados del enemigo
    {
        switch (estado)
        {
            case 0:
                FlyingPatrol();
                if (_myEnemyFOV._detected)
                    _enemystate = 1;
                break;
            case 1:
                FlyingChase();
                if (!_myEnemyFOV._detected)
                    _enemystate = 2;
                    break;
            case 2:
                ReturnPosition();
               
                if (_myEnemyFOV._detected)
                    _enemystate = 1;

                if (new Vector2(transform.position.x, transform.position.y) == _initialPosition)
                    _enemystate = 0;
                break;
        }    
    }
    
    #endregion

    void Start()
    {
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialPosition = transform.position;
        _enemystate = 0;
    }

    void Update()
    {
        ChangeEnemyState(_enemystate);
    }
}
