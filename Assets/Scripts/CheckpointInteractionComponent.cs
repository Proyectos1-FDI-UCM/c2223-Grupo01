using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.UIElements;
using UnityEngine;

public class CheckpointInteractionComponent : MonoBehaviour
{
    #region references
    private GameObject _player;
    private MightyLifeComponent _mightyLifeComponent;
    #endregion

    #region parameters
    [SerializeField] private float _healthRes;
    [SerializeField] private float _timeRes;
    private Transform _respawn;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameManager.instance._player)
        {
            // referencias en la colision
            _player = collision.gameObject;
            _mightyLifeComponent = _player.GetComponent<MightyLifeComponent>();

            // sanaci�n de Mighty
            _mightyLifeComponent.TakeDamage(-_healthRes);
            if (GameManager.instance._UImanager != null)
            {
                GameManager.instance._UImanager.ActualizarInterfaz(_mightyLifeComponent.GetHealth());
            }
            //aqui se deberia guardar el ultimo checkpoint y llamarlo respawner
            // to do

            // reseteo del cronometro
            GameManager.instance._currentTime = _timeRes + 1;
        }
    }
    private void Start()
    {
        _respawn = GetComponent<Transform>(); //tomo transform del checkpoint
    }
}
