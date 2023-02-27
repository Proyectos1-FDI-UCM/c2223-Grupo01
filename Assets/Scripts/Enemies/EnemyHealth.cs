using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _maxHealth = 100; //Vida máxima del enemigo.
    private int _currentHealth;  //Vida actual del enemigo.
    #endregion

    #region methods
    public void TakeDamage(int damage)  //Método para que el enemigo reciba daño.
    {
        _currentHealth -= damage; //Cada hostia le va bajando la vida.

        //Animación de recibir daño.

        if (_currentHealth <= 0) //Cuando la vida quede a 0 o menos, el enemigo muere.
        {
            Die();
        }
    }

    void Die()  //Método para que muera el enemigo.
    {
        //Animación de morir.

        //Quitar al enemigo.
        GetComponent<Collider2D>().enabled = false; //Desactiva el collider del enemigo al morir.
        this.enabled = false;   //Desactiva el EnemyComponent.
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;  //Al comienzo, el enemigo comienza con máxima vida.
    }
}