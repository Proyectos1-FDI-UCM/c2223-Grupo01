using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasGiratorias : MonoBehaviour
{
    private float _currentRotation = 0f; 
    private float _destinedRotation= 90f;
    private float _countdowntime = 0f;
    [SerializeField]private float _rotationSpeed;
    [SerializeField]private float _setTime = 3;
    

    private void RotatePlatform() 
        //El método incluye un contador que determina cada cuanto ejecuta la rotación.
        //Cuando se ejecuta la rotatación, le sumo a la rotación actual un valor y lo redondeo.
    {     
        _countdowntime += Time.deltaTime;

        if (_countdowntime > _setTime)
        {
            if (_currentRotation < _destinedRotation)
            {
                _currentRotation += _rotationSpeed*Time.deltaTime;
                _currentRotation=Mathf.Round(_currentRotation);
                transform.rotation = Quaternion.Euler(0, 0, _currentRotation);

            }
            else if (_currentRotation ==_destinedRotation)
            {              
                _destinedRotation += 90;
                _countdowntime = 0f;
            }
        }                    
    }
   
    
    void Update()
    {
        RotatePlatform();   
    }
}
