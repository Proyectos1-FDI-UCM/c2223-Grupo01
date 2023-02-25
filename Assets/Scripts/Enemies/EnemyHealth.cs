using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters
    public int maxHealth = 100; //Vida m�xima del enemigo.
    int currentHealth;  //Vida actual del enemigo.
    #endregion

    public EnemyMovement enemyMovement; //Sirve para comunicarse con el script de "EnemyMovement".

    #region methods
    public void TakeDamage(int damage)  //M�todo para que el enemigo reciba da�o.
    {
        currentHealth = currentHealth - damage; //Cada hostia le va bajando la vida.

        //Animaci�n de recibir da�o.

        if (currentHealth <= 0) //Cuando la vida quede a 0 o menos, el enemigo muere.
        {
            Die();
        }
    }

    void Die()  //M�todo para que muera el enemigo.
    {
        //Animaci�n de morir.

        //Quitar al enemigo.
        GetComponent<Collider2D>().enabled = false; //Desactiva el collider del enemigo al morir.
        this.enabled = false;   //Desactiva el EnemyComponent.
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  //Al comienzo, el enemigo comienza con m�xima vida.
    }
}
