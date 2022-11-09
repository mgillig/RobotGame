using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
    PLAYER MOVEMENT CONTROLLER
    Controls Torso rotation: tied to camera angle controlled by cinemachine object
    Controls player forward movement
    Controls player rotation
*/

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController controller;
    public CinemachineFreeLook freeLook;
    public Transform cam;
    public Animator characterAnimations;
    public GameObject torso;
    public float speed = 6f;
    public float turnSmoothTime = 0.01f;
    private float freeLookYRotation;
    private float torsoRotation;
    private float turnSmoothVelocity;
    
    void Update()
    {
    //TORSO ROTATION
    
        //getting FreeLook Y-Value from Cinemachine object
        freeLookYRotation = freeLook.m_YAxis.Value;
        //translating freelook rotation to torso rotation
        torsoRotation = 90 - ((120 * freeLookYRotation) - 60);
        //locking torso rotation at a max of 90
        if(torsoRotation > 90)
        {
            torsoRotation = 90;
        }
        //assining torso rotation to transform
        torso.transform.localRotation = Quaternion.Euler(torsoRotation, 0f, 0f);


    //PLAYER MOVENET

        //takes input for forward movement
        //the code will not allow the player to move backwards
        float verticalInput = Input.GetAxisRaw("Vertical");

        //player is always facing forward in relation to camera
        //camera is rotated using Input.GetAxisRaw("Horizontal").  See CM FreeLook1 object
        //takes current camera angle in degrees
        float targetAngle = cam.eulerAngles.y;
        //smoothly transitions player from it's current angle to the target angle (camera angle)
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //rotates player
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //activates the walk animation when moving forward
        characterAnimations.SetBool("Walking", verticalInput >= 0.1f);
        if (verticalInput >= 0.1f)
        {
            //translates the angle the player is facing from a float to a vector 3
            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            //moves player in direction it is facing
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}