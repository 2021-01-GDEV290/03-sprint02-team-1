using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {


}


public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -5f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    private Vector3 velocity;
    private float runSpeed;
    private bool isGrounded;
    private bool isSprinting;

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y <= 0) {
            velocity.y = -5f;
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        //needs to make sure we're not crouching when crouching is implemented
        if(isSprinting) {
            runSpeed = speed * 2f;
            //Debug.Log("sprinting");
        } else {
            runSpeed = speed;
            //Debug.Log("walking");
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * runSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime; //velocity is force of gravity over time

        controller.Move(velocity * Time.deltaTime); //delta y equation multiplies by time squared, so second application of time
    }
}
