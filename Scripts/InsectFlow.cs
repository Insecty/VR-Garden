using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
public class InsectFlow : MonoBehaviour {
    [SerializeField] ParticleSystem InsWater;
    grabObject my_selfGrab;
    bool play = false;
    bool isGrab = false;
	void Start () {
        my_selfGrab = GetComponent<grabObject>();
	}

    void Update()
    {
        if (transform.parent != null && transform.parent.tag == "righthand")
        {
            isGrab = true;
            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.HairTrigger))
            {

                if (!InsWater.isPlaying)
                {

                    NotificationCenter.DefaultCenter().PostNotification(this, "ControllerShake", false);
                    GameFacade.Instance.PlayNormalLoopSound(AudioManager.Sound_spray);
                    InsWater.Play();

                }
            }
            else
            {
                GameFacade.Instance.StopPlayLoopSound();
                InsWater.Stop();
            }
        }
        else
        {
            if(isGrab==true)
            {
                GameFacade.Instance.StopPlayLoopSound();
                isGrab = false;
            }
           // GameFacade.Instance.StopPlayLoopSound();
            InsWater.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(InsWater.isPlaying)
        {
            if (other.tag=="Plant")
            {
               Evolution temp= other.gameObject.GetComponent<Evolution>();
                if (temp != null)
                {
                    temp.deleteInsect();
                }
                
            }
        }
     
    }
    private void OnTriggerStay(Collider other)
    {
        if (InsWater.isPlaying)
        {
            if (other.tag == "Plant")
            {

                Evolution temp = other.gameObject.GetComponent<Evolution>();
                if (temp != null)
                {
                    temp.deleteInsect();
                }

            }
        }
    }



}
