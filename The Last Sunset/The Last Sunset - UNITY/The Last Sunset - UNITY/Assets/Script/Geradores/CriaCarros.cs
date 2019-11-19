using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriaCarros : MonoBehaviour
{

    [SerializeField] List <Transform> pos;  
    [SerializeField] List <GameObject> carros;
    [SerializeField] GameObject Fita;
    [SerializeField] Transform spawnFita;
    [SerializeField] float VelocidadeObstaculo;



    [HideInInspector] public CriaCarros instancia;

    int KMAnterior;
    int KMAtual;
    float geraFita;

    private void Start()
    {

        RetornaCarro();
        Instantiate(Fita, new Vector3(spawnFita.position.x, spawnFita.position.y - 1.5f, spawnFita.position.z), Fita.transform.rotation);
        KMAnterior = 0;
        KMAtual = 0;
    }
    // Update is called once per frame
    void Update()
    {

        KMAtual = (int)GameController.instancia.distancia;

        geraFita += 1 * Time.deltaTime;

        if (KMAtual != KMAnterior)
        {
            RetornaCarro();
            KMAnterior = KMAtual;
        }

        if (geraFita > 90)
        {
            Instantiate(Fita, new Vector3 (spawnFita.position.x, spawnFita.position.y - 1.5f, spawnFita.position.z), Fita.transform.rotation);
            geraFita = 0;
        }

        
    }

    void RetornaCarro()
    {


        GameObject veiculos = (GameObject)Instantiate(carros[Random.Range(0,carros.Count)], pos[Random.Range(0, pos.Count)].position, pos[0].rotation);


    }



}
