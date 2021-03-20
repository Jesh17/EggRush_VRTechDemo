using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Basket : MonoBehaviour
{
    public GMController gM;
    AudioSource audio;

    
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Egg"){
            gM.playerScore++;
            audio.Play();
            other.GetComponent<XRGrabInteractable>().enabled = false;
            Debug.Log("player score: " + gM.playerScore);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Egg"){

        }
    }
}
