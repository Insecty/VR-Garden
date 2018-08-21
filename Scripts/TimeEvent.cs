using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEvent : MonoBehaviour
{
    private bool isExist = false;
    private bool isBallGrab = false;

    private void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "IfBallGrab");
        NotificationCenter.DefaultCenter().AddObserver(this, "GrabBall");
        NotificationCenter.DefaultCenter().AddObserver(this, "IfBallReleas");
        NotificationCenter.DefaultCenter().AddObserver(this, "IfCowDie");
    }
    public void AddCow()
    {
        if (isExist == false)
        {
            Debug.Log("summon!!!!!!!!!!!");
            GameFacade.Instance.SummonCow(EnermyManager.Enermy_Cow);
            isExist = true;
        }
    }
    public void IfCowDie()
    {
        Debug.Log("COW DIEEEEEEEEEEEEEEEE");
        isExist = false;
    }
    public void DisPlayNoonBgMusic()
    {
        if (isBallGrab == false)
        {
             Debug.Log("PLAY DAY MUSIC!!!!");
            GameFacade.Instance.PlayDayMusic();
            GameFacade.Instance.PauseNightMusic();
        }
        //GameFacade.Instance.PlayBgSound(AudioManager.Sound_bg_noon);
    }
    public void DisPlayNightBgMusic()
    {
        Debug.Log("isBallGrab" + isBallGrab);
        Debug.Log("play night bg..........");
        if (isBallGrab == false)
        {
            //  Debug.Log("PLAY NIGHT MUSIC");
            GameFacade.Instance.PlayNightMusic();
            GameFacade.Instance.PauseDayMusic();
        }
    }

    public void IfBallGrab()
    {
        isBallGrab = true;
    }
    public void IfBallRelease()
    {
        isBallGrab = false;
    }
    public void LightsControl(bool LightControl)
    {
        GameObject AllLights = GameObject.Find("LightController");

        foreach (Transform T in AllLights.transform)
        {
            T.gameObject.SetActive(LightControl);
        }
    }
}
