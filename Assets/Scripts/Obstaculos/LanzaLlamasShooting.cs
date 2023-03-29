using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaLlamasShooting : MonoBehaviour
{
    //El comportamiento del lanzallamas es semejante al del enemigo tirador, pero con menos condiciones. No puede disparar cuando vea al jugador, es de manera aautomática, por lo cual solo se usarán contadores para instanciar las llamas

    #region Parameters
    [SerializeField] private GameObject _flame; //prefab de la llama
    [SerializeField] private float _coolDownShoot; //Contador para poder volver a disparar
    [SerializeField] private Transform _flameSpawnTransform;

    private float _initialCoolDownShoot; //Valor inicial del contador de disparo, util para reconfigurar el cooldown que se modifica en el Update

    private bool _canShoot; //Booleano que determina si puede disparar o no
    #endregion

    #region References
    [SerializeField] private AudioClip _llamaSFX;
    #endregion

    #region Methods
    public void Shoot()
    // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
    {
        GetComponent<AudioSource>().PlayOneShot(_llamaSFX);

        Instantiate(_flame, _flameSpawnTransform.position, _flameSpawnTransform.rotation);

        Debug.Log("llamarada");
        _canShoot = false;
    }
    #endregion

    private void Start()
    {
        _initialCoolDownShoot = _coolDownShoot;
        _canShoot = false;
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