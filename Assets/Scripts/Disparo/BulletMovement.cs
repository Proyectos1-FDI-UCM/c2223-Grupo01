using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed; // velociadad de la bala
    private GameObject _player; // referencia al player
    //private GameObject _blaster; // referencia al arma
    private Vector2 _direccion; //COmprueba la dirección hacia donde debe ir la bala
    #endregion

    #region References
    private InputComponent _myInputComponent; //referencia al input
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance._player; // inicializamos el player
        //_blaster = GameManager.instance._blaster; // inicializamos el player
        _myInputComponent = _player.GetComponent<InputComponent>();
        //Comprobación de cual será la dirección de la bala
        if (_player.transform.localScale.x > 0.0f)
        {
            _direccion = Vector2.right;       //derecha
        }
        else if (_player.transform.localScale.x < 0.0f)
        {
            _direccion = Vector2.left;      //izquierda
        }
        /*if (_blaster.transform.localPosition.y == 0.15f)
        {
            _direccion = Vector2.up;
        }*/
        if (_myInputComponent._lookUP)
        {
            _direccion = Vector2.right;
            gameObject.transform.Rotate(new Vector3(0, 0, 90));
        }
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(_speed * _direccion * Time.deltaTime);//desplazamiento de la bala
    }
}
