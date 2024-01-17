using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeaponEffect : MonoBehaviour
{
    //Privates

    //Publics
    public AudioSource Source;
    public AudioClip[] Clip;

    //Scripts
    public PlayerMovement PM;

    private void Update()
    {
        if (PM.PlayerFreeze == true)
        {
            Source.enabled = false;
        }
        else if (PM.PlayerFreeze == false)
        {
            Source.enabled = true;
        }
    }

    public void ShootEffect()
    {
        Source.clip = Clip[0];
        Source.Play();
    }

    public void MagOutEffect()
    {
        Source.clip = Clip[1];
        Source.Play();
    }

    public void MagInEffect()
    {
        Source.clip = Clip[2];
        Source.Play();
    }

    public void ChamberEffect()
    {
        Source.clip = Clip[3];
        Source.Play();
    }
}
