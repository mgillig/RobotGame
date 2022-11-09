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
    private bool grabR = false;
    private bool grabL = false;
    
    void Update()
    {
        isGrabbing = Input.GetAxisRaw("Grab") > 0;
        
        //triggers right arm action
        if(Input.GetAxisRaw("RightPunch") > 0.001)
        {
            //if the player is grabbing an object with the right arm it will throw that object
            if(grabR)
            {
                //throw grabbed object
                //implement later
            }
            //if the player is not currently grabbing an object with the right arm the right punch animation will trigger
            //if isGrabbing is true the right arm will "grab" an object it collides with during the punch animation if that object is grabbable.
            else
            {
                characterAnimations.SetTrigger("TrRightPunch");
            }
        }
        //triggers left punch
        else if(Input.GetAxisRaw("LeftPunch") > 0.001)
        {
            //if the player is grabbing an object with the left arm it will throw that object
            if(grabL)
            {
                //throw grabbed object
                //implement later
            }
            //if the player is not currently grabbing an object with the left arm the left punch animation will trigger
            //if isGrabbing is true the left arm will "grab" an object it collides with during the punch animation if that object is grabbable.
            else
            {
                characterAnimations.SetTrigger("TrLeftPunch");
            }
        }
    }

    //incomplete
    public void Grab(GameObject grabbedObject, bool isRightFist)
    {
        //disables the collider of the grabbed object to prevent collision with the body of the player.  
        //grabbedObject.GetComponent<BoxCollider>().enabled = false;

        if (isRightFist)
        {
            print("right grab");
            //grabR = true;
            //grabbedObject.GetComponent</*grabbable>().grabTransform = GameObject.Find("Bone_R.002_end").transform;
        }
        else
        {
            print("left grab");
            //grabL = true;
            //grabbedObject.GetComponent</*grabbable>().grabTransform = GameObject.Find("Bone_L.002_end").transform;
        }

    }
}