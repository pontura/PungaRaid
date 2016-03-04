﻿using UnityEngine;
using System.Collections;

public class RatiEnding : MonoBehaviour {

    public ParticleSystem particles;

    private Animator anim;
	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
    void Run()
    {
        anim.Play("grenadeRun", 0, 0);
    }
    void Aim()
    {
        anim.Play("grenadeAim", 0, 0);
    }
    void Shoot()
    {
        anim.Play("grenadeFire", 0, 0);
        ShootAgain();
    }
    void ShootAgain()
    {
        if (transform.localScale.x < 0)
        {
            Vector3 rot = particles.gameObject.transform.localEulerAngles;
            rot.x = 24;
            rot.y = 45;
            particles.gameObject.transform.localEulerAngles = rot;
        }
        particles.Play();
        Invoke("ShootAgain", 1);
        Events.OnSoundFX("Gunshot");
    }
}
