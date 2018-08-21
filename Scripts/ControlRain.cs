using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRain : MonoBehaviour {

    // Use this for initialization
    RainCameraController RC;
	void Start () {

        RC = GetComponent<RainCameraController>();
        RC.Refresh();
        RC.StopImmidiate();
        NotificationCenter.DefaultCenter().AddObserver(this,"STARTRAIN");
        NotificationCenter.DefaultCenter().AddObserver(this, "STOPRAIN");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void STARTRAIN()
    {
        RC.Play();
    }
    void STOPRAIN()
    {
        RC.Stop();
    }
}
