using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour



{


    public GameObject player;

    public float offset;
    // Start is called before the first frame update
    void Start()
    {

        offset = transform.position.z - player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y,offset + player.transform.position.z);
    }
}
