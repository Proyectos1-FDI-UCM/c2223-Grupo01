using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    #region References
    private CharacterController _myCharacterController;
    private ShootingComponent _myShootingComponent;
    private MeleeComponent _myMeleeComponent;
    private Animator _animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myShootingComponent = GetComponent<ShootingComponent>();
        _myMeleeComponent= GetComponent<MeleeComponent>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Comprobamos si estamos pulsando el espacio y si podemos saltar
        // (o estamos en el suelo o no hemos gastado el doble salto)
        if ((_myCharacterController._isgrounded && Input.GetKeyDown(KeyCode.Space)) || (_myCharacterController._doublejump && Input.GetKeyDown(KeyCode.Space)))
        {
            _myCharacterController.Jump();
        }
        else if(_myCharacterController._isgrounded && Input.GetKeyDown(KeyCode.LeftControl)) // input del dash
        {
            _myCharacterController.Dash();
        }
        
        // Disparamos
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
        // Movimiento horizontal
        _myCharacterController.MoveXAxis(Input.GetAxis("Horizontal"));

        //-Animaciones
        _animator.SetBool("_isRunning", Input.GetAxis("Horizontal") != 0);
        _animator.SetBool("_isLookUp", Input.GetKey(KeyCode.E));//esto hay que arreglarlo, se para el personaje mientras se pulsa la W o flecha arriba, pero no con otra tecla

    }
}
