using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject playPanel;
    public GameObject ResumePanel;
    public GameObject GameEndPanel;
    //public GameObject Anim;
    public GameObject introSound;
    public Button pauseButt;
    public Button playButt;
    public Button resumeButt;
    public Button exitButt;
    public Button ReplayButt;
    protected bool pause=false;
    public bool play=false;
    public TrafficController traff_controller;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        play = false;
        playPanel.SetActive(true);
        ResumePanel.SetActive(false);
        GameEndPanel.SetActive(false);
        pauseButt.gameObject.SetActive(false);
        playButt.onClick.AddListener(startGame);
        pauseButt.onClick.AddListener(pauseGame);
        resumeButt.onClick.AddListener(resumeGame);
        exitButt.onClick.AddListener(exitGame);
    }
    public void startGame()
    {
        Time.timeScale = 1;
        play = true;
        playPanel.SetActive(false);
        pauseButt.enabled = true;
        pauseButt.gameObject.SetActive(true);
        GameObject IntroS;
        IntroS = Instantiate(introSound);
        IntroS.GetComponent<AudioSource>().Play();
        //Anim.GetComponent<Animator>().Play("CaelaPlat", 0, 0.25f);
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        ResumePanel.SetActive(true);
        pause = true;
    }
    public void resumeGame()
    {
        ResumePanel.SetActive(false);
        Time.timeScale = 1;
        pause = false;
    }
    // Update is called once per frame
    void exitGame()
    {
        Application.Quit();
    }
    void Update()
    {
 
        if (traff_controller.GetComponent<TrafficController>().endGame == true)
        {
            StartCoroutine(WaitTillEnd());  
        }
    }

    IEnumerator WaitTillEnd()
    {
        yield return new WaitForSeconds(15);
        GameEndPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
