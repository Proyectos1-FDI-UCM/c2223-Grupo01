using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsLive : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            GameManager.instance._player.GetComponent<MightyLifeComponent>().OnPlayerHit(30);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
