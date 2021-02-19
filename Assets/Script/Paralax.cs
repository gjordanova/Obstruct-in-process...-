using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float offset;
    private Vector2 startPos;
    private float newXPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newXPos = Mathf.Repeat(Time.time * -moveSpeed, offset);
        transform.position = startPos + Vector2.right * newXPos;
    }
}
