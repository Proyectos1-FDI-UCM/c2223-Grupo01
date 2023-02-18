using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private float EnemySpeed = 5f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = (transform.right * EnemySpeed);//El Enemigo se moverá a la derecha con determinada velocidad
    }

}
