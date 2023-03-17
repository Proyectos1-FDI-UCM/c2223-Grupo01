using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointHeathResComponent : MonoBehaviour
{
    #region references
    private GameObject _player;
    private float _playerHealth;
    #endregion

    #region parameters
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance._player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Vaporeon besto pokimon");
    }

}
