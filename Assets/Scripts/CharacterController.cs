using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Parameters
    // esta variable puede ser leída desde otros scripts pero no cambiada, por el private set.
    public bool _isgrounded { get; private set; }
    public bool _doublejump { get; private set; }
    [SerializeField] private float _MovementSmoothing;
    private Vector3 _velocity = Vector3.zero;
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    public bool _isJumping;
    [Header("Dash")]
    [SerializeField] private float _dashFriction; //fricción del dash
    private bool _facingRight = true;
    [SerializeField] private float _dashForce;
    private bool _dash = false;
    [Header ("Escaleras")]
    [SerializeField] public float _climbVelocity = 10f; //velocidad de subida
    public bool _climbing;
    private float _initialGravity;                //de escaleras
    #endregion

    #region References
    //Referencia al Rigid Body
    private Rigidbody2D _myRigidBody2D;
    // referencia al colider
    private Collider2D _myCollider2D;
    //el número debe coincide con el layer de Ground
    [SerializeField] private LayerMask _groundLayer;
    private InputComponent _myInputComponent;
    private CollisionManager _myCollisionManager;
    #endregion

    #region Methods
    private bool IsGrounded()
    {
        // cada vez que tocamos el suelo reactivamos el doble salt0
        if (!_doublejump && _isgrounded)
        {
            _doublejump = true;
        }
        // detecto si estoy en el suelo

        return Physics2D.BoxCast(_myCollider2D.bounds.center, _myCollider2D.bounds.size, 0f, Vector2.down, .05f, _groundLayer);
    }

    public void MoveXAxis(float XAxismove)
    {
        // Muevo al personaje
        Vector3 targetVelocity = new Vector2(XAxismove * 10f, _myRigidBody2D.velocity.y);
        _myRigidBody2D.velocity = Vector3.SmoothDamp(_myRigidBody2D.velocity, targetVelocity,ref _velocity, _MovementSmoothing);

        // estas líneas sirven para que mighty mire a la dirección correcta
        if (XAxismove > 0 && !_facingRight)
        {
            Flip();
        }
        else if (XAxismove < 0 && _facingRight)
        {
            Flip();
        }
    }
    public void Climb()
    {
        if((_myInputComponent._input.y != 0 || _climbing) && _myCollisionManager._touchingLadder)
        {
            Vector2 targetVelocity = new Vector2(_myRigidBody2D.velocity.x, _myInputComponent._input.y * _climbVelocity); //velocidad de subida
            _myRigidBody2D.velocity = targetVelocity;
            _myRigidBody2D.gravityScale = 0;
            _climbing = true;
        }
        else
        {
            _myRigidBody2D.gravityScale = _initialGravity;
            _climbing = false;
        }
        if (_isgrounded)
        {
            _climbing = false;
        }
    }
    /*public void MoveYAxis(float YAxismove) //Movimiento en eje Y
    {
        // Muevo al personaje en objetos con tag "Escaleras"
        Vector3 targetVelocity = new Vector2(_myRigidBody2D.velocity.x, YAxismove * _climbVelocity); //velocidad
        _myRigidBody2D.velocity = Vector3.SmoothDamp(_myRigidBody2D.velocity, targetVelocity, ref _velocity, _MovementSmoothing);
    }*/
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);
    }

    public void Jump()
    {
        // miro si hemos saltado para desactivar el doble salto en el segundo salto
        if (!_isgrounded)
        {
            _doublejump = false;
        }
        else // si no hemos saltado no queremos desactivar el doble salto pero sí el isgrounded (hemos saltado)
        {
            _isgrounded = false;
        }
        _myRigidBody2D.velocity = new Vector2(_myRigidBody2D.velocity.x, 0);
        _myRigidBody2D.AddForce(new Vector2(0f, _jumpForce));
    }

    public void Dash()
    {
        transform.Rotate(new Vector3(0, 0, 90)); // tumbo al jugador
        _myRigidBody2D.velocity = Vector2.zero;
        if (_facingRight)
        {
            _myRigidBody2D.AddForce(_dashForce * Vector2.right);//Impulsa al jugador hacia la derecha

        }
        else
        {
            _myRigidBody2D.AddForce(_dashForce * Vector2.left);//Impulsa al jugador hacia la izquierda
        }
        _myInputComponent.enabled = false; //desactivo el input
        _dash = true;
        _myCollider2D.sharedMaterial.friction = _dashFriction;
    }
    #endregion

    private void Start()
    {
        //inicializo el Rigid Body y el collider
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        // inicializo el Collider
        _myCollider2D = GetComponent<Collider2D>();
        // inicializo el Input
        _myInputComponent = GetComponent<InputComponent>();
        // inicializo el Collision Manager
        _myCollisionManager = GetComponent<CollisionManager>();
        // guardo gravedad inicial
        _initialGravity = _myRigidBody2D.gravityScale;
    }

    private void Update()
    {
        //detecta que estemos tocando tierra (no seais informáticos y tocad césped)
        _isgrounded = IsGrounded();

        //comprobador de finalizado de dash
        if (_dash && _myRigidBody2D.velocity.x == 0)
        {
            transform.Rotate(new Vector3(0, 0, 270));
            _myInputComponent.enabled = true;
            _dash = false;
            _myCollider2D.sharedMaterial.friction = 0;
        }
    }
}