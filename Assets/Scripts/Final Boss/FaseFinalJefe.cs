using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FaseFinalJefe : MonoBehaviour
{
    [SerializeField] private Transform _myTransform;
    private GameObject _player;

    [SerializeField] private GameObject _bullet; //prefab de la bala
    [SerializeField] private float _coolDownShoot; //Contador para poder volver a disparar
    [SerializeField] private Transform _bulletSpawnTransform;

    private float _initialCoolDownShoot; //Valor inicial del contador de disparo, util para reconfigurar el cooldown que se modifica en el Update

    private bool _canShoot; //Booleano que determina si puede disparar o no

    private int _patata = 0;

    [SerializeField] private LayerMask _playerLayer;

    public void Shoot()
    // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
    {
        Instantiate(_bullet, _bulletSpawnTransform.position, Quaternion.Euler(0.0f,0.0f,0.0f));

        Debug.Log("dispara");
        _canShoot = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_myTransform.position, _player.transform.position - _myTransform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        _initialCoolDownShoot = _coolDownShoot;
        _canShoot = true;

        _myTransform = GetComponent<Transform>();
        _player = GameManager.instance._player;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.Raycast(_myTransform.position, _player.transform.position - _myTransform.position, 100000.0f, _playerLayer))
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

            _patata++;
            Debug.Log("debug" + _patata);
        }
    }
}
