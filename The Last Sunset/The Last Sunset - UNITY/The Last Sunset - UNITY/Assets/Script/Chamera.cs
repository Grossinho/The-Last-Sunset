using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamera : MonoBehaviour
{


    float offset;

    public GameObject camer;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.z - camer.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3( transform.position.x,transform.position.y, offset + camer.transform.position.z );
    }
}
