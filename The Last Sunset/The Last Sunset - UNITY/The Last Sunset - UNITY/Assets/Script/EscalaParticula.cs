using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EscalaParticula : MonoBehaviour
{
    ParticleSystem ad;

    [ExecuteInEditMode]

    private void Start()
    {
        ad = GetComponent<ParticleSystem>();
    }
    void Update()
    {
              ad.startSize = transform.lossyScale.magnitude;
    } 
    
}