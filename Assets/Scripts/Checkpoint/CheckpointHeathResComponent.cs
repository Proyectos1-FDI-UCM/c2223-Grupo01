using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHeathResComponent : MonoBehaviour
{
    #region references
    private GameObject _player;
    private float _playerHealth;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance._player;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
