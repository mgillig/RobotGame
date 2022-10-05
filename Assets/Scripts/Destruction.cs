using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public bool isGrabbed;
    public bool isDestroyed = false;
    public float punchStrength;
    public Rigidbody destructionPhysics;
    public Animator despawnAnimation;
    public Transform grabTransform;


    private float destructionCooldown = 2f;
    


    private void Update()
    {
        //determine isDestroyed
        if(!isGrabbed && !isDestroyed && destructionPhysics.velocity.magnitude > 2f)
        {
            isDestroyed = true;
        }
        
        //despawn when isDestroyed
        if (!isGrabbed && isDestroyed && destructionPhysics.velocity.magnitude == 0f )
        {
            //GetComponent<BoxCollider>().isTrigger = false;
            //print(destructionCooldown);
            if (destructionCooldown > 0f)
                {
                    destructionCooldown -= Time.deltaTime;
                }
                else
                {
                    despawnAnimation.SetTrigger("ActivateDespawn");
                }

        }
        if(isGrabbed && grabTransform != null)
        {
            transform.position = grabTransform.position;
            transform.rotation = grabTransform.rotation;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if(Input.GetAxisRaw("Grab") > 0.001)
        {
            other.GetComponentInParent<RobotController>().Grab(this.gameObject, other.CompareTag("Right"));
            isGrabbed = true;
        }
        else
        {
            Explosion(other.GetComponent<Rigidbody>().velocity, other.GetComponent<Transform>().position);
        }
    }

    public void Explosion(Vector3 hitVelocity, Vector3 hitPosition)
    {
        Vector3 hitForce = new Vector3(hitVelocity.x * punchStrength, Mathf.Abs(hitVelocity.y) * (punchStrength / 2), hitVelocity.z * punchStrength);
        destructionPhysics.AddForceAtPosition(hitForce, hitPosition, ForceMode.Force);
    }

    

}
