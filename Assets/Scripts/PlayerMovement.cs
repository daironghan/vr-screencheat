﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 1f;

    public float rotationSpeed = 100f;

    // For jumping, disabled
    public Transform groundCheck; 
    public float groundDistance = 0.4f;
    public LayerMask groundMask; // Ground layer

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Reset default volocity after jump
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Create moving vector
        // Right - red, forward - blue
        Vector3 move = transform.right * x + transform.forward * z;

        // Moving player
        controller.Move(move * speed * Time.deltaTime);

        // Check if player can jump, disabled
        //if(Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}

        // Falling down
        velocity.y += gravity * Time.deltaTime;

        // Execute the jump
        controller.Move(velocity * Time.deltaTime);
        if(lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
        

    }
}
