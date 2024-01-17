using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Privates
    private bool isGrounded;
    Vector3 Velocity;

    //Publics
    public CharacterController characterController;
    public float Speed;

    public float JumpHeight = 3f;

    public float Gravity = -9.12f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Player Variables
    public float Stamina;
    public float Battery;
    public bool PlayerFreeze;

    public Animator animator;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerFreeze == false)
        {
            #region Character Movement
            float x = Input.GetAxis("Horizontal"); //Take Access to Horizontal Axis from A = -1 and D = 1
            float z = Input.GetAxis("Vertical");   //Take Access to Verical Axis from S = -1 and W = 1

            Vector3 move = transform.right * x + transform.forward * z; //Create the direction based where the player looks

            characterController.Move(move * Speed * Time.deltaTime);
            #endregion

            #region Jumping
            //if (Input.GetButtonDown("Jump") && isGrounded)
            //{
            //Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity); 
            //}
            #endregion

            #region Velocity and Gravity
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Check if the player is grounded

            if (isGrounded && Velocity.y < 0)
            {
                Velocity.y = -2f;
            }

            Velocity.y += Gravity * Time.deltaTime; //Applying Gravity
            characterController.Move(Velocity * Time.deltaTime); //Put the Gravity into the player
            #endregion
        }
    }
}