using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAnim : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AudioClip _activeSFX;
    private bool _active;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MightyLifeComponent>() != null && _active)
        {
            GetComponent<AudioSource>().PlayOneShot(_activeSFX);
            _animator.SetTrigger("_active");
            _active = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _active = true;
        _animator = GetComponent<Animator>();
    }
}
