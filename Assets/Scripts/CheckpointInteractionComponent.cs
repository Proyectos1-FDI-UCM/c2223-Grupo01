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
    [SerializeField] private float _timeReset;
    private Transform _checkPointTransform;
    #endregion
    #region Getter
    public Transform GetCheckPointTransform()
    {
        return _checkPointTransform;
    }
    #endregion  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameManager.instance._player)
        {
            // referencias en la colision
            _player = collision.gameObject;
            _mightyLifeComponent = _player.GetComponent<MightyLifeComponent>();

            // sanacion de Mighty
            _mightyLifeComponent.TakeDamage(-_healthRes);
            if (GameManager.instance._UImanager != null)
            {
                GameManager.instance._UImanager.ActualizarInterfaz(_mightyLifeComponent.GetHealth());
            }
            //se guarda el transform del ultimo checkpoint tocado
            _checkPointTransform = this.transform;

            // reseteo del cronometro
            //GameManager.instance._currentTime = _timeReset + 1;
        }
    }
    // private void Start()
    // {
    //     _checkpointPosition = transform.position;
    // }
    // private void Update()
    // {
    //     _checkpointPosition = _currentRespawner.transform.position;
    //     if(_mightyLifeComponent.GetDeath()) //si mighty muere
    //     {
    //         _mightyLifeComponent.SetDeath(true); //resucitamos
    //         _player.transform.position = _checkpointPosition; // Lo posicionamos en el checkpoint
    //     }
    //     Debug.Log(_currentRespawner);
    // }
}
