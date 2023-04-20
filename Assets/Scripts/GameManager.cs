using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region parameters 
    public float _currentTime;                          //variable que controla nuestro tiempo actual.
    public int _currentWeapon { get; private set; }     //Variable que controla cu�l es nuestra arma actual.
    private bool _timeMusicActive; //Variable que determina si la musica de tiempo de muerte est� activa o no
    [SerializeField] private float _deathTimeDamage; //Da�o que quita cada ciclo
    public bool _canUseMelee { get; private set; }

    static private Vector2 _checkPointPos;
    #endregion

    #region References
    public static GameManager instance; // Singleton inicializado en el Awake
    public GameObject _player;// Para usarlo en otros scripts
    public MightyLifeComponent _mightyLifeComponent { get; private set;}
    public UIManager _UImanager { get; private set;}
    [SerializeField] private AudioClip _timeOut;
    public Transform _playerSpawner { get; private set; }
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

    public void CheckPointUpdatePos()
    {
        _checkPointPos= new Vector2(_player.transform.position.x, _player.transform.position.y);
    }

    //Comprueba si se puede usar el melee o no
    public bool HandleMeleeActivation(string _escena)
    {
        return _escena != "Nivel Hielo"; //Se le puede comparar sin necesidad de poner el nombre de la escena? No lo se
    }
    #endregion

    // awake para la instancia de la clase
    private void Awake()     //Inicializo el Singleton
    {
        instance = this;
    }

    void Start()
    {
        _playerSpawner = GetComponent<Transform>();

        _playerSpawner.position = _checkPointPos;

        //Actualiza la posicion por si cambia de nivel, no es totalmente necesario pq spawnea en el nivel correspondiente, pero antes de hacer cambios avisad
        _checkPointPos = new Vector2(_player.transform.position.x, _player.transform.position.y);

        _canUseMelee = false;
        _timeMusicActive = false;
        _currentWeapon = 2;
        _currentTime = 600.0f;
    }

    void Update()
    {
        //Debug.Log(_checkPointPos.x);
        _currentTime -= Time.deltaTime;

        // Resta progresivamente la vida al acabarse el tiempo
        if (_currentTime <= 0 && _mightyLifeComponent.GetHealth() > 0)
        {
            _mightyLifeComponent.DeathTime(_deathTimeDamage * Time.deltaTime); //El deltaTime es para tener mas controlado el da�o por segundo para no tener que usar valores tan peque�os
        }

        // Pone la m�sica de que se acaba el tiempo y la vida se resta
        if (_currentTime <= 0.0f && !_timeMusicActive)
        {
            GetComponent<AudioSource>().PlayOneShot(_timeOut);
            _timeMusicActive = true;
        }
    }
}
