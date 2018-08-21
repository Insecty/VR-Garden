using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScene : MonoBehaviour {

    [SerializeField] GameObject LeftHand;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Pressed()
    {
        Debug.Log("Last");
        LeftHand.SendMessage("SceneTeleport", false);
    }
}
