using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _timetext;
    [SerializeField] private Image _regular; //La imagen del arma de fuego regular.
    [SerializeField] private Image _fire;
    [SerializeField] private Image _ice;

    float _currentTime;
    float _health; //La cantidad de vida del jugador.
    float _maxHealth; //La vida m�xima que puede tener el jugador.
    public Slider _slider; //La Barra de vida
    int _currentWeapon; //Un int que determina qu� arma estamos usando ahora.
    
   void UpdateTimer ( float Currenttime)
    {
        float minutes = Mathf.FloorToInt(Currenttime / 60); //Redondea el n�mero para sacar los minutos.
        float seconds = Mathf.FloorToInt(Currenttime % 60); //Redondea el n�mero para sacar los segundos.

        _timetext.text = string.Format("{0:0}:{1:00}", minutes, seconds); //Coge el archivo de texto del timer para camiarlo.
    }

    public void SetMaxHealth (float health) //La m�xima cantdad de vida que tendremos
    {
        _slider.maxValue = health; 
        _slider.value = health;
    }

    public void SetHealth(float health) //El valor actual de nuestra vida
    {
        _slider.value = health;
    }

    private void currentWeaponState( int weapon) //M�todo que determina cu�l es la arma actual que muestra en la UI
    {
        switch (weapon)   //Se comprueba cu�l es nuestro estado actual (nuestra arma actual) a trav�s de un switch.
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
    void Start()
    {
        _maxHealth = GameManager.instance._MaxHealth;  
        SetMaxHealth(_maxHealth);
    }
    void Update()
    {
        _health = GameManager.instance._PlayerHealth; //En cada frame se comprobar� la vida del jugador.
        _currentTime = GameManager.instance._currentTime;
        _currentWeapon = GameManager.instance._currentWeapon; 
        UpdateTimer(_currentTime);
        SetHealth(_health);
        currentWeaponState(_currentWeapon);
        
       
    }

    
}
