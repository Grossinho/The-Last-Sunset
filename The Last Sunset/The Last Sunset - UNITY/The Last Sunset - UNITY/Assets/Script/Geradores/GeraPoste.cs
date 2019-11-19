using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeraPoste : MonoBehaviour
{




    [SerializeField] GameObject plane;
    float tempo;
    Vector3 startPos;

    [SerializeField] float cronometro = 1;

    void Update()
    {
        tempo += cronometro * Time.deltaTime;


        if (tempo > 5)
        {

            RetornaPoste();
            tempo = 0;

        }
    }


    void RetornaPoste()
    {


        GameObject poste = (GameObject)Instantiate(plane, transform.position, transform.rotation);


    }
}
