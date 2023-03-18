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

    private EnemyMovement _enemyMovement;
    private EnemyFlyingMovement _enemyFlyingMovement;
    private EnemyHealth _enemyHealth;
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
        _enemyHealth = collision.GetComponent<EnemyHealth>();
        _enemyHealth.TakeDamage(_iceBulletDamage);
        if(collision.GetComponent<EnemyHealth>().GetNumBalasCongelado() < 3)
        {
            _enemyHealth.SetNumBalasCongelado(_enemyHealth.GetNumBalasCongelado() + 1);
            if (collision.GetComponent<EnemyMovement>() != null)
            {
                _enemyMovement = collision.GetComponent<EnemyMovement>();
                _enemyMovement.SetEnemySpeed(_enemyMovement.GetEnemySpeed() - _ralentizado);
                _enemyMovement.SetEnemyDetectionSpeed(_enemyMovement.GetEnemyDetectionSpeed() - _ralentizado);
            }
            else if (collision.GetComponent<EnemyFlyingMovement>() != null)
            {
                _enemyFlyingMovement = collision.GetComponent<EnemyFlyingMovement>();
                _enemyFlyingMovement.SetEnemySpeed(_enemyFlyingMovement.GetEnemySpeed() - _ralentizado);
                _enemyFlyingMovement.SetEnemyDetectedSpeed(_enemyFlyingMovement.GetEnemyDetectedSpeed() - _ralentizado);
            }
        }
        
        if (collision.GetComponent<EnemyHealth>().GetNumBalasCongelado() == 3)
        {
            Congelado(collision);
        }
    }

    private void Congelado(GameObject collision)
    {
        if (collision.GetComponent<EnemyMovement>() != null)
        {
            _enemyMovement.SetEnemySpeed(0);
            _enemyMovement.SetEnemyDetectionSpeed(0);
        }
        else if (collision.GetComponent<EnemyFlyingMovement>() != null)
        {
            _enemyFlyingMovement.SetEnemySpeed(0);
            _enemyFlyingMovement.SetEnemyDetectedSpeed(0);
            collision.GetComponent<Rigidbody2D>().gravityScale = 10;
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
