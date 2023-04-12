using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed;  // velociadad de la bala

    private float _direccionX; // Comprueba la dirección X hacia donde debe ir la bala
    private float _direccionY; // Comprueba la dirección Y hacia donde debe ir la bala
    #endregion

    #region References
    private Rigidbody2D _myRigidbody;
    #endregion

    void Start()
    {
        //Inicializamos el Rigidbody2D.
        _myRigidbody = GetComponent<Rigidbody2D>();

        _direccionX = GameManager.instance._player.transform.position.x - gameObject.transform.position.x;
        _direccionY = GameManager.instance._player.transform.position.y - gameObject.transform.position.y;
    }

    void FixedUpdate()
    {
        _myRigidbody.velocity = new Vector2(_speed * _direccionX * Time.deltaTime, _speed * _direccionY * Time.deltaTime);//desplazamiento de la bala
    }
}
