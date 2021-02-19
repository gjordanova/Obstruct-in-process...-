using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public GameObject AirObj;
    public float launchDelayMin;
    public float launchDelayMax;
    private float launchTimer;
    public Animator anim;
    public bool[] levelDirection;
    public int[] levelNum;
    public int maxOccupancy;
    public GameObject scoreObj;
    private int score;
    public bool endGame;
    // Start is called before the first frame update
    void Start()
    {
        levelDirection = new bool[4];
        levelNum = new int[4];
        levelNum[0] = 0;
        levelNum[1] = 0;
        levelNum[2] = 0;
        levelNum[3] = 0;

        score = 0;
        endGame = false;
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        launchTimer -= Time.deltaTime;
        if (launchTimer < 0)
        {
            CheckForEndGame();
            if (!endGame)
            {
                StartCoroutine(WaitForAnim());  
            }
          
            launchTimer = Random.Range(launchDelayMin, launchDelayMax);
        }
    }
    public int GetAvailableLevel(bool forwardFlag)
    {
        int i = 0;
        while (i < 4)
        {
            if (levelNum[i] == 0)
            {
                levelDirection[i] = forwardFlag;
                levelNum[i]++;
                return i;
            }
            else
            {
                if ((levelDirection[i] == forwardFlag)&&(levelNum[i]<maxOccupancy))
                {
                    levelNum[i]++;
                    return i;
                }
            }
            i = i + 1;
        }
        return -1;
    }
    public void LeftLevel(int level)
    {
        levelNum[level]--;
    }
    public void AddScore(int n)
    {
        score += n;
        if (score < 0) score = 0;
        scoreObj.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
    }
    void CheckForEndGame()
    {
        int leftCount = 0;
        int rightCount = 0;
        foreach (GameObject soldierObj in GameObject.FindGameObjectsWithTag("Soldier"))
        {
            if (soldierObj.GetComponent<SoldierController>().onGround)
            {
                leftCount++;
            }
            else
            {
                rightCount++;
            }
        }
        if ((leftCount > 3) || (rightCount > 3))
        {
            endGame = true;
            foreach (GameObject soldierObj in GameObject.FindGameObjectsWithTag("Soldier")){
                if (soldierObj.GetComponent<SoldierController>().onGround)
                {
                    if (soldierObj.transform.position.x < 0)
                    {
                        if (leftCount > 3)
                        {
                            soldierObj.GetComponent<SoldierController>().destroyBuilding = true;
                        }
                    }
                    else
                    {
                        if (rightCount > 3)
                        {
                            soldierObj.GetComponent<SoldierController>().destroyBuilding = true;
                        }
                    }
                }
            }
         
        }
    }
    IEnumerator WaitForAnim()
    {
        Debug.Log(anim.runtimeAnimatorController.animationClips.Length);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        //yield return new WaitForSeconds(anim.runtimeAnimatorController.animationClips.Length);
        yield return new WaitForSeconds(4f);
        Instantiate(AirObj);
    }
}
