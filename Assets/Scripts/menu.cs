using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {





    // Use this for initialization
   void Start ()
    {

    }
	




	// Update is called once per frame
	void Update ()
    {
	
	}

    public void SetScene(int scene)
    {
        if (scene == 1)
        {
            SceneManager.LoadScene("jet plane");
        }
        if (scene == 0)
        {
            SceneManager.LoadScene("menu");
        }
        if (scene == 2)
        {
            SceneManager.LoadScene("Scoreboard");
        }
        if (scene == 3)
        {
            SceneManager.LoadScene("Settings");
        }

    }



}
