using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {
    Vector3 iniposition;
    Quaternion inirotation;
    grabObject my_grab;
    bool ini_position = true;
    [SerializeField]Transform righthand;
    [SerializeField] float threshold=3;
    // Use this for initialization
    private void Awake()
    {
        my_grab = GetComponent<grabObject>();
        iniposition = transform.position;
        inirotation = transform.rotation;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ini_position)
        {
            transform.position = iniposition;
            transform.rotation = inirotation;
        }
        if (my_grab.isGrab)
        {
            ini_position = false;
        }
        if (!my_grab.isGrab)
        {
            if (!ini_position)
            {
                float distance = Vector3.Distance(transform.position,righthand.position);
                //Debug.Log(distance + "distance");
                if (distance > threshold)
                {
                    Debug.Log(distance+"distance");
                    transform.position = iniposition;
                    transform.rotation = inirotation;
                    ini_position = true;
                }
            }
        }
	}

}
