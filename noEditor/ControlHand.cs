using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using HTC.UnityPlugin.Vive;
//using UnityEditor.Animations;
using UnityEngine.Animations;

public class ControlHand : MonoBehaviour {

    // Use this for initialization
    FingerRig FGR;
    Animator m_animator;
    //AnimatorController AC;
    //AnimatorStateMachine baseStateMachine;
    bool InInteraction = false;
    SendTarget.FingerTargets TF;
    ArrayList leftIK = new ArrayList();
    ArrayList rightIK = new ArrayList();
    Transform leftIKpos;
    Transform rightIKpos;
    Vector3 initialPos;
    [SerializeField]Transform parent;
    Quaternion initialRotation;
    [SerializeField]Rigidbody m_rigd;
    [SerializeField]FixedJoint m_joint;
    [SerializeField] SteamVR_TrackedObject trackedObj;
    [SerializeField]
    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }
    //IK动画一个san个状态
    //正在去抓
    bool isGrabing = false;
    bool isGrabed = false;
    float weight = 0;
    [SerializeField] float speed ;//IK动画的速度
  
    //手往回收
    bool isBacking = false;
  

    //进行IK动画前的位置和
    void Start () {
        TF.TargetObj = null;
        TF.targetposition = null;
        FGR = GetComponent<FingerRig>();
        m_animator = GetComponent<Animator>();
       // AC = m_animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
       // baseStateMachine = AC.layers[0].stateMachine;
      
        
    }
	
	// Update is called once per frame
	void Update () {
      //  Debug.Log("InInteraction"+InInteraction);
       // Debug.Log("in grab " + isGrabed);
        //如果没有交互的必要性，就完全自娱自乐了
        if (!InInteraction)
        {
            //if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.FullTrigger) && this.gameObject.tag == "lefthand")
            //{
            //    int temp = Random.Range(0, 20);
                
            //    m_animator.SetTrigger(m_animator.parameters[temp].name);

            //}
            if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.FullTrigger) && this.gameObject.tag == "righthand")
            {
                int temp = Random.Range(0, 20);
                m_animator.SetTrigger(m_animator.parameters[temp].name);
            }
        }//如果有交互的必然性
        else
        {
            


            if (isGrabing && TF.TargetObj != null)
            {
                Debug.Log("isGrabing");
                
                //如果正在抓，那么扳机键是取消抓取
                if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.FullTrigger) && this.gameObject.tag == "righthand")
                {
                    FGR.weight = 0;
                    m_animator.SetTrigger("Idle");
                    isGrabing = false;
                    isBacking = true;
                    transform.parent = parent;
                }
                //if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.FullTrigger) && this.gameObject.tag == "lefthand")
                //{
                //    FGR.weight = 0;
                //    m_animator.SetTrigger("Idle");
                //    isGrabing = false;
                //    isBacking = true;
                //    transform.parent = parent;
                //}
                weight += Time.deltaTime * speed;
                FGR.weight = weight;
                if (this.gameObject.tag == "lefthand")
                {
                    transform.position = Vector3.Lerp(initialPos, leftIKpos.position, weight);
                    transform.rotation = Quaternion.Lerp(initialRotation, leftIKpos.rotation, weight);
                }else if (this.gameObject.tag == "righthand")
                {
                    transform.position = Vector3.Lerp(initialPos, rightIKpos.position, weight);
                    transform.rotation = Quaternion.Lerp(initialRotation, rightIKpos.rotation, weight);
                }
                if (weight >= 1)
                {
                    TF.TargetObj.transform.parent = transform;
                   
                    isGrabing = false;
                    isBacking = true;
                    isGrabed = true;
                    TF.TargetObj.SendMessage("Grab",m_rigd,SendMessageOptions.DontRequireReceiver);
                    weight = 1;
                    m_joint.connectedBody = TF.TargetObj.transform.GetComponent<Rigidbody>();
                    transform.parent = parent;
                }
            }
            
            //如果抓取完成
          
            else if (isBacking )
            {
               
                weight -= Time.deltaTime * speed;
                Quaternion newRotation = transform.parent.rotation * Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
                if (gameObject.tag == "righthand")
                {
                    transform.position = Vector3.Lerp(transform.parent.position, rightIKpos.position, weight);
                    
                    transform.rotation = Quaternion.Slerp(newRotation, rightIKpos.rotation, weight);
                }else
                {
                    transform.position = Vector3.Lerp(transform.parent.position, leftIKpos.position, weight);
                    
                    transform.rotation = Quaternion.Slerp(newRotation, leftIKpos.rotation, weight);
                }
                if (weight <= 0)
                {
                 
                    isBacking = false;
                    
                    transform.position = transform.parent.position;
                    transform.rotation = newRotation;
                    
                }
            }
            else if(!isGrabed)
            {
                if (InInteraction && TF.TargetObj != null)
                {
                    //Debug.Log("inter");
                    if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.FullTrigger) && this.gameObject.tag == "righthand")
                    {
                        Debug.Log(TF.TargetObj.name);
                        grab(TF.TargetObj.GetComponent<SendTarget>().FT);
                        initialPos = transform.position;
                        initialRotation = transform.rotation;
                      
                        isGrabing = true;
                        
                        for (int i = 0; i < 5; i++)
                        {
                            FGR.fingers[i].target = (Transform)rightIK[i];
                        }
                        transform.parent = null;
                        m_animator.SetTrigger("Idle");
                       
                    }
                    //if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.FullTrigger) && this.gameObject.tag == "lefthand")
                    //{
                    //    grab(TF.TargetObj.GetComponent<SendTarget>().FT);
                    //    initialPos = transform.position;
                    //    initialRotation = transform.rotation;
                    
                    //    isGrabing = true;
                    //    transform.parent = null;
                    //    m_animator.SetTrigger("Idle");
                       
                    //    for (int i = 0; i < 5; i++)
                    //    {
                    //        FGR.fingers[i].target = (Transform)leftIK[i];
                    //    }
                   
                    //}
                }
            }
            if (isGrabed)
            {
                
                if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.FullTrigger) && this.gameObject.tag == "righthand")
                {
                   // TF.TargetObj.transform.parent = null;
                    m_animator.SetTrigger("Idle");
                    FGR.weight = 0;
                    isGrabed = false;
                   
                    TF.TargetObj.SendMessage("PutBack",Controller,SendMessageOptions.DontRequireReceiver);
                    m_joint.connectedBody = null;
                    TF.targetposition = null;
                    TF.TargetObj = null;
                }
                //if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.FullTrigger) && this.gameObject.tag == "lefthand")
                //{
                //    TF.TargetObj.transform.parent = null;
                //    m_animator.SetTrigger("Idle");
                //    FGR.weight = 0;
                //    isGrabed = false;
                //    TF.TargetObj.SendMessage("PutBack");
                //    TF.targetposition = null;
                //    TF.TargetObj = null;

                //}
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "IKtarget"&&!isGrabed)
        {
            InInteraction = true;
            TF.TargetObj = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "IKtarget" && !isGrabed)
        {
            InInteraction = true;
            TF.TargetObj = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "IKtarget" && !isGrabed)
        {
            if (!isGrabing && !isBacking)
            {
                ExitGrab();
            }
        }
    }
    public void grab(SendTarget.FingerTargets temp)
    {
        leftIK.Clear();
        rightIK.Clear();
       // Debug.Log("grab");
        InInteraction = true;
        TF = temp;
        for(int i = 1; i < 13; i++)
        {
            if (temp.targetposition[i].tag == "leftIK")
            {
                leftIK.Add(temp.targetposition[i]);
            }
            else if (temp.targetposition[i].tag == "rightIK")
            {
                rightIK.Add(temp.targetposition[i]);
            }else if (temp.targetposition[i].tag == "leftIKpos")
            {
                leftIKpos = temp.targetposition[i];
            }else if (temp.targetposition[i].tag == "rightIKpos")
            {
                rightIKpos = temp.targetposition[i];
            }
        }
    }
    public void ExitGrab()
    {
        Debug.Log("ExitGrab");
        FGR.weight = 0;
        InInteraction = false;
        
        TF.targetposition = null;
        TF.TargetObj = null;
    }
}
