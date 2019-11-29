using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameController.instancia.SaveRecord();
            GetComponent<Collider>().isTrigger = false;
            GameController.instancia.Pausar(true);
        }
    }
}
