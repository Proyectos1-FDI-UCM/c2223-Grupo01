using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsManager : MonoBehaviour
{
    public static SpawnsManager instance;
    private static Vector3 _respawnPosition;

    public void SetRespawnPosition(Vector3 newPosition)
    {
        _respawnPosition = newPosition;
    }

    public void ResetRespawnPosition()
    {
        _respawnPosition = Vector3.zero;
    }

    public void Respawn(GameObject player)
    {
        if(_respawnPosition != Vector3.zero)
        {
            player.transform.position = _respawnPosition;
        }
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
