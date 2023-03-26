using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlataformasGiratorias : MonoBehaviour
{
    private float _Timer = 0f;
  
    private Rigidbody2D _rigidbody;
    private Quaternion axisRotation; //la rotaci�n en el axis que har� la plataforma.
    private Quaternion desiredRotation;
    [SerializeField] private float _addRotation;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _setTime = 3;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        axisRotation = Quaternion.AngleAxis(_addRotation, Vector3.forward);
        desiredRotation = axisRotation * transform.rotation;
    }
    void Update()
    {
        //El m�todo incluye un contador que determina cada cuanto ejecuta la rotaci�n.
        //Cuando se ejecuta la rotataci�n, le sumo a la rotaci�n actual un valor y lo redondeo.
       
        _Timer += Time.deltaTime;
            
        if (_Timer > _setTime)

        {
            Quaternion moveRotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, _rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(moveRotation);
            float angle = Quaternion.Angle(transform.rotation, desiredRotation);
            if (angle < Mathf.Epsilon)
            {
                desiredRotation = axisRotation * desiredRotation;
                _Timer = 0;
            }
       }
    }
}
