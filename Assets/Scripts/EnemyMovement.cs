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
    private bool detected = false; //Booleano que determina si el enemigo ha detectado al jugador


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Player = GameManager.instance._player;

    }

    // Update is called once per frame
    void Update()
    {
        if (!detected) //Si el enemigo no ha detectado al jugador, este seguirá su patrón normal
            rigidbody.velocity = (transform.right * EnemySpeed);//El Enemigo se moverá a la derecha con determinada velocidad
        else if (detected) //Si el enemigo nos detecta. 
        {
            Vector2 Distfromplayer = new Vector2(Player.transform.position.x - transform.position.x, 0f); //la dirección a la que nuestro enemigo se moverá.
            Vector2 velocity = Distfromplayer * EnemySpeed;
            rigidbody.velocity = velocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D Other)
    {
        transform.Rotate(0f, 180f, 0f); // Cada vez que colisione con un collider, el enemigo dará la vuelta. 
       


    }


}
