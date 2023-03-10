using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MightyLifeComponent : MonoBehaviour
{
    #region Parameters
    //[SerializeField] 
    public float _health; //La cantidad de vida del jugador.
    [SerializeField] private float _coolDown;

    [SerializeField] private float _timerInputFalseAfterHit; //Diferencia de tiempo en la que se configurará cuando volver a poder usar el input tras el hit. El mayor valor del timer es 0.

    [SerializeField] private float _canRepeatLevelTimer; //Contador que al llegar a 0 reinicia el nivel. Se activa al morir
    public float _initialCoolDown { get; private set; }
    public bool _canBeDamaged { get; private set; }

    private bool _death;

    [SerializeField] private AudioClip _hurt;
    [SerializeField] private AudioClip _deathSFX;

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

    public void OnPlayerHit(float damage)
    //Cuando se haga hit, da�a al player
    {
        _animator.SetTrigger("_damaged");
        GetComponent<AudioSource>().PlayOneShot(_hurt);


        _canBeDamaged = false;
        TakeDamage(damage);
    }

    public void DeathTime(float damage)
    //Cuando se acabe el tiempo, da�a al player
    {
        _animator.SetTrigger("_timeOut");
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        if(GameManager.instance._UImanager != null){
            GameManager.instance._UImanager.ActualizarInterfaz(_health);
        }

        if (_health <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(_deathSFX);
            _death = true;
            _myInputComponent.enabled = false;
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
            TakeDamage(_health);
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
            //_renderC.material.color = _colores[0]; //Se vuelve transparente el sprite durante un tiempo
            _coolDown -= Time.deltaTime;
            _renderC.material.color = _colores[0]; //Se vuelve transparente el sprite durante un tiempo
            if (_coolDown > _timerInputFalseAfterHit)
            {
                _myInputComponent.enabled = false;
            }
            else _myInputComponent.enabled = true;

            if (_coolDown <= 0)
                _canBeDamaged = true;
        }
        else
        {
            _renderC.material.color = _colores[1]; //Recupera su color original tras el cool down
            _coolDown = _initialCoolDown;
        }

        if (_death)
        {
            _canRepeatLevelTimer -= Time.deltaTime;
            if (_canRepeatLevelTimer <= 0) SceneManager.LoadScene(1);
        }
    }
}
