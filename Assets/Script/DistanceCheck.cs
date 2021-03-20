using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{

	public Transform[] Grabables;
	public List<Ball> PriorityList = new List<Ball>();

	public Transform handLEFT;
	public Transform handRIGHT;

    void Update()
    {
    	foreach(Transform t in Grabables){
    		Debug.Log("Calculated");
    		Ball ballProp = t.gameObject.GetComponent<Ball>();
    		ballProp.priority = 1/(Vector3.Distance(t.position, handLEFT.position));
    		PriorityList.Add(ballProp);	
    	}

    	if(PriorityList.Count > 0){
    		PriorityList.Sort(delegate(Ball a, Ball b){
    			return (a.priority).CompareTo(b.priority);
    			});
    	}
    
    	PriorityList.Reverse();
    	
    	for(int i = 0; i < PriorityList.Count; i++){
    		if(i == 0){
    			PriorityList[i].canGrab = true;
    		} else {PriorityList[i].canGrab = false;}
    	}

    	PriorityList.Clear();
    	
    }
}
