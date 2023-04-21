using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR;

public class HandsManager : MonoBehaviour
{
    #region Parameters & References
    //transforms de barrido
    [Header("Manos")]
    [SerializeField] private GameObject[] _hands;
    [SerializeField] private LayerMask _layerManos;
    [SerializeField] private Transform[] _initialTransforms;
    [Header("Intermedio")]
    [SerializeField] private Transform[] _enemySpawns;
    [SerializeField] private GameObject[] _enemys;
    [Header("Barrido")]
    [SerializeField] private Transform[] _sweepPositions; //_leftDown, _leftTop, _rightDown, _rightUp
    [SerializeField] private Transform[] _upPositions;
    [Header("Caida")]
    [SerializeField] private LayerMask _layerPlayer;
    [SerializeField] private LayerMask _layerSuelo;
    private int _tipoDeCaida;
    [SerializeField] private int minCaida, maxCaida;
    private int _vecesPasado, _tocaCaer;
    private bool _caido;
    [SerializeField] private float _caidaSpeed;
    //velocidad yrotación 
    [SerializeField] private float _enemySpeed = 5f;
    //estados de las manos 
    private enum HandsStates {Patrullaje, Transición, Barrido,volviendo}; 
    private HandsStates _currentState, _nextState; //estado actual
    [Header("Estado")]
    [SerializeField] private float _cambioDeEstado = 40; // tiempo que se tarda en cambiar de estado
    private float _cambioDeEstadoInicial;
    #endregion

    #region Methods

    private void EnterState(HandsStates currenState)
    {
        switch (currenState)
        {
            case HandsStates.Patrullaje:
                {
                    foreach(GameObject _hand in _hands)
                    {
                        _hand.GetComponent<Collider2D>().isTrigger = true;
                    }
                    break;
                }
            case HandsStates.Barrido:
                {
                    foreach (GameObject _hand in _hands)
                    {
                        _hand.GetComponent<Collider2D>().isTrigger = false;
                    }
                    break;
                }
        }
    }
    private void UpdateState(HandsStates currenState)
    {
        switch (currenState)
        {
            case HandsStates.Patrullaje:
                {
                    PatrullajeUpdate();
                    break;
                }
            case HandsStates.Transición:
                {
                    TransicionUpdate();
                    break;
                }
            case HandsStates.Barrido:
                {
                    break;
                }
            case HandsStates.volviendo:
                {
                    VolviendoUpdate();
                    break;
                }
        }
    }

    private void TemporalChangeState()
    {
        _cambioDeEstado -= Time.deltaTime;
        if (_cambioDeEstado <= 0 && !_caido)
        {
            if (_currentState == HandsStates.Barrido)
            {
                _nextState = HandsStates.Patrullaje;
                _currentState = HandsStates.Transición;
            }
            else
            {
                _nextState = HandsStates.Barrido;
                _currentState = HandsStates.Transición;
            }
        }
    }
    #region Patrullaje
    private void PatrullajeUpdate()
    {
        foreach (GameObject _hand in _hands)
        {
            if (_enemySpeed > 0 && Physics2D.BoxCast(_hand.GetComponent<Collider2D>().bounds.center,
                _hand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.right, .01f, _layerManos))
            {
                _enemySpeed *= -1;
                PatrullajeMovement();
                Debug.Log("derecha");
            }
            else if (_enemySpeed < 0 && Physics2D.BoxCast(_hand.GetComponent<Collider2D>().bounds.center,
                    _hand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.left, .01f, _layerManos))
            {
                _enemySpeed *= -1;
                PatrullajeMovement();
                Debug.Log("izquierda");
            }

            DetectordeCaida();
            if (!_caido)
            {
                PatrullajeMovement();
            }
            else
            {
                caida();
            }
        }
    }

    private void PatrullajeMovement()
    {
        foreach (GameObject _hand in _hands)
        {
            _hand.GetComponent<Rigidbody2D>().velocity = (Vector3.right * _enemySpeed);
        }
    }
    private void DetectordeCaida()
    {
        if (!_caido)
        {
            if (Physics2D.Raycast((_hands[0].transform.position + _hands[1].transform.position) / 2, Vector2.down, 6f, _layerPlayer))
            {
                _vecesPasado++;
            }
        }

        if (_vecesPasado >= _tocaCaer)
        {
            if (!_caido)
            {
                _tipoDeCaida = Random.Range(0, 3);
                _caido = true;
            }
        }
    }

