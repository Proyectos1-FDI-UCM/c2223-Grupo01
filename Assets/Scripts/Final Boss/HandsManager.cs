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
    private enum HandsStates {Patrullaje, Transición, Barrido }; 
    private HandsStates _currenState; //estado actual
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
                    break;
                }
            case HandsStates.Barrido:
                {
                    break;
                }
        }
    }
    #region Patrullaje
    private void PatrullajeUpdate()
    {
        foreach (GameObject _hand in _hands)
        {
            if (_enemySpeed > 0 && Physics2D.BoxCast(_hand.GetComponent<Collider2D>().bounds.center,
                _hand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.right, .001f, _layerManos))
            {
                _enemySpeed *= -1;
                PatrullajeMovement();
            }
            else if (_enemySpeed < 0 && Physics2D.BoxCast(_hand.GetComponent<Collider2D>().bounds.center,
                    _hand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.left, .0005f, _layerManos))
            {
                _enemySpeed *= -1;
                PatrullajeMovement();
            }
            if (!_caido)
            {
                PatrullajeMovement();
            }

            DetectordeCaida();

            if (_caido)
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
        if (_caidaSpeed < 0 && Mathf.Approximately(_hands[0].transform.position.y, _hands[1].transform.position.y) && Mathf.Approximately(_hands[0].transform.position.y, 69.05f))
        {
            ChangeCaidaSpeed();
            _caido = false;
        }

        foreach(GameObject _hand in _hands)
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currenState = HandsStates.Patrullaje;
        _tocaCaer = Random.Range(minCaida, maxCaida);
        _vecesPasado = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(_currenState);
        Debug.Log(_vecesPasado + "=" + _tocaCaer);
    }
}