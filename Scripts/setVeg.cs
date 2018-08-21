using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setVeg : MonoBehaviour {

    public enum VEG
    {
        Carrot,
        Watermelon,
        Pumpkin,
        Cabbage,
        Nothing
    }
    [SerializeField] VEG veg_self;
    public GameObject rightHand;

    public void Pressed()
    {
        if(rightHand != null)
        {
            rightHand.GetComponent<PlantManager>().SetCurrentVeg(veg_self);
        }
    }
}
