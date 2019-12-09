using System.Collections;
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
        sound.mute = (PlayerPrefs.GetInt("mute") == 1);

        //Change icon texture
        icon = this.GetComponent<Image>();
       // on = Resources.Load("megaphone") as Sprite;
       // off = Resources.Load("megaphoneOff") as Sprite;
        if (PlayerPrefs.GetInt("mute") == 0)
        { icon.overrideSprite = off; }
        else { icon.overrideSprite = null; }


        //find the component controlling the button
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //is the game currently muted?
        int i = PlayerPrefs.GetInt("mute");

        //change the status of muting
        if (i == 1)
        { i = 0; }
        else { i = 1; }

        //save the status of muting
        PlayerPrefs.SetInt("mute", i);

        //mute game
        sound.mute = (PlayerPrefs.GetInt("mute") == 1);

        //Change icon
        if (PlayerPrefs.GetInt("mute") == 0)
        { icon.overrideSprite = off; }
        else { icon.overrideSprite = null; }
    }
}
	
