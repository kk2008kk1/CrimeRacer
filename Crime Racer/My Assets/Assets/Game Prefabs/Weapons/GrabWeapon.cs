using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWeapon : MonoBehaviour
{
    //Privates
    private bool CanInteract;
    //Publics
    public GameObject InteractionMessageUI;
    public GameObject Model;
    public int WeaponNumber;
    //Scripts
    public SoldierAnimController soldierAnimController;

    //Animator

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract= true;
            InteractionMessageUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
            InteractionMessageUI.SetActive(false);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract == true)
        {
            CanInteract= false;
            InteractionMessageUI.SetActive(false);
            Model.SetActive(false);
            soldierAnimController.PickUpPrimary(WeaponNumber);
            soldierAnimController.HasWeapon = true;
        }
    }
}
