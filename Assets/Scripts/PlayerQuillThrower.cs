using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuillThrower : MonoBehaviour
{
    public GameObject projectile;
    public float throwInterval = .2f;
    public float projectileSpeed = 20f;
    public GameObject player;

    private float timeSinceThrow = 0f;
    private List<Javelin> javelinList = new List<Javelin>();

    // Start is called before the first frame update
    void Start()
    {
        javelinList.Clear();
        timeSinceThrow = 0f;
    }

    // Update is called once per frame
    void Update() {
        timeSinceThrow += Time.deltaTime;

        if(Input.GetButtonDown("Fire")) {
            ThrowQuill();
        }
    }

    private void ThrowQuill() {
        int count = javelinList.Count;
        // ensure that there are no more than 3 quills on screen at once-
        // if there are 3 or more, remove until there are 2, so new quill becomes third
        if(count >= 3) {
            for(int i = count; i <= 3; i--) {
                Javelin toDie = javelinList[0];
                javelinList.RemoveAt(0);
                toDie.destroyJavelin();
            }
        }

        // instantiate and set up quill
        GameObject go = Instantiate<GameObject>(projectile);
        go.layer = LayerMask.NameToLayer("PlayerProjectile");
        go.transform.position = player.transform.position + new Vector3(0f, 1.5f, 0f);
        timeSinceThrow = 0f;


    }
}
