using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAnim : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AudioClip _activeSFX;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MightyLifeComponent>() != null)
        {
            GetComponent<AudioSource>().PlayOneShot(_activeSFX);
            _animator.SetTrigger("_active");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
