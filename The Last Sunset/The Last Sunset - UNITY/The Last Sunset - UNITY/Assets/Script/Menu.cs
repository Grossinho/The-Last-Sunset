using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void Despause ()
    {
        Time.timeScale = 1;
    }

    public void Sair()
    {
        Application.Quit();


        //Debug.Log("SaIUUUUUUU");
        
    }
}
