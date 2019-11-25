using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPal : MonoBehaviour
{

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.instancia.SaveRecord();
            Debug.Log(PlayerPrefs.GetFloat("Record"));
            SceneManager.LoadScene("Game Over");
        }
    }



}
