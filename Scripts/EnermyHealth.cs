using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class EnermyHealth : MonoBehaviour{
    private UnityEngine.Animator animator;
    public int HP=100;
    public int fleeHP = 40;
    private bool isDead = false;

	// Use this for initialization
	void Start () {
        // HP = 100;
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(HP<=0&&isDead==false)
        {
            isDead = true;
            animator.SetBool("isdead", true);
            this.GetComponent<BehaviorTree>().enabled = false;
            this.GetComponent<NavMeshAgent>().enabled = false;
            NotificationCenter.DefaultCenter().PostNotification(this, "IfCowDie");
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_howl);
            this.GetComponent<BoxCollider>().enabled = false;
            Destroy(this.gameObject, 10f);
        }
	}
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("isAttacked");
        //Debug.Log("DAMAGE!!");
       // Debug.Log("SHOOTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
        HP -= damage;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        

    }
    

}
