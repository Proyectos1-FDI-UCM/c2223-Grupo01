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
    private MightyLifeComponent _mightyhealth;
    #endregion

    #region parameters
    [SerializeField] private string _playertag;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_playertag))
        {
            // referencias de la colisión
            _player = collision.gameObject;
            _mightyhealth = GetComponent<MightyLifeComponent>();

            _mightyhealth._health = 100;
            //if (GameManager.instance._UImanager != null)
            //{
            //    GameManager.instance._UImanager.ActualizarInterfaz(_mightyhealth._health);
            //}




        }
    }
}
