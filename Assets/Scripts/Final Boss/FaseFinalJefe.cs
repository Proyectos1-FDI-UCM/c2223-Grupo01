using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FaseFinalJefe : MonoBehaviour
{
    private Transform _myTransform;
    private int _patata = 0;

    [SerializeField] private LayerMask _playerLayer;

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_myTransform.position, GameManager.instance._player.transform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.Raycast(_myTransform.position, GameManager.instance._player.transform.position, 100.0f, _playerLayer))
        {
            _patata++;
            Debug.Log("debug" + _patata);
        }
    }
}
