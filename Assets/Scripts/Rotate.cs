using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    GameObject player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("PlayerCharacter");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            transform.position = player.transform.position;
            transform.Rotate(Vector3.up, Time.deltaTime, Space.World);
        }
	}
}
