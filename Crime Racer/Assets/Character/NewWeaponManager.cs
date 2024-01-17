using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewWeaponManager : MonoBehaviour
{
    //Privates
    private float CurrentSpeed;
    private bool IsMoving;
    private bool IsRunning;

    private bool CanShoot = true;
    private bool CanReload = true;
    private bool IsReloading = false;

    //Publics
    [Header("Weapon Variables")]
    [Tooltip("Weapon out will help the Weapon[] recognise whitch weapon player has out inside in her elements 1/2")]
    public int WeaponOut;
    public int[] Weapon;
    public GameObject[] WeaponModel;
    public Animator[] WeaponAnimator;

    [Header("Ammo Variables")]
    public bool Realistic;
    public int[] Ammo;
    public int[] AmmoClip;
    public int[] MaxAmmo;
    public float[] ReloadTime;

    [Header("Gun Variables")]
    public float[] FireRate;
    public float[] Damage;
    public float[] Range;

    [Header("RayCastVariables")]
    public Transform RayCastStart;
    public LayerMask EnemyLayer;

    //Scripts
    public PlayerMovement PM;

    //Animator
    public Animator MoveAnimator;

    //[Header("UI Variables")]
    //public TextMeshProUGUI AmmoText;
    //public TextMeshProUGUI MaxAmmoText;
    //public TextMeshProUGUI HealthText;
    //public TextMeshProUGUI GoldText;

    private void Start()
    {
        CurrentSpeed = PM.Speed;
    }

    private void Update()
    {
        if (PM.PlayerFreeze == false)
        {
            #region Movement
            //Variables
            if (IsMoving == true && IsRunning == false)
            {
                MoveAnimator.SetBool("Run", false);
                MoveAnimator.SetBool("Walk", true);
                PM.Speed = CurrentSpeed;
            }
            else if (IsMoving == true && IsRunning == true)
            {
                MoveAnimator.SetBool("Run", true);
                PM.Speed = CurrentSpeed * 2;
            }
            else
            {
                MoveAnimator.SetBool("Run", false);
                MoveAnimator.SetBool("Walk", false);
                PM.Speed = 0;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)  || Input.GetKeyDown(KeyCode.D))
            {
                IsMoving = true;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                IsMoving = true;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    IsRunning = true;
                }
                else if (!Input.GetKeyDown(KeyCode.LeftShift))
                {
                    IsRunning = false;
                }
            }
            else if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D))
            {
                IsMoving = false;
                IsRunning = false;
            }
            #endregion

            #region Grab Shoot Reload
            //Grab Code
            if (Input.GetKeyDown(KeyCode.Alpha1) && CanShoot == true && CanReload == true && WeaponOut != 0)
            {
                WeaponOut = 0;
                StartCoroutine(GrabWeapon());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && CanShoot == true && CanReload == true && WeaponOut != 1)
            {
                WeaponOut = 1;
                StartCoroutine(GrabWeapon());
            }

            //Shooting
            if (IsReloading == false)
            {
                if (Input.GetMouseButtonDown(0) && CanShoot == true && CanReload == true && Ammo[WeaponOut] > 0)
                {
                    WeaponAnimator[Weapon[WeaponOut]].SetTrigger("SingleShot");
                    WeaponAnimator[Weapon[WeaponOut]].SetBool("Spray", true);
                    StartCoroutine(Shoot());
                }

                if (Input.GetMouseButton(0) && CanShoot == true && CanReload == true && Ammo[WeaponOut] > 0)
                {
                    WeaponAnimator[Weapon[WeaponOut]].SetTrigger("SingleShot");
                    WeaponAnimator[Weapon[WeaponOut]].SetBool("Spray", true);
                    StartCoroutine(Shoot());
                }

                if (Input.GetMouseButtonUp(0) || Ammo[WeaponOut] == 0)
                {
                    WeaponAnimator[Weapon[WeaponOut]].SetBool("Spray", false);
                    StopCoroutine(Shoot());
                }
            }
            
            //Reloading
            if (Input.GetKeyDown(KeyCode.R) && CanShoot == true && CanReload == true && Ammo[WeaponOut] < AmmoClip[WeaponOut] && MaxAmmo[WeaponOut] > 0)
            {
                StartCoroutine(Reload());
            }
            #endregion
        }

        //#region Assign UI
        //AmmoText.text = Ammo[WeaponOut].ToString();
        //MaxAmmoText.text = MaxAmmo[WeaponOut].ToString();
        //HealthText.text = PM.Health.ToString();
        //#endregion
    }

    #region WeaponSetUp
    public void WeaponSetUp(int Number)
    {
        Weapon[WeaponOut] = Number;
        StartCoroutine(GrabWeapon());
    }
    #endregion

    #region Grab Weapon
    IEnumerator GrabWeapon()
    {
        CanShoot = false;
        CanReload = false;
        WeaponAnimator[Weapon[WeaponOut]].SetTrigger("Out");
        //AmmoText.enabled = false;
        //MaxAmmoText.enabled = false;
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < WeaponModel.Length; i++)
        { 
            if (Weapon[WeaponOut] == i)
            {
                WeaponModel[i].SetActive(true);
                WeaponAnimator[i].SetTrigger("Grab");
                //AmmoText.enabled = true;
                //MaxAmmoText.enabled = true;
            }
            else
            {
                WeaponModel[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(1f);
        CanShoot = true;
        CanReload = true;
    }
    #endregion

    #region Shoot
    IEnumerator Shoot()
    {
        CanShoot = false;
        CanReload = false;
        WeaponAnimator[Weapon[WeaponOut]].SetTrigger("SingleShot");
        Ammo[WeaponOut] -= 1;
        Debug.DrawRay(RayCastStart.position, RayCastStart.forward * Range[WeaponOut], Color.red, 2f);
        RaycastHit hit;
        if (Physics.Raycast(RayCastStart.position, RayCastStart.forward, out hit, Range[WeaponOut], EnemyLayer, QueryTriggerInteraction.Ignore))
        {
            Debug.Log(hit.collider.name);
        }
        yield return new WaitForSeconds(FireRate[Weapon[WeaponOut]]);
        CanShoot = true;
        CanReload = true;
    }
    #endregion

    #region Reload
    IEnumerator Reload()
    {
        CanShoot = false;
        CanReload = false;
        IsReloading = true;
        WeaponAnimator[Weapon[WeaponOut]].SetTrigger("Reload");
        WeaponAnimator[Weapon[WeaponOut]].SetBool("IsReloading", true);
        if (Realistic == true)
        {
            Ammo[WeaponOut] = AmmoClip[WeaponOut];
            MaxAmmo[WeaponOut] -= AmmoClip[WeaponOut];
            yield return new WaitForSeconds(ReloadTime[Weapon[WeaponOut]]);
        }
        else
        {
            MaxAmmo[WeaponOut] += Ammo[WeaponOut];
            Ammo[WeaponOut] = 0;
            yield return new WaitForSeconds(ReloadTime[Weapon[WeaponOut]]);
            if (MaxAmmo[WeaponOut] - AmmoClip[WeaponOut] < 0)
            {
                Ammo[WeaponOut] += MaxAmmo[WeaponOut];
                MaxAmmo[WeaponOut] = 0;
            }
            else
            {
                Ammo[WeaponOut] += AmmoClip[WeaponOut];
                MaxAmmo[WeaponOut] -= AmmoClip[WeaponOut];
            }
        }
        WeaponAnimator[Weapon[WeaponOut]].SetBool("IsReloading", false);
        CanShoot = true;
        CanReload = true;
        IsReloading = false;
    }
    #endregion

}
