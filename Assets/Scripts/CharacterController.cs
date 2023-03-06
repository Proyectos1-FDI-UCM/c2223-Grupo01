using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterController : MonoBehaviour
{
    #region Parameters
    // private set: esta variable puede ser le�da desde otros scripts
    // pero no cambiada
    public bool _isgrounded { get; private set; }
    public bool _doublejump { get; private set; }
    public bool _isOnIce{get; private set;}

    [Header("Basic Movement")]
    [SerializeField] private float _MovementSmoothing; // fluidez con la que se moverá mighty
    [SerializeField] private float _movementSpeedX; // velocidad con la que se moverá mighty en el eje x
    [SerializeField] private LayerMask _iceMask;
    
    private Vector3 _velocity = Vector3.zero;

    private bool _aterrizado; // booleano que detecta cuando aterrizamos en el suelo

    [Header("Jump")]
    [SerializeField] private float _jumpForce; // fuerza del salto

    [Header("Dash")]
    private bool _facingRight = true;
    [SerializeField] private float _dashForce; // Fuerza para el dash
    private bool _dash = false; // Booleano que detecta si estamos en medio de un dash

    [Header("Climb")]
    private float _initialGravity; // gravedad inicial
    private float _climbVelocity = 10f; // velocidad de escalada
    public bool _isClimbing { get; private set; } // Booleano que comprueba si estamos escalando
    #endregion

    #region References
    [SerializeField]private Rigidbody2D _myRigidBody2D; // Referencia al Rigid Body del player
    private BoxCollider2D _myCollider2D; // Referencia al Colider del player
    [SerializeField] private LayerMask _groundLayer; // Layers que tomamos como suelo
    [SerializeField] private LayerMask _ladderLayer; // Layers que tomamos como escalabes
    private InputComponent _myInputComponent; // Referencia al input
    private Animator _animator; // Referencia al animator

    // Sonidos varios
    [SerializeField] private AudioClip _aterrizaje;
    [SerializeField] private AudioClip _dobleSalto;
    [SerializeField] private AudioClip _normalJump;
    [SerializeField] private AudioClip _slide;

    [SerializeField] private GameObject _slideObject;
    #endregion

    #region Methods
    private bool IsGrounded()
    // cada vez que tocamos el suelo reactivamos el doble salto
    // y detecto si estoy en el suelo
    {
        if (!_doublejump && _isgrounded)
        {
            _doublejump = true;
        }
        return Physics2D.BoxCast(_myCollider2D.bounds.center, _myCollider2D.bounds.size, 0f, Vector2.down, .05f, _groundLayer);
    }

    private bool IsCeiling()
    // cada vez que tocamos el suelo reactivamos el doble salto
    // y detecto si estoy en el suelo
    {
        return Physics2D.BoxCast(_myCollider2D.bounds.center, _myCollider2D.bounds.size, 0f, Vector2.up, .05f, _groundLayer);
    }

    public void MoveXAxis(float XAxismove)
    // Muevo al personaje en el eje X
    // hago Flip() para girar el sprite en la direccion a la que mire el Player
    {
        Vector3 targetVelocity = new Vector2(XAxismove * _movementSpeedX, _myRigidBody2D.velocity.y);
        _myRigidBody2D.velocity = Vector3.SmoothDamp(_myRigidBody2D.velocity, targetVelocity,ref _velocity, _MovementSmoothing);

        if (XAxismove > 0 && !_facingRight || XAxismove < 0 && _facingRight) Flip();
    }

    private void Flip()
    // Gira el sprite del player hacia donde est� mirando
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Jump()
    // Salto y Doble Salto. Se comprueba si ya hemos saltado
    // para desactivar la posibilidad de doble salto.
    // Si no hemos saltado se desactiva el isgrounded.
    {
        if (_doublejump && _isgrounded)
        GetComponent<AudioSource>().PlayOneShot(_normalJump);

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
            GetComponent<AudioSource>().PlayOneShot(_dobleSalto);
            _animator.SetTrigger("_doubleJump");
        }
    }

    public void Dash()
    // Deslizamiento sobre el suelo impulsando al jugador.
    // Hacia la derecha o izquierda. Se desactiva input si estamos dasheando.
    {
        if (!_dash)
        {
            _myCollider2D.isTrigger = true;
            _slideObject.SetActive(true);
            _myInputComponent.enabled = false;
            _myRigidBody2D.velocity = Vector2.zero;
            GetComponent<AudioSource>().PlayOneShot(_slide);
        }
        if (_facingRight)
        {
            _myRigidBody2D.AddForce(_dashForce * Vector2.right);
        }
        else
        {
            _myRigidBody2D.AddForce(_dashForce * Vector2.left);
        }
        _dash = true;
    }

    //metodo de escalada
    public void Climb(float YAxismove)
    {
        if ((YAxismove != 0 || _isClimbing) && _myRigidBody2D.IsTouchingLayers(_ladderLayer))
        {
            if (_doublejump)
            {
                Vector2 targetVelocity = new Vector2(_myRigidBody2D.velocity.x, YAxismove * _climbVelocity);
                _myRigidBody2D.velocity = targetVelocity;
                _myRigidBody2D.gravityScale = 0;
                _isClimbing = true;
            }
        }
        else
        {
            _myRigidBody2D.gravityScale = _initialGravity;
            if (_isClimbing && _doublejump)
            {
                _myRigidBody2D.velocity = new Vector2(_myRigidBody2D.velocity.x, 0);
            }
            _isClimbing = false;
        }

        if(YAxismove < 0){ //creo rayo para detectar si debajo hay escaleras
            Vector2 posRayo = new Vector2(transform.position.x - (_myCollider2D.size.x/2), transform.position.y - (_myCollider2D.size.y / 2) - 0.1f);
            RaycastHit2D hitRayo = Physics2D.Raycast(posRayo, Vector2.right, _myCollider2D.size.x, _ladderLayer);
            Debug.DrawRay(posRayo, Vector2.right * _myCollider2D.size.x, Color.red);
        }

        if (_isgrounded)
        {
            _isClimbing = false;
        }
    }
    #endregion

    #region Collision Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Hielo")) //pensado para hacerlo con layer
        {
            _MovementSmoothing = 3;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hielo"))
        {
            _MovementSmoothing = 0;
        }
    }
    #endregion
    private void Start()
    {
        // Inicializo componentes.
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _myCollider2D = GetComponent<BoxCollider2D>();
        _myInputComponent = GetComponent<InputComponent>();
        _animator = GetComponent<Animator>();

        // Guardo gravedad inicial.
        _initialGravity = _myRigidBody2D.gravityScale;
        //desactivamos el colider del dash al principio
        _slideObject.SetActive(false);
    }

    private void Update()
    {
        //Comprueba si estamos tocando el suelo
        _isgrounded = IsGrounded();

        if (!_aterrizado && _isgrounded) // si estamos atterizando en el suelo se produce un sonido, si no está en el suelo aterrizado es false
        {
            //Cuando aterrice en el suelo, reproducir� el sonido y se volver� true aterrizado
            GetComponent<AudioSource>().PlayOneShot(_aterrizaje);
            _aterrizado = true;
        }
        else if (!_isgrounded)
        {
            _aterrizado = false;
        }

        //Actualiza Animator
        _animator.SetBool("_dash", _dash);
        _animator.SetBool("_isGrounded", _isgrounded);

        //Comprueba si el dash ha acabado y devuelve al player a la normalidad
        if ((_dash && !_isgrounded || _dash && _myRigidBody2D.velocity.x == 0 ||_dash && UnityEngine.Input.GetKeyDown(KeyCode.Space)) && !IsCeiling())
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            _myInputComponent.enabled = true;
            _slideObject.SetActive(false);
            _myCollider2D.isTrigger = false;
            _dash = false;
        }
        else if(_myRigidBody2D.velocity.x == 0 && _dash && IsCeiling())
        {
            if (_facingRight)
            {
                transform.position += new Vector3(0.1f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(- 0.1f, 0, 0);
            }
        }
    }
}