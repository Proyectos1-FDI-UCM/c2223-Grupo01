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
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private AudioClip _selectingSFX;
    [SerializeField] private AudioClip _okSFX;
    #endregion

    #region Parameters
    private bool juegoPausado;
    #endregion

    #region Methods
    //Pausa el juego.

    private void OnEnable()
    {
        _pauseInput.Enable();
        _arrowsInput.Enable();
    }
    private void OnDisable()
    {
        _pauseInput.Disable();
        _arrowsInput.Disable();
    }

    public bool GetPause()
    {
        return juegoPausado;
    }
    public void Pausa()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        juegoPausado = true;
        //Para el juego, desactiva el botón de pausa, y activa el menú de pausa.
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    //Reanuda el juego.
    public void Reanudar()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        juegoPausado = false;
        //Reanuda el juego, activa el botón de pausa y cierra el menú de pausa.
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    //Reinicia el juego.
    public void Reinicia()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        juegoPausado = false;
        Time.timeScale = 1f;
        //Carga la escena desde el principio (recarga el nivel).
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Cierra el juego.
    public void Salir()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        Application.Quit();
    }
    public void SalirAlMenu()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        juegoPausado = false;
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        SceneManager.LoadScene(1);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        juegoPausado = false;
        menuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_pauseInput.triggered)
        {
            GetComponent<AudioSource>().PlayOneShot(_okSFX);
            if (!juegoPausado)
            {
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
    }
}
