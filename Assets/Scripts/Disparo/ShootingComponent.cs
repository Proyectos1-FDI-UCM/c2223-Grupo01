using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    #region references 
    [SerializeField] private GameObject _bullet; //prefab de la bala
    [SerializeField] private Transform _bulletSpawnTransform; //Spawn de la bala
    #endregion

    #region methods
    public void Shoot()
    {
        // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
        Instantiate(_bullet, _bulletSpawnTransform.transform.position, _bulletSpawnTransform.rotation);
    }
    #endregion
}
