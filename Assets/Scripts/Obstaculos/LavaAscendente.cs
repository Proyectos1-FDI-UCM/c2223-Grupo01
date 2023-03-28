using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAscendente : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed;  // velociadad de la lava
    private GameObject _player;             // referencia al player   
    [SerializeField] private float _lavaDamage; // cuanto daño hace la lava
    #endregion

    #region References
    private Rigidbody2D _myRigidbody;
    #endregion


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == _player && _player.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            _player.GetComponent<MightyLifeComponent>().OnPlayerHit(_lavaDamage);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance._player;
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (_player.GetComponent<MightyLifeComponent>().GetHealth() < 90) //una vez se haga el boton que lo accione, se cambiará la condición
        {

            _myRigidbody.velocity = new Vector2(0, _speed * Time.deltaTime);//desplazamiento de la bala
        }
    }
}
