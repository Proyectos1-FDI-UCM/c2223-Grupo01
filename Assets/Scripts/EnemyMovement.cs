using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _myrigidbody;
    [SerializeField] private float EnemySpeed = 5f;

    private void OnTriggerEnter2D(Collider2D Other)
    {
        transform.Rotate(0f, 180f, 0f); // Cada vez que colisione con un collider, el enemigo dará la vuelta. 
    }

    void Start()
    {
        _myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _myrigidbody.velocity = (transform.right * EnemySpeed);//El Enemigo se moverá a la derecha con determinada velocidad
    }
}
