using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipIntroVideo : MonoBehaviour
{
    #region References
    private Scene _scene;

    [SerializeField] private InputAction _pauseInput;
    [SerializeField] private InputAction _arrowsInput;
    [SerializeField] private InputAction _textBoxInput;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject textBox;

    [SerializeField] private AudioClip _selectingSFX;
    [SerializeField] private AudioClip _okSFX;
    [SerializeField] private VideoPlayer video;
    #endregion

    #region Parameters
    private bool juegoPausado;
    private bool textBoxVisible;
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
        _textBoxInput.Enable();
    }
    private void OnDisable()
    {
        _pauseInput.Disable();
        _arrowsInput.Disable();
        _textBoxInput.Disable();
    }

    public bool GetPause()
    {
        return juegoPausado;
    }
    public void Pausa()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        juegoPausado = true;
        textBoxVisible = false;
        textBox.SetActive(false);
        //Para el juego, desactiva el botón de pausa, y activa el menú de pausa.
        video.Pause();
        menuPausa.SetActive(true);
    }

    //Reanuda el juego.
    public void Reanudar()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        _textBoxInput.Reset();
        juegoPausado = false;
        textBoxVisible = false;
        //Reanuda el juego, activa el botón de pausa y cierra el menú de pausa.
        video.Play();
        menuPausa.SetActive(false);
    }

    //Reinicia el juego.
    public void Reinicia()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        _textBoxInput.Reset();
        juegoPausado = false;
        textBoxVisible = false;
        //Carga la escena desde el principio (recarga el nivel).
        Invoke("ReiniciaR",1);
    }

    private void ReiniciaR()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Cierra el juego.
    public void Salir()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        Invoke("SalirR", 1);
    }

    private void SalirR()
    {
        Application.Quit();
    }
    public void Skip()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        Invoke("SkipR", 0.4f);
    }

    private void SkipR()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _scene = SceneManager.GetActiveScene();
        if (_scene.buildIndex == 12)
        {
            PlayerPrefs.SetInt("LEVELSELECT", 1);
            PlayerPrefs.Save();
        }
        _timer = 0.0f;
        juegoPausado = false;
        textBoxVisible = false;
        menuPausa.SetActive(false);
        textBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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

        if (_textBoxInput.triggered && !juegoPausado)
        {
            if (!textBoxVisible)
            {
                textBoxVisible = true;
            }
            else
            {
                textBoxVisible = false;
            }
        }

        if (!textBoxVisible)
        {
            textBox.SetActive(false);
        }
        else
        {
            textBox.SetActive(true);
        }

        if (!juegoPausado)
        {
            _timer += Time.deltaTime;
            if (_timer > _maxTimer)
            {
                SceneManager.LoadScene(_sceneIndex);
            }
        }
    }
}
