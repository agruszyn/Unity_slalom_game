using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour {

    GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("PlayerCharacter");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        
        if (player.tag != "tumbling" && player.tag != "dead" && other.name != "PlayerCharacter")
        {
            //Debug.Log(other.name);
            //Debug.Log(player.tag);
            Vibration.Vibrate(200);
        }
    }
    private void OnTriggerStay(Collider other)
    {
       // Debug.Log(other.name);
       // Debug.Log(player.tag);
        if (player.tag != "tumbling" && player.tag != "dead" && other.name != "PlayerCharacter")
        {
            Vibration.Vibrate(200);
        }
    }

}
