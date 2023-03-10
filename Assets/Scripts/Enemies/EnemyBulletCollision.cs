using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour
{
    #region Parameters
    [SerializeField] private int _bulletDamage;

    #endregion

    #region methods
    private void OnCollisionStay2D(Collision2D collision) // Colisiones de la bala
    {

        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.gameObject.GetComponent<MightyLifeComponent>().OnPlayerHit(_bulletDamage);
        }
        Destroy(gameObject);
    }


    #endregion
}
