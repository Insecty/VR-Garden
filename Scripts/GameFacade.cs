using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameFacade : MonoBehaviour {

    private static GameFacade _instance;
    public static GameFacade Instance { get {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameFacade").GetComponent<GameFacade>();
            }
            return _instance;
        } }

    private AudioManager audioMng;
    private EnermyManager enermyMng;

    private bool isEnterPlaying = false;

    void Start () {
        InitManager();

	}
	
	// Update is called once per frame
	void Update () {
        UpdateManager();
        //if (isEnterPlaying)
        //{
        //    EnterPlaying();
        //    isEnterPlaying = false;
        //}
	}
    
    private void OnDestroy()
    {
        DestroyManager();
    }

    private void InitManager()
    {
        Debug.Log("InitManager!");
        audioMng = new AudioManager(this);
        enermyMng = new EnermyManager(this);

        audioMng.OnInit();
        enermyMng.OnInit();
    }
    private void DestroyManager()
    {
        enermyMng.OnDestroy();
        audioMng.OnDestroy();
    }
    private void UpdateManager()
    {
        audioMng.Update();
        enermyMng.Update();
    }

  
   
    public void PlayBgSound(string soundName)
    {
        audioMng.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName)
    {
        audioMng.PlayNormalSound(soundName);
        
    }

    public void EnterPlayingSync()
    {
        isEnterPlaying = true;
    }
    private void EnterPlaying()
    {
  
    }
    public void SummonInsect(string name, Transform trans, bool isWorld)
    {
        enermyMng.SummonInsect(name, trans.transform, isWorld);
    }
    public void SummonCow(string name)
    {
        enermyMng.SummonEnermy(name);
    }

    public void PlayDayMusic()
    {
        audioMng.ResumeMusic(audioMng.bgDaySouce);
    }
    public void PlayNightMusic()
    {
        audioMng.ResumeMusic(audioMng.bgnightSource);
    }
    public void PauseDayMusic()
    {
        audioMng.PauseMusic(audioMng.bgDaySouce);
    }
    public void PauseNightMusic()
    {
        audioMng.PauseMusic(audioMng.bgnightSource);
    }
    public void PlayNormalLoopSound(string soundName)
    {
        audioMng.PlayLoopSound(soundName);
    }
    public void StopPlayLoopSound()
    {
        audioMng.StopPlayLoopSound(audioMng.normalAudioSource);
    }
}
