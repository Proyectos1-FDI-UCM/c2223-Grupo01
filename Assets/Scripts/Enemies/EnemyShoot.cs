using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    #region Parameters
    [SerializeField] private GameObject _bullet; //prefab de la bala
    [SerializeField] private float _coolDownShoot; //Contador para poder volver a disparar
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private float _patrolFlip;
    private float _initialPatrolFlip;
    private int _patrullaje;

    private float _initialCoolDownShoot; //Valor inicial del contador de disparo, util para reconfigurar el cooldown que se modifica en el Update

    private bool _canShoot; //Booleano que determina si puede disparar o no
    #endregion

    #region References
    [SerializeField] private AudioClip _disparoNormal;
    private Animator _animator;
    #endregion

    #region Methods
    public void Shoot()
    // instanciamos la bala en la posición del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
    {
        GetComponent<AudioSource>().PlayOneShot(_disparoNormal);
        _animator.SetTrigger("_shoot");

        Instantiate(_bullet, _bulletSpawnTransform.position, _bulletSpawnTransform.rotation);
        
        Debug.Log("dispara");
        _canShoot = false;
    }
    #endregion

    private void Start()
    {
        _initialCoolDownShoot = _coolDownShoot;
         _initialPatrolFlip = _patrolFlip;
        _animator = GetComponent<Animator>();
        _canShoot= true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<EnemyFOV>().GetDetected() && gameObject.GetComponent<EnemyHealth>()._currentHealth > 0)
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
            if (GameManager.instance._player.transform.position.x <= gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (GameManager.instance._player.transform.position.x > gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            _patrolFlip -= Time.deltaTime;
            if (_patrolFlip <= 0)
            {
                if (_patrullaje == 0)
                {
                    _patrullaje += 180;
                }
                else
                {
                    _patrullaje -= 180;
                }

                gameObject.transform.rotation = Quaternion.Euler(0, _patrullaje, 0);
                _patrolFlip = _initialPatrolFlip;
            }
        }

        
    }
}
