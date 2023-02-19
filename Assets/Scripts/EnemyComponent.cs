using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    public int maxHealth = 100; //Vida máxima del enemigo.
    int currentHealth;  //Vida actual del enemigo.

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  //Al comienzo, el enemigo comienza con máxima vida.
    }

    public void TakeDamage(int damage)  //Método para que el enemigo reciba daño.
    {
        currentHealth = currentHealth - damage; //Cada hostia le va bajando la vida.

        //Animación de recibir daño.

        if (currentHealth <= 0) //Cuando la vida quede a 0 o menos, el enemigo muere.
        {
            Die();
        }
    }

    void Die()  //Método para que muera el enemigo.
    {
        Debug.Log("Samuerto");

        //Animación de morir.

        //Quitar al enemigo.
        GetComponent<Collider2D>().enabled = false; //Desactiva el collider del enemigo al morir.
        this.enabled = false;   //Desactiva el EnemyComponent.
    }
}
