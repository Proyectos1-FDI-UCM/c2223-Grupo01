using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MightyLifeComponent : MonoBehaviour
{
    #region Parameters
    //[SerializeField] 
    public float _health; //La cantidad de vida del jugador.
    [SerializeField] private float _coolDown;
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
    #endregion

    public void OnPlayerHit(float damage)
    //Cuando se haga hit, daña al player
    {
        _animator.SetTrigger("_damaged");
        GetComponent<AudioSource>().PlayOneShot(_hurt);


        _canBeDamaged = false;
        TakeDanage(damage);
    }

    public void DeathTime(float damage)
    //Cuando se acabe el tiempo, daña al player
    {
        _animator.SetTrigger("_timeOut");
        TakeDanage(damage);
    }

    private void TakeDanage(float damage)
    {
        _health -= damage;
        GameManager.instance._UImanager.ActualizarInterfaz(_health);

        if (_health <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(_deathSFX);
            _death = true;
            _myInputComponent.enabled = false;
            _boxColiderNormal.isTrigger = true;
            _myRigidBody2D.bodyType = RigidbodyType2D.Static;
            //Destroy(gameObject);
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
            _coolDown -= Time.deltaTime;

            if (_coolDown <= 0)
                _canBeDamaged = true;
        }
        else _coolDown = _initialCoolDown;
    }
}
