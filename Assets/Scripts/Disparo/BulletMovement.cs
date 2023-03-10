using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed;  // velociadad de la bala
    private GameObject _player;             // referencia al player
    //private GameObject _blaster;          // referencia al arma        

    private float _direccionX; // Comprueba la dirección X hacia donde debe ir la bala
    private float _direccionY; // Comprueba la dirección Y hacia donde debe ir la bala
    #endregion

    #region References
    private InputComponent _myInputComponent;
    private Rigidbody2D _myRigidbody;
    #endregion

    void Start()
    {
        // inicializamos el player
        _player = GameManager.instance._player; 
        //_blaster = GameManager.instance._blaster; 
        _myInputComponent = _player.GetComponent<InputComponent>();
        //Inicializamos el Rigidbody2D.
        _myRigidbody = GetComponent<Rigidbody2D>();


        //Comprobación de cual será la dirección de la bala: derecha o izquierda
        if (_player.transform.localRotation.y >= 0.0f)
        {
            _direccionX = 1.0f;
            _direccionY = 0.0f;
        }
        else if (_player.transform.localRotation.y < 0.0f)
        {
            _direccionX = -1.0f;
            _direccionY = 0.0f;
        }

        /*if (_blaster.transform.localPosition.y == 0.15f)
        {
            _direccion = Vector2.up;
        }*/

        if (_myInputComponent._lookUP)
        {
            //_direccion = Vector2.right;
            _direccionY = 1.0f;
            _direccionX = 0.0f;
            gameObject.transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    void FixedUpdate()
    {
        //_myRigidbody.velocity = _myRigidbody.velocity * _speed * _direccion * Time.deltaTime;//desplazamiento de la bala
        _myRigidbody.velocity = new Vector2(_speed * _direccionX * Time.deltaTime, _speed * _direccionY * Time.deltaTime);//desplazamiento de la bala
        Debug.Log(_direccionX);
        Debug.Log(_direccionY);
    }
}
