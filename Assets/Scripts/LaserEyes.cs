using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyes : MonoBehaviour
{
    public Animator characterAnimations;
    public GameObject LaserBeam;
    public Transform laserOrigin;
    
    
    void Update()
    {
        //takes input for face laser
        if(Input.GetAxisRaw("Laser") > 0.001)
        {
            //turns on laser animation (beams spin)
            characterAnimations.SetBool("Laser", true);
            //makes laser beam visible
            LaserBeam.SetActive(true);
            
            RaycastHit hit;
            Ray faceLaser = new Ray(laserOrigin.position, laserOrigin.forward + new Vector3(0f, -0.1f, 0f));
            //creates a raycast using the faceLaser object and returns true if a "destroyable" object (a game object set in the destroyable layer) is detected in the range of the laser.  
            //RaycastHit hit is assigned to point to that object.
            //parameters of Physics.Raycast: Vector3 origin, Vector3 direction (both defined in the faceLaser object),  out RaycastHit hitInfo, float maxDistance, int layerMask,  QueryTriggerInteraction (n/a)
            if (Physics.Raycast(faceLaser.origin, faceLaser.direction, out hit, 20f, LayerMask.GetMask("Destroyable"), QueryTriggerInteraction.Ignore))
            {
                //applies force to the object that is hit by the laser.  That is done by calling the Explosion method of the destruction class attached to that object.
                hit.transform.gameObject.GetComponent<Destruction>().Explosion(faceLaser.direction * 10f, hit.point);
            }
        }
        //turns off laser when button released
        else
        {
            LaserBeam.SetActive(false);
            characterAnimations.SetBool("Laser", false);
        }
    }
}
