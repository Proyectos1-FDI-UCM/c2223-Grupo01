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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_playertag))
        {
            // referencias en la colisi�n
            _player = collision.gameObject;

            // sanaci�n de Mighty
            _player.GetComponent<MightyLifeComponent>()._health = 100;
            //if (GameManager.instance._UImanager != null)
            //{
            //    GameManager.instance._UImanager.ActualizarInterfaz(_health);
            //}
        }
    }
}
