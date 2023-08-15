using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] menu;
    [SerializeField] private GameObject _fade;
    [SerializeField] private AudioClip _okSFX;
    public void Save()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        PlayerPrefs.SetInt("SCENE", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("X", SpawnsManager.instance.getSpawnPosition().x);
        PlayerPrefs.SetFloat("Y", SpawnsManager.instance.getSpawnPosition().y);
        PlayerPrefs.SetFloat("F",SpawnsManager.instance.GetIsFinishedGame());
        PlayerPrefs.Save();
    }
    public void Load()
    {
        GetComponent<AudioSource>().PlayOneShot(_okSFX);
        _fade.GetComponent<Animator>().SetTrigger("OUT");
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].SetActive(false);
        }
        Invoke("LoadR", 2f);
    }

    private void LoadR()
    {
        SpawnsManager.instance.SetRespawnPosition(new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), 0));
        SpawnsManager.instance.SetfinishedGame(PlayerPrefs.GetFloat("F"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("SCENE"));
    }
}
