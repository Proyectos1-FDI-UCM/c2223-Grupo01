using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    // Usar RigidBody2D, con body type = kinematic
    // ya que es para objetos que necesitan ser controlados directamente por el código
    #region References
    private GameObject _player;
    Rigidbody2D ObjetoAMover;
    [SerializeField] Transform punto1, punto2;
    #endregion

    #region Parameters
    [SerializeField] float velocidad;
    private Vector2 MoverHacia;
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==13)
        {
            _player.transform.SetParent(transform);
        }
       
    }
    private void OnCollisionExit2D (Collision2D collision)

    {
        if (collision.gameObject.layer == 13)
        {
            _player.transform.SetParent(null);
        }
    }

    private void Start()
    {
        //Al comienzo se mueve hacia el punto 2
        MoverHacia = punto2.position;
        ObjetoAMover = GetComponent<Rigidbody2D>();
        _player=GameManager.instance._player;
    }

    private void FixedUpdate()
    {
        // Calculamos la dirección hacia la que mover el objeto
        Vector2 direccion = MoverHacia - ObjetoAMover.position;
        
        // Movemos el objeto en la dirección calculada
        ObjetoAMover.MovePosition(ObjetoAMover.position + direccion.normalized * velocidad * Time.fixedDeltaTime);
    
        // Si el objeto llega al punto 2, cambiamos la dirección hacia el punto 1
        if (Vector3.Distance(ObjetoAMover.transform.position, punto2.position)<0.5f)
        {
            MoverHacia = punto1.position;
        }
    
        // Si el objeto llega al punto 1, cambiamos la dirección hacia el punto 2
        if (Vector3.Distance(ObjetoAMover.transform.position, punto1.position) < 0.5f)
        {
            MoverHacia = punto2.position;
        }

    }

}
    


