using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComponent : MonoBehaviour
{
    #region Parameters & References
    [SerializeField] private Transform attackPoint;     //El punto que checkea si le ha dao a un enemigo.
    [SerializeField] private LayerMask enemyLayers;     //Para referirse a dï¿½nde estï¿½n los enemigos (para pegarles). WIP: Recuerda hacer la layer de los enemigos.
    [SerializeField] private float attackRange = 0.5f;  //El rango con el que ataca (radio).
    [SerializeField] private int attackDamage = 40;     //Daï¿½o por golpe.
    [SerializeField] private float attackRate = 2f;     //Indica cuï¿½ntas veces se va a atacar por segundo.
    private float nextAttackTime = 0f;                  //Cooldown del ataque.
    [SerializeField] private float _coolDownMelee;      //tiempo en el que se permitirá usar el arma a melee
    private float _initialCoolDownMelee;

    // variables del kcnokbak
    [SerializeField] private float KnockbackForce;                        //Cuï¿½nta fuerza tendrï¿½ el knockback.
    [SerializeField] private float _KnockbackTime;                      //Cooldown del knockback.
    private bool KnockFromRight;
    private bool _canAttackMelee;//condiciï¿½n en la que se permitirï¿½ usar el arma a melee
    #endregion

    #region getters && setters
    public bool GetAttackMelee()
    {
        return _canAttackMelee;
    }

    public float SetKnockBackCounter()
    {
        return _KnockbackTime;
    }
    #endregion

    #region Methods
    //WIP: Falta hacer que al darle a la tecla de atacar, haga aleatoriamente (una u otra) las animaciones "Attack 1" y "Attack 2". Por el momento sï¿½lo he puesto "Attack 1".
    public void Attack()   
    {
        if (Time.time >= nextAttackTime) 
        {
            nextAttackTime = Time.time + 1f / attackRate;    


            //Detectar a los enemigos en el rango de ataque (el radio del golpe).

            //"hitEnemies" es un array de los enemigos que hay.
            //"OverlapCircleAll" crea un cï¿½rculo desde el objeto que le digas y del radio que le digas.
            //Funciona asï¿½: OverlapCircleAll("centro","radio","lo que quieras detectar"). Funciona con fï¿½sicas.
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Daï¿½o hacia los enemigos.

            foreach (Collider2D enemy in hitEnemies)
            //El contador comienza cada vez que le pegas un cate al enemigo.
            //Comprobamos si el jugador esta a la izda o dcha
            {
                if(enemy.GetComponent<HandsLive>() == null)
                {
                    if (enemy.transform.position.x <= transform.position.x)
                    {
                        KnockFromRight = true;
                    }
                    if (enemy.transform.position.x >= transform.position.x)
                    {
                        KnockFromRight = false;
                    }
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);

                    if (enemy.transform.position.x <= transform.position.x)
                    {
                        KnockFromRight = true;
                    }
                    if (enemy.transform.position.x >= transform.position.x)
                    {
                        KnockFromRight = false;
                    }
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);

                    Rigidbody2D _rigidbody = enemy.GetComponent<Rigidbody2D>();
                    //Si golpea por la derecha...
                    if (KnockFromRight)
                    {
                        //"-KnockbackForce" mueve al enemigo para atrï¿½s.
                        //Es el vector de la fuerza que pega el knockback.
                        _rigidbody.velocity = new Vector2(-KnockbackForce, 0);
                    }
                    //Si golpea por la izquierda...
                    if (!KnockFromRight)
                    {
                        //Esta vuelta manda al enemigo a la derecha.
                        _rigidbody.velocity = new Vector2(KnockbackForce, 0);
                    }


                    if (enemy.GetComponent<EnemyMovement>() != null)
                    {
                        enemy.GetComponent<EnemyMovement>().SetcknockBackCounter(_KnockbackTime);
                    }
                    else if (enemy.GetComponent<EnemyFlyingMovement>() != null)
                    {
                        enemy.GetComponent<EnemyFlyingMovement>().SetcknockBackCounter(_KnockbackTime);
                    }
                }
                else
                {
                    enemy.GetComponent<HandsLive>().TakeDamage(attackDamage);
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
        GameManager.instance.RegisterMeleeComponent(this);
    }

    private void Update()
    {
        Debug.Log(attackDamage);
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