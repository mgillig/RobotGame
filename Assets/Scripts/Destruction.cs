using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    
    public float punchStrength;
    public Rigidbody destructionPhysics;
    public Animator despawnAnimation;
    public bool grabbable = false;


    private float destructionCooldown = 2f;
    private bool isDestroyed = false;
    


    private void Update()
    {
        //sets isDestroyed to true if the parent object is moved at a velocity of 2 or greater
        //Once isDestroyed is true it will never be false
        if(!isDestroyed && !grabbable && destructionPhysics.velocity.magnitude >= 2f)
        {
            isDestroyed = true;
        }
        
        //despawn object after a period of time when isDestroyed and the object stopped moving.  
        if (isDestroyed && destructionPhysics.velocity.magnitude == 0f )
        {
            if (destructionCooldown > 0f)
            {
                destructionCooldown -= Time.deltaTime;
            }
            else
            {
                despawnAnimation.SetTrigger("ActivateDespawn");
            }

        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        PlayerArmController grabber = other.GetComponentInParent<PlayerArmController>();
        if(grabbable && grabber != null && grabber.isGrabbing) 
        {
            grabber.Grab(gameObject, other.CompareTag("Right"));
        }
        else
        {
            Explosion(other.GetComponent<Rigidbody>().velocity, other.GetComponent<Transform>().position);
        }
    }

    //adds force to the rigid body of the GameObject based on the velocity and exact position of the fist at the moment of collision.  
    public void Explosion(Vector3 hitVelocity, Vector3 hitPosition)
    {
        Vector3 hitForce = new Vector3(hitVelocity.x * punchStrength, Mathf.Abs(hitVelocity.y) * (punchStrength / 2), hitVelocity.z * punchStrength);
        destructionPhysics.AddForceAtPosition(hitForce, hitPosition, ForceMode.Force);
    }

    

}
