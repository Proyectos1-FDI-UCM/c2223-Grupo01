using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthpackComponent : MonoBehaviour
{
    #region references
    private MightyLifeComponent _myMightyLifeComponent;
    #endregion

    #region parameters
    [SerializeField] private float _sanation;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance._player)
        {
            // referencias en la colision
            _myMightyLifeComponent =collision.gameObject.GetComponent<MightyLifeComponent>();

            //Comprueba que la vida actual es menos que la vida máxima establecida.
            if (_myMightyLifeComponent.GetHealth() <= _myMightyLifeComponent.GetMaxHealth() - _sanation)
            {
                //Hacer que se le sume a la vida actual el "healthbonus". La vida sumada hacerlo en negativo porque es "Hacer daño" pero invertido.
                _myMightyLifeComponent.TakeDamage(-_sanation);

                //Quita el objeto de curación de la escena.
                Destroy(gameObject);
            }
            else
            {
                //Con la resta de la vida máxima y la vida actual conseguimos la vida que le falta, y se le cura la resta. Se pone en negativo porque el TakeDamage negativo es curación
                _myMightyLifeComponent.TakeDamage(-(_myMightyLifeComponent.GetMaxHealth() - _myMightyLifeComponent.GetHealth()));

                //Quita el objeto de curación de la escena.
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
