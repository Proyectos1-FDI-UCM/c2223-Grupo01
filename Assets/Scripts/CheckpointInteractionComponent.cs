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
    private float _playerhealth;
    #endregion

    #region parameters
    [SerializeField] private string _playertag;
    [SerializeField] private float _ammounthealthres;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_playertag))
        {
            // referencias en la colisión
            _player = collision.gameObject;
            _playerhealth = _player.GetComponent<MightyLifeComponent>()._health;

            // sanación de Mighty
            _playerhealth = _ammounthealthres;
            if (GameManager.instance._UImanager != null)
            {
                GameManager.instance._UImanager.ActualizarInterfaz(_playerhealth);
            }
        }
    }
}
