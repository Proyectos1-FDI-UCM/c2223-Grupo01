using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region References
    public static GameManager instance;                 // Singleton inicializado en el Awake
    public GameObject _player;                          // Para usarlo en otros scripts
    public GameObject _blaster;                         // referencia al arma de disparo que se puede usar desde otros Scrpts sin necesidad de String Typing (NO PONER PRIVADA)
    public float _currentTime { get; private set; }     //variable que controla nuestro tiempo actual.

    [SerializeField] public float _health = 100f;                      //Variable que controla nuestra vida de jugador.

    public int _currentWeapon { get; private set; }     //Variable que controla cu�l es nuestra arma actual.
    #endregion

    

    private void Awake()
    //Inicializo el Singleton
    {
        instance = this;
    }

    void Start()
    {   _currentWeapon = 2;
        _currentTime = 300f;
    }

    void Update()
    {    
        _currentTime -= Time.deltaTime;
        // Placeholder para comprobar el funcionamiento de la barra de vida.
        //_health -= Time.deltaTime;
    }
}
