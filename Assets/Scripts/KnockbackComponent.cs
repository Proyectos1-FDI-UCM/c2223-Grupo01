using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackComponent : MonoBehaviour
{
    #region parameters
    public float KnockbackForce;                        //Cuánta fuerza tendrá el knockback.
    public float KnockbackCounter;                      //Cooldown del knockback.
    public float KnockbackTotalTime;                    //Cuánto durará el knockback.
    public bool KnockFromRight;                         //Si el golpe se recibio por la derecha
    #endregion

    #region references
    private GameObject _player;
    private Rigidbody2D _rigidbody;
    #endregion

    #region Methods

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
        _player = GameManager.instance._player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
