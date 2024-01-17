using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionBars : MonoBehaviour
{


    //Scripts
    public PlayerMovement PM;

    //Animator
    public Animator Bars;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" || other.tag == "Car")
        {
            Bars.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player" || other.tag == "Car")
        {
            Bars.SetTrigger("Close");

        }
    }

}
