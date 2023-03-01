using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingMovement : MonoBehaviour
{
    private GameObject _player;
    private EnemyFOV _myEnemyFOV;
    [SerializeField] private float _enemySpeed = 5f;
    private Vector2 _initialPosition;

    private void FlyingChase() //método que provoca que el enemigo empieze a perseguir al jugador.
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemySpeed * Time.deltaTime);
    }
    private void ReturnPosition() //método que provoca que los enemigos vuelvan a su posición inicial.
    {
        transform.position = Vector2.MoveTowards(transform.position, _initialPosition, _enemySpeed*Time.deltaTime);

    }

    
    void Start()
    {
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_myEnemyFOV._detected)
            FlyingChase();
        else
            ReturnPosition();
      
    }
}
