using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator characterAnimations;

    public float speed = 6f;
    public float turnSmoothTime = 0.01f;
    private float turnSmoothVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //takes input for forward movement
        float vertical = Input.GetAxisRaw("Vertical");
        //just forward.  no backward.
        if(vertical >= 0)
        {
            //stolen from Brackeys
            Vector3 direction = new Vector3(0f, 0f, vertical).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (direction.magnitude >= 0.1f)
            {
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }
        }

        //triggers right punch on right click.
        if(Input.GetAxisRaw("RightPunch") > 0.001)
        {
            characterAnimations.SetTrigger("TrRightPunch");
        }
    }
}
