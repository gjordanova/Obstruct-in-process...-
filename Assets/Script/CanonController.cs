using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public float maxAngle;
    private float gunAngle;
    private bool fireFlag;
    public GameObject bulletObj;
    public float fireDelay;
    private float fireTimer;
    public GameObject TraficObj;
    
    // Start is called before the first frame update
    void Start()
    {
        TraficObj = GameObject.FindGameObjectWithTag("Traffic");
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (TraficObj.GetComponent<TrafficController>().endGame)
        {
            //Vector3 newPos;
            //newPos = transform.position;
            //if (newPos.y > -4.5f)
            //{
            //    newPos.y -= 0.4f * Time.deltaTime;
            //}
            //transform.position = newPos;
        }
        else
        {
            if (GameObject.FindObjectOfType<GameController>().play == true)
            {
              
                Vector3 mousePos = Input.mousePosition;
                float mouseSwing = (mousePos.x * 2) / Screen.width - 1;
                gunAngle = mouseSwing * -maxAngle;
                transform.rotation = Quaternion.AngleAxis(gunAngle, Vector3.forward);
                fireFlag = Input.GetButton("Fire1");
                fireTimer -= Time.deltaTime;
                if (fireFlag && (fireTimer < 0))
                {
                    ShootBullt();
                    fireTimer = fireDelay;
                }
            }
           
        }
        
    }
    void ShootBullt()
    {
        GameObject newBullet = Instantiate(bulletObj);
        newBullet.GetComponent<BulletController>().FireShoot(gunAngle + 90, transform.position);
        TraficObj.GetComponent<TrafficController>().AddScore(-1);
    }
    //void ShootBullet()
    //{
    //    GameObject newBullet = Instantiate(bulletObj);
    //    newBullet.GetComponent<BulletController>().FireShoot(gunAngle + 90, transform.position);
        
    //}
}
