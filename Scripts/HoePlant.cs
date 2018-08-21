using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoePlant : MonoBehaviour {
    grabObject my_self_grab;
    MeshCollider mc;
    [SerializeField] float hoespeed = -2;
    [SerializeField] SteamVR_TrackedObject trackedObj;
    [SerializeField]
    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }
    // Use this for initialization
    void Start () {
        my_self_grab = GetComponent<grabObject>();
        mc = GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (my_self_grab.isGrab)
        {
            mc.isTrigger = true;
        }
        else
        {
            mc.isTrigger = false;
        }
      
	}
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag=="Plant"&&Controller.velocity.y<hoespeed)
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_hoe);
            NotificationCenter.DefaultCenter().PostNotification(this, "ControllerShake", false);
            other.transform.parent.GetComponent<VegGrowth>().SetHasVeg(false);
            Destroy(other.gameObject);
        }
    }
 }
    


