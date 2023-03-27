using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField] private GameObject[] _bullet; //prefab de la bala
    private enum tiposDeBala { _normalBullet, _iceBullet, _fireBullet}
    private tiposDeBala _actualBullet;
    [SerializeField] private Transform _bulletSpawnTransform;   //Spawn de la bala
    #endregion

    #region Getters & Setters

    #endregion

    #region References
    private InputComponent _myInputComponent;
    [SerializeField] private AudioClip _disparoNormal;
    #endregion

    #region Methods
    public void Shoot()
    // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
    {
        GetComponent<AudioSource>().PlayOneShot(_disparoNormal);
        Instantiate(_bullet[(int)_actualBullet], _bulletSpawnTransform.transform.position, _bulletSpawnTransform.rotation);
    }

    public void ChangeBullet()
    {
        _actualBullet++;
        if((int)_actualBullet == 3)
        {
            _actualBullet = 0;
        }
        GameManager.instance._UImanager.currentWeaponState((int)_actualBullet);
    }
    #endregion

    private void Start()
    {
        _myInputComponent= GetComponent<InputComponent>();
        _actualBullet = tiposDeBala._normalBullet;
    }

    private void Update()
    {
        if (_myInputComponent._lookUP && gameObject.transform.rotation.y >= 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position= miposicion + new Vector2 (0.45f, 1.2f) ;
        }
        else if (_myInputComponent._lookUP && gameObject.transform.rotation.y < 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(-0.45f, 1.2f);
        }
        else if (!_myInputComponent._lookUP && gameObject.transform.rotation.y >= 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(1.2f, 0.015f);
        }
        else
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(-1.2f, 0.015f);
        }
    }
}
