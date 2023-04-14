using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossBulletMovement : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed;  // velociadad de la bala
    [SerializeField] private float _damage;  // daño de la bala

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
        /*if(collision.gameObject.layer == 30)
        {
            _direccionY *= -1;
            _direccionX *= -1;
        }*/

        if (collision.gameObject.layer == 13 && collision.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
        }
    }

    void Start()
    {
        //Inicializamos el Rigidbody2D.
        _myRigidbody = GetComponent<Rigidbody2D>();
        _direccionX = GameManager.instance._player.transform.position.x - gameObject.transform.position.x;
        _direccionY = GameManager.instance._player.transform.position.y - gameObject.transform.position.y;
    }

    private void Update()
    {
        if (_direccionY > 0.0f && Physics2D.BoxCast(GetComponent<Collider2D>().bounds.center, GetComponent<Collider2D>().bounds.size, 0.0f, Vector2.up, .001f, _layerTerreno))
        {
            _direccionY *= -1;
        }
        else if (_direccionY < 0.0f && Physics2D.BoxCast(GetComponent<Collider2D>().bounds.center, GetComponent<Collider2D>().bounds.size, 0.0f, Vector2.down, .001f, _layerTerreno))
        {
            _direccionY *= -1;
        }

        if (_direccionX < 0.0f && Physics2D.BoxCast(GetComponent<Collider2D>().bounds.center, GetComponent<Collider2D>().bounds.size, 0.0f, Vector2.left, .001f, _layerPared))
        {
            _direccionX *= -1;
        }
        else if (_direccionX > 0.0f && Physics2D.BoxCast(GetComponent<Collider2D>().bounds.center, GetComponent<Collider2D>().bounds.size, 0.0f, Vector2.right, .002f, _layerPared))
        {
            _direccionX *= -1;
        }
    }

        void FixedUpdate()
    {
        _myRigidbody.velocity = new Vector2(_direccionX, _direccionY).normalized * _speed * Time.deltaTime;//desplazamiento de la bala
    }
}
