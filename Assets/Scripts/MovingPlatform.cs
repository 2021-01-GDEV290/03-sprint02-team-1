using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float speed = 3;
    public float pauseTime = 1;
    public Vector3 endPosition;
    public Rigidbody rb;
    public bool away;
    public bool paused;

    private Vector3 lastPosition;
    private float pausedTime;
    private Vector3 startPosition;
    private Vector3 target;

    //public Vector3[] nodes;
    //private int currentNode;
    //private int nextNode;

    // Start is called before the first frame update
    void Start() {
        //currentNode = 0;
        //nextNode = 1;
        startPosition = transform.position;
        away = true;
        paused = false;
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    // bug on instantiation, first move goes wildly off track. 
    // afterwards, platform works perfectly fine.
    // probably has something to do with local vs global positions
    void Move() {
        if(!paused) {
            lastPosition = transform.position;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if(lastPosition == transform.position) {
                paused = true;
                pausedTime = 0;
            }
        } else {
            pausedTime += Time.deltaTime;
            if(pausedTime >= pauseTime) {
                paused = false;
                if(away) {
                    target = endPosition;
                } else {
                    target = startPosition;
                }
                away = !away;
            }
        }
        

    }
}
