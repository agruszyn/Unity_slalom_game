using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    AudioSource my_audio;
    Slider my_slider;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("PlayerCharacter");
        my_audio = player.GetComponent<AudioSource>();
        my_slider = GetComponent<Slider>();
        Debug.Log("volume script " + Mathf.Sqrt(PlayerPrefs.GetFloat("volume")));
        my_slider.value = PlayerPrefs.GetFloat("volume");
        my_slider.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(float newValue)
    {
        PlayerPrefs.SetFloat("volume", newValue);
        my_audio.volume = Mathf.Pow(newValue, 2.0f);
        if (newValue > 0.01f)
        {
            PlayerPrefs.SetInt("enable_sound", 1);
        }
    }

}
