using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraLateral : MonoBehaviour
{

    [SerializeField] float forcaBarreira;

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("oi");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.right * forcaBarreira * Time.deltaTime);
        }
    }
}
