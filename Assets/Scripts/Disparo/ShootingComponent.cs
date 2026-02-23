using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField] private GameObject[] _bullet; //prefab de la bala
    private enum tiposDeBala { _normalBullet, _iceBullet, _fireBullet}
    private tiposDeBala _actualBullet;
    [SerializeField] private Transform _bulletSpawnTransform;   //Spawn de la bala

    private bool _canAttackShoot; //Permite poder disparar

    [SerializeField] private bool _armaHielo_Desbloqueada;      //Determina si el arma de Hielo esta desbloqueada o no.
    [SerializeField] private bool _armaFuego_Desbloqueada;      //Determina si el arma de Fuego esta desbloqueada o no.
    //[SerializeField] private bool _armaSuperDisparo_Desbloqueada;      //Determina si el arma de SuperDisparo esta desbloqueada o no.

    [SerializeField] private float _coolDownShoot;      //tiempo en el que se permitir� usar el disparo
    private float _initialCoolDownShoot;
    #endregion

    #region References
    private Animator _animator;
    [SerializeField]
    private RuntimeAnimatorController[] _animatorControllers;
    private Scene _scene;
    #endregion

    #region Getters & Setters
    public bool GetAttackShoot()
    {
        return _canAttackShoot;
    }
    #endregion

    #region References
    private InputComponent _myInputComponent;
    [SerializeField] private AudioClip _disparoNormal;
    #endregion

    #region Methods
    public void Shoot()
    // instanciamos la bala en la posici�n del spawn (cuidado no es hija suya, no confundir con la sobrecarga del transform del parent)
    {
        GetComponent<AudioSource>().PlayOneShot(_disparoNormal);
        //Crea un tipo de Bala en base a actualBullet, y su direccion se deermina mediante la rotacion y posicion de _bulletSpawnTransform
        Instantiate(_bullet[(int)_actualBullet], _bulletSpawnTransform.transform.position, _bulletSpawnTransform.rotation);
        _canAttackShoot = false; //Dice que es false para activar el tiempo del cooldown en el metodo Update de abajo, y asi no poder disparar hasta que el tiempo pase
    }
    public void ChangeBullet()
    {
        _actualBullet++;
        if (!_armaHielo_Desbloqueada && (int)_actualBullet == 1) //Si no se ha desbloqueado el hielo, se salta al siguiente
        {
            _actualBullet++;
        }
        else if (!_armaFuego_Desbloqueada && (int)_actualBullet == 2) //Si no se ha desbloqueado el fuegoo, se salta al siguiente
        {
            _actualBullet++;
        }

        if ((!_armaFuego_Desbloqueada && !_armaHielo_Desbloqueada) || (int)_actualBullet >= 3)
        {
            _actualBullet = 0;
        }

        GameManager.instance._UImanager.currentWeaponState((int)_actualBullet); //Cambia el icono de abajo en la interfaz para determinar que arma se usara ahora
        _animator.runtimeAnimatorController = _animatorControllers[(int)_actualBullet]; //Se cambia de aspecto del Jugador para que sea acorde al poder que usa, queda wapo
    }

    //public void ChangeBullet()
    //{
    //    _actualBullet++;
    //    if(_scene.buildIndex == 2) //Si estamos en el tutorial
    //    {
    //        if((int)_actualBullet == 1) //que no pueda pasar de _actualBullet 0 (normal)
    //        {
    //            _actualBullet = 0;
    //        }
    //    }
    //    else if(_scene.buildIndex == 3 || _scene.buildIndex == 4) //Si estamos en nivel hielo o fábrica
    //    {
    //        if((int)_actualBullet == 2) //que no pueda pasar de _actualBullet 1 (ice)
    //        {
    //            _actualBullet = 0;
    //        }
    //    }
    //    else if(_scene.buildIndex == 5 || _scene.buildIndex == 6) //Si estamos en nivel lava o final boss
    //    {
    //        if((int)_actualBullet == 3) //que no pueda pasar de _actualBullet 2 (fire)
    //        {
    //            _actualBullet = 0;
    //        }
    //    }

    //    GameManager.instance._UImanager.currentWeaponState((int)_actualBullet);
    //    _animator.runtimeAnimatorController = _animatorControllers[(int)_actualBullet];
    //}
    #endregion

    private void Start()
    {
        _initialCoolDownShoot = _coolDownShoot;
        _canAttackShoot = true;

        _animator = GetComponent<Animator>();

        _myInputComponent = GetComponent<InputComponent>();
        _actualBullet = tiposDeBala._normalBullet;
        _scene = SceneManager.GetActiveScene(); //para ver a qué armas podemos cambiar
    }

    private void Update()
    {
        if (!_canAttackShoot)
        {
            _coolDownShoot -= Time.deltaTime;

            if (_coolDownShoot <= 0)
                _canAttackShoot = true;
        }
        else _coolDownShoot = _initialCoolDownShoot;

        if (_myInputComponent._lookUP && gameObject.transform.rotation.y >= 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position= miposicion + new Vector2 (0.40f, 1.0f) ;
        }
        else if (_myInputComponent._lookUP && gameObject.transform.rotation.y < 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(-0.40f, 1.0f);
        }
        else if (!_myInputComponent._lookUP && gameObject.transform.rotation.y >= 0)
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(1.0f, 0.015f);
        }
        else
        {
            Vector2 miposicion = transform.position;
            _bulletSpawnTransform.position = miposicion + new Vector2(-1.0f, 0.015f);
        }
    }
}
