using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public GameObject target;
    public GameObject button;
    public GameObject basePlate;

    public Transform playerCheck;
    public LayerMask playerMask;

    private Vector3 plateDimension;
    private bool active;
    private Renderer triggerRenderer;

    //public Material onMaterial;
    //public Material offMaterial;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        plateDimension = basePlate.transform.localScale;
        triggerRenderer = button.GetComponent<Renderer>();
        triggerRenderer.material.SetColor("_Color", new Color(0f, 0f, 1f, .2f));
    }

    // Update is called once per frame
    void Update()
    {
        bool detected = Physics.CheckBox(playerCheck.position, plateDimension, new Quaternion(), playerMask);

        if(!active && detected) {
            Activate();
        } else if(active && !detected) {
            Deactivate();
        }
    }

    void Activate() {
        //target.activate(); //put in here the method we want to be accomplished when the button is pressed
        active = true;
        triggerRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f, .2f));
        //Debug.Log("Active");
    }

    void Deactivate() {
        active = false;
        triggerRenderer.material.SetColor("_Color", new Color(0f, 0f, 1f, .2f));
        //Debug.Log("Inactive");
    }
}
