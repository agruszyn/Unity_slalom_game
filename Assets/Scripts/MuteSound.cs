﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{

    private Button myButton; 
    private AudioSource sound;
    private GameObject player;
    private Image icon;
    private Sprite on;
    public Sprite off;

    void Start()
    {
        //find the component controlling audio
        player = GameObject.Find("PlayerCharacter");
        sound = player.GetComponent<AudioSource>();

        //mute and unmute the sound on first load
        if (PlayerPrefs.HasKey("volume"))
        {
            sound.volume = Mathf.Pow(PlayerPrefs.GetFloat("volume"), 2.0f);
        }
        else
        {
            PlayerPrefs.SetFloat("volume", 0.4f);
            sound.volume = Mathf.Pow(0.4f, 2.0f);
        }

        //mute and unmute the sound on first load
        if (PlayerPrefs.HasKey("enable_sound"))
        {
            sound.mute = (PlayerPrefs.GetInt("enable_sound") == 0);
        }
        else
        {
            PlayerPrefs.SetInt("enable_sound", 1);
            sound.mute = false;
        }

        //Change icon texture
        icon = this.GetComponent<Image>();

        if (PlayerPrefs.GetInt("enable_sound") == 1)
        { icon.overrideSprite = off; }
        else { icon.overrideSprite = null; }


        //find the component controlling the button
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //is the game currently muted?
        int i = PlayerPrefs.GetInt("enable_sound");

        //change the status of muting
        if (i == 1)
        { i = 0; }
        else { i = 1; }

        //save the status of muting
        PlayerPrefs.SetInt("enable_sound", i);

        //mute game
        sound.mute = (PlayerPrefs.GetInt("enable_sound") == 0);

        //Change icon
        if (PlayerPrefs.GetInt("enable_sound") == 1)
        { icon.overrideSprite = off; }
        else { icon.overrideSprite = null; }
    }
}
	
