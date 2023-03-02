using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region References
    public static GameManager instance; // Singleton inicializado en el Awake
    public GameObject _player; // referencia al player que se puede usar desde otros Scrpts sin necesidad de String Typing (NO PONER PRIVADA)
    public GameObject _blaster; // referencia al arma de disparo que se puede usar desde otros Scrpts sin necesidad de String Typing (NO PONER PRIVADA)
    public float _currentTime { get; private set; } //variable que controla nuestro tiempo actual.
    public float _PlayerHealth { get; private set; } //Variable que controla nuestra vida de jugador
    public float _MaxHealth = 10f; //Variable que controla nuestra vida m�xima de jugador.
    public int _currentWeapon { get; private set; } //Variable que controla cu�l es nuestra arma actual.
    #endregion

    public void OnPlayerHit ()
    {
        _PlayerHealth--;
    }


    private void Awake()
    {
        instance = this; // esto inicializa el Singleton
    }

    // Start is called before the first frame update
    void Start()
    {   _currentWeapon = 2;
        _currentTime = 300f;
        _PlayerHealth = 10;
    }

    // Update is called once per frame
    void Update()
    {    
        
        _currentTime -= Time.deltaTime;
        _PlayerHealth -= Time.deltaTime; // Placeholder para comprobar el funcionamiento de la barra de vida.
       
    }
}
