using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PuertaFinalNivel : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpened;
    [SerializeField] private float _openCounter;
    [SerializeField] private int _escenaACargar;

    [SerializeField] private AudioClip _openedSFX;

    public float GetOpenCounter()
    {
        return _openCounter;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>() != null && collision.gameObject.GetComponent<CharacterController>()._doorTouched && !_isOpened)
        {
            _isOpened = true;
            GetComponent<AudioSource>().PlayOneShot(_openedSFX);
            _animator.SetTrigger("_activeDoor");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.RegisterPuertaFinalNivel(this);
        _animator = GetComponent<Animator>();
        _isOpened = false;
    }
    void Update()
    {
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
