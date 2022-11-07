using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEyes : MonoBehaviour
{
    public Animator characterAnimations;
    public GameObject LazerBeam;
    public Transform lazerOrigin;
    
    
    void Update()
    {
        if(Input.GetAxisRaw("Lazer") > 0.001)
        {
            characterAnimations.SetBool("Lazer", true);
            LazerBeam.SetActive(true);
            RaycastHit hit;
            Ray faceLazer = new Ray(lazerOrigin.position, lazerOrigin.forward + new Vector3(0f, -0.1f, 0f));
            Debug.DrawRay(faceLazer.origin, faceLazer.direction, Color.red, 1f, false);
            if (Physics.Raycast(faceLazer.origin, faceLazer.direction, out hit, 20f, LayerMask.GetMask("Destroyable"), QueryTriggerInteraction.Ignore))
            {
                hit.transform.gameObject.GetComponent<Destruction>().Explosion(faceLazer.direction * 10f, hit.point);
            }
        }
        else
        {
            LazerBeam.SetActive(false);
            characterAnimations.SetBool("Lazer", false);
        }
    }
}
