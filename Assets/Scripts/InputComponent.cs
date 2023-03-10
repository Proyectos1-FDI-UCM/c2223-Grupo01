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

    [SerializeField] private AudioClip _melee;
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
        if(!_myCharacterController._quieroBajarDeEscaleras)
        {
            _myCharacterController.Climb(Input.GetAxis("Vertical"));
        }
    }
    private void Update()
    {
        // Comprobamos si estamos pulsando el espacio y si podemos saltar
        // (o estamos en el suelo o no hemos gastado el doble salto)
        if ((_myCharacterController.GetIsGrounded() && Input.GetKeyDown(KeyCode.Space)) || (_myCharacterController._doublejump && Input.GetKeyDown(KeyCode.Space)))
        {
            _myCharacterController.Jump();
        }
        else if(_myCharacterController.GetIsGrounded() && Input.GetKeyDown(KeyCode.L)) // input del dash
        {
            _myCharacterController.Dash();
        }
        
        // Disparamos
        if (Input.GetKeyDown(KeyCode.K))
        {
            _animator.SetTrigger("_shoot");
            _myShootingComponent.Shoot();
        }

        // Ataca cuerpo a cuerpo

        if (Input.GetKeyDown(KeyCode.M) && _myMeleeComponent.GetAttackMelee())

        {
            GetComponent<AudioSource>().PlayOneShot(_melee);
            _animator.SetTrigger("_melee");
            _myMeleeComponent.Attack();
        }

        // Reinicia el nivel tutorial. El numero es en relaci�n con el orden de escenas al hacer la build
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(1);

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