    private void caida()
    {
        if (_caido)
        {
            _tocaCaer = Random.Range(minCaida, maxCaida);
            _vecesPasado = 0;
            MovimientoCaida();
        }
    }
    public void ChangeCaidaSpeed()
    {
            _caidaSpeed *= -1;
    }

    private void MovimientoCaida()
    {
        if (_caidaSpeed < 0 && _hands[0].transform.position.y >= 69.05f && _hands[1].transform.position.y >= 69.05f)
        {
            ChangeCaidaSpeed();
            _caido = false;
        }

        foreach (GameObject _hand in _hands)
        {
            if (_caidaSpeed > 0 && Physics2D.BoxCast(_hand.GetComponent<Collider2D>().bounds.center,
                _hand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.down, .001f, _layerSuelo))
            {
                ChangeCaidaSpeed();
            }
        }

        switch (_tipoDeCaida)
        {
            case 0:
            {
                _hands[0].GetComponent<Rigidbody2D>().velocity = (Vector3.down * _caidaSpeed);
                _hands[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            }
            case 1:
            {
                _hands[1].GetComponent<Rigidbody2D>().velocity = (Vector3.down * _caidaSpeed);
                _hands[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            }
            case 2:
            {
                foreach (GameObject hand in _hands)
                {
                    hand.GetComponent<Rigidbody2D>().velocity = (Vector3.down * _caidaSpeed);
                }
                break;
            }
        }
    }
    #endregion

    #region Transición
    private void TransicionUpdate()
    {
        TransicionMovement();
        if(_hands[0].GetComponent<Rigidbody2D>().velocity == Vector2.zero && _hands[1].GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            Instantiate(_enemys[Random.Range(0, _enemys.Length)], _hands[0].transform.position, _hands[0].transform.rotation);
            Instantiate(_enemys[Random.Range(0, _enemys.Length)], _hands[1].transform.position, _hands[1].transform.rotation);
            _currentState = HandsStates.volviendo;
        }
    }

    private void TransicionMovement()
    {

        if (Mathf.Approximately(_hands[0].transform.position.x, _enemySpawns[0].position.x) &&
                    Mathf.Approximately(_hands[0].transform.position.y, _enemySpawns[0].position.y))
        {
            _hands[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            _hands[0].GetComponent<Rigidbody2D>().velocity = (_enemySpawns[0].position - _hands[0].transform.position) * 5;
        }
        if (Mathf.Approximately(_hands[1].transform.position.x, _enemySpawns[1].position.x) &&
            Mathf.Approximately(_hands[1].transform.position.y, _enemySpawns[1].position.y))
        {
            _hands[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            _hands[1].GetComponent<Rigidbody2D>().velocity = (_enemySpawns[1].position - _hands[1].transform.position) * 5;
        }
    }
    #endregion

    #region Transición
    private void VolviendoUpdate()
    {
        VolviendoMovement();
    }

    private void VolviendoMovement()
    {
        if (Mathf.Approximately(_hands[0].transform.position.x, _initialTransforms[0].position.x) &&
            Mathf.Approximately(_hands[0].transform.position.y, _initialTransforms[0].position.y))
        {
            _hands[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
           _hands[0].GetComponent<Rigidbody2D>().velocity = (_initialTransforms[0].position - _hands[0].transform.position) * 5;
        }
        if (Mathf.Approximately(_hands[1].transform.position.x, _initialTransforms[1].position.x) &&
            Mathf.Approximately(_hands[1].transform.position.y, _initialTransforms[1].position.y))
        {
           _hands[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
           _hands[1].GetComponent<Rigidbody2D>().velocity = (_initialTransforms[1].position - _hands[1].transform.position) * 5;
        }
        if (_hands[0].GetComponent<Rigidbody2D>().velocity == Vector2.zero && _hands[1].GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            _cambioDeEstado = _cambioDeEstadoInicial;
            _currentState = HandsStates.Patrullaje;
        }
    }
    #endregion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentState = HandsStates.Patrullaje;
        _tocaCaer = Random.Range(minCaida, maxCaida);
        _vecesPasado = 0;
        _cambioDeEstadoInicial = _cambioDeEstado;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(_currentState);
        Debug.Log(_vecesPasado + "=" + _tocaCaer);
        if(_currentState != HandsStates.Transición && _currentState != HandsStates.volviendo)
        {
            TemporalChangeState();
        }
    }
}