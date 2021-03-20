using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMController : MonoBehaviour
{
    public int playerScore = 0;
    public float timer;
    float mins;
    float secs;
    public Text[] texts;
    public Text[] titles;

    public GameObject eggs;
    public GameObject flock;
    public bool started = false;
    
    void Start()
    {
        eggs.SetActive(false);
        flock.SetActive(false);
    }

    void Update()
    {
        if(started){
            timer -= Time.deltaTime;
            if(Time.frameCount % 3 == 0){
                secs = Mathf.FloorToInt(timer % 60);
                mins = Mathf.FloorToInt(timer / 60);

                foreach(Text t in texts){
                    t.text = string.Format("{0:00}:{1:00}", mins, secs);
                }
            }
        }

        if(timer <= 0){
            started = false;
            foreach(Text t in texts){
                t.text = playerScore.ToString();

            }
            foreach(Text t in titles){
                t.text = "Player Score:";
            }
        }
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void PauseGame(){
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        Time.timeScale = 1;
    }

    public void StartGame(){
        started = true;
        eggs.SetActive(true);
        flock.SetActive(true);
    }
}
