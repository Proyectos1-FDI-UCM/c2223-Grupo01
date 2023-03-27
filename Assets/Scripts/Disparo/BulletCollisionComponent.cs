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
    [Header("Disparo de Hielo")]
    [SerializeField] private int _fireBulletDamage;

    private EnemyMovement _enemyMovement;
    private EnemyFlyingMovement _enemyFlyingMovement;
    private EnemyHealth _enemyHealth;
    private Rigidbody2D _enemyRigidBody;
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
                    FireHit(collision.gameObject);
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

    #region Ice
    private void IcelHit(GameObject collision)
    {
        _enemyHealth = collision.GetComponent<EnemyHealth>();
        _enemyHealth.TakeDamage(_iceBulletDamage);
        if(collision.GetComponent<EnemyHealth>().GetNumBalasCongelado() < 3)
        {
            collision.GetComponent<EnemyStateManager>().SetRalentizado(true);
            collision.GetComponent<EnemyStateManager>().SetTiempoRalentizado(collision.GetComponent<EnemyStateManager>().GetTiempoRalentizadoInicial());
            _enemyHealth.SetNumBalasCongelado(_enemyHealth.GetNumBalasCongelado() + 1);
            if (collision.GetComponent<EnemyMovement>() != null)
            {
                _enemyMovement = collision.GetComponent<EnemyMovement>();
                if (_enemyMovement.GetEnemySpeed() - _ralentizado > 0)
                {
                    _enemyMovement.SetEnemySpeed(_enemyMovement.GetEnemySpeed() - _ralentizado);
                    _enemyMovement.SetEnemyDetectionSpeed(_enemyMovement.GetEnemyDetectionSpeed() - _ralentizado);
                }
                else
                {
                    _enemyMovement.SetEnemySpeed(0);
                    _enemyMovement.SetEnemyDetectionSpeed(0);
                }
            }
            else if (collision.GetComponent<EnemyFlyingMovement>() != null)
            {
                _enemyFlyingMovement = collision.GetComponent<EnemyFlyingMovement>();
                if (_enemyFlyingMovement.GetEnemyDetectedSpeed() - _ralentizado > 0 )
                {
                    _enemyFlyingMovement.SetEnemySpeed(_enemyFlyingMovement.GetEnemySpeed() - _ralentizado);
                    _enemyFlyingMovement.SetEnemyDetectedSpeed(_enemyFlyingMovement.GetEnemyDetectedSpeed() - _ralentizado);
                }
                else
                {
                    _enemyFlyingMovement.SetEnemySpeed(0);
                    _enemyFlyingMovement.SetEnemyDetectedSpeed(0);
                }
            }
        }
        
        if (collision.GetComponent<EnemyHealth>().GetNumBalasCongelado() == 3)
        {
            Congelado(collision);
        }
    }

    private void Congelado(GameObject collision)
    {
        collision.GetComponent<EnemyStateManager>().SetCongelado(true);
        collision.GetComponent<EnemyStateManager>().SetTiempoCongelado(collision.GetComponent<EnemyStateManager>().GetTiempoCongeladoInicial());
        Destroy(gameObject);
        if (collision.GetComponent<EnemyMovement>() != null)
        {
            _enemyMovement.SetEnemySpeed(0);
            _enemyMovement.SetEnemyDetectionSpeed(0);
        }
        else if (collision.GetComponent<EnemyFlyingMovement>() != null)
        {
            _enemyFlyingMovement.SetEnemySpeed(0);
            _enemyFlyingMovement.SetEnemyDetectedSpeed(0);
            _enemyRigidBody = collision.GetComponent<Rigidbody2D>();
            _enemyRigidBody.bodyType = RigidbodyType2D.Dynamic;
            _enemyRigidBody.gravityScale = 10;
        }
    }
    #endregion

    #region fire
    private void FireHit(GameObject collision)
    {
        _enemyHealth = collision.GetComponent<EnemyHealth>();
        _enemyHealth.TakeDamage(_fireBulletDamage);
        if( Random.RandomRange(0,100)<= 25)
        {
            collision.gameObject.GetComponent<EnemyStateManager>().SetQuemado(true);
        }
    } 
    #endregion

    #endregion

    #region collision methods
    private void OnCollisionEnter2D(Collision2D collision) // Colisiones de la bala
    {
        Hit(collision.gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision) //Colision de la llama del lanza llamas. No dudar quitarlo si el enemigo hace mas daño de lo normal con el proyectil
    {
        if (collision.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            collision.GetComponent<MightyLifeComponent>().OnPlayerHit(_enemysBulletDamage);
        }
    }
    #endregion
}
