using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZG : MonoBehaviour
{
    public float downAndUpSpeed;
    public float speed;
    public GameObject mainCamera; // Reference to the main camera.

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        

        if (Input.GetButton("RightTrigger"))
        {
            Debug.Log("Yes!");
        }

        float upDownInput = Input.GetAxis("UpDown");

        Vector3 upDownForce = Vector3.up * upDownInput * downAndUpSpeed;

        rb.AddForce(upDownForce * Time.deltaTime);

        // Check if a camera is assigned
        if (mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;

            // Normalize the directions
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate movement based on input axes
            Vector3 moveDirection = (cameraForward * Input.GetAxis("Vertical") +
                                     cameraRight * Input.GetAxis("Horizontal")).normalized;

            // Apply force in the calculated direction
            rb.AddForce(moveDirection * speed * Time.deltaTime);

        }
        else
        {
            Debug.LogWarning("Main camera is not assigned!");
        }
    }
}
