using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Total distance the platform moves
    public float moveDistance = 5f; 
    public float moveSpeed = 2f;    

    private Vector3 startPos;
    private float currentDistance;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the platform's current distance from the start position
        currentDistance = Mathf.Abs(transform.position.y - startPos.y);

        // Reverse direction
        if (currentDistance >= moveDistance)
        {
            moveSpeed *= -1;
        }

        // Move the platform
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
