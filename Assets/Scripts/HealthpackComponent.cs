using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthpackComponent : MonoBehaviour
{
    #region references
    private GameObject _player;
    private MightyLifeComponent _mightylifecomponent;
    #endregion

    #region parameters
    [SerializeField] private float _sanation;
    [SerializeField] private float _maximumHealth;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance._player)
        {
            // referencias en la colision
            _player = collision.gameObject;
            _mightylifecomponent = _player.GetComponent<MightyLifeComponent>();

            // sanacion de Mighty
            _mightylifecomponent._health = _mightylifecomponent._health + _sanation;

            if (_mightylifecomponent._health > 100)
            {
                _mightylifecomponent._health = _maximumHealth;
            }

            if (GameManager.instance._UImanager != null)
            {
                GameManager.instance._UImanager.ActualizarInterfaz(_mightylifecomponent._health);
            }
            Destroy(gameObject);
        }
    }
}
