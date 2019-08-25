using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");

        Vector3 movementH = transform.forward * v * Time.deltaTime * movementSpeed;
        Vector3 movementV = transform.right * h * Time.deltaTime * movementSpeed;

        Vector3 movement = movementH + movementV;

        Vector3 rotation = new Vector3(0f, mouseX, 0f) * rotationSpeed * Time.deltaTime;

        transform.position += movement;
        transform.eulerAngles += rotation;
    }
}
