using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraController : MonoBehaviour
{
    #region parameters
    private float _velocidadSubida;
    private float _velocidadBajada;
    private float _gravedadInicial;
    #endregion
    
    #region references
    [SerializeField] private BoxCollider2D _mightyCollider;
    [SerializeField] private Rigidbody2D _mightyRigidBody;
    private BoxCollider2D _escalerasCollider;
    #endregion

    #region Metodos
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.layer == 8)
        {

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
        
    }
}
