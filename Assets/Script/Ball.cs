using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
	public Transform pos;
	public Material mat;
	public float priority;
	public bool canGrab;
	//private Throwable THROW;
	//private Interactable INTERACT;
    
    void Start()
    {
    	canGrab = false;
    	/* THROW = GetComponent<Throwable>();
    	THROW.enabled = false;
    	INTERACT = GetComponent<Interactable>();
    	INTERACT.enabled = false; */
        mat = GetComponent<Renderer>().material;
        pos = gameObject.transform;
    }

    void Update(){

    	if(canGrab == true){
    		//THROW.enabled = true;
    		//INTERACT.enabled = true;
    		mat.color = Color.red;
    	} 
    	else{
    		//THROW.enabled = false;
    		//INTERACT.enabled = false;
    		mat.color = Color.blue;
    	}
    }
   
}
