using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    private void OnBecameInvisible()
    {

        Destroy(this.gameObject);
    }
}
