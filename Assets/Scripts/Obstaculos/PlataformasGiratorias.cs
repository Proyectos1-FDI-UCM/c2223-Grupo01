using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasGiratorias : MonoBehaviour
{
    private float _Timer = 0f;
    private float _initialrotation;
    private Rigidbody2D _rigidbody;
    [SerializeField]private float _DesiredRotation;
    [SerializeField]private float _rotationSpeed;
    [SerializeField]private float _setTime = 3;

    private void Start()
    {
        _initialrotation=_DesiredRotation;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //El método incluye un contador que determina cada cuanto ejecuta la rotación.
        //Cuando se ejecuta la rotatación, le sumo a la rotación actual un valor y lo redondeo.
        Debug.Log(transform.rotation.eulerAngles.z);
        Debug.Log(transform.rotation.eulerAngles.z + "  " + _DesiredRotation);
        _Timer += Time.deltaTime;


        if (_Timer > _setTime)
            
        {
            _rigidbody.MoveRotation (Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, _DesiredRotation), _rotationSpeed * Time.deltaTime));

            if (Mathf.Approximately(transform.rotation.eulerAngles.z, _DesiredRotation))

            {
                _DesiredRotation = (_DesiredRotation + _initialrotation) % 360;
                _Timer = 0;
            }
        }
    }
}
