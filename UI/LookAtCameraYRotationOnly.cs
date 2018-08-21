using UnityEngine;
using System.Collections;

public class LookAtCameraYRotationOnly : MonoBehaviour
{
    public Camera cameraToLookAt;
    [SerializeField]Transform hand;
    void Update()
    {
        if (hand) { transform.position = hand.position; }
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position);
        
        
    }
}