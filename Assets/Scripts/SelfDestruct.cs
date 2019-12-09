using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    GameObject player;
    MeshRenderer meshR;
    BoxCollider colliderB;
    public int quadrant;



	// Use this for initialization
	void Start ()
   {
        player = GameObject.Find("GAME MANAGER");
        meshR = GetComponent<MeshRenderer>();
        colliderB = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.tag == "enabled")
        {
            meshR.enabled = true;
            colliderB.enabled = true;
        }

        if (player.transform.position.z > (transform.position.z + 5))
        {
            gameObject.tag = "disabled";
            meshR.enabled = false;
            colliderB.enabled = false;
        }

    }


    void OnTriggerEnter(Collider other)
    {
        gameObject.tag = "disabled";
        meshR.enabled = false;
        colliderB.enabled = false;
    }
}
