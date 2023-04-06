using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Para gestionar la escena.
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region References
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private InputAction _pauseInput;
    #endregion

    #region Parameters
    private bool juegoPausado = false;
    #endregion


    private void OnEnable()
    {
        _pauseInput.Enable();
    }
    private void OnDisable()
    {
        _pauseInput.Disable();
    }

    #region Methods
    //Pausa el juego.
    public void Pausa()
    {
        juegoPausado = true;
        //Para el juego, desactiva el botón de pausa, y activa el menú de pausa.
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    //Reanuda el juego.
    public void Reanudar()
    {
        juegoPausado = false; 
        //Reanuda el juego, activa el botón de pausa y cierra el menú de pausa.
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    //Reinicia el juego.
    public void Reinicia()
    {
        juegoPausado = false;
        //Carga la escena desde el principio (recarga el nivel).
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Cierra el juego.
    public void Salir()
    {
        //Quita la aplicación.
        Debug.Log("Se ha cerrado el juego");
        Application.Quit();
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_pauseInput.triggered)
        {
            if(juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }
}
