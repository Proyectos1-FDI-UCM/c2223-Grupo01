using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region parameters && references
    private Animator _animator;

    [SerializeField] private int _maxHealth = 100;     //Vida m�xima del enemigo.
    public int _currentHealth { get; private set; }                        //Vida actual del enemigo.
    [SerializeField] private float _damage;            //da�o al jugador
    private EnemyMovement _enemyMovement;
    private EnemyFlyingMovement _enemyFlyingMovement;

    [SerializeField] private float _coolDownDeathAnim;

    [SerializeField] private float _cooldownDamagedColor;

    private float _initialCooldownDamagedColor;

    [SerializeField] private AudioClip _hurt;
    [SerializeField] private AudioClip _dead;
    private int _numbalasCongelado;

    public bool _death { get; private set; }
    private bool _damagedC; //Variable que gestiona cuando se puede activar el color de dañado en el EnemyStateManager

    [SerializeField] private GameObject _spawneableObject;
    [SerializeField] private int _spawnProbability;
    #endregion

    #region getter && setter
    public int GetNumBalasCongelado()
    {
        return _numbalasCongelado;
    }
    public void SetNumBalasCongelado(int numBalas)
    {
        _numbalasCongelado = numBalas;
    }

    public bool GetDañadoC()
    {
        return _damagedC;
    }
    #endregion

    #region methods
    public void TakeDamage(int damage)
    //M�todo para que el enemigo reciba da�o.
    //Cuando la vida quede a 0 o menos, el enemigo muere.
    {
        _currentHealth -= damage;
        _damagedC = true;

        //GetComponent<AudioSource>().PlayOneShot(_hurt);
        //Animaci�n de recibir da�o.

        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die()
    //M�todo para que muera el enemigo.
    //Desactiva el collider del enemigo al morir.
    //Desactiva el EnemyComponent.
    {
        _death = true;
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<AudioSource>().PlayOneShot(_dead);


        // si no va a pie vuela, no hace falta comprobar ambos
        if (_enemyMovement != null && _enemyFlyingMovement == null)
        {
            _enemyMovement.enabled = false;
        }
        else if (_enemyMovement == null && _enemyFlyingMovement != null)
        {
            _enemyFlyingMovement.enabled = false;
        }
    }

    private void Spawn()
    {
        if (Random.RandomRange(0,100) <= _spawnProbability)
        {
            Instantiate(_spawneableObject, gameObject.transform.position, Quaternion.Euler(0, 0, 0));

        }
    }
    #endregion

    #region collisions methods
    private void OnCollisionStay2D(Collision2D collision)
    // Colisiones del jugador con los enemigos
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged && !GetComponent<EnemyStateManager>().GetCongelado())
        {
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
        }
        if (collision.gameObject.layer == 10)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletCollisionComponent>() != null && collision.gameObject.GetComponent<BulletCollisionComponent>()._normalBulletValue != 0)
        {
            GetComponent<AudioSource>().PlayOneShot(_hurt);
        }
    }
    #endregion

    private void Start()
    {
        //Al comienzo, el enemigo comienza con m�xima vida.
        _currentHealth = _maxHealth;  
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyFlyingMovement = GetComponent <EnemyFlyingMovement>();
        _death = false;
        _damagedC = false;
        _animator = GetComponent<Animator>();
        _numbalasCongelado = 0;
        _initialCooldownDamagedColor = _cooldownDamagedColor;
    }

    private void Update()
    {
        //Debug.Log(_currentHealth);
        _animator.SetBool("_death", _death);

        if (_damagedC)
        {
            _cooldownDamagedColor -= Time.deltaTime;
            if (_cooldownDamagedColor <= 0)
            {
                _damagedC = false;
            }
        }
        else
        {
            _cooldownDamagedColor = _initialCooldownDamagedColor;
        }
        
        if (_death)
        {
            _coolDownDeathAnim -= Time.deltaTime;
            
            if (_coolDownDeathAnim <= 0)
            {
                Spawn();
                Destroy(gameObject);
            }
        }
    }
}