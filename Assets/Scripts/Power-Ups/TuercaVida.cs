using UnityEngine;

public class TuercaVida : MonoBehaviour
{
    #region References
    //Referencia al componente de la vida de Mighty.
    MightyLifeComponent _myMightyLifeComponent;

    //La vida que le sube a Mighty.
    [SerializeField] private float _healthbonus = 15f;
    [SerializeField] private float _vidacurada;
    #endregion

    //El Awake se llama antes que el Start.
    private void Awake()
    {
        _myMightyLifeComponent= GetComponent<MightyLifeComponent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba que la vida actual es menos que la vida máxima establecida.
        if (_myMightyLifeComponent.GetHealth() < _myMightyLifeComponent.GetMaxHealth())
        {
            //Quita el objeto de curación de la escena.
            Destroy(gameObject);

            //Hacer que se le sume a la vida actual el "healthbonus". La vida sumada hacerlo en negativo porque es "Hacer daño" pero invertido.
            /*_myMightyLifeComponent.TakeDamage()*/
        }
    }
}
