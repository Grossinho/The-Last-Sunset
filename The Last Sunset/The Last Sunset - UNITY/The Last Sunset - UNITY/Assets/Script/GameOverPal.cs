using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPal : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {            
            GameController.instancia.SaveRecord();
            Debug.Log(PlayerPrefs.GetFloat("Record"));
            SceneManager.LoadScene("Game Over");
        }           
    }

}
