using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] Light farolV, farolA;
    [SerializeField]float velo;
    Rigidbody rgb;
 
    void Start()
    {
        velo = 40f;
        StartCoroutine(Pisca());
        rgb = GetComponent<Rigidbody>();
    }

 
    void Update()
    {
        velo += 1f * Time.deltaTime;
        rgb.velocity = transform.forward * velo;

        if (velo >= 150)
            velo = 150;

        if (farolV.enabled)
        {
            farolA.enabled = false;
        }
        else farolA.enabled = true;

        transform.position = new Vector3(player.position.x, transform.position.y , transform.position.z); 
    }


    IEnumerator Pisca()
    {
        yield return new WaitForSeconds(0.3f);
        farolV.enabled = !farolV.enabled;
        StartCoroutine(Pisca());
    }
}
