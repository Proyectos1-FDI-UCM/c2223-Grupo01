using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsLive : MonoBehaviour
{
    [SerializeField] private int _vidaManos = 400, _dañoManos = 30;

    #region methods
    public void TakeDamege(int damage)
    {
        _vidaManos -= damage;
        if(_vidaManos <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.transform.parent.GetComponent<FinalBossManager>().enabled = false;
        Destroy(gameObject);
    }

    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            GameManager.instance._player.GetComponent<MightyLifeComponent>().OnPlayerHit(_dañoManos);
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
