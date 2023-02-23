using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _timetext;
    float _currentTime;
    
   void UpdateTimer ( float Currenttime)
    {
        float minutes = Mathf.FloorToInt(Currenttime / 60); //Redondea el número para sacar los minutos.
        float seconds = Mathf.FloorToInt(Currenttime % 60); //Redondea el número para sacar los segundos.

        _timetext.text = string.Format("{0:00} : {1:00}", minutes, seconds); //Coge el archivo de texto del timer para camiarlo.
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime = GameManager.instance._currentTime;
        UpdateTimer(_currentTime);
       
    }

    
}
