using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthpackComponent : MonoBehaviour
{
    #region references
    private GameObject _player;
    private MightyLifeComponent _mightylifecomponet;
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
            _mightylifecomponet = _player.GetComponent<MightyLifeComponent>();

            // sanacion de Mighty
            _mightylifecomponet.SetHealth(_mightylifecomponet.GetHealth() + _sanation);

            if (_mightylifecomponet.GetHealth() > 100)
            {
                _mightylifecomponet.SetHealth(_maximumHealth);
            }

            if (GameManager.instance._UImanager != null)
            {
                GameManager.instance._UImanager.ActualizarInterfaz(_mightylifecomponet.GetHealth());
            }
            Destroy(gameObject);
        }
    }
}
