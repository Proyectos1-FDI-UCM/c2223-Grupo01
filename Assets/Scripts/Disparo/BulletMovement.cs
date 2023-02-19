using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _speed;
    private GameObject player;
    private Vector2 direccion; //COmprueba la dirección hacia donde debe ir la bala
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance._player;
        //Comprobación de cual será la dirección inicial de la bala
        if (player.transform.localScale.x > 0)
        {
            direccion = Vector2.right;       //derecha
        }
        else if (player.transform.localScale.x < 0)
        {
            direccion = Vector2.left;      //izquierda
        }
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(_speed * direccion * Time.deltaTime);//desplazamiento de la bala
    }
}
