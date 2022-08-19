using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public bool selected;


    private void Update()
    {
        if(selected)
        {
            //do something
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        this.transform.position = this.transform.position - new Vector3(0f, this.transform.localScale.y, 0f);
    }

}
