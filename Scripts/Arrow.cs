using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enermy")
        {
            collision.gameObject.GetComponent<EnermyHealth>().TakeDamage(40);
        }
        Destroy(this.gameObject, 0.7f);
    }
    private void OnDestroy()
    {
        
    }
}
