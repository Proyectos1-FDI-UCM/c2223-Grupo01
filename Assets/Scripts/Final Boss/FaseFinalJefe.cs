using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseFinalJefe : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private GameObject _bullet; //prefab de la bala
    [SerializeField] private float _coolDownShoot; //Contador para poder volver a disparar
    [SerializeField] private Transform _bulletSpawnTransform;

    private float _initialCoolDownShoot; //Valor inicial del contador de disparo, util para reconfigurar el cooldown que se modifica en el Update

    private bool _canShoot; //Booleano que determina si puede disparar o no

    [SerializeField] private LayerMask _playerLayer;

    private void Shoot()
    // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
    {
        GetComponent<Animator>().SetTrigger("_attack");
        Instantiate(_bullet, _bulletSpawnTransform.position, _bulletSpawnTransform.rotation);
        _canShoot = false;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void ChangeState()
    {
        GetComponent<Animator>().SetBool("_change", enabled);
    }

    // Start is called before the first frame update
    void Start()
    {
        _initialCoolDownShoot = _coolDownShoot;
        _canShoot = true;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
            if (_canShoot)
            {
                _coolDownShoot = _initialCoolDownShoot;
                Shoot();
            }
            else
            {
                _coolDownShoot -= Time.deltaTime;
                if (_coolDownShoot <= 0)
                {
                    _canShoot = true;
                }
            }
    }
}
