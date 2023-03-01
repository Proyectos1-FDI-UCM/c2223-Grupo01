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
    float _health;                                  //La cantidad de vida del jugador.
    float _maxHealth;                               //La vida máxima que puede tener el jugador.
    public Slider _slider;                          //La Barra de vida
    int _currentWeapon;                             //Un int que determina qué arma estamos usando ahora.
    #endregion

    #region Methods
    void UpdateTimer(float Currenttime)
    //Redondea el número para sacar los minutos y segundos
    //Coge el archivo de texto del timer para cambiarlo.
    {
        float minutes = Mathf.FloorToInt(Currenttime / 60);
        float seconds = Mathf.FloorToInt(Currenttime % 60);
        _timetext.text = string.Format("{0:0}:{1:00}", minutes, seconds);   
    }

    public void SetMaxHealth (float health)
    //La máxima cantdad de vida que tendremos
    {
        _slider.maxValue = health; 
        _slider.value = health;
    }

    public void SetHealth(float health)
    //El valor actual de nuestra vida
    {
        _slider.value = health;
    }

    private void currentWeaponState( int weapon)
    //Determina cuál es la arma actual que muestra en la UI
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

    void Start()
    {
        _maxHealth = GameManager.instance._MaxHealth;  
        SetMaxHealth(_maxHealth);
    }
    void Update()
    {
        _health = GameManager.instance._PlayerHealth;
        _currentTime = GameManager.instance._currentTime;
        _currentWeapon = GameManager.instance._currentWeapon;
        UpdateTimer(_currentTime);
        SetHealth(_health);
        currentWeaponState(_currentWeapon);
    }
}
