using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RobotController : MonoBehaviour
{
    public CharacterController controller;
    public CinemachineFreeLook freeLook;
    public Transform cam;
    public Animator characterAnimations;
    public GameObject torso;
    public Transform lazerOrigin;
    public GameObject LazerBeam;

    public bool grabR = false;
    public bool grabL = false;
    
    public bool isSmashing = false;
    public float speed = 6f;
    public float turnSmoothTime = 0.01f;
    private float freeLookYRotation;
    private float torsoRotation;
    private float turnSmoothVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        freeLook.m_YAxis.Value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Torso Rotation
        //getting FreeLook Y-Value from Cinemachine object
        freeLookYRotation = freeLook.m_YAxis.Value;
        //translating freelook rotation to torso rotation
        torsoRotation = 90 - ((120 * freeLookYRotation) - 60);
        //locking torso rotation at a max of 90
        if(torsoRotation > 90)
        {
            torsoRotation = 90;
        }
        //assining torso rotation
        torso.transform.localRotation = Quaternion.Euler(torsoRotation, 0f, 0f);

        
        
        
        //takes input for forward movement
        float vertical = Input.GetAxisRaw("Vertical");
        //just forward.  no backward.
        if (vertical >= 0)
        {
            //stolen* from Brackeys
            Vector3 direction = new Vector3(0f, 0f, vertical).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            characterAnimations.SetBool("Walking", direction.magnitude >= 0.1f);

            if (direction.magnitude >= 0.1f)
            {
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }
        }


        //triggers right punch on right click.
        if(Input.GetAxisRaw("RightPunch") > 0.001)
        {
            if(grabR)
            {

            }
            else
            {
                characterAnimations.SetTrigger("TrRightPunch");
            }
        }
        else if(Input.GetAxisRaw("LeftPunch") > 0.001)
        {
            if(grabL)
            {

            }
            else
            {
                characterAnimations.SetTrigger("TrLeftPunch");
            }
        }

        //FaceLazer
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

    public void Grab(GameObject grabbedObject, bool isRightFist)
    {
        grabbedObject.GetComponent<BoxCollider>().enabled = false;

        if (isRightFist)
        {
            grabR = true;
            grabbedObject.GetComponent<Destruction>().grabTransform = GameObject.Find("Bone_R.002_end").transform;
        }
        else
        {
            grabL = true;
            grabbedObject.GetComponent<Destruction>().grabTransform = GameObject.Find("Bone_L.002_end").transform;
        }

    }
}
