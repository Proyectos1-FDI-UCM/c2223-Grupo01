using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField] private int _bulletDamage;
    #endregion

    #region methods
    // Colisiones de la bala  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if(collision.gameObject.GetComponent<EnemyHealth>() != null)
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(_bulletDamage);
        }
    }
    #endregion
}
