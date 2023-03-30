using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaLlamasShooting : MonoBehaviour
{
    //El comportamiento del lanzallamas es semejante al del enemigo tirador, pero con menos condiciones. No puede disparar cuando vea al jugador, es de manera aautomática, por lo cual solo se usarán contadores para instanciar las llamas

    #region Parameters
    [SerializeField] private float _coolDownShootFalse; //Contador para poder volver a hacer daño

    [SerializeField] private float _coolDownShootTrue; //Contador para ver cuanto tiempo puede hacer daño

    private float _initialCoolDownShootFalse; //Valor inicial del contador de daño en FALSE, util para reconfigurar el cooldown que se modifica en el Update
    private float _initialCoolDownShootTrue; //Valor inicial del contador de daño en TRUE, util para reconfigurar el cooldown que se modifica en el Update

    public bool _canShootFire { get; private set; } //Booleano que determina si puede disparar o no
    #endregion

    #region References
    [SerializeField] private AudioClip _llamaSFX;
    private SpriteRenderer _mySpriteRenderer;
    #endregion

    private void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _initialCoolDownShootFalse = _coolDownShootFalse;
        _initialCoolDownShootTrue = _coolDownShootTrue;
        _canShootFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!_canShootFire)
        {
            _mySpriteRenderer.enabled = false;
            _coolDownShootTrue = _initialCoolDownShootTrue;
            _coolDownShootFalse -= Time.deltaTime;
            if (_coolDownShootFalse <= 0)
            {
                _canShootFire = true;
            }
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(_llamaSFX);
            _mySpriteRenderer.enabled = true;
            _coolDownShootFalse = _initialCoolDownShootFalse;
            _coolDownShootTrue -= Time.deltaTime;
            if (_coolDownShootTrue <= 0)
            {
                _canShootFire = false;
            }

        }
    }
}