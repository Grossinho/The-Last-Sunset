using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Recorde : MonoBehaviour
{
    [SerializeField] Text textoRecorde;
    float recordes, novoRecorde;
    string nomes; 
    int contador;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        contador = 2;       

        novoRecorde = PlayerPrefs.GetFloat("recorde");
        InvokeRepeating("AtualizaRecorde", 0, 1);
    }

    void AtualizaRecorde()
    {
        if (--contador <= 0) CancelInvoke("AtualizaRecorde");
        textoRecorde.text = novoRecorde.ToString();
    }
}

