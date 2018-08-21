using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AttackPlant : MonoBehaviour
{
    float defaultDestroyTime = 2.3f;
    float destroyTime;
    bool isStartEating = false;
    DateTime collidTime;
    // Use this for initialization
    void Start()
    {
        destroyTime = defaultDestroyTime;


    }

    // Update is called once per frame
    void Update()
    {
        if (isStartEating&&destroyTime>0)
        {
            //Debug.Log(destroyTime);
            destroyTime -= Time.deltaTime;
            if(destroyTime<=0)
            {
                destroyTime = 0;
            }
        }
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Plant")
        {
            collidTime = new DateTime();
           // Debug.Log("time is" + collidTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plant")
        {
            isStartEating = true;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Plant" && destroyTime == 0)
        {
            DestroyPlant(other);
        }
    }
      private void DestroyPlant(Collider plant)
    {
        if(plant.gameObject.tag=="Plant")
        {
            plant.transform.position = new Vector3(200, 200, 200);
            isStartEating = false;
            plant.transform.parent.GetComponent<VegGrowth>().SetHasVeg(false);
            destroyTime = defaultDestroyTime;
        }
    }
}
