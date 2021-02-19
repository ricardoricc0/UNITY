using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float horizontalMove = 0f; // Axis Input
	public float runSpeed = 40f; // Player Velocity Controll

	bool jump = false;
	bool crouch = false;
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
        {
			jump = true; // true iquals jumping
			animator.SetBool("IsJumping", true);
        }

		if (Input.GetButtonDown("Crouch"))
        {
			crouch = true; 
        }
		else if (Input.GetButtonUp("Crouch"))
        {
			crouch = false;
        }

	}

	public void OnLanding ()
    {
		animator.SetBool("IsJumping", false);
    }

	public void OnCrouching (bool isCrouching)
    {
		animator.SetBool("IsCrouching", isCrouching);
    }

	void FixedUpdate ()
    {
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); // horizontalMove, Crouch, Jump
		jump = false; // Make jump once for press the button
    }
}
