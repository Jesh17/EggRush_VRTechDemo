using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public GameObject PlayerHead;
    public GameObject playerMarker;
    public GameObject PlayerBody;
    public Rigidbody rb;
    Transform playerBod;
    Transform headPOS;
    public Transform playerCamera;
    Vector2 POSstart;
    Vector2 POSnew;
    Vector3 move;
    public float veloctiy = 1.5f;
    Vector2 direction;
    Vector2 BADdirection = new Vector2(0,0);

    void Start()
    {
        headPOS = PlayerHead.GetComponent<Transform>();
        playerBod = PlayerBody.GetComponent<Transform>();
    }

    void Update()
    {
      headPOS.transform.position = new Vector3(playerCamera.position.x, playerCamera.position.y, playerCamera.position.z);
/*
       	if(SteamVR_Actions._default.ButtonA_Move.GetStateDown(SteamVR_Input_Sources.Any)){
       		GetHeadPOS();
       		Debug.Log("ButtonA_Move has be activated!");
      	}

      	if(SteamVR_Actions._default.ButtonA_Move.GetState(SteamVR_Input_Sources.Any))
        {
          POSnew = new Vector2(headPOS.localPosition.x, headPOS.localPosition.z);
          direction = POSnew - POSstart;

          move.x = direction.x;
          move.y = 0f;
          move.z = direction.y;
            
          playerBod.position += move * veloctiy;      
            
          Debug.Log("Moving player.");
 
          POSnew = new Vector2(headPOS.localPosition.x, headPOS.localPosition.z);
      	}
*/
    }
    
    public void GetHeadPOS(){
    	playerMarker.transform.position = new Vector3(headPOS.position.x, playerMarker.transform.position.y, headPOS.position.z);
      POSstart = new Vector2(playerMarker.transform.localPosition.x, playerMarker.transform.localPosition.z);
    }

    void OnCollisionEnter(Collision other){
      if(other.collider.tag == "Solid"){
        BADdirection = direction;
      }
    }

    void OnCollisionExit(Collision other){
      if(other.collider.tag == "Solid"){
        BADdirection = new Vector2(0,0);
      }
    }

    public void MoveCharacter(){
          POSnew = new Vector2(headPOS.localPosition.x, headPOS.localPosition.z);
          direction = POSnew - POSstart;

          move.x = direction.x;
          move.y = 0f;
          move.z = direction.y;
            
          playerBod.position += move * veloctiy;      
            
          Debug.Log("Moving player.");
 
          POSnew = new Vector2(headPOS.localPosition.x, headPOS.localPosition.z);
    }


}
