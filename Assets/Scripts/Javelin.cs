using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : PhysicsObject
{   
    public float projectileSpeed;
    public float rotationSpeed;
    public GameObject player;
    public float minAge;

    [SerializeField]
    private Vector3 throwPosition;
    private bool frozen = false;
    private float age;

    // Start is called before the first frame update
    void Start() {
        frozen = false;

        age = 0f;

        throwPosition = transform.position;

        // read in mouse position so we can fire this quiil at it
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Find the delta from the launchPos to the mousePos3D
        Vector3 launchDirection = -throwPosition - -mousePos3D;
        launchDirection.Normalize();
        launchDirection *= projectileSpeed;

        velocity.y = launchDirection.y;
        velocity.x = launchDirection.x;
        targetVelocity = launchDirection;

        //Debug.Log("launchDirection is " + launchDirection);
        //Debug.Log("Projectile's velocity is " + velocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        age += Time.deltaTime;

        grounded = false;

        Vector3 positionChange = velocity * Time.deltaTime;

        //Vector3 moveAlongGround = new Vector3(groundNormal.y, -groundNormal.x, 0f);

        Vector3 move = new Vector3(1f, 0f, 0f) * positionChange.x;

        Movement(move, false);

        move = Vector3.up * positionChange.y;

        Movement(move, true);

        //targetVelocity = Vector3.zero;
        //ComputeVelocity();
        //Debug.Log("Projectile's velocity is " + velocity);
    }

    public void destroyJavelin() {
        Destroy(gameObject);
    }

    protected override void Movement(Vector3 move, bool yMovement) {

        // overrided because javelins do not need to care about being 
        // grounded, and need to deal with collision differently

        float distance = move.magnitude;

        if(frozen && age >= minAge) {
            velocity.y = 0;
            return;
        }

        // quill rotation through air
        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //                                    Quaternion.LookRotation(velocity),
        //                                    Time.deltaTime * rotationSpeed);
        transform.LookAt(transform.position + velocity);

        if (distance > minMoveDistance) {

            bool collide = rb.SweepTest(move, out hitInfo, distance + shellRadius, QueryTriggerInteraction.Ignore);

            // need to use hitInfo at some point to check the layer of the 
            // object hit, so that we can see whether or not it's an enemy 
            // or another javelin. also, we need a way to notify 
            // PlayerQuillThrower to remove it from the javelinList

            if (collide && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) {

                frozen = true;

                //gameObject.layer = LayerMask.NameToLayer("ground");

                //float projection = Vector3.Dot(velocity, currentNormal);

                //if (projection < 0) {
                //    velocity = velocity - projection * currentNormal;
                //}

                float modifiedDistance = hitInfo.distance - shellRadius;

                if (distance > modifiedDistance)
                {
                    distance = modifiedDistance;
                }
            }

            rb.position = rb.position + move.normalized * distance;
        }
    }

    public void SetVelocity(Vector3 vel) {
        velocity = vel;
        targetVelocity = vel;
        //Debug.Log("Velocity & tV set to " + velocity);
    }
}
