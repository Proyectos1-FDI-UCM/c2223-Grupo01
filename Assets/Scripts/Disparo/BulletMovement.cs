using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed; // velociadad de la bala
    private GameObject _player; // referencia al player
    private Vector2 _direccion; //COmprueba la dirección hacia donde debe ir la bala
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance._player; // inicializamos el player
        //Comprobación de cual será la dirección de la bala
        if (_player.transform.localScale.x > 0)
        {
            _direccion = Vector2.right;       //derecha
        }
        else if (_player.transform.localScale.x < 0)
        {
            _direccion = Vector2.left;      //izquierda
        }
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(_speed * _direccion * Time.deltaTime);//desplazamiento de la bala
    }
}
