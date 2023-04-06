using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    #region Parameters & References
    //transforms de barrido
    [Header("Manos")]
    [SerializeField] private GameObject[] _hands;
    [SerializeField] private LayerMask _layerManos;
    [SerializeField] private float _turCoolDown;
    private float _turCoolDownInicial;
    private bool _canturn;
    [Header("Barrido")]
    [SerializeField] private Transform[] _sweepPositions; //_leftDown, _leftTop, _rightDown, _rightUp
    [Header("Caida")]
    public Transform[] _upPositions;
    //velocidad yrotación 
    private bool _isflipped; //Si el enemigo ha dado la vuelta
    [SerializeField] private float _enemySpeed = 5f;
    //estados de las manos 
    private enum HandsStates {Patrullaje, Transición, Barrido }; 
    private HandsStates _currenState; //estado actual
    #endregion

    #region Methods
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

    private void PatrullajeUpdate()
    {
        foreach (GameObject _hand in _hands)
        {
            if (!_isflipped)
            {
                _hand.GetComponent<Rigidbody2D>().velocity = (Vector3.right * _enemySpeed);
            }
            else
            {
                _hand.GetComponent<Rigidbody2D>().velocity = (Vector3.left * _enemySpeed);
            }
            
            if((Physics2D.BoxCast(_hand.GetComponent<Collider2D>().bounds.center, 
                _hand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.right, .001f, _layerManos) ||
                Physics2D.BoxCast(_hand.GetComponent<Collider2D>().bounds.center,
                _hand.GetComponent<Collider2D>().bounds.size, 0f, Vector2.left, .001f, _layerManos)) && _canturn)
            {
                _isflipped = !_isflipped;
                _canturn = false;
            }

            if (!_canturn)
            {
                _turCoolDown -= Time.deltaTime;
                if(_turCoolDown <= 0)
                {
                    _canturn = true;
                    _turCoolDown = _turCoolDownInicial;
                }
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currenState = HandsStates.Patrullaje;
        _canturn = true;
        _turCoolDownInicial = _turCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(_currenState);
    }
}