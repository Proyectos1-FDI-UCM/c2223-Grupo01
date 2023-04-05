using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilaVida : MonoBehaviour
{
    #region References
    private MightyLifeComponent _myLifeComponent;
    #endregion

    #region Parameters
    //La vida máxima aumentada.
    public int maxHealthIncrease = 10;
    #endregion

    private void Start()
    {
        _myLifeComponent = GetComponent<MightyLifeComponent>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.instance._player)
        {
            // Aumentar la cantidad de vida máxima del personaje.
            _myLifeComponent.TakeDamage(-maxHealthIncrease);

            // Actualizar la barra de vida para reflejar el nuevo valor de la vida máxima (visualmente).
            
            // Destruir el item.
            Destroy(gameObject);
        }
    }
}