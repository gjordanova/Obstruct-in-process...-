using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public GameObject collectables;
    public float launchDelayMin;
    public float launchDelayMax;
    private float launchTimer;
    public GameObject traffController;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 newPos = new Vector3();
        //newPos = transform.position;
        //if (Random.value < 0.5)
        //{
        //    newPos.y = -9;
        //    Instantiate(collectables);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        launchTimer -= Time.deltaTime;
        
        if (launchTimer < 0)
        {
            if (!traffController.GetComponent<TrafficController>().endGame)
            {
                var position = new Vector3(Random.Range(-10, 10), 10, 0);

                Instantiate(collectables, position, Quaternion.identity);
                //position.y = speed * Time.deltaTime;
                launchTimer = Random.Range(launchDelayMin, launchDelayMax);

            }
        

        }

    }

}
