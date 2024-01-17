using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionDoors : MonoBehaviour
{
  

    //Scripts
    public PlayerMovement PM;

    //Animator
    public Animator DoorAnim;

private void OnTriggerEnter(Collider other){

if (other.tag == "Player"||other.tag == "Car"){
      DoorAnim.SetTrigger("Open");
        }
}

private void OnTriggerExit(Collider other){

if (other.tag == "Player" || other.tag == "Car")
        {
            DoorAnim.SetTrigger("Close");

        }
}
    
}
