using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;
    public Rigidbody rb;

    protected Vector3 targetVelocity;
    protected bool grounded;
    protected Vector3 groundNormal;
    protected Vector3 velocity;
    
    protected RaycastHit hitInfo;               // use the RaycastHit to get info about the collision
    protected float minMoveDistance = 0.001f;
    protected float shellRadius = 0.001f;         // padding to make sure object doesnt clip into anything else
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        targetVelocity = Vector3.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity() {

    }

    void FixedUpdate() {
        velocity += gravityModifier * Physics.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector3 positionChange = velocity * Time.deltaTime;

        Vector3 moveAlongGround = new Vector3(groundNormal.y, -groundNormal.x, 0f);

        Vector3 move = moveAlongGround * positionChange.x;

        Movement(move, false);
        
        move = Vector3.up * positionChange.y;

        Movement(move, true);

        //targetVelocity = Vector3.zero;
        //ComputeVelocity();
    }

    void Movement(Vector3 move, bool yMovement) {
        float distance = move.magnitude;

        if(distance > minMoveDistance) {

            bool collide = rb.SweepTest(move, out hitInfo, distance + shellRadius, QueryTriggerInteraction.Ignore);

            if (collide) {

                Vector3 currentNormal = hitInfo.normal;     // check whether or not object is grounded

                if (currentNormal.y > minGroundNormalY) {
                    grounded = true;

                    if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                } else {
                    grounded = false;
                }

                //Debug.Log("grounded is " + grounded);
            
                float projection = Vector3.Dot(velocity, currentNormal);

                if (projection < 0) {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitInfo.distance - shellRadius;

                if (distance > modifiedDistance) {
                    distance = modifiedDistance;
                }
            }

            rb.position = rb.position + move.normalized * distance;
        }

        
    }
}

