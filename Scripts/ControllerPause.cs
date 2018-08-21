
using UnityEngine;

using System.Collections;



public class ControllerPause : MonoBehaviour
{


    public int defaultIndex = 3;
    SteamVR_TrackedObject tracked;





    // Use this for initialization

    void Start()
    {


        tracked = GetComponent<SteamVR_TrackedObject>();
        NotificationCenter.DefaultCenter().AddObserver(this,"ControllerShake");
    }



    // Update is called once per frame


    void ControllerShake(Notification noti)
    {
        bool left = (bool)noti.data;
        if (this.tag == "LeftController")
        {
            if (left)
            {
                StartCoroutine(Shake(defaultIndex));
            }
            else
            {

            }
        }
        else
        {
            if (left)
            {

            } else
            {
               StartCoroutine( Shake(defaultIndex));
            }
        }
    }
    IEnumerator Shake(int index)
    {
        for(int i=0;i<index; i++)
        {
            var device = SteamVR_Controller.Input((int)tracked.index);
            device.TriggerHapticPulse(20000);
            yield return null;
        }
    }
}
