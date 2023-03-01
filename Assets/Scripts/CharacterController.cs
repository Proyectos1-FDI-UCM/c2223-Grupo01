using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Parameters
    // private set: esta variable puede ser leída desde otros scripts
    // pero no cambiada
    public bool _isgrounded { get; private set; }
    public bool _doublejump { get; private set; }
    [SerializeField] private float _MovementSmoothing;
    private Vector3 _velocity = Vector3.zero;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    public bool _isJumping;

    [Header("Dash")]
    [SerializeField] private float _dashFriction;
    private bool _facingRight = true;
    [SerializeField] private float _dashForce;
    private bool _dash = false;

    [Header("Climb")]
    private float _initialGravity;
    private float _vertical;
    private float _climbSpeed = 10f;
    private bool _isTouchingLadder = false;
    private bool _isClimbing;
    #endregion

    #region References
    [SerializeField]private Rigidbody2D _myRigidBody2D;
    private Collider2D _myCollider2D;
    [SerializeField] private LayerMask _groundLayer;
    private InputComponent _myInputComponent;
    private Animator _animator;
    #endregion

    #region Methods
    private bool IsGrounded()
    {
        // cada vez que tocamos el suelo reactivamos el doble salto
        // y detecto si estoy en el suelo
        if (!_doublejump && _isgrounded)
        {
            _doublejump = true;
        }
        return Physics2D.BoxCast(_myCollider2D.bounds.center, _myCollider2D.bounds.size, 0f, Vector2.down, .05f, _groundLayer);
    }

    public void MoveXAxis(float XAxismove)
    // Muevo al personaje en el eje X
    // hago Flip() para girar el sprite en la direccion a la que mire el Player
    {
        Vector3 targetVelocity = new Vector2(XAxismove * 10f, _myRigidBody2D.velocity.y);
        _myRigidBody2D.velocity = Vector3.SmoothDamp(_myRigidBody2D.velocity, targetVelocity,ref _velocity, _MovementSmoothing);

        if (XAxismove > 0 && !_facingRight || XAxismove < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    // Gira el sprite del player hacia donde esté mirando
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Jump()
    // Salto y Doble Salto. Se comprueba si ya hemos saltado
    // para desactivar la posibilidad de doble salto.
    // Si no hemos saltado se desactiva el isgrounded.
    {
        if (!_isgrounded)
        {
            _doublejump = false;
        }
        else
        {
            _isgrounded = false;
        }
        _myRigidBody2D.velocity = new Vector2(_myRigidBody2D.velocity.x, 0);
        _myRigidBody2D.AddForce(new Vector2(0f, _jumpForce));

        if (!_doublejump)
        {
            _animator.SetTrigger("_doubleJump");
        }
    }

    public void Dash()
    // Deslizamiento sobre el suelo. Se tumba al jugador y se le impulsa
    // hacia la derecha o izquierda. Se desactiva input si estamos dasheando.
    {
        transform.Rotate(new Vector3(0, 0, 90));
        _myRigidBody2D.velocity = Vector2.zero;
        if (_facingRight)
        {
            _myRigidBody2D.AddForce(_dashForce * Vector2.right);
        }
        else
        {
            _myRigidBody2D.AddForce(_dashForce * Vector2.left);
        }
        _myInputComponent.enabled = false;
        _dash = true;
        _myCollider2D.sharedMaterial.friction = _dashFriction;
    }
    #endregion

    #region Collision Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            _isTouchingLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            _isTouchingLadder = false;
            _isClimbing = false;
        }
    }
    #endregion

    private void Start()
    {
        // Inicializo componentes.
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _myCollider2D = GetComponent<Collider2D>();
        _myInputComponent = GetComponent<InputComponent>();
        _animator = GetComponent<Animator>();

        // Guardo gravedad inicial.
       /* _myRigidBody2D.gravityScale = _initialGravity;*/
    }

    private void Update()
    {
        //Comprueba si estamos tocando el suelo
        _isgrounded = IsGrounded();

        //Actualiza Animator
        _animator.SetBool("_dash", _dash);
        _animator.SetBool("_isGrounded", _isgrounded);

        //Comprueba si el dash ha acabado
        if (_dash && _myRigidBody2D.velocity.x == 0)
        {
            transform.Rotate(new Vector3(0, 0, 270));
            _myInputComponent.enabled = true;
            _dash = false;
            _myCollider2D.sharedMaterial.friction = 0;
        }

        // Actualizo input de eje Y
        // y compruebo si puedo escalar
       /* _vertical = Input.GetAxis("Vertical");
        if(_isTouchingLadder && Mathf.Abs(_vertical) > 0)
        {
            _isClimbing = true;
        }*/
    }
    /*private void FixedUpdate()
    {
        if (_isClimbing)
        {
            _myRigidBody2D.gravityScale = 0;
            _myRigidBody2D.velocity = new Vector2(_myRigidBody2D.velocity.x, _vertical * _climbSpeed);
        }
        if(!_isClimbing || !_isTouchingLadder)
        {
            _myRigidBody2D.gravityScale = _initialGravity;
        }
    }*/
}