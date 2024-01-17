using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewWeaponInteraction : MonoBehaviour
{
    //Privates
    private bool CanInteract;

    //Publics
    public GameObject Message;
    public int Cost;
    public TextMeshProUGUI CostText;

    [Header("Weapon Variables")]
    public int WeaponNumber;
    public int Ammo;
    public int AmmoClip;
    public int MaxAmmo;

    //Scripts
    public PlayerMovement PM;
    public NewWeaponManager NewWM;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = true;
            Message.SetActive(true);
            CostText.text = Cost.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
            Message.SetActive(false);
            CostText.text = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract == true)
        {
                CanInteract = false;
                Message.SetActive(false);
                CostText.text = null;
                NewWM.Ammo[NewWM.WeaponOut] = Ammo;
                NewWM.AmmoClip[NewWM.WeaponOut] = AmmoClip;
                NewWM.MaxAmmo[NewWM.WeaponOut] = MaxAmmo;
                NewWM.WeaponSetUp(WeaponNumber);
        }
    }
}
