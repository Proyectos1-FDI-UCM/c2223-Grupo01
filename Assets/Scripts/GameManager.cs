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
    public float _PlayerHealth { get; private set; }    //Variable que controla nuestra vida de jugador
    public float _MaxHealth = 10f;                      //Variable que controla nuestra vida máxima de jugador.
    public int _currentWeapon { get; private set; }     //Variable que controla cuál es nuestra arma actual.
    #endregion

    public void OnPlayerHit()
    //Cuando se haga hit, daña al player
    {
        _PlayerHealth--;
    }

    private void Awake()
    //Inicializo el Singleton
    {
        instance = this;
    }

    void Start()
    {   _currentWeapon = 2;
        _currentTime = 300f;
        _PlayerHealth = 10;
    }

    void Update()
    {    
        _currentTime -= Time.deltaTime;
        // Placeholder para comprobar el funcionamiento de la barra de vida.
        _PlayerHealth -= Time.deltaTime;
    }
}
