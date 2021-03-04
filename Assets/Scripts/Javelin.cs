using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : PhysicsObject
{
    private bool frozen;

    // Start is called before the first frame update
    void Start() {
        frozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyJavelin() {
        Destroy(gameObject);
    }

    protected override void Movement(Vector3 move, bool yMovement) {

        // overrided because javelins do not need to care about being 
        // grounded, and need to deal with collision differently

        float distance = move.magnitude;

        if(frozen) {
            distance = 0;
        }

        if (distance > minMoveDistance) {

            bool collide = rb.SweepTest(move, out hitInfo, distance + shellRadius, QueryTriggerInteraction.Ignore);

            // need to use hitInfo at some point to check the layer of the 
            // object hit, so that we can see whether or not it's an enemy 
            // or another javelin. also, we need a way to notify 
            // PlayerQuillThrower to remove it from the javelinList

            if (collide) {

                frozen = true;

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
}
