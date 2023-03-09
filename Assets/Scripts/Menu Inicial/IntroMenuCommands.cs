using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenuCommands : MonoBehaviour
{
    public void Comenzar()
    {
        SceneManager.LoadScene(2);
    }
    public void SalirDeljuego()
    {
        Application.Quit();
    }
}
