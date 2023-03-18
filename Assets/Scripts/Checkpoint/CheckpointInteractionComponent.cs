using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class CheckpointInteractionComponent : MonoBehaviour
{
    #region references
    private GameObject _player;
    #endregion

    #region parameters
    [SerializeField] private string _playertag;
    #endregion

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_playertag))
        {
            _player = collision.gameObject;

            Debug.Log(_player.name);




        }
    }
}
