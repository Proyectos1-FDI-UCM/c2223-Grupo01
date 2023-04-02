using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAscendente : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _speed;  // velociadad de la lava
    private GameObject _player;             // referencia al player   
    [SerializeField] private float _lavaDamage; // cuanto daño hace la lava

    //La siguiente lista de booleanos determina que en una determinada posicion alcanzada en el nivel, la lava pueda avanzar o no desde esa posicion. Al inicio no se necesita para la FASE 1

    private bool _canAdvance1;

    private bool _canAdvance2;

    #endregion

    #region References
    private Rigidbody2D _myRigidbody;
    private Transform _myTransform;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance._player;
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myTransform = GetComponent<Transform>();
        _canAdvance1 = false;
        _canAdvance2 = false;
    }

    void FixedUpdate()
    {
        //Desplazamiento lava en FASE 1
        if (_player.GetComponent<MightyLifeComponent>()._switchLava1Detected)
        {
            _myRigidbody.velocity = new Vector2(0, _speed * Time.deltaTime);//desplazamiento de la lava
        }

        //Desplazamiento lava en FASE 2
        if (_player.GetComponent<MightyLifeComponent>()._switchLava2Detected && _canAdvance1 == false) //una vez se haga el boton que lo accione, se cambiará la condición
        {
            _myTransform.localPosition = new Vector3(0.0f, 50.0f, 0.0f);
            _canAdvance1 = true;
        }
        else if (_player.GetComponent<MightyLifeComponent>()._switchLava2Detected && _canAdvance1 == true)
        {
            _myRigidbody.velocity = new Vector2(0, _speed * 3 * Time.deltaTime);//desplazamiento de la lava
        }

        //Desplazamiento lava en FASE 3
        if (_player.GetComponent<MightyLifeComponent>()._switchLava3Detected && _canAdvance2 == false) //una vez se haga el boton que lo accione, se cambiará la condición
        {
            _myTransform.localPosition = new Vector3(0.0f, 230.0f, 0.0f);
            _canAdvance2 = true;
        }
        else if (_player.GetComponent<MightyLifeComponent>()._switchLava3Detected && _canAdvance2 == true)
        {
            _myRigidbody.velocity = new Vector2(0, _speed * 3 * Time.deltaTime);//desplazamiento de la lava
        }
    }
}
