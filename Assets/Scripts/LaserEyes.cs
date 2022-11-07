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
        if(Input.GetAxisRaw("Lazer") > 0.001)
        {
            characterAnimations.SetBool("Lazer", true);
            LaserBeam.SetActive(true);
            RaycastHit hit;
            Ray faceLazer = new Ray(laserOrigin.position, laserOrigin.forward + new Vector3(0f, -0.1f, 0f));
            Debug.DrawRay(faceLazer.origin, faceLazer.direction, Color.red, 1f, false);
            if (Physics.Raycast(faceLazer.origin, faceLazer.direction, out hit, 20f, LayerMask.GetMask("Destroyable"), QueryTriggerInteraction.Ignore))
            {
                hit.transform.gameObject.GetComponent<Destruction>().Explosion(faceLazer.direction * 10f, hit.point);
            }
        }
        else
        {
            LaserBeam.SetActive(false);
            characterAnimations.SetBool("Lazer", false);
        }
    }
}
