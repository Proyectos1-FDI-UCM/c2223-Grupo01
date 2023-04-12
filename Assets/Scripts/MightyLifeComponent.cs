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

    //Hay dos contadores de invisible o no puesto que con uno solo con valores tan bajos como 0.05 al parpadear se queda en invisible todo el rato, por lo que es mejor dejarlo con 2 para cada estado al recibir daño que es lo que funciona
    [SerializeField] private float _coolDownInvFalse; //Contador para poder volver a ser invisible
    [SerializeField] private float _coolDownInvTrue; //Contador para ver cuanto tiempo puede ser invisible

    private float _initialCoolDownInv; //Valor inicial del contador de invisible en TRUE y FALSE, util para reconfigurar el cooldown que se modifica en el Update
    public float _initialCoolDown { get; private set; }
    public bool _canBeDamaged { get; private set; }

    public bool _switchLava1Detected { get; private set; }
    public bool _switchLava2Detected { get; private set; }
    public bool _switchLava3Detected { get; private set; }

    private bool _death;

    private bool _invisible;

    [SerializeField] private AudioClip _hurt;
    [SerializeField] private AudioClip _deathSFX;

    [SerializeField] private AudioClip _cureSFX;

    #endregion

    #region References
    private Animator _animator;
    private InputComponent _myInputComponent;

    private Rigidbody2D _myRigidBody2D;
    private BoxCollider2D _boxColiderNormal;
    private CheckpointInteractionComponent _checkp;

    [SerializeField]
    private Color[] _colores;   //Array de colores del player

    [SerializeField]
    private Renderer _renderC; //Renderiza el color del player

    private SpriteRenderer _mySpriteRenderer; //Render del sprite

    #endregion

    #region getters y setters
    public float GetHealth()
    {
        return _health;
    }
    public void SetHealth(float health)
    {
        _health = health;
    }
    public float GetMaxHealth()
    {
        return _maxhealth;
    }
    public void SetMaxHealth(float MaxHealth)
    {
        _maxhealth = MaxHealth;
    }
    public bool GetDeath()
    {
        return _death;
    }
    public void SetDeath(bool dead)
    {
        _death = dead;
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
        if ((collision.gameObject.layer == 22 && _canBeDamaged && collision.gameObject.GetComponent<LanzaLlamasShooting>()._canShootFire) || collision.gameObject.layer == 23)
        {
            OnPlayerHit(_fireDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 21)
        {
            GetComponent<AudioSource>().PlayOneShot(_cureSFX);
        }

        if (other.gameObject.layer == 25)
        {
            _switchLava1Detected = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 26)
        {
            _switchLava2Detected = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 27)
        {
            _switchLava3Detected = true;
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _myInputComponent = GetComponent<InputComponent>();
        _boxColiderNormal = GetComponent<BoxCollider2D>();
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.instance.RegisterMightyComponent(this);

        _initialCoolDown = _coolDown;
        _canBeDamaged = true;

        _death = false;

        _switchLava1Detected = false;
        _switchLava2Detected = false;
        _switchLava3Detected = false;

        //Parametros para el parpadeo al recibir daño
        _invisible = false;
        _initialCoolDownInv = _coolDownInvTrue; //Da igual si lo igualamos a true o false, ambos deben tener el mismo timing
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

            //El parpadeo que tanto queria Julian basado en el contador del lanzallamas :)
            if (!_invisible)
            {
                _mySpriteRenderer.enabled = true;
                _coolDownInvTrue = _initialCoolDownInv;
                _coolDownInvFalse -= Time.deltaTime;

                if (_coolDownInvFalse <= 0) _invisible = true;
            }
            else
            {
                _mySpriteRenderer.enabled = false;
                _coolDownInvFalse = _initialCoolDownInv;
                _coolDownInvTrue -= Time.deltaTime;

                if (_coolDownInvTrue <= 0) _invisible = false;
            }


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
            _mySpriteRenderer.enabled = true;
            //_coolDown = _initialCoolDown;
        }

        if (_death)
        {
            _myInputComponent.enabled = false;
            _canRepeatLevelTimer -= Time.deltaTime;
            if (_canRepeatLevelTimer <= 0) SceneManager.LoadScene(1); //no
            //_checkp.Respawn();
        }
    }
}
