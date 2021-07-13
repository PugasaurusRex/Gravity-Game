﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Controller
    Rigidbody2D Rig;
    Animator Anim;
    SpriteRenderer SR;

    public GameObject ControlMenu;
    SettingsController Controls;

    public GameObject Menu;
    Menu MenuScript;

    // Audio
    AudioSource Speaker;
    public AudioClip JumpSound;

    // Start is called before the first frame update
    void Start()
    {
        Speaker = GetComponent<AudioSource>();
        Speaker.volume = PlayerPrefs.GetFloat("volume", 1);

        Controls = ControlMenu.GetComponent<SettingsController>();

        Rig = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
        SR = this.GetComponent<SpriteRenderer>();

        Anim.SetBool("Run", false);

        MenuScript = Menu.GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && MenuScript.GameStart)
        {
            Anim.SetBool("Run", true);

            Speaker.clip = JumpSound;
            Speaker.PlayOneShot(Speaker.clip);

            if (Input.GetKeyDown(Controls.keys["Jump"]))
            {
                if(Rig.gravityScale == 1)
                {
                    Rig.gravityScale = -1;
                    SR.flipY = true;
                }
                else
                {
                    Rig.gravityScale = 1;
                    SR.flipY = false;
                }
            }
        }
    }
}
