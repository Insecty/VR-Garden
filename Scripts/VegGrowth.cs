using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using System;

public class VegGrowth : MonoBehaviour {

    public float total_life;
    public GameObject[] Vegs;

    private float veg_life;
    private bool hasVeg;
    public Transform childTrans;
    private setVeg.VEG cVeg;
    private int waterTimes;
    private float target;
    float startPlantTime;
    float currentPlantTime;
    float intervalTime = 45f;
    public bool ifInsectExist = false;
    bool full = false;
	// Use this for initialization
	void Start () {
        veg_life = 0.0f;
        hasVeg = false;
        total_life = 100.0f;
        NotificationCenter.DefaultCenter().AddObserver(this, "STOPRAIN");
        cVeg = setVeg.VEG.Nothing;
	}
	
	// Update is called once per frame
	void Update () {
        currentPlantTime = Time.time;
		if(hasVeg&&!full)
        {
            if(currentPlantTime-startPlantTime>intervalTime)
            {
                float t = UnityEngine.Random.Range(0,10);
                if(t<=1)
                {
                    Debug.Log("START SUMMON!!!!");
                    GetComponentInChildren<Evolution>().createInsect();
                   

                }
                startPlantTime = Time.time;
            }
            if(veg_life < 0.2f)
                veg_life += 1 / total_life;
            childTrans.localScale = new Vector3(1, 1, 1) * (1 + veg_life);

            if (veg_life >= 1.0f)
            {
                Debug.Log("destroy");
                full = true;
                Destroy(this.childTrans.gameObject, 3);
                
            }
            if (veg_life < target)
            {
                veg_life += 0.004f;
            }
        }
	}

    public bool HasVeg()
    {
        return hasVeg;
    }
    public void SetHasVeg(bool has)
    {
        hasVeg = has;
    }
    public void InitPlant(setVeg.VEG veg)
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "ControllerShake", false);
        cVeg = veg;
        veg_life = 0.0f;
        target = 0.0f;
        hasVeg = true;
        Instantiate(Vegs[(int)veg], this.transform, false);
        childTrans = GetComponentsInChildren<Transform>()[1];
        waterTimes = 0;
        startPlantTime = Time.time;

    }

    public void addWater()
    {
        if (waterTimes <= 1)
        {
            waterTimes += 1;
            target = veg_life + 1.0f;
           
        }
    }

    public void STOPRAIN()
    {
        if (hasVeg)
        {
            addWater();
        }
    }
}
