
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    //Privates
    private int WeaponOut;
    private float NormalSpeed;
    private bool IsMoving;
    private bool IsRunning;
    private bool IsCrouching;
    private bool CanShoot = true;
    private bool CanReload = true;
    private bool FullReload = false;

    //Publics
    public float RunningSpeed;


    [Header("Weapon Variables")]
    public int[] Weapon; //This will help me recognise whitch weapon i will get
    public GameObject[] WeaponModel;
    public Animator[] WeaponAnimator;// POSITION CHANGED TO HAVE A BETTER INSPECTOR
    public ParticleSystem[] WeaponParticles;
    public bool RealisticOnOff;
    public int[] Ammo;
    public int[] AmmoClip;
    public int[] MaxAmmo;
    public float[] FireRate;
    public float[] WeaponDamage;
    public float[] FullReloadTime;
    public float[] ReloadTime;


    [Header("RayCast Variables")]
    public float[] Range;
    public Transform RayCastStart;
    public LayerMask EnemyLayer;
    //WeaponEffect
    public GameObject WeaponLight;

    //Scripts
    private PlayerMovement PM;

    //Animators
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = GetComponent<PlayerMovement>();
        NormalSpeed = PM.Speed;
        for (int i = 0; i < WeaponParticles.Length; i++)
        {
            WeaponParticles[i].Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region GrabWeapon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponOut = 0;
            GrabWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponOut = 1;
            GrabWeapon();
        }
        #endregion

        #region Movement
        //Change Speed
        if (IsMoving == true)
        {
            if (IsRunning == true)
            {
                PM.Speed = RunningSpeed;
            }
            else
            {
                PM.Speed = NormalSpeed;
            }
        }
        else
        {
            PM.Speed = NormalSpeed;
        }

        //Is Moving
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Walk", true);
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Walk", true);
            IsMoving = true;
        }
        else if (!Input.GetKeyDown(KeyCode.W) || !Input.GetKeyDown(KeyCode.S))
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Walk", false);
            IsMoving = false;
        }

        //Is Running
        if (Input.GetKeyDown(KeyCode.LeftShift) && IsMoving == true)
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Run", true);
            IsRunning = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && IsMoving == true)
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Run", true);
            IsRunning = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Run", false);
            IsRunning = false;
        }
        #endregion

        #region Aim-Shoot-Reload
        //Aim

        //Shoot
        if (Input.GetMouseButtonDown(0) && CanShoot == true && Ammo[Weapon[WeaponOut]] > 0)
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Shoot", true);
            StartCoroutine(Shooting());
        }
        else if (Input.GetMouseButton(0) && CanShoot == true && Ammo[Weapon[WeaponOut]] > 0)
        {
            WeaponAnimator[Weapon[WeaponOut]].SetBool("Shoot", true);
            StartCoroutine(Shooting());
        }
        else if ((!Input.GetMouseButton(0) || Ammo[Weapon[WeaponOut]] == 0) && CanShoot == true && CanReload == true)
        {
            if (Ammo[Weapon[WeaponOut]] == 0)
            {
                WeaponAnimator[Weapon[WeaponOut]].SetBool("Shoot", false);
                WeaponParticles[Weapon[WeaponOut]].Stop();
                StopCoroutine(Shooting());
                StartCoroutine(WeaponReload());
            }
            else
            {
                WeaponAnimator[Weapon[WeaponOut]].SetBool("Shoot", false);
                StopCoroutine(Shooting());
            }
        }

        //Relaod
        if (Input.GetKeyDown(KeyCode.R) && CanShoot == true && CanReload == true && MaxAmmo[Weapon[WeaponOut]] > 0 && (Ammo[Weapon[WeaponOut]] != AmmoClip[Weapon[WeaponOut]]))
        {
            WeaponParticles[Weapon[WeaponOut]].Stop();
            StartCoroutine(WeaponReload());
        }

        #endregion
    }

    #region Weapon Manage
    public void SetUpWeapon(int Number)
    {
        for (int i = 0; i < WeaponModel.Length; i++)
        {
            if (Number == i)
            {
                Weapon[WeaponOut] = Number;
                WeaponModel[Number].SetActive(true);
            }
            else
            {
                WeaponModel[i].SetActive(false);
            }
        }
    }

    public void GrabWeapon()
    {
        for (int i = 0; i < WeaponModel.Length; i++)
        {
            if (Weapon[WeaponOut] == i)
            {
                WeaponAnimator[i].SetTrigger("Grab");
                WeaponModel[i].SetActive(true);
            }
            else
            {
                WeaponModel[i].SetActive(false);
            }
        }
    }
    #endregion

    #region Weapon Mechanics
    //Aim

    //Shoot
    IEnumerator Shooting()
    {
        if (CanShoot == true)
        {
            CanShoot = false;
            if (Ammo[Weapon[WeaponOut]] > 0)
            {
                Ammo[Weapon[WeaponOut]] -= 1;
                WeaponParticles[Weapon[WeaponOut]].Play();
            }


            Debug.DrawRay(RayCastStart.position, RayCastStart.forward * Range[Weapon[Weapon[WeaponOut]]], Color.blue, 2f);
            RaycastHit hit;
            if (Physics.Raycast(RayCastStart.position, RayCastStart.forward, out hit, Range[Weapon[Weapon[WeaponOut]]], EnemyLayer, QueryTriggerInteraction.Ignore))
            {
                Debug.Log(hit.collider.name);
            }

            yield return new WaitForSeconds(FireRate[Weapon[WeaponOut]]);

            CanShoot = true;
        }
    }

    //Reloading
    IEnumerator WeaponReload()
    {
        CanShoot = false;
        CanReload = false;
        WeaponParticles[Weapon[Weapon[WeaponOut]]].Stop();

        //Animation
        if (Ammo[Weapon[WeaponOut]] == 0)
        {
            WeaponAnimator[Weapon[WeaponOut]].SetTrigger("FullReload");
            FullReload = true;
        }
        else
        {
            WeaponAnimator[Weapon[WeaponOut]].SetTrigger("Reload");
            FullReload = false;
        }

        //Bullets
        if (RealisticOnOff == true)
        {
            Ammo[Weapon[WeaponOut]] = 0;
            MaxAmmo[Weapon[WeaponOut]] -= AmmoClip[Weapon[WeaponOut]];
            Ammo[Weapon[WeaponOut]] = AmmoClip[Weapon[WeaponOut]];
        }
        else
        {
            MaxAmmo[Weapon[WeaponOut]] += Ammo[Weapon[WeaponOut]];
            Ammo[Weapon[WeaponOut]] = 0;
            if (MaxAmmo[Weapon[WeaponOut]] - AmmoClip[Weapon[WeaponOut]] < 0)
            {
                Ammo[Weapon[WeaponOut]] += MaxAmmo[Weapon[WeaponOut]];
                MaxAmmo[Weapon[WeaponOut]] = 0;
            }
            else
            {
                MaxAmmo[Weapon[WeaponOut]] -= AmmoClip[Weapon[WeaponOut]];
                Ammo[Weapon[WeaponOut]] += AmmoClip[Weapon[WeaponOut]];
            }
        }

        if (FullReload == true)
        {
            yield return new WaitForSeconds(FullReloadTime[Weapon[WeaponOut]]);
        }
        else
        {
            yield return new WaitForSeconds(ReloadTime[Weapon[WeaponOut]]);
        }
        CanShoot = true;
        CanReload = true;
    }
    #endregion
}
