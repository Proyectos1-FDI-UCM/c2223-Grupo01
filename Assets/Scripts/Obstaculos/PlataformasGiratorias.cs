using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlataformasGiratorias : MonoBehaviour
{
    private float _Timer = 0f;
    private float _initialrotation;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _DesiredRotation;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _setTime = 3;

    private bool ComparaValores(float Rotacion , float Rotacionquequiero)
    {
        return Math.Abs(Rotacion - Rotacionquequiero) < 0.0001f;
    }

    private void Start()
    {
        _initialrotation = _DesiredRotation;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //El método incluye un contador que determina cada cuanto ejecuta la rotación.
        //Cuando se ejecuta la rotatación, le sumo a la rotación actual un valor y lo redondeo.
        Debug.Log(ComparaValores(transform.rotation.eulerAngles.z, _DesiredRotation));
        Debug.Log(transform.rotation.eulerAngles.z);
        Debug.Log(_DesiredRotation);

        

        _Timer += Time.deltaTime;


        if (_Timer > _setTime)

        {
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, _DesiredRotation), _rotationSpeed * Time.deltaTime));

            if (ComparaValores(transform.rotation.eulerAngles.z, _DesiredRotation))

            {
                _DesiredRotation = (_DesiredRotation + _initialrotation) % 360;
                _Timer = 0;
            }
        }
    }
}
