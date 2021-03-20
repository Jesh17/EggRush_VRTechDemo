using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ExampleListener : MonoBehaviour
{
	public UnityEvent ButtonDown;
	public UnityEvent ButtonUp;
	public UnityEvent ButtonHold;
	public UnityEvent AxisChange;
	public UnityEvent Trigger;
	public UnityEvent Grip;

	public ButtonHandler primaryAxisClickHandler = null;
	public Axis2DHandler primaryAxisHandler = null;
	public AxisHandler triggerHandler = null;
	public AxisHandler gripHandler = null;

	bool buttonPressed = false;
	public float pressedTime = 0f;


    public void OnEnable(){
    	primaryAxisClickHandler.OnButtonDown += PrintPrimaryButtonDown;
    	primaryAxisClickHandler.OnButtonUp += PrintPrimaryButtonUp;
    	primaryAxisHandler.OnValueChange += PrintPrimaryAxis;
    	triggerHandler.OnValueChange += PrintTrigger;
		gripHandler.OnValueChange += PrintGrip;
    }

    public void OnDisable(){
    	primaryAxisClickHandler.OnButtonDown -= PrintPrimaryButtonDown;
    	primaryAxisClickHandler.OnButtonUp -= PrintPrimaryButtonUp;
    	primaryAxisHandler.OnValueChange -= PrintPrimaryAxis;
    	triggerHandler.OnValueChange -= PrintTrigger;
		gripHandler.OnValueChange -= PrintGrip;
    }

    private void PrintPrimaryButtonDown(XRController controller){
    	ButtonDown.Invoke();
    	buttonPressed = true;
    	Debug.Log("Primary button down");
    }

    private void PrintPrimaryButtonUp(XRController controller){
    	ButtonUp.Invoke();
    	buttonPressed = false;
    	pressedTime = 0f;
    	Debug.Log("Primary button up");
    }

    private void PrintPrimaryAxis(XRController controller, Vector2  value){
    	//Debug.Log("Primary axis: " + value);
    }

    private void PrintTrigger(XRController controller, float value){
    	//Debug.Log("Tigger: " + value);
    }

	private void PrintGrip(XRController controller, float value){
    	//Debug.Log("Grip: " + value);
    }

    void Update(){
    	if(buttonPressed){
    		ButtonHold.Invoke();
    		Debug.Log("Button is Held");
    		
    	}
    }
}
