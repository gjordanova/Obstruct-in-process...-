using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colecctables : MonoBehaviour
{
    public float speed;
    public GameObject TrafficObj;
    // Start is called before the first frame update
    void Start()
    {
        TrafficObj = GameObject.FindWithTag("Traffic");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3();
        newPosition = transform.position;
        newPosition.y -= speed * Time.deltaTime;
        transform.position = newPosition;
        if (CheckCollisions())
        {
            Debug.Log("pogodi");
            Destroy(gameObject);
            //TrafficObj.GetComponent<TrafficController>().AddScore(1);
        }
    }
    bool CheckCollisions()
    {
        SpriteRenderer SR;
        SR = gameObject.GetComponent<SpriteRenderer>();
        
        foreach (GameObject bullObj in GameObject.FindGameObjectsWithTag("Bullet"))      
        {
            SpriteRenderer bullSR = bullObj.GetComponent<SpriteRenderer>();
            if (bullSR.bounds.Intersects(SR.bounds))
            {
                Destroy(bullObj);
                return true;
            }
        }
        return false;
    }
}
