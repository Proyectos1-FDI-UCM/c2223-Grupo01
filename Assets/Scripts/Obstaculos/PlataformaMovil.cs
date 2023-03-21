using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    #region References
    [SerializeField]private float _distancia;
    private Vector2 _Destino;
    private enum Orientacion {Horizontal, Vertical,DiagonalSuperior,DiagonalInferior};
    [SerializeField]private Orientacion _Orientation;
    private Rigidbody2D rb;
    [SerializeField] float velocidad;
    #endregion

    #region Parameters
   
  
    #endregion

    private void Start()
    {
      rb = GetComponent<Rigidbody2D>();
        switch (_Orientation)
        {
            case Orientacion.Horizontal:
                _Destino = new Vector2(transform.position.x + _distancia, transform.position.y);
                break;
            case Orientacion.Vertical:
                _Destino = new Vector2(transform.position.x, transform.position.y + _distancia);
                break;
            case Orientacion.DiagonalInferior:
                _Destino = new Vector2(transform.position.x + _distancia, transform.position.y - _distancia);
                break;
            case Orientacion.DiagonalSuperior:
                _Destino = new Vector2(transform.position.x + _distancia, transform.position.y + _distancia);
                break;
        }
     
    }

    private void Update()
    {
        rb.velocity = ((_Destino - (Vector2) transform.position).normalized * velocidad);
        if(Vector2.Distance(_Destino,(Vector2)transform.position) <0.5f)
            {
            _Destino = -_Destino;
            }
        
    }

    //Para cuando haya que hacerlo con Rigidbody, probar con método MovePosition.
}
    


