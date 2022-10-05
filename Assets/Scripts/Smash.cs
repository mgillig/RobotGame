using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    private bool isSmashed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<RobotController>().isSmashing)
        {
            transform.position -= new Vector3(0f, 5f, 0f);
            print("I AM SMASHED");
        }
    }
}
