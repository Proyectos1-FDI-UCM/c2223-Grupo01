//Parte del código fue obtenido del siguiente video https://www.youtube.com/watch?v=lV47ED8h61k

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    #region parameters 
    [SerializeField][Range(0f, 360f)] private float _visionAngle = 45f; //El ángulo de visión del enemigo.
    [SerializeField] private float _visionDistance = 10f; //La máxima distancia de nuestro cono de visión.
    public bool _detected { get; private set; } //Booleano que determina si el enemigo ha detectado al jugador
    #endregion

    #region references
    private GameObject _player; //El jugador
    #endregion

    #region methods
    Vector3 PointforAngles(float angles, float distance) //El código devuelve los puntos que forman los vectoers directrores del ángulo.
    {
        return transform.TransformDirection(new Vector2(Mathf.Cos(angles * Mathf.Deg2Rad), Mathf.Sin(angles * Mathf.Deg2Rad)) * distance);
    }
    #endregion

    private void OnDrawGizmos() //Método para ver el cono de visión
    {
        if (_visionAngle <= 0f) return; //no se hace nada si el ángulo es menor que cero

        float halfVisionAngle = _visionAngle * 0.5f;
        Vector2 p1, p2;
        p1 = PointforAngles(halfVisionAngle, _visionDistance);
        p2 = PointforAngles(-halfVisionAngle, _visionDistance);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + p1); //Esto dibuja el ángulo.
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + p2);
        Gizmos.DrawRay(transform.position, transform.right * 4f);
    }

    void Start()
    {
        _player = GameManager.instance._player;
        _detected = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_detected);
        if(_player!= null)
        {
            Vector2 PlayerVector = _player.transform.position - transform.position; //El vector que indica la distancia del jugador al enemigo.

            if (Vector3.Angle(PlayerVector.normalized, transform.right) < _visionAngle * 0.5f)  //Comprueba si el jugador está dentro del ángulo de visión del enemigo
            {
                if (PlayerVector.magnitude < _visionDistance) //Comprueba si estamos a una distancia que es detectable para el enemigo.
                {
                    _detected = true;
                }
                else _detected = false;  //Si nos salimos del cono de visión entonces el detecta volverá a ser falso.
            }
        }    
    }
}
