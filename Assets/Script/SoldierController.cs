using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public float gravity;
    public float fallSpeed;
    public float groundPos;
    public bool onGround;

    public GameObject ballonObj;
    private GameObject currentBallon;
    private bool usedBallon;
    public GameObject particleObj;
    public GameObject TraficObj;
    public bool destroyBuilding;
    public GameObject fireObj;
    public GameObject hitSound;
    public GameObject squishSound;
    public GameObject screamSound;
    // Start is called before the first frame update
    void Start()
    {
        TraficObj = GameObject.FindWithTag("Traffic");
        onGround = false;
        usedBallon = false;
        destroyBuilding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onGround)
        {
            Vector3 newPos;
            newPos = transform.position;
            newPos.y -= fallSpeed * Time.deltaTime;
            if (currentBallon == null)
            {
                fallSpeed += gravity *Time.deltaTime;
            }
           
            if (newPos.y < groundPos)
            {
                newPos.y = groundPos;
                onGround = true;
                if (currentBallon != null)
                {
                    Destroy(currentBallon);
                }
                if (fallSpeed > 2f)
                {
                    TraficObj.GetComponent<TrafficController>().AddScore(2);
                    GameObject explode = Instantiate(particleObj);
                    explode.transform.position = transform.position;
                    GameObject squish;
                    squish = Instantiate(squishSound);
                    squish.GetComponent<AudioSource>().panStereo = transform.position.x / 9f;
                    Destroy(gameObject);
                }
            }
            transform.position = newPos;
            if ((!usedBallon) && (fallSpeed > 3f))
            {
                SpawnBallon();
                fallSpeed = 1f;
                usedBallon = true;
            }
            else
            {
                if (usedBallon && (fallSpeed > 3f) && !GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
            if (CheckCollision())
            {
                TraficObj.GetComponent<TrafficController>().AddScore(2);
                GameObject explode = Instantiate(particleObj);
                explode.transform.position = transform.position;
                GameObject hit;
                hit = Instantiate(hitSound);
                hit.GetComponent<AudioSource>().panStereo = transform.position.x / 9f;
                Destroy(gameObject);
            }
        }
        else
        {
            if (destroyBuilding)
            {
                Vector3 newPos;
                newPos = transform.position;
                if (newPos.x < 0)
                {
                    newPos.x += 2f * Time.deltaTime;
                    if (newPos.x > 0)
                    {
                        Instantiate(fireObj);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    newPos.x -= 2f * Time.deltaTime;
                    if (newPos.x < 0)
                    {
                        Instantiate(fireObj);
                        Destroy(gameObject);
                    }
                }
                transform.position = newPos;
            }
            else
            {
                if (CheckForFallingSoldiers())
                {
                    TraficObj.GetComponent<TrafficController>().AddScore(2);
                    GameObject explode = Instantiate(particleObj);
                    explode.transform.position = transform.position;
                    GameObject scream;
                    scream = Instantiate(screamSound);
                    scream.GetComponent<AudioSource>().panStereo = transform.position.x / 9f+0.5f;
                    Destroy(gameObject);
                }
            }
           
        }
    }
    void SpawnBallon()
    {
        currentBallon = Instantiate(ballonObj);
        currentBallon.transform.position = transform.position;
        currentBallon.transform.parent = gameObject.transform;
    }
    bool CheckCollision()
    {
        SpriteRenderer mySR;
        mySR = gameObject.GetComponent<SpriteRenderer>();
        foreach (GameObject bullObj in GameObject.FindGameObjectsWithTag("Bullet")){
            SpriteRenderer bullSR = bullObj.GetComponent<SpriteRenderer>();
            if (bullSR.bounds.Intersects(mySR.bounds))
            {
                Destroy(bullObj);
                return true;
            }
        }
        return false;
    }
    bool CheckForFallingSoldiers()
    {
        SpriteRenderer mySR;
        mySR = gameObject.GetComponent<SpriteRenderer>();
        foreach (GameObject soldierObj in GameObject.FindGameObjectsWithTag("Soldier"))
        {
            if ((soldierObj.transform != transform.parent) && (soldierObj.GetComponent<SoldierController>().fallSpeed>2f))
            {
                SpriteRenderer soldierSR = soldierObj.GetComponent<SpriteRenderer>();
                if (soldierSR.bounds.Intersects(mySR.bounds))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
