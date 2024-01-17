using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetInTheCar : MonoBehaviour
{
    //Privates 
    private bool CanInteract;
    private bool InTheCar = false;
    public GameObject Meter;
    //Publics
    public GameObject InteractionMessageUI;
    public string InteractionText;
    public TextMeshProUGUI NEWInteractionText;

    public GameObject Player;
    public GameObject CarCamera;

    public Transform PlayerTransform;
    public Transform CarTransform;
    public Rigidbody CarRigidBody;
    public AudioSource[] CarAudio;

    //Scripts
    public PlayerMovement playerMovement;
    public VehicleControl vehicleControl;

   


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract= true;
            InteractionMessageUI.SetActive(true);
            NEWInteractionText.text = InteractionText;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
            InteractionMessageUI.SetActive(false);
            NEWInteractionText.text = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Meter.SetActive(false);
        InTheCar = false;
        Player.SetActive(true);
        CarCamera.SetActive(false);
        playerMovement.PlayerFreeze = false;
        vehicleControl.activeControl = false;
        CarAudio[0].Stop();
        CarAudio[1].Stop();
        CarAudio[2].Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract == true)
        {
            if (InTheCar == false)
            {
                CanInteract = false;
                InteractionMessageUI.SetActive(false);
                NEWInteractionText.text = null;
                InTheCar = true;
                Meter.SetActive(true) ;
                Player.SetActive(false);
                CarCamera.SetActive(true);
                playerMovement.PlayerFreeze = true;
                vehicleControl.activeControl = true;
                CarAudio[0].Play();
                CarAudio[1].Play();
               
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && InTheCar == true)
        {
        
            InTheCar = false;
            Meter.SetActive(false);
            Player.SetActive(true);
            CarCamera.SetActive(false);
            playerMovement.PlayerFreeze = false;
            vehicleControl.activeControl = false;
            CarAudio[0].Stop();
            CarAudio[1].Stop();
            CarAudio[2].Stop();
            StartCoroutine(DragCar());
        }
        if (InTheCar == true)
        {
            PlayerTransform.position = CarTransform.position;
        }
    }

    IEnumerator DragCar()
    {
        CarRigidBody.drag = 8;
        yield return new WaitForSeconds(4f);
        CarRigidBody.drag = 0;
    }
}
