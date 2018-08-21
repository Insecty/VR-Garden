using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myButton : MonoBehaviour {

    // Use this for initialization
    
    enum State
    {
        highlighted,
        pressed,
        normal,
        leave,
        disable,
        cont
    }
    Animator m_animator;
    State m_state=State.normal;
    State last_state = State.cont;
    private float m_weight = 0;
    private float Speed=1;
    void Start () {
        
        gameObject.SetActive(false);
        m_animator = GetComponent<Animator>();
        NotificationCenter.DefaultCenter().AddObserver(this, "Leave");
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.SetActive(false);
        
    }
    public void HighLighted()
    {
        
        
            m_state = State.highlighted;
        
        //m_animator.SetTrigger("Highlighted");
    }
    public void Normal()
    {
        
            m_state = State.normal;
        //weight = 0;
        //m_animator.SetTrigger("Normal");
    }
    public void Pressed()
    {
      //  Debug.Log("Pressed");
            m_state = State.pressed;
       // m_animator.SetTrigger("Pressed");
    }
    public void Leave()
    {
        
            m_state = State.leave;
       // m_animator.SetTrigger("Leave");
    }
    private void LateUpdate()
    {
        if (last_state != m_state)
        {
            if (m_state == State.normal)
            {
                m_animator.SetTrigger("Normal");
                //m_animator.SetBool("Highlighted", false);
                //m_animator.Play("Normal", 0);
            }
            else if (m_state == State.highlighted)
            {
                m_animator.SetTrigger("Highlighted");
               // m_animator.Play("Highlighted", 0);
            }
            else if (m_state == State.pressed)
            {
                //m_animator.Play("Pressed", 0);
                m_animator.SetTrigger("Pressed");
            }
            else if (m_state == State.leave)
            {
               // m_animator.Play("Leave", 0);
                
                
                m_animator.SetTrigger("Leave");
            }
            last_state = m_state;
        }
        else
        {

        }
    }
}
