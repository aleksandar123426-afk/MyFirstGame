using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float leftLimit = -8f;
    public float rightLimit = 8f;
    public int shipNumber = 1;
    public float rotationSpeed = 180f; // degrees per second when rotating

    private bool isRotating = false;

    private float startY;

    void Start()
    {
        startY = transform.position.y;

        if (shipNumber == 2)
            moveSpeed = Mathf.Abs(moveSpeed);
        else
            moveSpeed = -Mathf.Abs(moveSpeed);
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }

        float nextX = transform.position.x + moveSpeed * Time.deltaTime;

        if (nextX >= rightLimit)
        {
            transform.position = new Vector3(rightLimit, startY, transform.position.z);
            moveSpeed = -Mathf.Abs(moveSpeed);
            return;
        }

        if (nextX <= leftLimit)
        {
            transform.position = new Vector3(leftLimit, startY, transform.position.z);
            moveSpeed = Mathf.Abs(moveSpeed);
            return;
        }

        transform.position = new Vector3(nextX, startY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || !collision.gameObject.CompareTag("Boundary"))
            return;

        if (transform.position.x >= rightLimit)
            moveSpeed = -Mathf.Abs(moveSpeed);
        else if (transform.position.x <= leftLimit)
            moveSpeed = Mathf.Abs(moveSpeed);

        // start continuous right rotation when hitting a boundary
        isRotating = true;
    }
}