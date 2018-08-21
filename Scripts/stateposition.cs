using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateposition : MonoBehaviour {

    // Use this for initialization
  

        private Vector3 InitialPos;
        private Vector3 hmdPos;
    private Vector3 RelativePos;
        public GameObject HMD;
        public Transform CameraPos;

        void Start()
        {
            
        }

        // Update is called once per frame
        void LateUpdate()
        {
        //hmdPos = HMD.transform.position;                         Can't work in world position
   
    }

    
}
