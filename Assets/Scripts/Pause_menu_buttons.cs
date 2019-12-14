using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_menu_buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            if (GetComponentInParent<Button>().IsInteractable() == true)
            {
                GetComponentInParent<Button>().interactable = false;
            }
            if (GetComponentInParent<Image>().enabled == true)
            {
                GetComponentInParent<Image>().enabled = false;
            }
            if (GetComponentInChildren<Text>().enabled == true)
            {
                GetComponentInChildren<Text>().enabled = false;
            }
        }

        else if (PlayerPrefs.GetInt("pause") == 1)
        {
            if (GetComponentInParent<Button>().IsInteractable() == false)
            {
                GetComponentInParent<Button>().interactable = true;
            }
            if (GetComponentInParent<Image>().enabled == false)
            {
                GetComponentInParent<Image>().enabled = true;
            }
            if (GetComponentInChildren<Text>().enabled == false)
            {
                GetComponentInChildren<Text>().enabled = true;
            }
        }

    }
}
