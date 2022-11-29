using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
PLAYER ARM CONTROLLER
    triggers right and left punch on player input
    controlls grab and throw (to be implemented later)
*/

public class PlayerArmController : MonoBehaviour
{
    public Animator characterAnimations;
    
    
    public bool isGrabbing = false;
    private Fist right;
    private Fist left;
    private GameObject grabbedObject;

    void Start()
    {
        right = new Fist(true, GameObject.Find("Bone_R.002_end").transform);
        left = new Fist(false, GameObject.Find("Bone_L.002_end").transform);
    }
    
    void Update()
    {
        isGrabbing = Input.GetAxisRaw("Grab") > 0;
        
        //triggers right arm action
        if(Input.GetMouseButtonUp(1))
        {
            characterAnimations.SetTrigger(right.fistAction());
        }
        //triggers left arm action
        else if(Input.GetMouseButtonUp(0))
        {
            characterAnimations.SetTrigger(left.fistAction());
        }
        else if((Input.GetMouseButtonDown(0) && left.grab) || (Input.GetMouseButtonDown(1) && right.grab))
        {
            //shows throw arch
        }
    }

    public void Grab(GameObject grabbedObjectIn, bool isRightFist)
    {
        //disables the collider of the grabbed object to prevent collision with the body of the player.  
        //grabbedObject.GetComponent<BoxCollider>().enabled = false;
        grabbedObject = grabbedObjectIn;
        if (isRightFist)
        {
            //print("right grab");
            right.grab = true;
            grabbedObject.transform.SetParent(right.fistLocation);
        }
        else
        {
            //print("left grab");
            left.grab = true;
            grabbedObject.transform.SetParent(left.fistLocation);
        }
        grabbedObject.GetComponent<Collider>().attachedRigidbody.isKinematic = true;
        grabbedObject.GetComponent<Collider>().enabled = false;
        grabbedObject.GetComponent<Animator>().enabled = false;
        grabbedObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        grabbedObject.transform.localPosition = Vector3.zero;
    }

    public void Throw()
    {
        if(grabbedObject != null)
        {
            //print("I AM THROWN");
            grabbedObject.GetComponent<Collider>().enabled = true;
            grabbedObject.GetComponent<Collider>().attachedRigidbody.isKinematic = false;
            grabbedObject.transform.SetParent(null);
        }
    }

}