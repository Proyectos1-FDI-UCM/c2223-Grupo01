using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters
    //Vida m�xima del enemigo.
    [SerializeField] private int _maxHealth = 100;
    //Vida actual del enemigo.
    private int _currentHealth;
    #endregion

    #region methods
    //M�todo para que el enemigo reciba da�o.
    public void TakeDamage(int damage) 
    {
        //Cada hostia le va bajando la vida.
        _currentHealth -= damage;

        //Animaci�n de recibir da�o.

        //Cuando la vida quede a 0 o menos, el enemigo muere.
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    //M�todo para que muera el enemigo.
    void Die()  
    {
        //Animaci�n de morir.

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
        //Al comienzo, el enemigo comienza con m�xima vida.
        _currentHealth = _maxHealth;  
    }
}