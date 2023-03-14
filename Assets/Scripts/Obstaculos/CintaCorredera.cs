using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CintaCorredera : MonoBehaviour
{
    #region Parameters
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _direccion;
    #endregion

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<CharacterController>().GetIsGrounded())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(_speed * _direccion);
        }
    }
}
