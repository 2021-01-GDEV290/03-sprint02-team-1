using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer2DPlayerController : PhysicsObject {

    public float jumpTakeOffSpeed = 5f;
    public float maxSpeed = 7f;
    public float jumpMinTime = .25f;
    public bool groundJump = true;
    public bool midAirJump = true;

    [SerializeField] 
    private float timeSinceJump = 0f;
    private bool jumpReleased = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ComputeVelocity() {

        Vector3 move = Vector3.zero;

        timeSinceJump += Time.deltaTime;

        move.x = Input.GetAxis("Horizontal");

        if(grounded) {
            groundJump = true;
            midAirJump = true;
        } else {
            groundJump = false;
        }

        if(Input.GetButtonDown("Jump") || Input.GetKeyDown("w")) {
            if(groundJump || midAirJump) {
                timeSinceJump = 0f;
                jumpReleased = false;
                velocity.y = jumpTakeOffSpeed;
                if(groundJump) {
                    groundJump = false;
                } else {
                    midAirJump = false;
                }
            }
        }
        
        if(Input.GetButtonUp("Jump") || Input.GetKeyUp("w")) {
            jumpReleased = true;
        }
        
        if(timeSinceJump >= jumpMinTime && jumpReleased) {
            jumpReleased = false;
            if(velocity.y > 0f) {
                velocity.y *= .5f;
            }
        }

        //Debug.Log("GJ: " + groundJump + ", MAJ: " + midAirJump);
        //Debug.Log(grounded);

        targetVelocity = move * maxSpeed;
    }
}
