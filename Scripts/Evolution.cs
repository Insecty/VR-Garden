using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour {

    public GameObject Growup;
    public ParticleSystem insectPar;
    private VegGrowth par_vegGrowth;
	// Use this for initialization
	void Start () {
        par_vegGrowth = GetComponentInParent<VegGrowth>();

    }

    private void OnDestroy()
    {
        GameObject temp=Instantiate(Growup, this.transform.parent, false);
        GetComponentInParent<VegGrowth>().childTrans = temp.transform;

        GameFacade.Instance.PlayNormalSound(AudioManager.Sound_harvest);
        //Growup.transform.parent = transform.parent;
    }

    public void createInsect()
    {
        insectPar.Play();
        par_vegGrowth.ifInsectExist = true;
    }

    public void deleteInsect()
    {
        insectPar.Stop();
        par_vegGrowth.ifInsectExist = false;

    }
}
