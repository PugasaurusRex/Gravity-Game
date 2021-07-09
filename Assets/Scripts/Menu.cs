﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Menu Panels
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    // Control Menu
    public GameObject ControlMenu;
    SettingsController Controls;

    int level;

    // Audio
    AudioSource Speaker;
    public AudioSource[] Sources;
    public GameObject volumeSlider;

    public AudioClip ApplySound;
    public AudioClip CancelSound;
    public AudioClip ForwardSound;
    public AudioClip BackwardSound;

    // Start is called before the first frame update
    void Start()
    {
        Speaker = GetComponent<AudioSource>();
        Speaker.volume = PlayerPrefs.GetFloat("volume", 1);

        Sources = FindObjectsOfType<AudioSource>();

        //volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume", 1);

        foreach (AudioSource i in Sources)
        {
            i.volume = PlayerPrefs.GetFloat("volume", 1);
        }

        Controls = ControlMenu.GetComponent<SettingsController>();
        level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (level != 0)
        {
            // Pause
            if (Input.GetKeyDown(Controls.keys["Cancel"]))
            {
                if (Time.timeScale != 0)
                {
                    Speaker.clip = ApplySound;
                    Speaker.PlayOneShot(Speaker.clip);

                    //PauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    Speaker.clip = CancelSound;
                    Speaker.PlayOneShot(Speaker.clip);

                    Time.timeScale = 1;
                    //PauseMenu.SetActive(false);
                }
            }
        }
    }

    public void setPanel(int p)
    {
        Speaker.clip = ForwardSound;
        Speaker.PlayOneShot(Speaker.clip);

        switch (p)
        {
            case 1:
                p1.SetActive(true);
                p2.SetActive(false);
                p3.SetActive(false);
                break;
            case 2:
                p1.SetActive(false);
                p2.SetActive(true);
                p3.SetActive(false);
                break;
            case 3:
                p1.SetActive(false);
                p2.SetActive(false);
                p3.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        try
        {
            Speaker.clip = BackwardSound;
            Speaker.PlayOneShot(Speaker.clip);

            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        catch
        {
            Application.Quit();
        }
    }

    public void Restart()
    {
        Speaker.clip = ApplySound;
        Speaker.PlayOneShot(Speaker.clip);

        Time.timeScale = 1;
        int temp = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(temp);
    }

    public void ResumeGame()
    {
        Speaker.clip = ForwardSound;
        Speaker.PlayOneShot(Speaker.clip);

        //PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Gameover()
    {
        Time.timeScale = 0;
        //UI.SetActive(false);
        //GameOverScreen.SetActive(true);
    }

    public void Victory()
    {
        Time.timeScale = 0;
        //UI.SetActive(false);
        //VictoryScreen.SetActive(true);
    }

    public void VolumeChanged()
    {
        //PlayerPrefs.SetFloat("volume", volumeSlider.GetComponent<Slider>().value);
        PlayerPrefs.Save();

        Sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource i in Sources)
        {
            i.volume = PlayerPrefs.GetFloat("volume", 1);
        }
    }
}