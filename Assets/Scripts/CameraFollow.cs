using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 Offset = new Vector3(0f, 3f, -13f);
    [SerializeField] private Transform target; //El transform de nuestro jugador.
    [SerializeField] private float smoothTime = 0.01f;
    private Vector3 Currentvelocity = Vector3.zero;
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
