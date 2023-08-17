using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroMenuCommands : MonoBehaviour
{
    [SerializeField] private InputAction _arrowsInput;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject _fade;
    [SerializeField] private GameObject botonSelectorDeNiveles;
    [SerializeField] private GameObject fondoSelectorNiveles;

    private int _escena;
    private int _escenaSelectorDeNiveles;
    [SerializeField] private int _selectorDeNiveles; //Vale 1 cuando está activado

    [SerializeField] private AudioClip _selectingSFX;
    [SerializeField] private AudioClip _okSFX;
    int _x , _y;

    private void OnEnable()
    {
        _arrowsInput.Enable();
    }
    private void OnDisable()
    {
        _arrowsInput.Disable();
    }
    public void Comenzar(int escene)
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        _escena = escene;
        _fade.GetComponent<Animator>().SetTrigger("OUT");
        menu.SetActive(false);
        fondoSelectorNiveles.SetActive(false);
        Invoke("ComenzarR", 2);
    }
    private void ComenzarR()
    {
        SpawnsManager.instance.ResetRespawnPosition();
        SceneManager.LoadScene(_escena);
    }
    public void SetRespawnX( int x)
    {
        _x = x;
    }
    public void SetRespawnY(int y)
    {
        _y = y;
    }
    public void ComenzarMitad (int escene)
    {
        SpawnsManager.instance.SetRespawnPosition(new Vector3(_x,_y,0));
        SceneManager.LoadScene(escene);
    }
    public void SalirDeljuego()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        _fade.GetComponent<Animator>().SetTrigger("OUT");
        menu.SetActive(false);
        Invoke("SalirR", 2);
    }
    private void SalirR()
    {
        Application.Quit();
    }

    public void BorrarPartida()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        PlayerPrefs.SetInt("SCENE", 0);
        PlayerPrefs.SetFloat("X", 0);
        PlayerPrefs.SetFloat("Y", 0);
        PlayerPrefs.SetFloat("F", 0);
        PlayerPrefs.SetInt("LEVELSELECT", 0);
        PlayerPrefs.Save();
    }

    void Start()
    {
        _selectorDeNiveles = PlayerPrefs.GetInt("LEVELSELECT");
        _escena = 0;
        menu.SetActive(true);
    }

    void Update()
    {
        _selectorDeNiveles = PlayerPrefs.GetInt("LEVELSELECT");

        if (_arrowsInput.triggered)
        {
            GetComponent<AudioSource>().PlayOneShot(_selectingSFX);
        }

        if (_selectorDeNiveles == 1)
        {
            botonSelectorDeNiveles.SetActive(true);
        }
        else
        {
            botonSelectorDeNiveles.SetActive(false);
        }
    }
}
