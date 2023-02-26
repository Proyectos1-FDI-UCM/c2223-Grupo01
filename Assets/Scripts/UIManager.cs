using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _timetext;
    float _currentTime;
    float _health; //La cantidad de vida del jugador.
    float _maxHealth;
    public Slider _slider;
    
   void UpdateTimer ( float Currenttime)
    {
        float minutes = Mathf.FloorToInt(Currenttime / 60); //Redondea el número para sacar los minutos.
        float seconds = Mathf.FloorToInt(Currenttime % 60); //Redondea el número para sacar los segundos.

        _timetext.text = string.Format("{0:00}:{1:00}", minutes, seconds); //Coge el archivo de texto del timer para camiarlo.
    }

    public void SetMaxHealth (float health) //La máxima cantdad de vida que tendremos
    {
        _slider.maxValue = health; 
        _slider.value = health;
    }

    public void SetHealth(float health) //El valor actual de nuestra vida
    {
        _slider.value = health;
    }
    void Start()
    {
        _maxHealth = GameManager.instance._MaxHealth;
        SetMaxHealth(_maxHealth);
    }
    void Update()
    {
        _health = GameManager.instance._PlayerHealth; //En cada frame se comprobará la vida del jugador.
        _currentTime = GameManager.instance._currentTime;
        UpdateTimer(_currentTime);
        SetHealth(_health);
        
       
    }

    
}
