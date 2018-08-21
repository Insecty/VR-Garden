using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObject : MonoBehaviour {
    Rigidbody m_Rigidbody;
    public bool isGrab = false;
    private bool isPutBack = false;
    private float lastTime = 0;
    // Use this for initialization
    private Vector3 lastpos;
    private Transform parent;
    void Start () {
        parent = transform.parent;
        m_Rigidbody = GetComponent<Rigidbody>();
       
    }
	
	// Update is called once per frame
	void Update () {
		if(isPutBack && Time.time - lastTime > 10)
        {
            lastTime = Time.time;
            m_Rigidbody.AddForce(Random.Range(0,5), 10, Random.Range(0, 5));
        }
	}
    private void LateUpdate()
    {
        
    }
    void Grab(Rigidbody rig)
    {
        m_Rigidbody.velocity = new Vector3(0,0,0);
        m_Rigidbody.constraints =  RigidbodyConstraints.FreezeAll;
      
        isPutBack = false;
        isGrab = true;
    }
    void PutBack(SteamVR_Controller.Device Controller)
    {
        transform.parent = parent;
        isPutBack = true;
        m_Rigidbody.constraints = RigidbodyConstraints.None;
        lastTime = Time.time;
        m_Rigidbody.velocity=Quaternion.AngleAxis(-90, Vector3.up) * Controller.velocity*3;
       
        
        m_Rigidbody.angularVelocity = Quaternion.AngleAxis(-90, Vector3.up) * Controller.angularVelocity;
        
        isGrab = false;
    }

}
