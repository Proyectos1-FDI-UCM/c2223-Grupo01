using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region References
    private CharacterController _myCharacterController;
    #endregion

    #region Movement Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Escalera"))
        {
            _myCharacterController._isOnStairs = true;
            Debug.Log("Toco escaleras");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Escalera"))
        {
            _myCharacterController._isOnStairs = false;
            Debug.Log("salgo de escaleras");
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_myCharacterController._isOnStairs);
    }
}
