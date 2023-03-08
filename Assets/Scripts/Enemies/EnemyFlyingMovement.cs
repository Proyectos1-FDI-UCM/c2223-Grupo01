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
    private enum Estados { patrullaje, perseguir, regresar};
    private Estados _estados;
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

    private void OnTriggerEnter2D(Collider2D Other)
    // Cada vez que colisione con un collider, el enemigo dará la vuelta.
    {   
        transform.Rotate(0f, 180f, 0f);
    }
    private void ChangeEnemyState ( Estados estados) //Método que cambia los distintos estados del enemigo
    {
        switch (estados)
        {
            case Estados.patrullaje:
                FlyingPatrol();
                if (_myEnemyFOV._detected)
                    _estados = Estados.perseguir;
                break;
            case Estados.perseguir:
                FlyingChase();
                if (!_myEnemyFOV._detected)
                    _estados = Estados.regresar;
                    break;
            case Estados.regresar:
                ReturnPosition();
               
                if (_myEnemyFOV._detected)
                   _estados = Estados.perseguir;

                if (new Vector2(transform.position.x, transform.position.y) == _initialPosition)
                    _estados = Estados.patrullaje;
                break;
        }    
    }
    
    #endregion

    void Start()
    {
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialPosition = transform.position;
        _estados = Estados.patrullaje;
    }

    void Update()
    {
       //Debug.Log(_myEnemyFOV._detected);
        //Debug.Log(_estados);
        ChangeEnemyState(_estados);
    }
}
