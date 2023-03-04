using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MightyLifeComponent : MonoBehaviour
{
    #region Parameters
    //[SerializeField] 
    public float _health; //La cantidad de vida del jugador.
    [SerializeField] private float _damage;
    #endregion

    #region References
    public static MightyLifeComponent instance;
    //private UIManager _myUIManager; //Referencia al UI Manager
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //_health = GameManager.instance._health;
        //_myUIManager = GetComponent<UIManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    // Colisiones del jugador con los enemigos
    {
        if (collision.gameObject.GetComponent<EnemyHealth>() != null)
        {
            
            OnPlayerHit(_damage);
        }
    }

    public void OnPlayerHit(float damage)
    //Cuando se haga hit, daña al player
    {
        _health = _health - damage;
        UIManager.instance.ActualizarInterfaz(_health);
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void Awake()
    //Inicializo el Singleton
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
