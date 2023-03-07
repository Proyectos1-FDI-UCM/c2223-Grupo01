using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
//Seguimiento de la cámara al player.
{
    #region parameters
    [SerializeField] private Vector3 Offset = new Vector3(0f, 0f, 13f);
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.01f;
    private Vector3 Currentvelocity = Vector3.zero;
    #endregion

    private void Start()
    {
        target = GameManager.instance._player.transform;   
    }
    void FixedUpdate()
    {
        Vector3 TargetPosition = target.position - Offset;

        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref Currentvelocity, smoothTime);
    }
}
