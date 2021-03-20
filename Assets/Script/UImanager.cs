using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSurvey(){
    	Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeWcgSCOnX_k9HhSdm_t-6pjVGRhCVKRPLeKsiUoHOjfYij0Q/viewform?usp=sf_link");
    }

    public void ExitGame(){
    	Application.Quit();
    }
}
