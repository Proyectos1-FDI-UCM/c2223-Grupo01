using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    #region References
    [SerializeField] private GameObject _objetoaMover;
    [SerializeField] private Transform _puntoDePartida;
    [SerializeField] private Transform _puntoFinal;
    #endregion
    #region Parameters
    [SerializeField] private float _velocidad;
    private Vector3 _moverHacia;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Queremos que al inicar se mueva hacia el punto final.
        _moverHacia = _puntoFinal.position;
    }

    // Update is called once per frame
    void Update()
    {
        //La posición de la plataforma cambia según MoveTowards("Posición actual", "Hacia dónde se mueve", "Velocidad con la que se mueve")
        _objetoaMover.transform.position = Vector3.MoveTowards(_objetoaMover.transform.position, _moverHacia, _velocidad * Time.deltaTime);

        //Si la posición de la plataforma == posición final del recorrido...
        if(_objetoaMover.transform.position == _puntoFinal.position)
        {
            //...se mueve hacia el punto de partida.
            _moverHacia = _puntoDePartida.position;
        }

        //Lo mismo que arriba pero al revés.
        if(_objetoaMover.transform.position == _puntoDePartida.position)
        {
            _moverHacia = _puntoFinal.position;
        }
    }
}

