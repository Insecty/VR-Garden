using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFly : MonoBehaviour {
    [SerializeField] m_weatjer weather;
    private UniStormWeatherSystem_C weatherSys;
    grabObject m_grab;
    bool isfast=false;
    [SerializeField] List<float> time=new List<float>();
	// Use this for initialization
	void Start () {
        weatherSys = GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>();
        m_grab = GetComponent<grabObject>();

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(weatherSys.Hour);
        if (m_grab.isGrab && !isfast)
        {

            isfast = true;
            weather.SetTimeSpeed(time[0],time[1]);
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_speedup);
            NotificationCenter.DefaultCenter().PostNotification(this, "ControllerShake", false);
            NotificationCenter.DefaultCenter().PostNotification(this, "IfBallGrab");
            GameFacade.Instance.PauseDayMusic();
            GameFacade.Instance.PauseNightMusic();
            
        }
        if (!m_grab.isGrab && isfast)
        {
            Debug.Log(weatherSys.Hour);
            NotificationCenter.DefaultCenter().PostNotification(this, "IfBallRelease");
            if (weatherSys.Hour>7&&weatherSys.Hour<18)
            {
                GameFacade.Instance.PlayDayMusic();
            }
            else
            {
                GameFacade.Instance.PlayNightMusic();
            }
            isfast = false;
            NotificationCenter.DefaultCenter().PostNotification(this, "ControllerShake", false);
            weather.TimeSpeedBack();
        }
	}
}
