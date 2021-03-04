using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerFollowCamera : MonoBehaviour
{

    public Transform followObject;
    public float smoothSpeed = .01f;
    public float mouseDistancePercent = .4f;

    private Vector3 smoothPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(followObject.position.x, followObject.position.y, transform.position.z);

        // read in mouse position so we can fire this quiil at it
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 direction = mousePos3D - followObject.position;
        targetPosition.x += mouseDistancePercent * direction.x;
        targetPosition.y += .33f * mouseDistancePercent * direction.y;

        smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
