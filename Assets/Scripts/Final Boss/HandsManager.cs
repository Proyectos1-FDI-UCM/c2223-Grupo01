using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    private int _nManos;
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

    [Header("Una sola mano")]
    [SerializeField] GameObject _onlyHand;
    private enum OneStates { Patrulla, Intermedio, Desliza, Vuelve};
    private OneStates _estadoActual;
    public enum HandsStates {Patrullaje, Transición, Barrido,Volviendo, UnaSolaMano}; 
    private HandsStates _currentState, _nextState; //estado actual
    [Header("Estado")]
    [SerializeField] private float _cambioDeEstado = 40; // tiempo que se tarda en cambiar de estado
    private float _cambioDeEstadoInicial;

    private Animator _animator;
    #endregion

    public HandsStates GetCurrentState()
    {
        return _currentState;
    }

    public bool GetCaida()
    {
        return _caido;
    }

    public int GetNManos()
    {
        return _nManos;
    }

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
            case HandsStates.Volviendo:
                {
                    VolviendoUpdate();
                    break;
                }
            case HandsStates.UnaSolaMano:
                {
                    OnlyOneHandUpdate();
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

    public void OnHandDie()
    {
        _nManos--;
        if(_nManos > 0)
        {
            foreach (GameObject _hand in _hands)
            {
                if (_hand.GetComponent<HandsLive>() != null && _hand.GetComponent<HandsLive>().GetVidaManos() > 0)
                {
                    _onlyHand = _hand;
                    _currentState = HandsStates.UnaSolaMano;
                    _estadoActual = OneStates.Patrulla;
                }
            }
        }
    }

    #region Only One Hand
    private void OnlyOneHandUpdate()
    {
        UpdateOneState(_estadoActual);
    }

    private void UpdateOneState(OneStates currenState)
    {
        switch (currenState)
        {
            case OneStates.Patrulla:
                {
                    PatrullaUpdate();
                    break;
                }
            case OneStates.Intermedio:
                {
                    break;
                }
            case OneStates.Desliza:
                {
                    break;
                }
            case OneStates.Vuelve:
                {
                    break;
                }
        } 
    }

    private void PatrullaUpdate()
    {
        if(!_caido)
        {
            DetectorCaidaMano();
        }
        else
        {
            CaidaMano();
        }
    }

    private void PatrullajeManoMovement()
    {
        _onlyHand.GetComponent<Rigidbody2D>().velocity = (Vector3.right * _enemySpeed);
    }

    private void DetectorCaidaMano()
    {  
        if (Physics2D.Raycast(_onlyHand.transform.position, Vector2.down, 6f, _layerPlayer))
        {
            _vecesPasado++;
        }

        if (_vecesPasado >= _tocaCaer)
        {
            if (!_caido)
            {
                _tocaCaer = Random.Range(minCaida, maxCaida);
                _vecesPasado = 0;
                _caido = true;
            }
        }
    }

    private void CaidaMano()
    {
        _onlyHand.GetComponent<Rigidbody2D>().velocity = (Vector3.down * _caidaSpeed);

        if (_caidaSpeed <= 0 && _caido && _onlyHand.transform.position.y >= 69.05f)
        {
            ChangeCaidaSpeed();
            PatrullajeManoMovement();
            _caido = false;
        }

        if (_caidaSpeed > 0 && Physics2D.BoxCast(_onlyHand.GetComponent<Collider2D>().bounds.center,
                _onlyHand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.down, .001f, _layerSuelo))
        {
            ChangeCaidaSpeed();
        }
    }

    public void ChangeSpeed()
    {
        _enemySpeed *= -1;
        _onlyHand.GetComponent<Rigidbody2D>().velocity = (Vector3.right * _enemySpeed);
    }
    #endregion

    #region Patrullaje
    private void PatrullajeUpdate()
    {
        if (_enemySpeed > 0 && Physics2D.BoxCast(_hands[1].GetComponent<Collider2D>().bounds.center,
                _hands[1].GetComponent<Collider2D>().bounds.size, 0f, Vector2.right, .01f, _layerManos))
        {
            _enemySpeed *= -1;
            PatrullajeMovement();
        }
        else if (_enemySpeed < 0 && Physics2D.BoxCast(_hands[0].GetComponent<Collider2D>().bounds.center,
                _hands[0].GetComponent<Collider2D>().bounds.size, 0f, Vector2.left, .01f, _layerManos))
        {
            _enemySpeed *= -1;
            PatrullajeMovement();
        }

        foreach (GameObject _hand in _hands)
        {
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
        _animator.SetTrigger("Puño");
        TransicionMovement();
        if(_hands[0].GetComponent<Rigidbody2D>().velocity == Vector2.zero && _hands[1].GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            _animator.SetTrigger("AbrirMano");
            Instantiate(_enemys[Random.Range(0, _enemys.Length)], _hands[0].transform.position, _hands[0].transform.rotation);
            Instantiate(_enemys[Random.Range(0, _enemys.Length)], _hands[1].transform.position, _hands[1].transform.rotation);
            _currentState = HandsStates.Volviendo;
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

    #region Volviendo
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
        _animator = GetComponent<Animator>();
        _currentState = HandsStates.Patrullaje;
        _tocaCaer = Random.Range(minCaida, maxCaida);
        _vecesPasado = 0;
        _cambioDeEstadoInicial = _cambioDeEstado;
        _nManos = 2;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(_currentState);
        if(_currentState != HandsStates.Transición && _currentState != HandsStates.Volviendo && _currentState != HandsStates.UnaSolaMano)
        {
            TemporalChangeState();
        }
    }
}