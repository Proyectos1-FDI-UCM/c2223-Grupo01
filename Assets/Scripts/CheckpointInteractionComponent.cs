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
    private MightyLifeComponent _mightylifecomponet;
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
            _mightylifecomponet = _player.GetComponent<MightyLifeComponent>();

            // sanación de Mighty
            _mightylifecomponet._health = _ammounthealthres;
            if (GameManager.instance._UImanager != null)
            {
                GameManager.instance._UImanager.ActualizarInterfaz(_mightylifecomponet._health);
            }
        }
    }
}
