using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Parameters
    [SerializeField] private TMP_Text _timetext;
    [SerializeField] private Image[] _Weapons;        //La imagen del arma de fuego regular.
    [SerializeField] private Image _Melee;
    float _currentTime;
    [SerializeField] private Image _slider;                          //La Barra de vida
    #endregion

    #region References
    private MightyLifeComponent _mightyLifeComponent;
    private CharacterController _characterController;
    #endregion
    
    #region Methods
    public void UpdateTimer(float Currenttime)
    //Redondea el n�mero para sacar los minutos y segundos
    //Coge el archivo de texto del timer para cambiarlo.
    {
        if (Currenttime > 0 && _mightyLifeComponent.GetHealth() > 0 && !_characterController._doorTouched)
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
       _slider.fillAmount = health / GameManager.instance._player.GetComponent<MightyLifeComponent>().GetMaxHealth();
    }

    public void currentWeaponState( int weapon)
    //Determina cu�l es la arma actual que muestra en la UI
    //Comprueba estado (arma) actual
    {
        switch (weapon)
        {
            case 0:
                _Weapons[0].gameObject.SetActive(true);
                _Weapons[1].gameObject.SetActive(false);
                _Weapons[2].gameObject.SetActive(false);
                break;
            case 1:
                _Weapons[0].gameObject.SetActive(false);
                _Weapons[1].gameObject.SetActive(true);
                _Weapons[2].gameObject.SetActive(false);
                break;
            case 2:
                _Weapons[0].gameObject.SetActive(false);
                _Weapons[1].gameObject.SetActive(false);
                _Weapons[2].gameObject.SetActive(true);
                break;
        }
    }
    #endregion

    private void Start()
    {
        GameManager.instance.RegisterUIMManager(this);
        _Melee.gameObject.SetActive(GameManager.instance.HandleMeleeActivation(SceneManager.GetActiveScene().buildIndex));
        _mightyLifeComponent = GameManager.instance._player.GetComponent<MightyLifeComponent>();
        _characterController = GameManager.instance._player.GetComponent<CharacterController>();
        _currentTime = GameManager.instance._currentTime;
    }
}
