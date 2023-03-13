using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComponent : MonoBehaviour
{
    #region Parameters & References
    [SerializeField] private Transform attackPoint;     //El punto que checkea si le ha dao a un enemigo.
    [SerializeField] private LayerMask enemyLayers;     //Para referirse a dónde están los enemigos (para pegarles). WIP: Recuerda hacer la layer de los enemigos.
    [SerializeField] private float attackRange = 0.5f;  //El rango con el que ataca (radio).
    [SerializeField] private int attackDamage = 40;     //Daño por golpe.
    [SerializeField] private float attackRate = 2f;     //Indica cuántas veces se va a atacar por segundo.
    private float nextAttackTime = 0f;                  //Cooldown del ataque.
    private EnemyMovement _enemyMovement = null;        //Sirve para comunicarse con el script de "EnemyMovement".
    [SerializeField] private float _coolDownMelee;      //tiempo en el que se permitirá usar el arma a melee
    private float _initialCoolDownMelee;
    public bool _canAttackMelee { get; private set; }   //condición en la que se permitirá usar el arma a melee
    #endregion

    #region Methods
    //WIP: Falta hacer que al darle a la tecla de atacar, haga aleatoriamente (una u otra) las animaciones "Attack 1" y "Attack 2". Por el momento sólo he puesto "Attack 1".
    public void Attack()   
    {
        if (Time.time >= nextAttackTime) 
        {
            nextAttackTime = Time.time + 1f / attackRate;    


            //Detectar a los enemigos en el rango de ataque (el radio del golpe).

            //"hitEnemies" es un array de los enemigos que hay.
            //"OverlapCircleAll" crea un círculo desde el objeto que le digas y del radio que le digas.
            //Funciona así: OverlapCircleAll("centro","radio","lo que quieras detectar"). Funciona con físicas.
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Daño hacia los enemigos.

            foreach (Collider2D enemy in hitEnemies)
            //El contador comienza cada vez que le pegas un cate al enemigo.
            //Comprobamos si el jugador esta a la izda o dcha
            {
                if(enemy.GetComponent<EnemyMovement>() != null)
                {
                    _enemyMovement = enemy.GetComponent<EnemyMovement>();
                    _enemyMovement.KnockbackCounter = _enemyMovement.KnockbackTotalTime;

                    if (enemy.transform.position.x <= transform.position.x)
                    {
                        _enemyMovement.KnockFromRight = true;
                    }
                    if (enemy.transform.position.x >= transform.position.x)
                    {
                        _enemyMovement.KnockFromRight = false;
                    }
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                }
            }
            _canAttackMelee = false;
        }
    }

    private void OnDrawGizmosSelected()
    //Esto dibuja una esfera que indica el radio del ataque.
    //Estos dibujos se llaman "Gizmos".
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
    #endregion

    private void Start()
    {
        _initialCoolDownMelee = _coolDownMelee;
        _canAttackMelee = true;
    }

    private void Update()
    {
        //Cooldown programado para poder usar el arma a melee o no
        if (!_canAttackMelee)
        {
            _coolDownMelee -= Time.deltaTime;

            if (_coolDownMelee <= 0)
                _canAttackMelee = true;
        }
        else _coolDownMelee = _initialCoolDownMelee;
    }
}