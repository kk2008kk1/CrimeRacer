using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Privates
    private float xRotation = 0f;


    //Publics
    public float MouseSensitivity = 100f;
    public Transform PlayerBody;
    public float PositiveRotation;
    public float NegativeRotation;

    //Scripts
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; //Lock and disable cursor
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.PlayerFreeze == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime; //Moving Camera Left and Right
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime; //Moving Camera Up and Down

            xRotation -= mouseY;  //Move the Camera Up and Down
            xRotation = Mathf.Clamp(xRotation, NegativeRotation, PositiveRotation); //Limit camera's rotation to that i added

            PlayerBody.Rotate(Vector3.up * mouseX); //Put the Rotation LeftRight into the PlayerController

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Add the value into the Camera because we move only the camera
            
        }
    }
}