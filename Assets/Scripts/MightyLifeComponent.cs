using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MightyLifeComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField] private float _health = 100; //La cantidad de vida del jugador.
    [SerializeField] private float _maxhealth = 100; // vida máxima
    [SerializeField] private float _coolDown;
    [SerializeField] private float _fireDamage;

    [SerializeField] private float _timerInputFalseAfterHit; //Diferencia de tiempo en la que se configurará cuando volver a poder usar el input tras el hit. El mayor valor del timer es 0.

    [SerializeField] private float _canRepeatLevelTimer; //Contador que al llegar a 0 reinicia el nivel. Se activa al morir
    public float _initialCoolDown { get; private set; }
    public bool _canBeDamaged { get; private set; }

    private bool _death;

    [SerializeField] private AudioClip _hurt;
    [SerializeField] private AudioClip _deathSFX;

    [SerializeField] private AudioClip _cureSFX;

    #endregion

    #region References
    private Animator _animator;
    private InputComponent _myInputComponent;

    [SerializeField] private Rigidbody2D _myRigidBody2D;
    [SerializeField] private BoxCollider2D _boxColiderNormal;

    [SerializeField]
    private Color[] _colores;   //Array de colores del player

    [SerializeField]
    private Renderer _renderC; //Renderiza el color del player
    #endregion

    #region getters y setters
    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxHealth()
    {
        return _maxhealth;
    }

    public void SetMaxHealth(float MaxHealth)
    {
        _maxhealth = MaxHealth;
    }
    #endregion

    public void OnPlayerHit(float damage)
    //Cuando se haga hit, herir al player
    {
        _animator.SetTrigger("_damaged");
        GetComponent<AudioSource>().PlayOneShot(_hurt);

        _canBeDamaged = false;
        TakeDamage(damage);
    }

    public void DeathTime(float damage)
    //Cuando se acabe el tiempo, herir al player
    {
        _animator.SetTrigger("_timeOut");
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(GameManager.instance._UImanager != null)
        {
            GameManager.instance._UImanager.ActualizarInterfaz(GetHealth());
        }

        if (_health <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(_deathSFX);
            _death = true;
            _boxColiderNormal.enabled = false;
            _myRigidBody2D.bodyType = RigidbodyType2D.Static;
            //Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            _animator.SetTrigger("_damaged");
            GetComponent<AudioSource>().PlayOneShot(_hurt);
            TakeDamage(GetHealth());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 21)
        {
            GetComponent<AudioSource>().PlayOneShot(_cureSFX);
        }

        if (collision.gameObject.layer == 22 && _canBeDamaged)
        {
            OnPlayerHit(_fireDamage);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _death = false;
        _boxColiderNormal = GetComponent<BoxCollider2D>();
        _myRigidBody2D = GetComponent<Rigidbody2D>();
    
        _animator = GetComponent<Animator>();
        _myInputComponent = GetComponent<InputComponent>();
        GameManager.instance.RegisterMightyComponent(this);
        _initialCoolDown = _coolDown;
        _canBeDamaged = true;
    }

    // Update is called once per frame
    private void Update()
    {
        _animator.SetBool("_isDead", _death);

        if (!_canBeDamaged)
        {
            if(_coolDown <= 0)
            {
                _coolDown = _initialCoolDown;
            }
            //_renderC.material.color = _colores[0]; //Se vuelve transparente el sprite durante un tiempo
            _coolDown -= Time.deltaTime;
            _renderC.material.color = _colores[0]; //Se vuelve transparente el sprite durante un tiempo
            if (_coolDown > _timerInputFalseAfterHit)
            {
                _myInputComponent.enabled = false;
            }
            else _myInputComponent.enabled = true;

            if (_coolDown <= 0) _canBeDamaged = true;
        }
        else
        {
            _renderC.material.color = _colores[1]; //Recupera su color original tras el cool down
            //_coolDown = _initialCoolDown;
        }

        if (_death)
        {
            _myInputComponent.enabled = false;
            _canRepeatLevelTimer -= Time.deltaTime;
            if (_canRepeatLevelTimer <= 0) SceneManager.LoadScene(1); //no
            //respawn 
        }
    }
}
