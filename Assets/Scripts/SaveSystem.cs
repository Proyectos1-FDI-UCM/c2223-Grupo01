using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
 
    // Start is called before the first frame update

  public void Save()
    {
        PlayerPrefs.SetInt("SCENE", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("X", SpawnsManager.instance.getSpawnPosition().x);
        PlayerPrefs.SetFloat("Y", SpawnsManager.instance.getSpawnPosition().y);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat("X"));
    }
    public void Load()
    {
        SpawnsManager.instance.SetRespawnPosition(new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), 0));
        SceneManager.LoadScene(PlayerPrefs.GetInt("SCENE"));
    }
}
