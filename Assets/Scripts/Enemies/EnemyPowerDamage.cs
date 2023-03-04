using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPowerDamage : MonoBehaviour
{
    #region Parameters
    [SerializeField] private float _damage;
    #endregion

    private void OnCollisionStay2D(Collision2D collision)
    // Colisiones del jugador con los enemigos
    {
        Debug.Log("1");
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canAttack)
        {
            Debug.Log("2");
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_damage);
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
