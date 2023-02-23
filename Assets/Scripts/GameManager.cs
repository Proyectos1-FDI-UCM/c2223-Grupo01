using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region References
    public static GameManager instance; // Singleton inicializado en el Awake
    public GameObject _player; // referencia al player que se puede usar desde otros Scrpts sin necesidad de String Typing (NO PONER PRIVADA)
    public float _currentTime { get; private set; }
    #endregion
    
    private void Awake()
    {
        instance = this; // esto inicializa el Singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        _currentTime =_currentTime / 1;
        Debug.Log(_currentTime);
    }
}
