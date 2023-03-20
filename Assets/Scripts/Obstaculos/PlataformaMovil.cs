using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    #region References
    [SerializeField] private Transform _puntoDePartida;
    [SerializeField] private Transform _puntoFinal;
    Rigidbody2D rb;
    #endregion

    #region Parameters
    [SerializeField] private float _velocidad;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Queremos que al inicar se mueva hacia el punto final.
        rb.MovePosition(_puntoFinal.position * Time.deltaTime * _velocidad);
    }
    private void FixedUpdate()
    {
        //Si la posición de la plataforma == posición final del recorrido...
        if (transform.position == _puntoFinal.position)
        {
            //...se mueve hacia el punto de partida.
            rb.MovePosition(_puntoDePartida.position * Time.deltaTime * _velocidad);
        }

        //Lo mismo que arriba pero al revés.
        if (transform.position == _puntoDePartida.position)
        {
            rb.MovePosition(_puntoFinal.position * Time.deltaTime * _velocidad);  
        }
        
    }
}

