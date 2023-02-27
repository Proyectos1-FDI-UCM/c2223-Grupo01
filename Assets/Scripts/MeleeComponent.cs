using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComponent : MonoBehaviour
{
    public Animator animator;

    //El punto que checkea si le ha dao a un enemigo.
    [SerializeField] private Transform attackPoint;
    //Para referirse a dónde están los enemigos (para pegarles). WIP: Recuerda hacer la layer de los enemigos.
    [SerializeField] private LayerMask enemyLayers;

    //El rango con el que ataca (radio).
    [SerializeField] private float attackRange = 0.5f;
    //Daño por golpe.
    [SerializeField] private int attackDamage = 40;

    //Indica cuántas veces se va a atacar por segundo.
    [SerializeField] private float attackRate = 2f;
    //Cooldown del ataque.
    private float nextAttackTime = 0f;

    //Sirve para comunicarse con el script de "EnemyMovement".
    private EnemyMovement _enemyMovement = null; 

    //WIP: Falta hacer que al darle a la tecla de atacar, haga aleatoriamente (una u otra) las animaciones "Attack 1" y "Attack 2". Por el momento sólo he puesto "Attack 1".
    public void Attack()   
    {
        //"Time.time" es el tiempo al empezar cada frame, y si es mayor que el cooldown, podrá atacar.
        if (Time.time >= nextAttackTime) 
        {
            //Por ejemplo, si tu ataque es 2, dividimos 1/2 que es 0.5 sumado al tiempo actual. Ese será el tiempo que tendremos hasta atacar otra vez.
            nextAttackTime = Time.time + 1f / attackRate;    

            //Animación de ataque.
            animator.SetTrigger("Attack");

            //Detectar a los enemigos en el rango de ataque (el radio del golpe).

            //"hitEnemies" es un array de los enemigos que hay.
            //"OverlapCircleAll" crea un círculo desde el objeto que le digas y del radio que le digas.
            //Funciona así: OverlapCircleAll("centro","radio","lo que quieras detectar"). Funciona con físicas.
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Daño hacia los enemigos.

            //Para cada (foreach) enemigo (Collider2D enemy) en (in) el grupo de enemigos (hitEnemies).
            foreach (Collider2D enemy in hitEnemies) 
            {

                _enemyMovement = enemy.GetComponent<EnemyMovement>();
                //El contador comienza cada vez que le pegas un cate al enemigo.
                _enemyMovement.KnockbackCounter = _enemyMovement.KnockbackTotalTime;

                //Si el jugador está a la izquierda...
                if (enemy.transform.position.x <= transform.position.x) 
                { 
                    _enemyMovement.KnockFromRight = true;
                }

                //Si el jugador está a la derecha...
                if (enemy.transform.position.x >= transform.position.x) 
                {
                    _enemyMovement.KnockFromRight = false;
                }

                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
        }
    }

    //Esto dibuja una esfera que indica el radio del ataque. Estos dibuos se llaman "Gizmos".
    private void OnDrawGizmosSelected() 
    {
        //Esto es por si el "attackPoint" no está asignado (para que no haya errores en la consola).
        if (attackPoint != null)    
        {
            //Esto hace que se dibuje.
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);   
        }
    }
}