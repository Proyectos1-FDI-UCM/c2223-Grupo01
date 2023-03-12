// Reutilizado de la pr�ctica 1 de Motores
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedTimeLife : MonoBehaviour
{
    #region Paramaters
    [SerializeField] private float _maxLifetime;
    #endregion

    #region Methods
    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        Invoke("SelfDestroy", _maxLifetime * Time.deltaTime);
    }
}