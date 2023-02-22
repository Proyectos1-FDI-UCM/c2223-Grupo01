using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    #region References
    private CharacterController _myCharacterController; // referencia a otro script inicializado en el Start
    private ShootingComponent _myShootingComponent; // referencia a otro script inicializado en el Start
    private MeleeComponent _myMeleeComponent; // referencia a otro script inicializado en el Start
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
        //llamamos a climb
        if (_myCharacterController._isClimbing)
        {
            _input.y = Input.GetAxisRaw("Vertical");
            _myCharacterController.Climb();
        }
        /*if (_myCharacterController._isClimbing)
        { //entonces activamos controles en eje Y (WS)
            _myCharacterController.MoveYAxis(Input.GetAxis("Vertical"));
            if (_myCharacterController._isgrounded)
            {
                _myCharacterController._isClimbing = false;
            }
        }*/
        //disparamos
        if (Input.GetKeyDown(KeyCode.X))
        {
            _myShootingComponent.Shoot();
        }
        // Ataca cuerpo a cuerpo
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _myMeleeComponent.Attack();
        }
        //nos movemos
        _myCharacterController.MoveXAxis(Input.GetAxis("Horizontal")); 
    }
}
