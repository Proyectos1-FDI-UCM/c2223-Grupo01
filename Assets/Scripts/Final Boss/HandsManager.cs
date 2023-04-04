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
    [Header("Barrido")]
    [SerializeField] private Transform[] _sweepPositions; //_leftDown, _leftTop, _rightDown, _rightUp
    [Header("Caida")]
    public Transform[] _upPositions;
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
                    foreach(GameObject _hand in _hands)
                    {
                        
                    }
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currenState = HandsStates.Patrullaje;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(_currenState);
    }
}