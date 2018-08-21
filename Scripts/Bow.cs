using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
public class Bow : MonoBehaviour {
    [SerializeField] GameObject bullet;
    [SerializeField] Transform Dir;
    [SerializeField] bool isGrabed = false;
    [SerializeField] float speed = 10;
    //[SerializeField] Vector3 offset=(;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(Dir.position, Dir.forward);
        if (transform.parent!=null&&transform.parent.tag == "righthand")
        {
           if( ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.HairTrigger)){
                NotificationCenter.DefaultCenter().PostNotification(this, "ControllerShake",false);
                GameFacade.Instance.PlayNormalSound(AudioManager.Sound_shoot);
                GameObject temp = GameObject.Instantiate(bullet);
                GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ArrowShoot);
                temp.transform.position = Dir.position;
                temp.transform.forward = -this.transform.up;
                temp.GetComponentInChildren<Rigidbody>().velocity = Dir.forward * speed;
            }
        }
	}
}
