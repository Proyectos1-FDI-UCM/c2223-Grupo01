using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region References
    private CharacterController _myCharacterController;
    private Rigidbody2D _myRigidbody2D;
    #endregion

    #region Movement Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Escalera"))
        {
            _myCharacterController._isClimbing = true; //estamos tocando escaleras
            _myRigidbody2D.gravityScale = 0; //gravedad a 0 para no caerme de las escaleras
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Escalera"))
        {
            _myCharacterController._isClimbing = false; //salgo de escaleras
            _myRigidbody2D.gravityScale = 1;//vuelvo a poner gravedad a 1
            //Tendriamos que hacer que una vez salga de las escaleras por arriba, pare en seco en vez de salir
            //con impulso de la velocidad 
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
}
