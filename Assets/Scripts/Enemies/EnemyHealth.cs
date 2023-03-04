using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _maxHealth = 100;     //Vida m�xima del enemigo.
    private int _currentHealth;                        //Vida actual del enemigo.
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

    void Die()
    //M�todo para que muera el enemigo.
    //Desactiva el collider del enemigo al morir.
    //Desactiva el EnemyComponent.
    {
        //Animaci�n de morir.

        //Quitar al enemigo.

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;   
    }
    #endregion

    void Start()
    {
        //Al comienzo, el enemigo comienza con m�xima vida.
        _currentHealth = _maxHealth;  
    }
}