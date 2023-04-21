using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenuCommands : MonoBehaviour
{
    int _x , _y;  
    public void Comenzar(int escene)
    {
        SpawnsManager.instance.ResetRespawnPosition();
        SceneManager.LoadScene(escene);
    }

    public void SetRespawnX( int x)
    {
        _x = x;
    }
    public void SetRespawnY(int y)
    {
        _y = y;
    }
    public void ComenzarMitad (int escene)
    {
        SpawnsManager.instance.SetRespawnPosition(new Vector3(_x,_y,0));
        SceneManager.LoadScene(escene);
    }
    public void SalirDeljuego()
    {
        Application.Quit();
    }
}
