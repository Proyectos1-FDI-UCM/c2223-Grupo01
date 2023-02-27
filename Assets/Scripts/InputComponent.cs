using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    #region References
    private CharacterController _myCharacterController; // referencia a otro script inicializado en el Start
    private ShootingComponent _myShootingComponent; // referencia a otro script inicializado en el Start
    private MeleeComponent _myMeleeComponent; // referencia a otro script inicializado en el Start

    private Animator _animator;
    #endregion
    #region parameters
    public Vector2 _input;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myShootingComponent = GetComponent<ShootingComponent>();
        _myMeleeComponent= GetComponent<MeleeComponent>();
        _animator = GetComponent<Animator>();
    }

    //1º llamo al salto (no va)
    //2º llamo al movimiento (para mover a Mighty)
    private void Update()
    {
        //comprobamos si estamos pulsando el espacio y si podemos saltar (o estamos en el suelo o no hemos gastado el doble salto)
        if ((_myCharacterController._isgrounded && Input.GetKeyDown(KeyCode.Space)) || (_myCharacterController._doublejump && Input.GetKeyDown(KeyCode.Space)))
        {
            _myCharacterController.Jump();
        }
        else if(_myCharacterController._isgrounded && Input.GetKeyDown(KeyCode.LeftControl)) // input del dash
        {
            _myCharacterController.Dash();
        }
        //llamamos a Climb() y cojo info del axisY
        _input.y = Input.GetAxisRaw("Vertical");
        _myCharacterController.Climb();
        
        //disparamos
        if (Input.GetKeyDown(KeyCode.X))
        {
            _animator.SetTrigger("_shoot");
            _myShootingComponent.Shoot();
        }
        // Ataca cuerpo a cuerpo
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _animator.SetTrigger("_melee");
            _myMeleeComponent.Attack();
        }
        //nos movemos
        _myCharacterController.MoveXAxis(Input.GetAxis("Horizontal"));

        _animator.SetBool("_isRunning", Input.GetAxis("Horizontal") != 0); //mas preciso que con la velocidad, porque a veces se queda caminando cuando esta quieto si va la condicion por rigidbody


        _animator.SetBool("_isLookUp", Input.GetKey(KeyCode.E));//esto hay que arreglarlo, se para el personaje mientras se pulsa la W o flecha arriba, pero no con otra tecla

    }
}
