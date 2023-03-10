using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarambanosDeHielo : MonoBehaviour
{
    #region parameters && references
    private bool _isfalling;
    [SerializeField] private float _distancia;
    [SerializeField] private float _damage;

    RaycastHit2D _playerDebajo;

    [SerializeField] private AudioClip _hurt;
    #endregion

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _isfalling = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        _playerDebajo = Physics2D.Raycast(transform.position, Vector2.down, _distancia);

        Debug.DrawRay(transform.position, Vector2.down * _distancia, Color.red);

        if ( _playerDebajo.collider == gameObject.GetComponent<MightyLifeComponent>() && !_isfalling)
        {
            Debug.DrawRay(transform.position, Vector2.down * _distancia, Color.green);
            _isfalling = true;
        }

        if (_isfalling)
        {
            Debug.Log("AAAAAAAAAAA");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
