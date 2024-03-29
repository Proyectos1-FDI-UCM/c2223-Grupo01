using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    #region Parameters
    // private set: esta variable puede ser leida desde otros scripts
    // pero no cambiada
    private bool _isgrounded;
    public bool _doublejump {get; private set;}
    public bool _isOnIce {get; private set;}
    public bool _doorTouched { get; private set; }

    [Header("Basic Movement")]
    [SerializeField] private float _MovementSmoothing; // fluidez con la que se moverá mighty
    [SerializeField] private float _IceMoveSmoothing;
    private float _originalMoveSmoothing;
    [SerializeField] private float _movementSpeedX; // velocidad con la que se moverá mighty en el eje x
    private float _initialMovementSpeedX; // velocidad inicial con la que se moverá mighty en el eje x. Util para poder reconfigurar la velocidad, cuando estas en una plataforma que ralentiza por ejemplo
    [SerializeField] private float _slowSpeedX; // velocidad con la que se moverá mighty en el eje x por las plataformas ralentizantes

    private Vector3 _velocity = Vector3.zero; // velocidad en MoveXAxis

    private bool _aterrizado; // booleano que detecta cuando aterrizamos en el suelo

    [Header("Jump")]
    [SerializeField] private float _jumpForce; // fuerza del salto

    [Header("Dash")]
    [SerializeField] private float _dashForce; // Fuerza para el dash
    [SerializeField] private float _smallDashForce; // Fuerza para el dash en las plataformas ralentizantes
    private bool _facingRight = true;
    private float _initialdDashForce; // Fuerza inicial para el dash.
    //Util para poder reconfigurar la fuerza del dash, cuando estas en una plataforma que ralentiza por ejemplo
    private bool _dash = false; // Booleano que detecta si estamos en medio de un dash
    [SerializeField] private GameObject _slideObject;

    private bool _estela; // booleano que determina cuando se ve la estela de velocidad o no
    [SerializeField] private float _ghostingDuration; // Contador de cuanto dura la estela al activarse
    private float _initialGhostingDuration;

    [Header("Climb")]
    [SerializeField] private float _climbVelocity; // velocidad de escalada
    private float _initialGravity; // gravedad inicial
    public bool _isClimbing { get; private set; } // Booleano que comprueba si estamos escalando
    private Collider2D _currentLadder;
    private float _ladderTop;
    #endregion

    #region References
    private Rigidbody2D _myRigidBody2D; // Referencia al Rigid Body del player
    private BoxCollider2D _myCollider2D; // Referencia al Colider del player
    [Header("Layers")]
    [SerializeField] private LayerMask _groundLayer; // Layers que tomamos como suelo
    [SerializeField] private LayerMask _techoLayer; // Layers que tomamos como techo
    private InputComponent _myInputComponent; // Referencia al input
    private Animator _animator; // Referencia al animator
    private UniversalInput _newInput;
    private GhostingDash _myGhostingDash; // Referencia a la estela cuando se mueve
    // Sonidos varios
    [Header("Sounds")]
    [SerializeField] private AudioClip _aterrizaje;
    [SerializeField] private AudioClip _dobleSalto;
    [SerializeField] private AudioClip _normalJump;
    [SerializeField] private AudioClip _voicedJump;
    [SerializeField] private AudioClip _slide;
    #endregion

    #region getters && setters
    public bool GetIsGrounded()
    {
        return _isgrounded;
    }

    public bool GetDash()
    {
        return _dash;
    }

    public bool GetGhosting()
    {
        return _estela;
    }
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
        return Physics2D.BoxCast(_slideObject.GetComponent<Collider2D>().bounds.center, _slideObject.GetComponent<Collider2D>().bounds.size, 0f, Vector2.up, 1f, _groundLayer)
            && Physics2D.BoxCast(_slideObject.GetComponent<Collider2D>().bounds.center, _slideObject.GetComponent<Collider2D>().bounds.size, 0f, Vector2.up, 1f, _techoLayer)
            && !Physics2D.BoxCast(_slideObject.GetComponent<Collider2D>().bounds.center, _slideObject.GetComponent<Collider2D>().bounds.size, 0f, Vector2.left, 0.1f, 12)
            && !Physics2D.BoxCast(_slideObject.GetComponent<Collider2D>().bounds.center, _slideObject.GetComponent<Collider2D>().bounds.size, 0f, Vector2.right, 0.1f, 12);
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
        {
            GetComponent<AudioSource>().PlayOneShot(_normalJump);
            GetComponent<AudioSource>().PlayOneShot(_voicedJump);
        }

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

        _estela = true;
        _ghostingDuration = _initialGhostingDuration;
    }

    public void Climb(float YAxismove)
    //Metodo de escalada.
    {
        if ((YAxismove != 0 || _isClimbing) && _currentLadder != null && _currentLadder.gameObject.layer == 8)
        //Se comprueba si el jugador está moviéndose verticalmente o ya está subiendo,
        //si la escalera actual no es nula y está en la capa "Escalable".
        { 
            //Se establece la velocidad del jugador en la dirección vertical
            //y se desactiva la gravedad para que el jugador pueda subir por la escalera.  
            Vector2 targetVelocity = new Vector2(_myRigidBody2D.velocity.x, YAxismove * _climbVelocity);
            _myRigidBody2D.velocity = targetVelocity;
            _myRigidBody2D.gravityScale = 0;
            _isClimbing = true;

            //check si ha llegado a la cima de la escalera
            Vector2 footPosition = transform.position - GetComponent<BoxCollider2D>().bounds.extents;
            _ladderTop = _currentLadder.bounds.max.y;
            if (footPosition.y >= _ladderTop)
            {
                _myRigidBody2D.velocity = new Vector2(_myRigidBody2D.velocity.x, 0);
                if(YAxismove < 0)
                {
                    _myRigidBody2D.velocity = targetVelocity;
                }
            }
        }
        else
        // De lo contrario, se restaura la gravedad, se establece isClimbing a false
        // y se detiene el movimiento vertical.
        {
            _myRigidBody2D.gravityScale = _initialGravity;
            if(_isClimbing)
            {
                _myRigidBody2D.velocity = new Vector2(_myRigidBody2D.velocity.x, 0);
            }
            _isClimbing = false;
        }
    }

    public void QUIETO()
    {
        _myInputComponent.enabled = false;
        _myRigidBody2D.bodyType = RigidbodyType2D.Static;
        
        _myCollider2D.enabled = false;
    }
    #endregion

    #region Collision Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cuando Mighty toque layer de Ice (núm 9), el suavizado se incrementará
        // para dar efecto de resbalar.
        if(collision.gameObject.layer == 9)
        {
            _MovementSmoothing = _IceMoveSmoothing;
        }

        //Cuando Mighty toque layer de Ralentizante (núm 11), la velocidad se reducirá al igual que la fuerza del dash
        if (collision.gameObject.layer == 11 && _isgrounded) //El isgrounded no es necesario ahora, pero puede que de cara al futuro si, PARA MAS PREGUNTAS CONSULTAR A JOSE ANTONIO EL PORQUÉ
        {
            _movementSpeedX = _slowSpeedX;
            _dashForce = _smallDashForce;
        }
        
        //Si colisiona con la Plataforma Móvil.
        if(collision.gameObject.layer == 16)
        {   
            //Hace que Mighty se mueva acorde a la plataforma móvil en lugar de quedarse en la misma posición al pararse.
            //En resumen, hace que la plataforma transporte a Mighty.
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Cuando Mighty salga de la layer de Ralentizante (núm 11), recupera la velocidad original y la fuerza de dash original
        if (collision.gameObject.layer == 11 || !_isgrounded) //El isgrouded no es necesario ahora, pero puede que de cara al futuro si, PARA MAS PREGUNTAS CONSULTAR A JOSE ANTONIO EL PORQUÉ
        {
            _movementSpeedX = _initialMovementSpeedX;
            _dashForce = _initialdDashForce;
        }

        //Al bajarnos de la plataforma móvil.
        if (collision.gameObject.layer == 16)
        {
            //Vuelve nuestro transform original.
            transform.parent = null;
        }

        if (collision.gameObject.layer == 9 || !_isgrounded)
        {
            _MovementSmoothing = _originalMoveSmoothing;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //se comprueba si el objeto con el que el jugador ha chocado tiene la capa "Escalable"
        // y se asigna a currentLadder.
        if (other.gameObject.layer == 8)
        {
            _currentLadder = other;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //CAMBIO DE ESCENAS SI TOCAMOS DOOR
        if (collision.gameObject.layer == 18 && _isgrounded && !_dash)
        {
            _doorTouched = true;
            SpawnsManager.instance.ResetRespawnPosition();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Si el objeto que el jugador ha dejado es la escalera actual
        // y se establece currentLadder a null, se establece isClimbing a false
        // y se restaura la gravedad inicial del objeto.
        if (other == _currentLadder)
        {
            _currentLadder = null;
            _isClimbing = false;
        }
    }
    #endregion

    private void OnEnable()
    {
        _newInput.Enable();
    }
    private void OnDisable()
    {
        _newInput.Disable();
    }
    private void Awake()
    {
        _newInput = new UniversalInput();
    }
    private void Start()
    {
        // Inicializo componentes.
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _myCollider2D = GetComponent<BoxCollider2D>();
        _myInputComponent = GetComponent<InputComponent>();
        _animator = GetComponent<Animator>();
        _myGhostingDash = GetComponent<GhostingDash>();
        GameManager.instance.RegisterCharacterController(this);

        //Inicializo parámetros
        _originalMoveSmoothing = _MovementSmoothing;
        _initialMovementSpeedX = _movementSpeedX;
        _initialdDashForce = _dashForce;
        _initialGhostingDuration = _ghostingDuration;
        _doorTouched = false;
        _estela = false;

        // Guardo gravedad inicial.
        _initialGravity = _myRigidBody2D.gravityScale;
        
        //desactivamos el collider del dash al principio
        _slideObject.SetActive(false);
    }

    private void Update()
    {
        _isgrounded = IsGrounded();

        if (_isClimbing)
        {
            _isgrounded = false;
        }

        if (_doorTouched)
        {
            _isgrounded = true;
            _animator.SetBool("_isRunning", false);
            QUIETO();
        }

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
        _animator.SetBool("_isClimbing", _isClimbing);

        if (_estela)
        {
            _myGhostingDash.enabled = true;
            _ghostingDuration -= Time.deltaTime;
            if (_ghostingDuration <= 0.0f)
            {
                _ghostingDuration = _initialGhostingDuration;
                _estela = false;
            }
        }
        else
        {
            _myGhostingDash.enabled = false;
        }

        //Comprueba si el dash ha acabado y devuelve al player a la normalidad
        if (_dash && (!_isgrounded || _myRigidBody2D.velocity.x == 0 ||_newInput.Mighty.Jump.triggered) && !IsCeiling())
        {
            if (_newInput.Mighty.Jump.triggered)
            {
                Jump();
            }

            _slideObject.SetActive(false);
            _myCollider2D.isTrigger = false;
            _myInputComponent.enabled = true;
            _dash = false;
        }
        else if(_dash && _myRigidBody2D.velocity.x <= 0.5f && IsCeiling())
        {
            if (_facingRight)
            {
                transform.position += new Vector3(0.005f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(- 0.005f, 0, 0);
            }
        }
    }
}