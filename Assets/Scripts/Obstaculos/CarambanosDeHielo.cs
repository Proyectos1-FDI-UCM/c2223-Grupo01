using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarambanosDeHielo : MonoBehaviour
{
    #region parameters && references
    //Booleano que determina si cae o no el cono. Puede no ser necesario en un principio así que lo dejaré comentado por ahora. Aunque si lo quieres quitar adelante, de momento el script hace la misma funcion sin este booleano
    //private bool _isfalling;
    //Coordenadas del eje X para luego en el editor poner como de ancho puede ser el cono en la IZQ y DER. De momento se me ocurrió solo esto
    [SerializeField] private float _anguloX1;
    [SerializeField] private float _anguloX2;
    //Distancia de deteccción
    [SerializeField] private float _distancia;
    //Daño al chocar con el player
    [SerializeField] private float _damage;

    [SerializeField] private LayerMask _player;

    [SerializeField] private AudioClip _hurt;
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, _distancia * (Vector2.down + new Vector2(_anguloX1, 0.0f)));
        Gizmos.DrawRay(transform.position, _distancia * (Vector2.down + new Vector2(_anguloX2, 0.0f)));
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //_isfalling = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, (Vector2.down + new Vector2(_anguloX1, 0.0f)) * _distancia, Color.red);
        Debug.DrawRay(transform.position, (Vector2.down + new Vector2(_anguloX2, 0.0f)) * _distancia, Color.red);

        //Si uno de los 2 rayos detecta al player, deja que el carambano se caiga
        if ((Physics2D.Raycast(transform.position, Vector2.down + new Vector2(_anguloX1, 0.0f), _distancia, _player) || Physics2D.Raycast(transform.position, Vector2.down + new Vector2(_anguloX2, 0.0f), _distancia, _player)))
        {
            Debug.DrawRay(transform.position, (Vector2.down + new Vector2(_anguloX1, 0.0f)) * _distancia, Color.green);
            Debug.DrawRay(transform.position, (Vector2.down + new Vector2(_anguloX2, 0.0f)) * _distancia, Color.green);
            //De cara al futuro sería interesante poder configurar la masa para que caiga a cierta velocidad, pero de momento no lo necesitamos
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        /*if ( (Physics2D.Raycast(transform.position, Vector2.down + new Vector2(_anguloX1, 0.0f), _distancia, _player) || Physics2D.Raycast(transform.position, Vector2.down + new Vector2(_anguloX2, 0.0f), _distancia, _player)) && !_isfalling)
        {
            Debug.DrawRay(transform.position, (Vector2.down + new Vector2(_anguloX1, 0.0f)) * _distancia, Color.green);
            Debug.DrawRay(transform.position, (Vector2.down + new Vector2(_anguloX2, 0.0f)) * _distancia, Color.green);
            _isfalling = true;
        }*/

            /*if (_isfalling)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }*/
    }
}
