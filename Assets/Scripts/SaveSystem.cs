using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private void Awake()
    {
        instance = this;
    }
    public void Save()
    {
        PlayerPrefs.SetInt("SCENE", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("X", SpawnsManager.instance.getSpawnPosition().x);
        PlayerPrefs.SetFloat("Y", SpawnsManager.instance.getSpawnPosition().y);
        PlayerPrefs.SetFloat("FINISHED",SpawnsManager.instance.GetIsFinishedGame());
        PlayerPrefs.Save();
    }
    public void Load()
    {
        SpawnsManager.instance.SetRespawnPosition(new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), 0));
        SpawnsManager.instance.SetfinishedGame(PlayerPrefs.GetFloat("FINISHED"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("SCENE"));
    }
}
