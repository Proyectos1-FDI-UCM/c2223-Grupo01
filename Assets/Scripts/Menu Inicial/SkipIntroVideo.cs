using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipIntroVideo : MonoBehaviour
{
    #region References
    [SerializeField] private InputAction _pauseInput;
    [SerializeField] private InputAction _arrowsInput;
    [SerializeField] private InputAction _selectInput;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private AudioClip _selectingSFX;
    [SerializeField] private AudioClip _okSFX;
    [SerializeField] private VideoPlayer video;
    #endregion

    #region Parameters
    private bool juegoPausado;
    private bool selected;
    [SerializeField] private float _timer = 0.0f;
    [SerializeField] private float _maxTimer;
    [SerializeField] private int _sceneIndex;
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
        video.Pause();
        menuPausa.SetActive(true);
    }

    //Reanuda el juego.
    public void Reanudar()
    {
        juegoPausado = false;
        //Reanuda el juego, activa el botón de pausa y cierra el menú de pausa.
        video.Play();
        menuPausa.SetActive(false);
    }

    //Reinicia el juego.
    public void Reinicia()
    {
        juegoPausado = false;
        //Carga la escena desde el principio (recarga el nivel).
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Cierra el juego.
    public void Salir()
    {
        Application.Quit();
    }

    public void Skip()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0.0f;
        selected = true;
        juegoPausado = false;
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
            _timer += Time.deltaTime;
            if (_timer > _maxTimer)
            {
                SceneManager.LoadScene(_sceneIndex);
            }
        }
    }
}
