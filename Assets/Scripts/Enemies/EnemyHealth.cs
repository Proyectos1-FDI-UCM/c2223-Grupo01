using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters
    //Vida máxima del enemigo.
    [SerializeField] private int _maxHealth = 100;
    //Vida actual del enemigo.
    private int _currentHealth;
    #endregion

    #region methods
    //Método para que el enemigo reciba daño.
    public void TakeDamage(int damage) 
    {
        //Cada hostia le va bajando la vida.
        _currentHealth -= damage;

        //Animación de recibir daño.

        //Cuando la vida quede a 0 o menos, el enemigo muere.
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    //Método para que muera el enemigo.
    void Die()  
    {
        //Animación de morir.

        //Quitar al enemigo.

        //Desactiva el collider del enemigo al morir.
        GetComponent<Collider2D>().enabled = false;
        //Desactiva el EnemyComponent.
        this.enabled = false;   
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Al comienzo, el enemigo comienza con máxima vida.
        _currentHealth = _maxHealth;  
    }
}