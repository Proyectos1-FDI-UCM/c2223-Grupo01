using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    #region Parameters
    [SerializeField] private GameObject _bullet; //prefab de la bala
    [SerializeField] private float _coolDownShoot; //Contador para poder volver a disparar

    private float _initialCoolDownShoot; //Valor inicial del contador de disparo, util para reconfigurar el cooldown que se modifica en el Update

    private bool _canShoot; //Booleano que determina si puede disparar o no
    #endregion

    #region References
    [SerializeField] private AudioClip _disparoNormal;
    #endregion

    #region Methods
    public void Shoot()
    // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
    {
        //GetComponent<AudioSource>().PlayOneShot(_disparoNormal);
        Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
        Debug.Log("dispara");
        _canShoot = false;
    }
    #endregion

    private void Start()
    {
        _initialCoolDownShoot = _coolDownShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canShoot)
        {
            _coolDownShoot -= Time.deltaTime;
            if (_coolDownShoot <= 0)
            {
                _canShoot = true;
            }
        }
        else
        {

            _coolDownShoot = _initialCoolDownShoot;
            Shoot();
        }

    }
}
