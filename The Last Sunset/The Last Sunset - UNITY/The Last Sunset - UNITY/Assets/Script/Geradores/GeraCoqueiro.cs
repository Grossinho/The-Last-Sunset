using System.Collections;

using System.Collections.Generic;
using UnityEngine;



public class GeraCoqueiro : MonoBehaviour
{
    public GameObject plane;
   

    float tempo;
    int KMAnterior;
    int KMAtual;

    Vector3 startPos;

    [SerializeField]float cronometro = 1;


    void Start()
    {
        KMAnterior = 0;
        KMAtual = 0;


    }

    void Update()
    {
        tempo += cronometro * Time.deltaTime;
        KMAtual = (int)GameController.instancia.distancia;

        if (KMAtual != KMAnterior)
        {
            RetornaCoqueiro();
            KMAnterior = KMAtual;
        }

        if (tempo > 5)
        {

            RetornaCoqueiro();
            tempo = 0;

        }
    }


    void RetornaCoqueiro()
    {


        GameObject Coco = (GameObject)Instantiate(plane, transform.position, transform.rotation);


    }
}

