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
            _myCharacterController._isClimbing = true;
            Debug.Log("Toco escaleras");
            _myRigidbody2D.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Escalera"))
        {
            _myCharacterController._isClimbing = false;
            _myRigidbody2D.gravityScale = 1;
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
