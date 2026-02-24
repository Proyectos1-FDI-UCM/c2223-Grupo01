using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TopeEscalera : MonoBehaviour
{
    private TilemapCollider2D _topeEscalera;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>() != null && (collision.gameObject.GetComponent<CharacterController>()._isClimbing || collision.gameObject.GetComponent<InputComponent>()._lookDOWN))
        {
            _topeEscalera.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>() != null)
        {
            _topeEscalera.isTrigger = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _topeEscalera = GetComponent<TilemapCollider2D>();
    }
}
