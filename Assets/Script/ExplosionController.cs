using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private float speed;
    private float spin;
    public float gravityAmount;
    private float fallSpeed;
    public float lifeTime;
    private float currentLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        spin = Random.Range(360 * 2, 360 * 5);
        currentLifeTime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3();
        newPos = transform.position;
        newPos.x += speed * Time.deltaTime;
        newPos.y -= fallSpeed * Time.deltaTime;
        transform.position = newPos;
        fallSpeed += gravityAmount * Time.deltaTime;
        transform.Rotate(Vector3.forward * (Time.deltaTime * spin));

        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime < 0)
        {
            Destroy(gameObject);

        }
        else
        {
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = currentLifeTime / lifeTime;
            GetComponent<SpriteRenderer>().color = col;
        }
    }
    public void CreateExplosion(float initSpeed, Vector3 initPos)
    {
        transform.position = initPos;
        speed = initSpeed;
        fallSpeed = Random.Range(-1f, 0);
    }
}
