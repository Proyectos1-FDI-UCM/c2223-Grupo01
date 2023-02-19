using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    #region references 
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnTransform;
    #endregion

    #region methods
    public void Shoot()
    {
        Instantiate(_bullet, _bulletSpawnTransform.transform.position, _bulletSpawnTransform.rotation);
    }
    #endregion
}
