using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPowerDamage : MonoBehaviour
{
    #region Parameters
    [SerializeField] private float _damage;



    #endregion

    #region References
    private MightyLifeComponent _playerLife;




    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    // Colisiones del jugador con los enemigos
    {
        Debug.Log("1");
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null)
        {
            Debug.Log("2");
            _playerLife.OnPlayerHit(_damage);
            
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerLife = GetComponent<MightyLifeComponent>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
