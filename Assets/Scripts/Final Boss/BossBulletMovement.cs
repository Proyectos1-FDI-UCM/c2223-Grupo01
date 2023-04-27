using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed, _damage;  // velociadad de la bala y daño de la bala
    [SerializeField] private int _nRebotes;

    private float _direccionX; // Comprueba la dirección X hacia donde debe ir la bala
    private float _direccionY; // Comprueba la dirección Y hacia donde debe ir la bala

    #endregion

    #region References
    private Rigidbody2D _myRigidbody;
    [SerializeField] private LayerMask _layerTerreno;
    [SerializeField] private LayerMask _layerPared;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 13 && collision.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
        }

        // Colision con paredes
        if ((_direccionX < 0.0f && collision.gameObject.layer == 12) || (_direccionX > 0.0f && collision.gameObject.layer == 12))
        {
            _direccionX *= -1;
            _nRebotes--;
        }

        // Colision con techo y suelo
        if ((_direccionY < 0.0f && collision.gameObject.layer == 6) || (_direccionY > 0.0f && collision.gameObject.layer == 6))
        {
            _direccionY *= -1;
            _nRebotes--;
        }
        if(_nRebotes == 0)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Inicializamos el Rigidbody2D.
        _myRigidbody = GetComponent<Rigidbody2D>();
        _direccionX = GameManager.instance._player.transform.position.x - gameObject.transform.position.x;
        _direccionY = GameManager.instance._player.transform.position.y - gameObject.transform.position.y;
    }

    private void FixedUpdate()
    {
        _myRigidbody.velocity = new Vector2(_direccionX, _direccionY).normalized * _speed * Time.deltaTime;//desplazamiento de la bala
    }
}