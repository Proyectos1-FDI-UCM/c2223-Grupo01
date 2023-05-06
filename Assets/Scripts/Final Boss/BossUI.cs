using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BossUI : MonoBehaviour
{
    [SerializeField] private Image[] _slider;
    [SerializeField] private RawImage[] _contenedores;
    [SerializeField] private GameObject[] _hands;
    [SerializeField] private GameObject _cabeza;
    //private HeadHealth _headHealth;
    //private HandsLive[] _handsLive;

    #region Methods
    public void ActualizarInterfazManos()
    {
        if (_hands[0].GetComponent<HandsLive>() != null)
        {
            _slider[0].fillAmount = _hands[0].GetComponent<HandsLive>().GetVidaManos() / _hands[0].GetComponent<HandsLive>().GetVidaManosInicial();
        }
        else
        {
            _slider[0].fillAmount = 0.0f;
        }
        
        if (_hands[1].GetComponent<HandsLive>() != null)
        {
            _slider[1].fillAmount = _hands[1].GetComponent<HandsLive>().GetVidaManos() / _hands[1].GetComponent<HandsLive>().GetVidaManosInicial();
        }
        else
        {
            _slider[1].fillAmount = 0.0f;
        }
    }

    public void ActualizarUI()
    {
        _slider[0].gameObject.SetActive(false);
        _slider[1].gameObject.SetActive(false);
        _slider[2].gameObject.SetActive(true);
        _contenedores[0].gameObject.SetActive(false);
        _contenedores[1].gameObject.SetActive(true);
    }

    public void ActualizaVidaCabeza()
    {
        _slider[2].fillAmount = _cabeza.GetComponent<HeadHealth>().GetVidaCabeza() / _cabeza.GetComponent<HeadHealth>().GetVidaCabezaInicial();
    }
    #endregion
    /*private void Start()
    {
        if (_hands[0].GetComponent<HandsLive>() != null)
        {
            _handsLive[0] = _hands[0].GetComponent<HandsLive>();
        }
        if (_hands[1].GetComponent<HandsLive>() != null)
        {
            _handsLive[1] = _hands[1].GetComponent<HandsLive>();
        }
        _headHealth = _cabeza.GetComponent<HeadHealth>();
    }*/

    private void Update()
    {
        ActualizarInterfazManos();
        ActualizaVidaCabeza();
    }
}
