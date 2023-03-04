using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MightyLifeComponent : MonoBehaviour
{
    #region Parameters
    //[SerializeField] 
    public float _health; //La cantidad de vida del jugador.
    [SerializeField] private float _coolDown;
    public float _initialCoolDown { get; private set; }
    public bool _canAttack { get; private set; }
    #endregion

    #region References
    //private UIManager _myUIManager; //Referencia al UI Manager
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _initialCoolDown = _coolDown;
    }


    public void OnPlayerHit(float damage)
    //Cuando se haga hit, daña al player
    {
        _canAttack = false;
        _health -= damage;
        UIManager.instance.ActualizarInterfaz(_health);
        
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_canAttack);
        if (!_canAttack)
        {
            _coolDown -= Time.deltaTime;

            if (_coolDown <= 0)
                _canAttack = true;
        }
        else _coolDown = _initialCoolDown;
    }
}
