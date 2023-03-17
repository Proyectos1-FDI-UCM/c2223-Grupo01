using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraController : MonoBehaviour
{
    //Este script lo tendrá Mighty para ser capaz de subir y bajar escaleras.
    #region parameters
    private float _velocidadEscalada;
    private float _gravedadInicial;
    #endregion

    #region references
    [SerializeField] private BoxCollider2D _mightyCollider;
    [SerializeField] private Rigidbody2D _mightyRigidBody;
    #endregion

    #region Metodos
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 8)
        {
            _mightyRigidBody.gravityScale = 0;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.layer == 8)
        {
            _mightyRigidBody.gravityScale = _gravedadInicial;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _gravedadInicial = _mightyRigidBody.gravityScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,_velocidadEscalada);
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_velocidadEscalada);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
