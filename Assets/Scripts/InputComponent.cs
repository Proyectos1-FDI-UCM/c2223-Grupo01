using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    #region References
    private CharacterController _myCharacterController;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }

    //1º llamo al salto (no va)
    //2º llamo al movimiento (para mover a Mighty)
    private void Update()
    {
        if ((_myCharacterController._isgrounded && Input.GetKeyDown(KeyCode.Space)) || (_myCharacterController._doublejump && Input.GetKeyDown(KeyCode.Space)))
        {
            _myCharacterController.Jump();
        }
        else if (_myCharacterController._isOnStairs)
        {
            _myCharacterController.MoveYAxis(Input.GetAxis("Vertical"));
        }
        _myCharacterController.MoveXAxis(Input.GetAxis("Horizontal")); 
    }
}
