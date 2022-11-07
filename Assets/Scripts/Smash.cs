using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.position -= new Vector3(0f, 5f, 0f);
    }
}
