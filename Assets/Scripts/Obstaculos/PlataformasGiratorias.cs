using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlataformasGiratorias : MonoBehaviour
{
    private float _Timer = 0f;
  
    private Rigidbody2D _rigidbody;
    private Quaternion axisRotation; //la rotación en el axis que hará la plataforma.
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
        //El método incluye un contador que determina cada cuanto ejecuta la rotación.
        //Cuando se ejecuta la rotatación, le sumo a la rotación actual un valor y lo redondeo.
       
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
