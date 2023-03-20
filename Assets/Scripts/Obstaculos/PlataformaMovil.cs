using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    #region References
    [SerializeField] GameObject ObjetoaMover;
    [SerializeField] Transform punto1, punto2;
    #endregion

    #region Parameters
    [SerializeField] float velocidad;
    private Vector3 MoverHacia;
    #endregion

    private void Start()
    {
        //Al comienzo se mueve hacia el punto 2
        MoverHacia = punto2.position;
    }

    private void Update()
    {
        //Si la posición es punto 2, se mueve a punto 1.
        if (ObjetoaMover.transform.position == punto2.position)
        {
            MoverHacia = punto1.position;
        }

        //Lo mismo que antes pero al revés.
        if (ObjetoaMover.transform.position == punto1.position)
        {
            MoverHacia = punto2.position;
        }
    }

    //Para cuando haya que hacerlo con Rigidbody, probar con método MovePosition.
}
    


