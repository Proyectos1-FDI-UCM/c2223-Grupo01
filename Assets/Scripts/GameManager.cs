using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region parameters 
    public float _currentTime;                          //variable que controla nuestro tiempo actual.
    public float _health = 100f;                      //Variable que controla nuestra vida de jugador.
    public int _currentWeapon { get; private set; }     //Variable que controla cuál es nuestra arma actual.

    private bool _timeMusicActive; //Variable que determina si la musica de tiempo de muerte está activa o no

    [SerializeField] private float _deathTimeDamage; //Daño que quita cada ciclo
    #endregion

    #region References
    public static GameManager instance; // Singleton inicializado en el Awake
    public GameObject _player;// Para usarlo en otros scripts
    public MightyLifeComponent _mightyLifeComponent { get; private set;}
    public UIManager _UImanager { get; private set;}

    [SerializeField] private AudioClip _timeOut;
    #endregion

    #region methods
    // Registramos la clase de Mighty Component.
    public MightyLifeComponent RegisterMightyComponent(MightyLifeComponent mightyLifeComponent)
    {
        _mightyLifeComponent = mightyLifeComponent;
        return _mightyLifeComponent;
    }

    // Registramos la clase del UI Manager
    public UIManager RegisterUIMManager ( UIManager UIManager)
    {
        _UImanager = UIManager;
        return _UImanager;
    }

    public MeleeComponent RegisterMeleeComponent( MeleeComponent mele)
    {
        return mele;
    }

    #endregion

    // awake para la instancia de la clase
    private void Awake()     //Inicializo el Singleton
    {
        instance = this;
    }

    void Start()
    {
        _timeMusicActive = false;
        _currentWeapon = 2;
        _currentTime = 120.0f;
    }

    void Update()
    {    
        _currentTime -= Time.deltaTime;

        // Resta progresivamente la vida al acabarse el tiempo
        if (_currentTime <= 0 && _mightyLifeComponent._health > 0)
        {
            _mightyLifeComponent.DeathTime(_deathTimeDamage * Time.deltaTime); //El deltaTime es para tener mas controlado el daño por segundo para no tener que usar valores tan pequeños

        }

        // Pone la música de que se acaba el tiempo y la vida se resta
        if (_currentTime <= 0.0f && !_timeMusicActive)
        {
            GetComponent<AudioSource>().PlayOneShot(_timeOut);
            _timeMusicActive = true;
        }
    }
}
