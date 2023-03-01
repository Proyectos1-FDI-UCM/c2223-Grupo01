using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingMovement : MonoBehaviour
{
    #region Parameters & References
    private GameObject _player;
    private EnemyFOV _myEnemyFOV;
    [SerializeField] private float _enemySpeed = 5f;
    private Vector2 _initialPosition;
    #endregion

    #region Methods
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
    #endregion

    void Start()
    {
        _player = GameManager.instance._player;
        _myEnemyFOV = GetComponent<EnemyFOV>();
        _initialPosition = transform.position;
    }

    void Update()
    {
        if (_myEnemyFOV._detected)
            FlyingChase();
        else
            ReturnPosition();
    }
}
