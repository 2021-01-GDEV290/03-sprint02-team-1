using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer2DPlayerController : PhysicsObject {

    public float jumpTakeOffSpeed = 5f;
    public float maxSpeed = 7f;
    public bool groundJump = true;
    public bool midAirJump = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ComputeVelocity() {

        Vector3 move = Vector3.zero;

        move.x = Input.GetAxis("Horizontal");

        if(grounded) {
            groundJump = true;
            midAirJump = true;
        } else {
            groundJump = false;
        }

        if(Input.GetButtonDown("Jump")) {
            if(groundJump || midAirJump) {
                velocity.y = jumpTakeOffSpeed;
                if(groundJump) {
                    groundJump = false;
                } else {
                    midAirJump = false;
                }
            }
        } else if(Input.GetButtonUp("Jump")) {
            if(velocity.y > 0f) {
                velocity.y *= .5f;
            }
        }

        //Debug.Log("GJ: " + groundJump + ", MAJ: " + midAirJump);
        //Debug.Log(grounded);

        targetVelocity = move * maxSpeed;
    }
}
