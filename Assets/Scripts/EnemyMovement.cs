//Parte del código fue obtenido del siguiente video https://www.youtube.com/watch?v=lV47ED8h61k


using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private float EnemySpeed = 5f;
    private GameObject Player; //El jugador
    [Range(0f, 360f)] public float VisionAngle = 45f; //El ángulo de visión del enemigo.
    public float VisionDistance = 10f; //La máxima distancia de nuestro cono de visión.
    private bool detected = false; //Booleano que determina si el enemigo ha detectado al jugador


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Player = GameManager.instance._player;

    }

    // Update is called once per frame
    void Update()
    {
       
        Vector2 PlayerVector = Player.transform.position-transform.position; //El vector que indica la distancia del jugador al enemigo.
        
        if (Vector3.Angle(PlayerVector.normalized, transform.right) < VisionAngle * 0.5f)  //Comprueba si el jugador está dentro del ángulo de visión del enemigo
        {
            if (PlayerVector.magnitude< VisionDistance) //Comprueba si estamos a una distancia que es detectable para el enemigo.
            {
                detected= true;
            }
            else detected=false;  //Si nos salimos del cono de visión entonces el detecta volverá a ser falso.
        }
        if (!detected) //Si el enemigo no ha detectado al jugador, este seguirá su patrón normal
        {
            
            rigidbody.velocity = (transform.right * EnemySpeed);//El Enemigo se moverá a la derecha con determinada velocidad
        }
        else if (detected) //Si el enemigo nos detecta. 
        {     
            Vector2 Distfromplayer = new Vector2(Player.transform.position.x - transform.position.x, 0f); //la dirección a la que nuestro enemigo se moverá.
            Vector2 velocity = Distfromplayer * EnemySpeed;
            rigidbody.velocity = velocity;
        }
    }

    private void OnDrawGizmos() //Método para ver el cono de visión
    {
        if (VisionAngle <= 0f) return; //no se hace nada si el ángulo es menor que cero

        float halfVisionAngle = VisionAngle * 0.5f;
        Vector2 p1, p2;
        p1 = PointforAngles(halfVisionAngle, VisionDistance);
        p2 = PointforAngles(-halfVisionAngle, VisionDistance);

        Gizmos.color = Color.red;
       
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + p1); //Esto dibuja el ángulo.
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + p2);
        Gizmos.DrawRay(transform.position, transform.right*4f) ;
    }

    Vector3 PointforAngles (float angles, float distance) //El código devuelve los puntos que forman los vectoers directrores del ángulo.
    {
        return transform.TransformDirection(new Vector2( Mathf.Cos(angles*Mathf.Deg2Rad), Mathf.Sin(angles * Mathf.Deg2Rad))*distance);
    }
    private void OnTriggerEnter2D(Collider2D Other)
    {
       
        transform.Rotate(0f, 180f, 0f); // Cada vez que colisione con un collider, el enemigo dará la vuelta. 
  
    }


}
