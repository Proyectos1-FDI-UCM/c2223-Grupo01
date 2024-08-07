using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region parameters 
    public float _currentTime;                          //variable que controla nuestro tiempo actual.
    public int _currentWeapon { get; private set; }     //Variable que controla cu�l es nuestra arma actual.
    private bool _timeMusicActive; //Variable que determina si la musica de tiempo de muerte est� activa o no
    [SerializeField] private float _deathTimeDamage; //Da�o que quita cada ciclo
    public bool _canUseMelee { get; private set; }
    public bool _canExitLevel { get; private set; }
    #endregion

    #region References
    public static GameManager instance; // Singleton inicializado en el Awake
    public GameObject _player;// Para usarlo en otros scripts
    public GameObject _fade;
    [SerializeField] private GameObject[] _UIs;
    public MightyLifeComponent _mightyLifeComponent { get; private set;}
    public CharacterController _characterController { get; private set; }
    public PuertaFinalNivel _puertaFinalNivel { get; private set; }
    public UIManager _UImanager { get; private set;}
    [SerializeField] private AudioClip _timeOut;
    #endregion

    #region methods
    // Registramos la clase de Mighty Component y Character Controller.
    public MightyLifeComponent RegisterMightyComponent(MightyLifeComponent mightyLifeComponent)
    {
        _mightyLifeComponent = mightyLifeComponent;
        return _mightyLifeComponent;
    }

    public CharacterController RegisterCharacterController(CharacterController characterController)
    {
        _characterController = characterController;
        return _characterController;
    }
    //Registramos otra clase que es la de las puertas de final de nivel

    public PuertaFinalNivel RegisterPuertaFinalNivel(PuertaFinalNivel puertaFinalNivel)
    {
        _puertaFinalNivel = puertaFinalNivel;
        return _puertaFinalNivel;
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

    //Comprueba si se puede usar el melee o no
    public bool HandleMeleeActivation(int _escena)
    {
        return (_escena != 2 && _escena != 3); //Se le puede comparar sin necesidad de poner el nombre de la escena? No lo se
    }
    #endregion

    // awake para la instancia de la clase
    private void Awake()     //Inicializo el Singleton
    {
            instance = this;
    }

    void Start()
    {
        _canUseMelee = false;
        _timeMusicActive = false;
        _canExitLevel = false;
        _currentWeapon = 2;
        _currentTime = 600.0f;
    }

    void Update()
    {
        _currentTime -= Time.deltaTime;
        _UImanager.UpdateTimer(_currentTime);
        // Resta progresivamente la vida al acabarse el tiempo
        if (_currentTime <= 0 && _mightyLifeComponent.GetHealth() > 0 && !_characterController._doorTouched)
        {
            _mightyLifeComponent.DeathTime(_deathTimeDamage * Time.deltaTime); //El deltaTime es para tener mas controlado el da�o por segundo para no tener que usar valores tan peque�os
        }

        // Pone la m�sica de que se acaba el tiempo y la vida se resta
        if (_currentTime <= 0.0f && !_timeMusicActive)
        {
            GetComponent<AudioSource>().PlayOneShot(_timeOut);
            _timeMusicActive = true;
        }

        if (_mightyLifeComponent.GetHealth() <= 0.0f || _characterController._doorTouched)
        {
            _player.GetComponent<PauseMenu>().enabled = false;
            if (_characterController._doorTouched)
            {
                _canExitLevel = true;
            }

        }

        if (_mightyLifeComponent.GetRepTimer() <= 2.5f)
        {
            for (int i = 0; i < _UIs.Length; i++)
            {
                _UIs[i].SetActive(false);
            }
            _fade.GetComponent<Animator>().SetTrigger("OUT");
        }

        if (_puertaFinalNivel != null && _puertaFinalNivel.GetOpenCounter() <= 2.0f)
        {
            for (int i = 0; i < _UIs.Length; i++)
            {
                _UIs[i].SetActive(false);
            }
            _fade.GetComponent<Animator>().SetTrigger("OUT");

        }

    }
}
