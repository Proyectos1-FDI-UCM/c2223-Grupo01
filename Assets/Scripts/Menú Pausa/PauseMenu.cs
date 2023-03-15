using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region References
    [SerializeField] private GameObject BotonPausar;
    
    #endregion

    #region Methods
    //Pausa el juego.
    public void Pausa()
    {
        Time.timeScale = 0f;
    }

    //Reanuda el juego.
    public void Reanudar()
    {
        Time.timeScale = 1f;
    }


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
