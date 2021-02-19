using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed;
    private bool forwordDirection;
    private int flightLevel;
    public GameObject TraficObj;
    public GameObject explodeObj;
    public GameObject boomObj;

    public GameObject soldierObj;
    private bool payloadFlag;
    private float payloadPos;
    // Start is called before the first frame update
    void Start()
    {
        TraficObj = GameObject.FindWithTag("Traffic");
        Vector3 newPos = new Vector3();
        newPos = transform.position;
        if (Random.value < 0.5)
        {
            forwordDirection = true;
            newPos.x = -9;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            forwordDirection = false;
            newPos.x = 9;
            speed = -speed;
        }
        flightLevel = TraficObj.GetComponent<TrafficController>().GetAvailableLevel(forwordDirection);
        if (flightLevel == -1)
        {
            Destroy(gameObject);
        }
        newPos.y = 3.5f - flightLevel * 1.0f;
        transform.position = newPos;
        payloadFlag = true;
        payloadPos = Random.Range(-8f, 8f);
        if (payloadPos > 0)
        {
            payloadPos = Mathf.Max(0.8f, payloadPos);
        }
        if (payloadPos <= 0)
        {
            payloadPos = Mathf.Min(-0.8f, payloadPos);
        }


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3();
        newPosition = transform.position;
        newPosition.x += speed * Time.deltaTime;
        transform.position = newPosition;
        GetComponent<AudioSource>().panStereo = newPosition.x / 9f;
        if ((newPosition.x < -9) || (newPosition.x > 9)){
            TraficObj.GetComponent<TrafficController>().LeftLevel(flightLevel);
            Destroy(gameObject);
        }
        if (payloadFlag == true)
        {
            if (forwordDirection && (newPosition.x > payloadPos) || !forwordDirection && (newPosition.x < payloadPos))
            {
                GameObject soldier = Instantiate(soldierObj);
                soldier.transform.position = transform.position;
                payloadFlag = false;
            }
        }
        if (CheckCollision())
        {
            TraficObj.GetComponent<TrafficController>().LeftLevel(flightLevel);
            TraficObj.GetComponent<TrafficController>().AddScore(5);
            explodeSpawn();
            Destroy(gameObject);
        }
    }
    bool CheckCollision()
    {
        SpriteRenderer mySP;
        mySP = gameObject.GetComponent<SpriteRenderer>();
        foreach (GameObject bullObj in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            SpriteRenderer bullSR = bullObj.GetComponent<SpriteRenderer>();
            if (bullSR.bounds.Intersects(mySP.bounds))
            {
                Destroy(bullObj);
                return true;
            }
        }
        return false;
    }
    void explodeSpawn()
    {
        GameObject newExplo;
        Vector3 newPos;
        newPos = transform.position;
        newExplo = Instantiate(explodeObj);
        newExplo.GetComponent<ExplosionController>().CreateExplosion(Random.Range(speed*0.8f,speed), newPos);
        newPos.x = transform.position.x - 0.2f;
        newExplo = Instantiate(explodeObj);
        newExplo.GetComponent<ExplosionController>().CreateExplosion(Random.Range(speed * 0.8f, speed), newPos);
        newPos.x = transform.position.x + 0.2f;
        newExplo = Instantiate(explodeObj);
        newExplo.GetComponent<ExplosionController>().CreateExplosion(Random.Range(speed * 0.8f, speed), newPos);
        GameObject boom;
        boom = Instantiate(boomObj);
        boom.GetComponent<AudioSource>().panStereo = newPos.x / 9f;
        boom.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
    }
    public void CreateExplosion(float initSpeed, Vector3 initPos)
    {
        transform.position = initPos;
        speed = initSpeed;
    }
}
