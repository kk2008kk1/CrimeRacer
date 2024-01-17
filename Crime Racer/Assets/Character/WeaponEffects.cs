using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    public AudioSource WeaponAudio;
    public AudioClip[] Clip; //Clip[0] = Shoot Audio, Clip[1] = MagOut Audio, Clip[2] = MagIn Audio, Clip[3] = FullReload Audio
    public ParticleSystem Particle;

    public void ShootEffect()
    {
        WeaponAudio.PlayOneShot(Clip[0]);
        Particle.Play();
    }

    public void MagOutEffect()
    {
        WeaponAudio.PlayOneShot(Clip[1]);
    }

    public void MagInEffect()
    {
        WeaponAudio.PlayOneShot(Clip[2]);
    }

    public void FullReload()
    {
        WeaponAudio.PlayOneShot(Clip[3]);
    }
}
