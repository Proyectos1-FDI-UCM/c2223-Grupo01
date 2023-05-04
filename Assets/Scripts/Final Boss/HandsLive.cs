using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsLive : MonoBehaviour
{
    [SerializeField] private int _vidaManos = 400, _dañoManos = 30;
    [SerializeField] private FaseFinalJefe _cuerpo;

    public int GetVidaManos()
    {
        return _vidaManos;
    }    
    #region methods
    public void TakeDamage(int damage)
    {
        if(gameObject.transform.parent.GetComponent<HandsManager>() != null
            && gameObject.transform.parent.GetComponent<HandsManager>().GetCurrentState() != HandsManager.HandsStates.Transición
            && gameObject.transform.parent.GetComponent<HandsManager>().GetCurrentState() != HandsManager.HandsStates.Volviendo 
            && !gameObject.transform.parent.GetComponent<HandsManager>().GetCaida()) 
        {
            _vidaManos -= damage;
            if (_vidaManos < 0)
            {
                gameObject.transform.parent.GetComponent<HandsManager>().OnHandDie();
                Die();
            }
        }
    }

    private void Die()
    {
        if(gameObject.transform.parent.GetComponent<HandsManager>().GetNManos() < 1)
        {
            _cuerpo.Enable();
            Destroy(gameObject.transform.parent.gameObject);
        }
        Destroy(gameObject);
    }
     //Animaciones manos
    public void CerrarMano()
    {
        GetComponent<Animator>().SetTrigger("Spawn");
    }
    public void AbrirMano()
    {
        GetComponent<Animator>().SetTrigger("AbrirMano");
    }

    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MightyLifeComponent>() != null && collision.gameObject.GetComponent<MightyLifeComponent>()._canBeDamaged)
        {
            GameManager.instance._player.GetComponent<MightyLifeComponent>().OnPlayerHit(_dañoManos);
        }

        if (gameObject.transform.parent.GetComponent<HandsManager>().GetNManos() == 1 && collision.gameObject.layer == 12)
        {
            gameObject.transform.parent.GetComponent<HandsManager>().ChangeSpeed();
        }
    }
}
