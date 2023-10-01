using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZG : MonoBehaviour
{
    //public float rotateSpeed;
    //public float turnSpeed;
    //public float downAndUpSpeed;
    public float speed;
    public GameObject mainCamera; // Reference to the main camera.

    private Rigidbody rb;

    public RayGun rayGun;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (speed > 0)
        {
            speed = 5;
        }

        if (Input.GetButton("RightTrigger"))
        {
            //rayGun.Painting = true;
            
            Debug.Log("Yes!");
        }
        //transform.Rotate(new Vector3(0, 0, -1) * rotateSpeed * Input.GetAxis("Roll") * Time.deltaTime);
        //transform.Rotate(new Vector3(1, 0, 0) * downAndUpSpeed * Input.GetAxis("UpDown") * Time.deltaTime);
        //transform.Rotate(new Vector3(0, 1, 0) * turnSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);

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
