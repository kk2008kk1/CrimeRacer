using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimController : MonoBehaviour
{
    //private
    private bool IsMoving;
    private bool GrabbedWeapon;
    private bool CanGrab = true;

    //publics
    public float SmoothAnim;
    public bool WeaponOut;
    public bool CanShoot = true;

    //scripts
    public PlayerMovement playerMovement;

    //animator
    public Animator SoldierAnimator;

    [Header("Weapon Manager")]
    [Header("PrimaryWeapon")]
    
    public GameObject[] BackPrimaryWeapon;
    public GameObject[] HandPrimaryWeapon;
    public int PrimaryNumber;
    public float[] FireRate;
    public bool HasWeapon;

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.PlayerFreeze == false)
        {
            #region Soldier Movement
            if (Input.GetKey(KeyCode.W))
            {
                SoldierAnimator.SetFloat("Speed", 1, SmoothAnim, Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                SoldierAnimator.SetFloat("Speed", -1, SmoothAnim, Time.deltaTime);
            }
            else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                SoldierAnimator.SetFloat("Speed", 0, SmoothAnim, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                SoldierAnimator.SetFloat("Direction", -1, SmoothAnim, Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                SoldierAnimator.SetFloat("Direction", 1, SmoothAnim, Time.deltaTime);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                SoldierAnimator.SetFloat("Direction", 0, SmoothAnim, Time.deltaTime);
            }
            #endregion

            #region Weapon Anim
            if (Input.GetKeyDown(KeyCode.Alpha1) && HasWeapon == true)
            {
                StartCoroutine(GrabPrimaryWeapon(PrimaryNumber));
            }

            if(Input.GetMouseButton(1) && WeaponOut == true)
            {
                SoldierAnimator.SetBool("Aim", true);
            }
            else if (Input.GetMouseButtonUp(1) && WeaponOut == true)
            {
                SoldierAnimator.SetBool("Aim", false);
            }
            if (Input.GetMouseButtonDown(0) && CanShoot == true && WeaponOut == true)
            {
                SoldierAnimator.SetBool("Shoot", true);
                Shoot();
            }
            else if (Input.GetMouseButton(0) && CanShoot == true && WeaponOut == true)
            {
                SoldierAnimator.SetBool("Shoot", true);
                Shoot();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SoldierAnimator.SetBool("Shoot", false);
            }
            #endregion
        }
    }
    #region Grab Weapon And Setup
    public void PickUpPrimary (int Number)
    {
        for (int i = 0; i< HandPrimaryWeapon.Length; i++) 
        {
            HandPrimaryWeapon[i].SetActive(false);
        }
        for (int i =0; i< BackPrimaryWeapon.Length; i++)
        {
            if (i == Number)
            {
                HasWeapon = true;  
                BackPrimaryWeapon[i].SetActive(true);
                PrimaryNumber = Number;
            }
            else
            {
                BackPrimaryWeapon[i].SetActive(false);
            }
        }
    }

    IEnumerator GrabPrimaryWeapon(int WeaponNumber)
    {
        if (GrabbedWeapon == false && CanGrab == true)
        {
            CanGrab = false;
            SoldierAnimator.SetTrigger("Grab");
            yield return new WaitForSeconds(1f);
            BackPrimaryWeapon[WeaponNumber].SetActive(false);
            HandPrimaryWeapon[WeaponNumber].SetActive(true);
            yield return new WaitForSeconds(2.2f);
            GrabbedWeapon = true;
            CanGrab = true;
            WeaponOut = true; 

        }
        else if (GrabbedWeapon == true && CanGrab == true)
        {
            CanGrab = false;
            SoldierAnimator.SetTrigger("Grab");
            yield return new WaitForSeconds(2f);
            HandPrimaryWeapon[WeaponNumber].SetActive(false);
            BackPrimaryWeapon[WeaponNumber].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            GrabbedWeapon = false;
            CanGrab = true;
            WeaponOut= false;
        }
    }
    #endregion


    #region Shoot Reload
    public void Shoot()
    {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        CanShoot = false;
      
        //RayCast
        yield return new WaitForSeconds(FireRate[PrimaryNumber]);
        CanShoot= true;
    }
    #endregion
}
