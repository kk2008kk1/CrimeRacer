using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamps : MonoBehaviour
{
    //Privates
    private bool Destroyed;
  
    //Publics
    public GameObject Light;
public MeshCollider MC;
    //Scripts
    public DayCycle DC;
    //Animator
    public Animator LampAnimator;

    

    
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car" && Destroyed == false)
        {
            Destroyed = true;
		MC.enabled = false;
Light.SetActive(false);
       //     Light.SetActive(false);
            LampAnimator.SetTrigger("Destroy");
	
        }
        

    
    }

    // Update is called once per frame
  //  void Update()
  //  {
   
       // if(DC.CurentState == "Day" && Destroyed == false )
     //   {
          //  Light.SetActive(false);

     //   }
     //   else if (DC.CurentState == "Night" && Destroyed == false)
     //   {
     //       Light.SetActive(true);
     //   }
  //  }
}
