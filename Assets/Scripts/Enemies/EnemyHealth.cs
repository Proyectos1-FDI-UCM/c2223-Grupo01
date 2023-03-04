using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _maxHealth = 100;     //Vida máxima del enemigo.
    private int _currentHealth;                        //Vida actual del enemigo.
    #endregion

    #region methods
    public void TakeDamage(int damage)
    //Método para que el enemigo reciba daño.
    //Cuando la vida quede a 0 o menos, el enemigo muere.
    {
        _currentHealth -= damage;

        //Animación de recibir daño.
        
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    void Die()
    //Método para que muera el enemigo.
    //Desactiva el collider del enemigo al morir.
    //Desactiva el EnemyComponent.
    {
        //Animación de morir.

        //Quitar al enemigo.

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;   
    }
    #endregion

    void Start()
    {
        //Al comienzo, el enemigo comienza con máxima vida.
        _currentHealth = _maxHealth;  
    }
}