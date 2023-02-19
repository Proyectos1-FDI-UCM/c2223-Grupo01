using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region References
    public static GameManager instance; // Singleton inicializado en el Awake
    public GameObject _player; // referencia al player que se puede usar desde otros Scrpts sin necesidad de String Typing (NO PONER PRIVADA)
    #endregion

    private void Awake()
    {
        instance = this; // esto inicializa el Singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
