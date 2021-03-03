using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerFollowCamera : MonoBehaviour
{

    public Transform followObject;
    public float smoothSpeed = .01f;

    private Vector3 smoothPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(followObject.position.x, followObject.position.y, transform.position.z);
        smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
