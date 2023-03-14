using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed;  // velociadad de la bala

    private float _direccionX; // Comprueba la dirección X hacia donde debe ir la bala
    #endregion

    #region References
    private Rigidbody2D _myRigidbody;
    #endregion

    void Start()
    {
        //Inicializamos el Rigidbody2D.
        _myRigidbody = GetComponent<Rigidbody2D>();

        //Comprobación de cual será la dirección de la bala: derecha o izquierda
        if (gameObject.transform.position.x <= GameManager.instance._player.transform.position.x)
        {
            _direccionX = 1.0f;
        }
        else
        {
            _direccionX = -1.0f;
        }
    }

    void FixedUpdate()
    {
        _myRigidbody.velocity = new Vector2(_speed * _direccionX * Time.deltaTime, 0.0f);//desplazamiento de la bala
    }
}
