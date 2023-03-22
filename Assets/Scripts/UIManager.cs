using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Parameters
    [SerializeField] private TMP_Text _timetext;
    [SerializeField] private Image _regular;        //La imagen del arma de fuego regular.
    [SerializeField] private Image _fire;
    [SerializeField] private Image _ice;
    float _currentTime;
    public Image _slider;                          //La Barra de vida
    int _currentWeapon;                             //Un int que determina qu� arma estamos usando ahora.
    #endregion

    #region References
    public static UIManager instance;
    private MightyLifeComponent _mightyLifeComponent;
    #endregion
    #region Methods
    void UpdateTimer(float Currenttime)
    //Redondea el n�mero para sacar los minutos y segundos
    //Coge el archivo de texto del timer para cambiarlo.
    {
        if (Currenttime > 0 && _mightyLifeComponent.GetHealth() > 0)
        {
            float minutes = Mathf.FloorToInt(Currenttime / 60);
            float seconds = Mathf.FloorToInt(Currenttime % 60);
            _timetext.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        if (Currenttime<0)
        {
            _timetext.text = "ERROR";
            _timetext.color = new Color(1, 0, 0, 1);
        }
    }



    public void ActualizarInterfaz(float health)
    {
       _slider.fillAmount = health / 100;
    }

    private void currentWeaponState( int weapon)
    //Determina cu�l es la arma actual que muestra en la UI
    //Comprueba estado (arma) actual
    {
        switch (weapon)
        {
            case 0:
                _regular.gameObject.SetActive(true);
                _ice.gameObject.SetActive(false);
                _fire.gameObject.SetActive(false);
                break;
            case 1:
                _regular.gameObject.SetActive(false);
                _ice.gameObject.SetActive(true);
                _fire.gameObject.SetActive(false);
                break;
            case 2:
                _regular.gameObject.SetActive(false);
                _ice.gameObject.SetActive(false);
                _fire.gameObject.SetActive(true);
                break;
        }
    }
    #endregion

    private void Start()
    {
        GameManager.instance.RegisterUIMManager(this);
        _mightyLifeComponent = GameManager.instance._player.GetComponent<MightyLifeComponent>();
        _currentTime = GameManager.instance._currentTime;
        /*_maxHealth = GameManager.instance._MaxHealth;  
        SetMaxHealth(_maxHealth);*/
        //_health = GameManager.instance._MaxHealth;
    }
    private void Update()
    {
        _mightyLifeComponent.SetHealth(_mightyLifeComponent.GetHealth());
        _currentTime = GameManager.instance._currentTime;
        _currentWeapon = GameManager.instance._currentWeapon;
        UpdateTimer(_currentTime);
    }
}
