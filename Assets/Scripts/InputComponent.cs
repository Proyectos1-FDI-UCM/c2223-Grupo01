using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputComponent : MonoBehaviour
{
    #region Parameters
    public bool _lookUP { get; private set; }  //condici�n para mirar a arriba
    public bool _lookDOWN { get; private set; }  //condici�n para mirar a abajo (escaleras)
    #endregion

    #region References
    private CharacterController _myCharacterController;
    private ShootingComponent _myShootingComponent;
    private MeleeComponent _myMeleeComponent;
    private Animator _animator;
    private PauseMenu _pausa;

    [SerializeField] private AudioClip _melee;
    [SerializeField] private AudioClip _airMelee;
    [SerializeField] private AudioClip _changeWeapon;
    #endregion

    void Start()
    //Inicializo referencias
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myShootingComponent = GetComponent<ShootingComponent>();
        _myMeleeComponent= GetComponent<MeleeComponent>();
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        _myCharacterController.Climb(Input.GetAxis("Vertical"));
    }
    private void Update()
    {
       
        // Comprobamos si estamos pulsando el espacio y si podemos saltar
        // (o estamos en el suelo o no hemos gastado el doble salto)
        if ((_myCharacterController.GetIsGrounded()||_myCharacterController._doublejump)&& (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            if (_myCharacterController.GetIsGrounded()) _animator.SetTrigger("_jump");
            _myCharacterController.Jump();
        }
        else if(_myCharacterController.GetIsGrounded() && ( Input.GetKeyDown(KeyCode.L)||Input.GetKeyDown(KeyCode.Joystick1Button5))) // input del dash
        {
            _myCharacterController.Dash();
        }
        
        // Disparamos
        if (_myShootingComponent.GetAttackShoot() && Input.GetKeyDown(KeyCode.K)||Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            _animator.SetTrigger("_shoot");
            _myShootingComponent.Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            GetComponent<AudioSource>().PlayOneShot(_changeWeapon);
            _myShootingComponent.ChangeBullet();
        }

        // Ataca cuerpo a cuerpo

        if ((Input.GetKeyDown(KeyCode.M)||Input.GetKeyDown(KeyCode.Joystick1Button3)) && _myMeleeComponent.GetAttackMelee())
        {
            if (_myCharacterController.GetIsGrounded())
            {
                GetComponent<AudioSource>().PlayOneShot(_melee);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(_airMelee);
            }
            _animator.SetTrigger("_melee");
            _myMeleeComponent.Attack();
        }

        // Reinicia el nivel tutorial. El numero es en relaci�n con el orden de escenas al hacer la build
        if (Input.GetKeyDown(KeyCode.R)||Input.GetKeyDown(KeyCode.Joystick1Button6)) SceneManager.LoadScene(1);

        // Movimiento
        _myCharacterController.MoveXAxis(Input.GetAxis("Horizontal"));
       
        // Animaciones
        _animator.SetBool("_isRunning", Input.GetAxis("Horizontal") != 0);
        _lookUP = Input.GetAxis("Vertical") > 0;
        _lookDOWN = Input.GetAxis("Vertical") < 0;
        _animator.SetBool("_isLookUp", _lookUP);//esto hay que arreglarlo, se para el personaje mientras se pulsa la W o flecha arriba, pero no con otra tecla
        _animator.SetBool("_isLookDown", _lookDOWN);
    }

}
