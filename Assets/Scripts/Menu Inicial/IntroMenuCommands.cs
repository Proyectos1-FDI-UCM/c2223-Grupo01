using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroMenuCommands : MonoBehaviour
{
    [SerializeField] private InputAction _arrowsInput;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject _fade;

    private int _escena;

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
        menu.SetActive(false);
        _escena = escene;
        _fade.GetComponent<Animator>().SetTrigger("OUT");
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
        menu.SetActive(false);
        _fade.GetComponent<Animator>().SetTrigger("OUT");
        Invoke("SalirR", 2);
    }
    private void SalirR()
    {
        Application.Quit();
    }

    void Start()
    {
        _escena = 0;
        menu.SetActive(true);
    }

    void Update()
    {
        if (_arrowsInput.triggered)
        {
            GetComponent<AudioSource>().PlayOneShot(_selectingSFX);
        }
    }
}
