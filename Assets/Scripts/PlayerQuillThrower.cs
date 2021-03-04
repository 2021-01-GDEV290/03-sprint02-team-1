using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuillThrower : MonoBehaviour
{
    public GameObject projectile;
    public float throwInterval = .2f;
    //public float projectileSpeed = 20f;
    public GameObject player;
    public Vector3 test;

    //[SerializeField]
    private float timeSinceThrow = 0f;
    //[SerializeField]
    private List<GameObject> javelinList = new List<GameObject>();
    //[SerializeField]
    private Vector3 throwPosition;

    // Start is called before the first frame update
    void Start()
    {
        javelinList.Clear();
        timeSinceThrow = 0f;
    }

    // Update is called once per frame
    void Update() {
        timeSinceThrow += Time.deltaTime;
        throwPosition = player.transform.position + new Vector3(0f, .5f, 0f);

        if (Input.GetButtonDown("Fire1")) {
            //Debug.Log("Fire1 clicked");
            ThrowQuill();
        }
    }

    private void ThrowQuill() {

        if(timeSinceThrow < throwInterval) {
            return;
        }

        int count = javelinList.Count;
        // ensure that there are no more than 3 quills on screen at once-
        // if there are 3 or more, remove until there are 2, so new quill becomes third
        if(count >= 3) {
            for(int i = count; i >= 3; i--) {
                GameObject toDie = javelinList[0];
                javelinList.RemoveAt(0);
                Destroy(toDie);
            }
        }
        //Debug.Log("count is " + count);

        // instantiate and set up quill
        GameObject go = Instantiate<GameObject>(projectile);
        javelinList.Add(go);
        go.layer = LayerMask.NameToLayer("PlayerProjectile");
        go.transform.position = throwPosition;
        timeSinceThrow = 0f;

        // screw it, let the javelin throw itself on instantiation

        //// read in mouse position so we can fire quiils at it
        //Vector3 mousePos2D = Input.mousePosition;
        //mousePos2D.z = -Camera.main.transform.position.z;
        //Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        
        //// Find the delta from the launchPos to the mousePos3D
        //Vector3 launchDirection =  -throwPosition - -mousePos3D;
        //launchDirection.Normalize();

        //Javelin javelinScript = projectile.GetComponent<Javelin>();
        //if(javelinList != null) {
        //    javelinScript.SetVelocity(launchDirection * projectileSpeed);
        //}

        //Debug.Log("Projectile's initial velocity is " + javelinScript.)

        //if(projectile is Javelin jav) {
        //    jav.velocity = launchDirection.Normalized();
        //} else {
        //    Debug.Log("Tried to set projectile's velocity, projectile is not a javelin");
        //}
        
    }
}
