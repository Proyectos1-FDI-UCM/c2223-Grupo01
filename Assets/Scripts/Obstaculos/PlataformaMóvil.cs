using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMóvil : MonoBehaviour
{
    #region References
    [SerializeField] GameObject _objetoaMover;
    [SerializeField] Transform _puntoDePartida;
    [SerializeField] Transform _puntoFinal;
    [SerializeField] float velocidad;
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
        _objetoaMover.transform.position = Vector3.MoveTowards(_objetoaMover.transform.position, _moverHacia, velocidad * Time.deltaTime);

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

