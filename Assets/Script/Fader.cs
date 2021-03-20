using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public GameObject Blackout;

    void Start()
    {
        Blackout = GameObject.Find("VRBlackout");
        Blackout.SetActive(false);
    }

    private void OnTriggerEnter(Collider other){
    	Debug.Log("TRIGGERENTER");
    	if(other.tag == "Solid"){ Blackout.SetActive(true); Debug.Log("fade on"); }
    }

    private void OnTriggerExit(Collider other){
    	Debug.Log("TRIGGEREXIT");
    	if(other.tag == "Solid"){ Blackout.SetActive(false); }
    }
}
