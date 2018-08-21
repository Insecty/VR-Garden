using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
public class PlantManager : MonoBehaviour {

    private setVeg.VEG currentVeg; 
    //private int currentVeg; // 0 cabbage 1 carrot 2 wm 3 pumpkin
    private Vector3 PlantTransform;
    
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
    private GameObject hitObject;
    private bool shouldTeleport;

    private GameObject LastObject=null;
    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
  
    void Start()
    {
        hitObject = null;
        currentVeg = 0;
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.FullTrigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                hitObject = hit.collider.gameObject;
                PlantTransform = hitObject.transform.position;

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teletportReticleOffset;
                shouldTeleport = true;
            }
            else
            {
                laser.SetActive(false);
                if (LastObject)
                {
                    LastObject.GetComponent<MeshRenderer>().enabled = false;
                    LastObject = null;
                }
            }

        }
        if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.FullTrigger))
        { 
            Plant();
        }
    }

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public void SetCurrentVeg(setVeg.VEG veg)
    {
        currentVeg = veg;
    }

    void ClearCurrentVeg()
    {
        currentVeg = setVeg.VEG.Nothing;
    }

    private void ShowLaser(RaycastHit hit)
    {
        MeshRenderer target = hit.collider.gameObject.GetComponent<MeshRenderer>();
        target.enabled = true;
        if (hit.collider.gameObject.GetComponent<VegGrowth>().HasVeg())
        {
           target.material.SetColor("_Color", new Color(0.9f, 0.1f, 0.1f, 0.2f));
        }
        else
        {
            target.material.SetColor("_Color", new Color(0.11f, 0.082f, 0.012f, 0.65f));
        }
        
        if (LastObject&&LastObject!=hit.collider.gameObject) { LastObject.GetComponent<MeshRenderer>().enabled = false; }
        LastObject = hit.collider.gameObject;
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
            laserTransform.localScale.y, hit.distance);
    }

    //private void Teleport()
    //{
    //    Debug.Log("Teleport");
    //    shouldTeleport = false;
    //    reticle.SetActive(false);
    //    Vector3 difference = cameraRigTransform.position - headTransform.position;
    //    difference.y = 0;
    //    cameraRigTransform.position = hitPoint + difference;

    //}
    private void Plant()
    {
        if(LastObject)
        {
            LastObject.GetComponent<MeshRenderer>().enabled = false;
            LastObject = null;
        }
        reticle.SetActive(false);
        laser.SetActive(false);
        if (hitObject != null && hitObject.transform.childCount==0)
        {
            if(currentVeg != setVeg.VEG.Nothing)
            {
                //Instantiate(Vegs[(int)currentVeg], hitObject.transform, false);
                hitObject.GetComponent<VegGrowth>().InitPlant(currentVeg);
            }
            else
            {
                // 提示选择蔬菜
            }
        }
    }

}
