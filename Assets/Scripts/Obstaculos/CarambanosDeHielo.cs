using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarambanosDeHielo : MonoBehaviour
{
    #region parameters && references
    //Distancia de deteccción
    [SerializeField] private float _distancia;

    //Daño al chocar con el player
    [SerializeField] private float _damage;

    [SerializeField] private LayerMask _player;

    [SerializeField] private AudioClip _hurt;
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + new Vector3(-0.45f, 0.0f, 0.0f), _distancia * Vector2.down);
        Gizmos.DrawRay(transform.position + new Vector3(0.45f, 0.0f, 0.0f), _distancia * Vector2.down);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Si uno de los 2 rayos detecta al player, deja que el carambano se caiga
        if ((Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0.0f, 0.0f), Vector2.down, _distancia, _player) || Physics2D.Raycast(transform.position + new Vector3(0.45f, 0.0f, 0.0f), Vector2.down, _distancia, _player)))
        {
            //De cara al futuro sería interesante poder configurar la masa para que caiga a cierta velocidad, pero de momento no lo necesitamos
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
