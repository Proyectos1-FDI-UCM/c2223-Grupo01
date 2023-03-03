using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieEnemyComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _LifeTime;
    #endregion

    #region references
    private Collider2D _myCollider2D;
    private Transform _myTransform;
    #endregion

    #region methods
    public void Die()
    {
        // desactiva el collider
        // triggea la animación de muerte
        // hace salir al enemy por abajo
        // lo destruye
        _myCollider2D.enabled = !_myCollider2D.enabled; 
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider2D = GetComponent<Collider2D>();      
        _myTransform = GetComponent<Transform>();
    }
}
