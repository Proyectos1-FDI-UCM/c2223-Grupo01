using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasGiratorias : MonoBehaviour
{
    private float _countdowntime = 0f;
    private float _DesiredRotation = 90f;
    [SerializeField]private float _rotationSpeed;
    [SerializeField]private float _setTime = 3;
    
    void Update()
    {
        //El método incluye un contador que determina cada cuanto ejecuta la rotación.
        //Cuando se ejecuta la rotatación, le sumo a la rotación actual un valor y lo redondeo.
        _countdowntime += Time.deltaTime;

        if (_countdowntime > _setTime)
        {

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, _DesiredRotation), _rotationSpeed * Time.deltaTime);

            if (transform.rotation.eulerAngles.z == _DesiredRotation)

            {
                _DesiredRotation = (_DesiredRotation + 90f) % 360;
                _countdowntime = 0;
            }
        }
    }
}
