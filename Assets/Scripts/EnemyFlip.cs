using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    [SerializeField]private GameObject Enemy;
         
    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy.transform.Rotate(0f, 180f, 0f); //Una vez que deje de detectar un colider, el enemigo dará la vuelta.
    }
}
