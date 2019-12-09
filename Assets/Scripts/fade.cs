using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour {



    MeshRenderer meshR;
    public float myAlpha;
    Renderer rend;
    private GameObject player;
    private float transition;
    public float blind;
    private float fStartTime;

    // Use this for initialization
    void Start ()
    {
	    meshR = GetComponent<MeshRenderer>();
        meshR.enabled = false;
        player = GameObject.Find("PlayerCharacter");
        rend = GetComponent<Renderer>();
        // rend.material.shader = Shader.Find("standard");
        //rend.material.SetColor("_SpecColor", Color.red);
        rend.material.SetColor("_Color", new Color(0, 0, 0, 0));
        transition = 0;
        fStartTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            if (player.tag == "tumbling")
            {
                meshR.enabled = true;
                rend.material.SetColor("_Color", new Color(255, 255, 255, 255));
                if (fStartTime == 0)
                {
                    fStartTime = Time.time;
                }
                if (Time.time - fStartTime > 0.05f)
                {
                    meshR.enabled = false;
                    rend.material.SetColor("_Color", new Color(0, 0, 0, 0));
                }
            }

            if (player.tag == "dead")
            {
                meshR.enabled = true;
                //rend.material.SetColor("_Color", new Color(rend.material.color.r, rend.material.color.b, rend.material.color.g, 0));
                rend.material.SetColor("_Color", new Color(transition, 0, 0, blind));
                if (transition < 255)
                {
                    transition = transition + .02f;
                }
                if (blind < 255)
                {
                    blind = blind + .01f;
                }
                if (blind >= 2f)
                {
                    SceneManager.LoadScene("Scoreboard");
                }
            }
        }
    }
}
