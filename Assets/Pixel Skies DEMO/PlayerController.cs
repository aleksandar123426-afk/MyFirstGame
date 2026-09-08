using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 180f;
    public float hInput;
    public float vInput;

    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        float turnInput = 0f;
        if (Input.GetKey(KeyCode.E))
            turnInput += 1f;
        if (Input.GetKey(KeyCode.Q))
            turnInput -= 1f;

        transform.Rotate(0f, 0f, turnInput * rotationSpeed * Time.deltaTime);

        Vector3 movement = new Vector3(hInput, vInput, 0f).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}