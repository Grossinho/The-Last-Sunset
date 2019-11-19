using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroNpc : MonoBehaviour
{
    Rigidbody rgb;
    [SerializeField] private float velo;


    // Start is called before the first frame update
    void Start()
    {
        
        rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
            rgb.velocity = transform.forward *  velo;
      
    }
}
