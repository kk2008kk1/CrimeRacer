using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabWeapon : MonoBehaviour
{
    //Privates
    private bool CanInteract;
    //Publics
    public GameObject MessageUI;
    public TextMeshProUGUI InteractionText;
    public string Text;
    public int WeaponNumber;
    //Scripts
    public WeaponManager WM;
    public PlayerMovement PM;

    //Animator

    private void OnTriggerEnter(Collider other)
    {
if (other.tag == "Player")
        {
            CanInteract= true;
            MessageUI.SetActive(true);
            InteractionText.text = Text;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
            MessageUI.SetActive(false);
            InteractionText.text = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && CanInteract == true)
        {
            CanInteract = false;
            MessageUI.SetActive(false);
            InteractionText.text = null;
            WM.SetUpWeapon(WeaponNumber);
        }
    }
}
