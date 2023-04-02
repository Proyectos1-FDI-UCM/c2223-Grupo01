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
    public void ComenzarHIELO()
    {
        SceneManager.LoadScene(3);
    }
    public void ComenzarFABRICA()
    {
        SceneManager.LoadScene(4);
    }
    public void ComenzarHIELOmitad()
    {
        SceneManager.LoadScene(5);
    }
    public void ComenzarFABRICAmitad()
    {
        SceneManager.LoadScene(6);
    }

    public void ComenzarLava()
    {
        SceneManager.LoadScene(7);
    }

    public void ComenzarLavaCheck1()
    {
        SceneManager.LoadScene(8);
    }

    public void ComenzarLavaCheck2()
    {
        SceneManager.LoadScene(9);
    }

    public void SalirDeljuego()
    {
        Application.Quit();
    }
}
