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
            _mightylifecomponent.TakeDamage(- _sanation);

            if (_mightylifecomponent.GetHealth() > 100)
            {
                _mightylifecomponent.TakeDamage(-_maximumHealth);
            }

            if (GameManager.instance._UImanager != null)
            {
                GameManager.instance._UImanager.ActualizarInterfaz(_mightylifecomponent.GetHealth());
            }
            Destroy(gameObject);
        }
    }
}
