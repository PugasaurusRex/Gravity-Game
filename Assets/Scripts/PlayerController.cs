using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Controller
    public Rigidbody2D Rig;
    public Animator Anim;
    SpriteRenderer SR;

    public GameObject Menu;
    Menu MenuScript;
    SettingsController Controls;

    public GameObject Background;

    // Audio
    AudioSource Speaker;
    public AudioClip JumpSound;
    public AudioClip DieSound;

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
        Camera.main.transform.position = new Vector3(transform.position.x, 0, -10);
        Background.transform.position = new Vector2(transform.position.x, 0);

        if (Time.timeScale != 0 && MenuScript.GameStart)
        {
            Rig.velocity = new Vector2(speed, 0) + new Vector2(0, Rig.velocity.y);

            if (Input.GetKeyDown(Controls.keys["Jump"]))
            {
                Anim.SetBool("Run", false);
                Anim.SetBool("Jump", true);

                Speaker.clip = JumpSound;
                Speaker.PlayOneShot(Speaker.clip);

                if (Rig.gravityScale == 2)
                {
                    Rig.gravityScale = -2;
                    SR.flipY = true;
                }
                else
                {
                    Rig.gravityScale = 2;
                    SR.flipY = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.timeScale != 0 && MenuScript.GameStart)
        {
            if((transform.position.y > 0 && Rig.gravityScale <= 0) || (transform.position.y < 0 && Rig.gravityScale >= 0))
            {
                Anim.SetBool("Run", true);
                Anim.SetBool("Jump", false);
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            Anim.SetBool("Hurt", true);
            MenuScript.GameStart = false;
            StartCoroutine(GameOver(1f));
        }
    }

    IEnumerator GameOver(float waitTime)
    {
        Speaker.clip = DieSound;
        Speaker.PlayOneShot(Speaker.clip);
        yield return new WaitForSeconds(waitTime);
        MenuScript.Gameover();
    }
}
