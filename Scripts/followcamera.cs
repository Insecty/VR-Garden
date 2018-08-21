using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcamera : MonoBehaviour {

    // Use this for initialization
    [SerializeField] Transform eye;
    Vector3 relative = new Vector3(0, 0, 0);
	void Start () {
        relative = eye.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
       
        transform.position = eye.position - relative;
	}
}
