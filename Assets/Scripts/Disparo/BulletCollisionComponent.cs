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
    private void OnCollisionStay2D(Collision2D collision) // Colisiones de la bala
    {
        if(collision.gameObject != GameManager.instance._player)
        {
            Destroy(gameObject);
            if (collision.gameObject.GetComponent<EnemyHealth>() != null)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(_bulletDamage);
            }
        }
        else if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_bulletDamage);
        }
        Destroy(gameObject);
    }


    #endregion
}
