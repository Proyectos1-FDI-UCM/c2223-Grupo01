using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters && references
    [SerializeField] private int _maxHealth = 100;     //Vida m�xima del enemigo.
    private int _currentHealth;                        //Vida actual del enemigo.
    [SerializeField] private float _damage;            //da�o al jugador
    private EnemyMovement _enemyMovement;
    private EnemyFlyingMovement _enemyFlyingMovement;
    #endregion

    #region methods
    public void TakeDamage(int damage)
    //M�todo para que el enemigo reciba da�o.
    //Cuando la vida quede a 0 o menos, el enemigo muere.
    {
        _currentHealth -= damage;

        //Animaci�n de recibir da�o.
        
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die()
    //M�todo para que muera el enemigo.
    //Desactiva el collider del enemigo al morir.
    //Desactiva el EnemyComponent.
    {
        //Animaci�n de morir.

        
        //Quitar al enemigo.
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Destroy(gameObject,1);

        // si no va a pie vuela, no hace falta comprobar ambos
        if(_enemyMovement != null)
        {
            _enemyMovement.enabled = false;
        }
        else
        {
            _enemyFlyingMovement.enabled = false;
        }
    }
    #endregion

    #region collisions methods
    private void OnCollisionStay2D(Collision2D collision)
    // Colisiones del jugador con los enemigos
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
        }
    }
    #endregion

    private void Start()
    {
        //Al comienzo, el enemigo comienza con m�xima vida.
        _currentHealth = _maxHealth;  
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyFlyingMovement = GetComponent <EnemyFlyingMovement>();
    }
}