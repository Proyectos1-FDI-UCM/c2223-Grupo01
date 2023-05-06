using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsLive : MonoBehaviour
{
    [SerializeField] private float _vidaManos = 400, _dañoManos = 30;
    private float _initialVidaManos;
    [SerializeField] private FaseFinalJefe _cuerpo;
    [SerializeField] private BossUI _bossUI;
    [SerializeField] private AudioClip _hurt;
    public float GetVidaManos()
    {
        return _vidaManos;
    }

    public float GetVidaManosInicial()
    {
        return _initialVidaManos;
    }

    #region methods
    public void TakeDamage(int damage)
    {
        if(gameObject.transform.parent.GetComponent<HandsManager>() != null
            && gameObject.transform.parent.GetComponent<HandsManager>().GetCurrentState() != HandsManager.HandsStates.Transición
            && gameObject.transform.parent.GetComponent<HandsManager>().GetCurrentState() != HandsManager.HandsStates.Volviendo 
            && !gameObject.transform.parent.GetComponent<HandsManager>().GetCaida()) 
        {
            GetComponent<AudioSource>().PlayOneShot(_hurt);
            _vidaManos -= damage;
            if (_vidaManos < 0)
            {
                gameObject.transform.parent.GetComponent<HandsManager>().OnHandDie();
                Die();
            }
            _bossUI.ActualizarInterfazManos();
        }
    }

    private void Die()
    {
        if(gameObject.transform.parent.GetComponent<HandsManager>().GetNManos() < 1)
        {
            _cuerpo.Enable();
            _cuerpo.ChangeState();
            Destroy(gameObject.transform.parent.gameObject);
            _bossUI.ActualizarUI();
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

    private void Start()
    {
        _initialVidaManos = _vidaManos;
    }
}
