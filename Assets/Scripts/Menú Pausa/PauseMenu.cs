using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region References
    [SerializeField] private InputAction _pauseInput;
    [SerializeField] private InputAction _arrowsInput;
    [SerializeField] private InputAction _selectInput;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private AudioClip _selectingSFX;
    [SerializeField] private AudioClip _okSFX;
    #endregion

    #region Parameters
    private bool juegoPausado;
    private bool selected;
    #endregion

    #region Methods
    //Pausa el juego.

    private void OnEnable()
    {
        _pauseInput.Enable();
        _arrowsInput.Enable();
        _selectInput.Enable();
    }
    private void OnDisable()
    {
        _pauseInput.Disable();
        _arrowsInput.Disable();
        _selectInput.Disable();
    }

    public bool GetPause()
    {
        return juegoPausado;
    }
    public void Pausa()
    {
        juegoPausado = true;
        selected = false;
        //Para el juego, desactiva el botón de pausa, y activa el menú de pausa.
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    //Reanuda el juego.
    public void Reanudar()
    {
        juegoPausado = false;
        //Reanuda el juego, activa el botón de pausa y cierra el menú de pausa.
        Time.timeScale = 1f;
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
        Application.Quit();
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        juegoPausado = false;
        selected = true;
        menuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectInput.triggered && !selected)
        {
            GetComponent<AudioSource>().PlayOneShot(_okSFX);
            selected = true;
            if (juegoPausado)
            {
                selected = false;
            }
        }
        //Debug.Log(_pauseInput.triggered);
        if (_pauseInput.triggered)
        {
            if (!juegoPausado)
            {
                GetComponent<AudioSource>().PlayOneShot(_okSFX);
                Pausa();
            }
            else
            {
                Reanudar();
            }
        }

        if (_arrowsInput.triggered && juegoPausado)
        {
            GetComponent<AudioSource>().PlayOneShot(_selectingSFX);
        }

        if (!juegoPausado)
        {
            selected = true;
        }
    }
}
