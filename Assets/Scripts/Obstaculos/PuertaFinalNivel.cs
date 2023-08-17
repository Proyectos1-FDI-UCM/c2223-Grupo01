using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PuertaFinalNivel : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpened;
    private bool _canOpen;
    [SerializeField] private float _openCounter;
    [SerializeField] private int _escenaACargar;

    [SerializeField] private AudioClip _openedSFX;

    public float GetOpenCounter()
    {
        return _openCounter;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.RegisterPuertaFinalNivel(this);
        _animator = GetComponent<Animator>();
        _isOpened = false;
        _canOpen = true;
    }
    void Update()
    {
        if (GameManager.instance._canExitLevel && _canOpen)
        {
            _isOpened = true;
            _canOpen = false;
            GetComponent<AudioSource>().PlayOneShot(_openedSFX);
            _animator.SetTrigger("_activeDoor");
        }
        if (_isOpened == true)
        {
            _openCounter -= Time.deltaTime;
            if (_openCounter <= 0)
            {
                SceneManager.LoadScene(_escenaACargar);
            }
        }
        
    }
}
