using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager {

    public AudioManager(GameFacade facade) : base(facade) {
       // Debug.Log("lalala");
    }
    private const string Sound_Prefix = "Sounds/";
    public const string Sound_ArrowShoot = "ArrowShoot";
    public const string Sound_Miss = "Miss";
    public const string Sound_UpFlow = "UpFlow";
    public const string Sound_bg_noon = "α-pav-d";
    public const string Sound_bg_night = "α-pav-n";


    public const string Sound_shoot = "shoot";
    public const string Sound_harvest = "harvest";
    public const string Sound_pourwater = "pourwater";
    public const string Sound_howl = "howl";
    public const string Sound_speedup = "timespeed";
    public const string Sound_spray = "spray";
    public const string Sound_hoe = "hoe";


    public AudioSource bgDaySouce;
    public AudioSource normalAudioSource;
    public AudioSource bgnightSource;

    public float dayMusicTime=0;
    public float nightMusicTime=0;

    public override void OnInit()
    {

        //Debug.Log("start playing bgmusic");
        GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
        bgDaySouce = audioSourceGO.AddComponent<AudioSource>();
        bgnightSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

        LoadDayMusic("α-pav-d");
        LoadNightMusic("α-pav-n");

   

        ResumeMusic(bgDaySouce);

    }

    public void PlayBgSound(string soundName)
    {
        PlaySound(bgDaySouce, LoadSound(soundName), 0.4f, true);
    }
    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource, LoadSound(soundName), 1f);
    }
    public void PlayLoopSound(string soundName)
    {
        PlaySound(normalAudioSource, LoadSound(soundName), 1f,true);
    }
    public void StopPlayLoopSound(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void PlaySound( AudioSource audioSource,AudioClip clip,float volume, bool loop=false)
    { 

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void LoadDayMusic(string soundName)
    {
        LoadMusic(bgDaySouce, LoadSound(soundName), 0.4f, true);
    }
    public void LoadNightMusic(string soundName)
    {
        LoadMusic(bgnightSource, LoadSound(soundName), 0.4f, true);
    }

    private void LoadMusic(AudioSource audioSource, AudioClip clip, float volume, bool loop = false)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Stop();
        if (audioSource == bgDaySouce)
        {
            dayMusicTime = bgDaySouce.time;
        }
        if (audioSource == bgnightSource)
        {
            nightMusicTime = bgnightSource.time;
        }
    }
    public void PauseMusic(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            if (audioSource == bgDaySouce)
            {
                dayMusicTime = bgDaySouce.time;
               // Debug.Log("daymusictime" + dayMusicTime);
            }
            if (audioSource == bgnightSource)
            {
                nightMusicTime = bgnightSource.time;
              //  Debug.Log("nightmusictime" + nightMusicTime);
            }
            audioSource.Stop();
           
        }
    }
    public void ResumeMusic(AudioSource audioSource)
    {
        Debug.Log("resume!!!!!!!!!!!!!!!!!");
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
            if (audioSource == bgDaySouce)
            {
                Debug.Log("daymusictime111111111" + dayMusicTime);
                bgDaySouce.time = dayMusicTime;
            }
            if (audioSource == bgnightSource)
            {
                bgnightSource.time = nightMusicTime;
                //Debug.Log("nightmusictime" + nightMusicTime);
            }
        }
    }

    private AudioClip LoadSound(string soundsName)
    {
        return Resources.Load<AudioClip>(Sound_Prefix + soundsName);
    }
}
