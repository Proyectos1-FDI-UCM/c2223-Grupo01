using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BulletCollisionComponent : MonoBehaviour
{
    #region Parameters
    [Header("Disparo al player")]
    [SerializeField] private int _enemysBulletDamage;
    [Header("Disparo Normal")]
    [SerializeField] private int _normalBulletDamage;
    [Header("Disparo de Hielo")]
    [SerializeField] private int _iceBulletDamage;
    [SerializeField] private int _ralentizado;

    public enum typeOfDamage { Normal, Ice, Fire };
    [SerializeField]private typeOfDamage _actualDamage;
    #endregion

    #region methods

    private void Hit(GameObject collision)
    {
        if (collision.GetComponent<EnemyHealth>() != null)
        {
            OnHitEnemy(collision);
        }
        else if (collision.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.GetComponent<MightyLifeComponent>().OnPlayerHit(_enemysBulletDamage);
        }
    }

    private void OnHitEnemy(GameObject collision)
    {
        switch (_actualDamage)
        {
            case typeOfDamage.Normal:
                {
                    NormalHit(collision.gameObject);
                    break;
                }

            case typeOfDamage.Ice:
                {
                    IcelHit(collision.gameObject);
                    break;
                }

            case typeOfDamage.Fire:
                {
                    break;
                }
        }
    }

    // Comprueba con qué objeto hemos colisionado y le aplica su daño
    private void NormalHit(GameObject collision)
    {
        if (collision.GetComponent<EnemyHealth>() != null)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(_normalBulletDamage);
        }
    }

    private void IcelHit(GameObject collision)
    {

        if (collision.GetComponent<EnemyMovement>() != null)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(_iceBulletDamage);
            collision.GetComponent<EnemyMovement>().SetEnemySpeed(collision.GetComponent<EnemyMovement>().GetEnemySpeed() - _ralentizado);
            collision.GetComponent<EnemyMovement>().SetEnemyDetectionSpeed(collision.GetComponent<EnemyMovement>().GetEnemyDetectionSpeed() - _ralentizado);
            collision.GetComponent<EnemyHealth>().SetNumBalasCongelado(collision.GetComponent<EnemyHealth>().GetNumBalasCongelado() + 1);
            if(collision.GetComponent<EnemyHealth>().GetNumBalasCongelado() == 3)
            {
                collision.GetComponent<EnemyMovement>().SetEnemySpeed(0);
                collision.GetComponent<EnemyMovement>().SetEnemyDetectionSpeed(0);
            }
        }
    }

    #endregion

    #region collision methods
    private void OnCollisionEnter2D(Collision2D collision) // Colisiones de la bala
    {
        Hit(collision.gameObject);
        Destroy(gameObject);
    }
    #endregion
}
