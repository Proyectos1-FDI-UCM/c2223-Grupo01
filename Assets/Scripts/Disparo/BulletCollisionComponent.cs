using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField] private int _bulletDamage;

    public enum typeOfDamage { Normal, Ice, Fire };
    [SerializeField]private typeOfDamage _actualDamage;
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

        /*switch (_actualDamage)
        {
            case typeOfDamage.Normal:
                {
                    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(_bulletDamage);
                    break;
                }
        }*/

    }
    #endregion
}
