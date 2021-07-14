using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Controller
    Rigidbody2D Rig;
    public Animator Anim;
    SpriteRenderer SR;

    public GameObject Menu;
    Menu MenuScript;
    SettingsController Controls;

    public GameObject Background;

    // Audio
    AudioSource Speaker;
    public AudioClip JumpSound;

    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Speaker = GetComponent<AudioSource>();
        Speaker.volume = PlayerPrefs.GetFloat("volume", 1);

        Rig = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
        SR = this.GetComponent<SpriteRenderer>();

        Anim.SetBool("Run", false);

        MenuScript = Menu.GetComponent<Menu>();
        Controls = Menu.GetComponent<SettingsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && MenuScript.GameStart)
        {
            Rig.velocity = new Vector2(speed, 0) + new Vector2(0, Rig.velocity.y);
            Camera.main.transform.position = new Vector3(transform.position.x, 0, -10);
            Background.transform.position = new Vector2(transform.position.x, 0);

            if (Input.GetKeyDown(Controls.keys["Jump"]))
            {
                Anim.SetBool("Run", false);
                Anim.SetBool("Jump", true);

                Speaker.clip = JumpSound;
                Speaker.PlayOneShot(Speaker.clip);

                if (Rig.gravityScale == 1)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.timeScale != 0 && MenuScript.GameStart)
        {
            if((transform.position.y > 0 && Rig.gravityScale == -1) || (transform.position.y < 0 && Rig.gravityScale == 1))
            {
                Anim.SetBool("Run", true);
                Anim.SetBool("Jump", false);
            } 
        }
    }
}
