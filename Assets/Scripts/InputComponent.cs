using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    #region Parameters
    public bool _lookUP { get; private set; }  //condici�n para mirar a arriba
    public bool _lookDOWN { get; private set; }  //condici�n para mirar a abajo (escaleras)

    private int _animMeleeValue;

    private int _sfxMeleeValue;

    private int _sfxMeleeVoiceValue;
    #endregion

    #region References
    private CharacterController _myCharacterController;
    private ShootingComponent _myShootingComponent;
    private MeleeComponent _myMeleeComponent;
    private Animator _animator;
    private PauseMenu _pausa;
    private Scene _scene;

    private UniversalInput _newInput;
    private InputAction _movement;

    [SerializeField] private AudioClip _melee1;
    [SerializeField] private AudioClip _melee2;
    [SerializeField] private AudioClip _meleeVoice1;
    [SerializeField] private AudioClip _meleeVoice2;
    [SerializeField] private AudioClip _meleeVoice3;
    [SerializeField] private AudioClip _meleeVoice4;
    [SerializeField] private AudioClip _airMelee;
    [SerializeField] private AudioClip _changeWeapon;
    #endregion
    //El Enable, Disable y Awake están para que el nuevo Input funcione.
    private void OnEnable()
    {
        _newInput.Enable();
        _movement = _newInput.Mighty.Movement;
        _movement.Enable();
    }

    private void OnDisable()
    {
        _newInput.Disable();
        _movement.Disable();
    }

    private void Awake()
    {
        _newInput = new UniversalInput();
    }

    void Start()
    //Inicializo referencias
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myShootingComponent = GetComponent<ShootingComponent>();
        _pausa = GetComponent<PauseMenu>();
        _myMeleeComponent = GetComponent<MeleeComponent>();
        _animator = GetComponent<Animator>();
        _scene = SceneManager.GetActiveScene();
        _animMeleeValue = 0;
        _sfxMeleeValue = 0;
        _sfxMeleeVoiceValue = 0;
}
    private void FixedUpdate()
    {
        _myCharacterController.Climb(_movement.ReadValue<Vector2>().y);
    }
    private void Update()
    {
        if (!_pausa.GetPause())
        {

            // Comprobamos si estamos pulsando el espacio y si podemos saltar
            // (o estamos en el suelo o no hemos gastado el doble salto)
            if ((_myCharacterController.GetIsGrounded() || _myCharacterController._doublejump) && _newInput.Mighty.Jump.triggered)
            {
                if (_myCharacterController.GetIsGrounded()) _animator.SetTrigger("_jump");
                _myCharacterController.Jump();
            }
            else if (_myCharacterController.GetIsGrounded() && _newInput.Mighty.Dash.triggered) // input del dash
            {
                _myCharacterController.Dash();
            }

            // Disparamos
            if (_myShootingComponent.GetAttackShoot() && _newInput.Mighty.Shoot.triggered)
            {
                _animator.SetTrigger("_shoot");
                _myShootingComponent.Shoot();
            }
            else if (_newInput.Mighty.Change.triggered)
            {
                GetComponent<AudioSource>().PlayOneShot(_changeWeapon);
                _myShootingComponent.ChangeBullet();
            }

            // Ataca cuerpo a cuerpo

            if (GameManager.instance.HandleMeleeActivation(_scene.buildIndex) && _newInput.Mighty.Melee.triggered && _myMeleeComponent.GetAttackMelee())
            {
                if (_myCharacterController.GetIsGrounded())
                {
                    _sfxMeleeValue = Random.Range(0, 2);
                    if (_sfxMeleeValue == 0)
                    {
                        GetComponent<AudioSource>().PlayOneShot(_melee1);
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(_melee2);
                    }
                    
                }
                else
                {
                    GetComponent<AudioSource>().PlayOneShot(_airMelee);
                }

                _sfxMeleeVoiceValue = Random.Range(0, 4);

                if (_sfxMeleeVoiceValue == 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(_meleeVoice1);
                }
                else if (_sfxMeleeVoiceValue == 1)
                {
                    GetComponent<AudioSource>().PlayOneShot(_meleeVoice2);
                }
                else if (_sfxMeleeVoiceValue == 2)
                {
                    GetComponent<AudioSource>().PlayOneShot(_meleeVoice3);
                }
                else if (_sfxMeleeVoiceValue == 3)
                {
                    GetComponent<AudioSource>().PlayOneShot(_meleeVoice4);
                }

                _animator.SetTrigger("_melee");
                _animMeleeValue = Random.Range(0, 4);
                _animator.SetInteger("_animMelee", _animMeleeValue);
                _myMeleeComponent.Attack();
            }

            // Movimiento
            _myCharacterController.MoveXAxis(_movement.ReadValue<Vector2>().x);

            // Animaciones
            _animator.SetBool("_isRunning", _movement.ReadValue<Vector2>().x != 0);
            _lookUP = _movement.ReadValue<Vector2>().y > 0;
            _lookDOWN = _movement.ReadValue<Vector2>().y < 0;
            _animator.SetBool("_isLookUp", _lookUP);//esto hay que arreglarlo, se para el personaje mientras se pulsa la W o flecha arriba, pero no con otra tecla
            _animator.SetBool("_isLookDown", _lookDOWN);
        }

    }
}
