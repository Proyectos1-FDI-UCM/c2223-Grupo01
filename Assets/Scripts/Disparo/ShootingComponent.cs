using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField] private GameObject _bullet; //prefab de la bala
    [SerializeField] private Transform _bulletSpawnTransform; //Spawn de la bala
    //private Vector2 _oldposition;
    #endregion

    #region References
    private InputComponent _myInputComponent;
    [SerializeField] private AudioClip _disparoNormal;
    #endregion

    #region methods
    public void Shoot()
    {
            GetComponent<AudioSource>().PlayOneShot(_disparoNormal);
            // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
            Instantiate(_bullet, _bulletSpawnTransform.transform.position, _bulletSpawnTransform.rotation);
    }
    #endregion
    private void Start()
    {
        _myInputComponent= GetComponent<InputComponent>();
    }

    private void Update()
    {
        if (_myInputComponent._lookUP && gameObject.transform.rotation.y >= 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position= miposicion + new Vector2 (0.5f, 1.5f) ;
        }
        else if (_myInputComponent._lookUP && gameObject.transform.rotation.y < 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(-0.5f, 1.5f);
        }
        else if (!_myInputComponent._lookUP && gameObject.transform.rotation.y >= 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(1.2f, 0);
        }
        else
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(-1.2f, 0);
        }
    }
}
