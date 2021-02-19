using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollision())
        {
            Destroy(gameObject);
        }
    }
    bool CheckCollision()
    {
        SpriteRenderer mySR;
        mySR = gameObject.GetComponent<SpriteRenderer>();
        foreach (GameObject bullObj in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            SpriteRenderer bullSR = bullObj.GetComponent<SpriteRenderer>();
            if (bullSR.bounds.Intersects(mySR.bounds))
            {
                Destroy(bullObj);
                return true;
            }
        }
        foreach (GameObject soldierObj in GameObject.FindGameObjectsWithTag("Soldier"))
        {
            if (soldierObj.transform != transform.parent)
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
