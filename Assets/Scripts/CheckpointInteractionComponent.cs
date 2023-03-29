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
    private CharacterController _myCharacterController;
    #endregion

    #region parameters
    [SerializeField] private float _healthRes;
    [SerializeField] private float _timeRes;
    private bool _startCheckpoint;
    private bool _midCheckpoint;
    private Vector3 _checkpointPosition;
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
            if(_mightyLifeComponent.GetDeath()) //si mighty muere
            {
                _mightyLifeComponent.SetDeath(true); //resucitamos
                _player.transform.position = _checkpointPosition; // Lo posicionamos en el checkpoint
                if (_startCheckpoint || _midCheckpoint) // Si el checkpoint es de inicio o de mitad de nivel
                {
                    _myCharacterController.SetCurrentCheckpoint(this); // Lo guardamos como el checkpoint actual del personaje
                }
            }

            // reseteo del cronometro
            GameManager.instance._currentTime = _timeRes + 1;
        }
    }
    private void Start()
    {
        _checkpointPosition = transform.position;
    }
}
