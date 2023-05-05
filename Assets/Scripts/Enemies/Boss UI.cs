using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BossUI : MonoBehaviour
{
    [SerializeField] private Image[] _slider;
    [SerializeField] private GameObject[] _hands;
    [SerializeField] private GameObject _cabeza;
    private HeadHealth _headHealth;
    private HandsLive[] _handsLive;

    #region Methods
    public void ActualizarInterfazManos()
    {
        _slider[0].fillAmount = _handsLive[0].GetVidaManos() / _handsLive[0].GetVidaManosInicial();
        _slider[1].fillAmount = _handsLive[1].GetVidaManos() / _handsLive[1].GetVidaManosInicial();
    }

    public void ActualizarUI()
    {
        _slider[0].enabled = false;
        _slider[1].enabled = false;
        _slider[2].enabled = true;
    }

    public void ActualizaVidaCabeza()
    {
        _slider[2].fillAmount = _headHealth.GetVidaCabeza() / _headHealth.GetVidaCabezaInicial();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _handsLive[0] = _hands[0].GetComponent<HandsLive>();
        _handsLive[1] = _hands[1].GetComponent<HandsLive>();
        _headHealth = _cabeza.GetComponent<HeadHealth>();
        _slider[2].enabled = false;
    }
}
