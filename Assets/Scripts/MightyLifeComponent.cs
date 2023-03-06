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
    [SerializeField] private AudioSource _timeOut;

    #endregion

    #region References
    private Animator _animator;
    private InputComponent _myInputComponent;
    #endregion

    public void OnPlayerHit(float damage)
    //Cuando se haga hit, da�a al player
    {
        
        GetComponent<AudioSource>().PlayOneShot(_hurt);


        _canBeDamaged = false;
        _health -= damage;
        GameManager.instance._UImanager.ActualizarInterfaz(_health);
        
        if (_health <= 0)
        {
            _death = true;
            _myInputComponent.enabled = false;
            //Destroy(gameObject);
        }
        else
        {
            _animator.SetTrigger("_damaged");
        }
    }

    public void DeathTime(float damage)
    //Cuando se acabe el tiempo, da�a al player
    {
        _health -= damage;
        GameManager.instance._UImanager.ActualizarInterfaz(_health);
        if (_health <= 0)
        {
            _death = true;
            //Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _death = false;
        _timeOut = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _myInputComponent = GetComponent<InputComponent>();
        GameManager.instance.RegisterMightyComponent(this);
        _initialCoolDown = _coolDown;
        _canBeDamaged = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_canBeDamaged)
        {
            _coolDown -= Time.deltaTime;

            if (_coolDown <= 0)
                _canBeDamaged = true;
        }
        else _coolDown = _initialCoolDown;

        if (GameManager.instance._currentTime == 0)
        {
            _timeOut.Play();
        }
        _animator.SetBool("_isDead", _death);

        }
    }
}
