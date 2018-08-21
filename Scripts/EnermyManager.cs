using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyManager : BaseManager
{
    public EnermyManager(GameFacade facade):base(facade){ }
    public const string Enermy_Prifix = "Enermy/";
    public const string Enermy_Cow = "COW";
    public const string Enermy_insect = "insect";


    //private const string Arable_Area = "arableLand";
    //private GameObject arableArea;
    //private List<VegGrowth> arablePlants;
    // Use this for initialization
    void Start () {
        //arableArea = null;
        //arablePlants = null;
	}
    public override void OnInit()
    {
       // GetArableArea(Arable_Area);

       // SummonEnermy(Enermy_Cow);
       
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void SummonEnermy(string name)
    {
     //   Debug.Log("Summon!!!!!!!!!!!!!!!!!!!!!!!");
        GameObject eny=Resources.Load(Enermy_Prifix + name) as GameObject;
        GameObject go = GameObject.Instantiate(eny);
    }
    public void SummonInsect(string insectName,Transform trans,bool isWorld)
    {
        GameObject eny = Resources.Load(Enermy_Prifix + insectName) as GameObject;
        GameObject go = GameObject.Instantiate(eny,trans.transform,isWorld);
        go.transform.localPosition = new Vector3(0, 2, 0);
        Debug.Log("iNSECT!!!!!!!sUMON");
    }
    //private void GetArableArea(string name)
    //{
    //    if (arableArea == null)
    //    {
    //        arableArea = GameObject.Find(name);
    //    }
    //}
    //private void GetArablePlants()
    //{ 
    //    foreach (Transform child in arableArea.transform)
    //    {
    //        if (child.childCount && child.tag == "Plant")
    //        {
    //            arablePlants.Add(child);
    //        }
    //        Debug.Log("count" + child.childCount);
    //    }

    //}
}
