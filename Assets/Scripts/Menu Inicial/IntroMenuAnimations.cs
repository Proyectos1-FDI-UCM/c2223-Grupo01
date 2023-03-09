using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMenuAnimations : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _direccion; //Direccion a la que ira el sprite. PONER 1 O -1
    [SerializeField] private float _animSpeed; //Velocidad a la que se mueve el sprite

    // Start is called before the first frame update
    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();

        _rb.AddForce(_animSpeed * new Vector2(_direccion,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
