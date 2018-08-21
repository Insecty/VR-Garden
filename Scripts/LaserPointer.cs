using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
public class LaserPointer : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Transform headTransform;
    public Vector3 teletportReticleOffset;
    public LayerMask teleportMask;
    public LayerMask movableMask;
    private bool shouldTeleport;
    public Transform[] staticPoint;
    private int currentScene = 0;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
            laserTransform.localScale.y, hit.distance);
    }

    private void Teleport()
    {
        Debug.Log("Teleport");
        shouldTeleport = false;
        reticle.SetActive(false);
        laser.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;

    }
    public void SceneTeleport(bool next=true)

    {
        Debug.Log("SceneTeleport");
        if (next)
        {
            currentScene++;
            if (currentScene >= 3)
            {
                currentScene = 0;
                
            }
            move2Scene(currentScene);
        }
        else
        {
            currentScene--;
            if (currentScene < 0)
            {
                currentScene = 2;
               
            }
            move2Scene(currentScene);
        }
        
    }
    void move2Scene(int scene)
    {
        Debug.Log("Move2Scene");
        cameraRigTransform.position = staticPoint[scene].position;
        cameraRigTransform.rotation = staticPoint[scene].localRotation;
    }
    // Use this for initialization
    void Start () {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.FullTrigger))
        {
            RaycastHit hit;
            if(Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask)|| Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, movableMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teletportReticleOffset;
                shouldTeleport = true;
            }
            else
            {
                laser.SetActive(false);
            }
           
        }
 
        if (ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.FullTrigger) && shouldTeleport)
        {
            Teleport();
        }
    }
}
