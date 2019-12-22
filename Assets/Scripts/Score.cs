using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    public int score = 0;
    int completion;
    Text text;
    private bool gameOn;
    private int framerate = 0;
    public int level = 0;
    public int total_level = 0;
    public float multiplier = 1.0f;
    private int level_Length = 300;
    // Use this for initialization
    void Start()
    {
        gameOn = true;
        text = GetComponent<Text>();
        //transform.position = new Vector3(distance.x, transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = Vector3.Dot(Input.gyro.gravity*90, Vector3.left); //small sides -> bottom down and top up is + (this is the important one)
        pos.y = Vector3.Dot(Input.gyro.gravity*90, Vector3.down); // long sides -> bottom left and top right is +
        pos.x = Vector3.Dot(Input.gyro.gravity*90, Vector3.back); // faces -> face down back up is +

        if (PlayerPrefs.GetInt("pause") == 0)
        {
            if (framerate == 4)
            {
            if (gameOn == true)
            {
                score = score + 1;
                completion = score - (total_level * level_Length);
            }

            CalculateLevel();

            text.text = "Score: " + score;
            //text.text = "here" + m_Gyro.enabled + pos;
            framerate = 0;
        }
            framerate++;
        }
        
    }
        // Update is called once per frame
        void Update()
    {

    }


    void CalculateLevel()
    {
        if (completion > level_Length)
        {
            level++;
            total_level++;
            if (level > 3)
            {
                multiplier += 0.2f;
                level = 0;
            }

        }
    }

    public void HighScore()
    {
        
        if (gameOn == true)
        {
            if (PlayerPrefs.HasKey("5") == true && score > PlayerPrefs.GetFloat("5"))
            {
                PlayerPrefs.SetFloat("5", score);
            }
            if (PlayerPrefs.HasKey("4") == true && score > PlayerPrefs.GetFloat("4"))
            {
                PlayerPrefs.SetFloat("5", PlayerPrefs.GetFloat("4"));
                PlayerPrefs.SetFloat("4", score);
            }
            if (PlayerPrefs.HasKey("3") != true || score > PlayerPrefs.GetFloat("3"))
            {
                PlayerPrefs.SetFloat("4", PlayerPrefs.GetFloat("3"));
                PlayerPrefs.SetFloat("3", score);
            }
            if (PlayerPrefs.HasKey("2") != true || score > PlayerPrefs.GetFloat("2"))
            {
                PlayerPrefs.SetFloat("3", PlayerPrefs.GetFloat("2"));
                PlayerPrefs.SetFloat("2", score);
            }
            if (PlayerPrefs.HasKey("1") != true || score > PlayerPrefs.GetFloat("1"))
            {
                PlayerPrefs.SetFloat("2", PlayerPrefs.GetFloat("1"));
                PlayerPrefs.SetFloat("1", score);
            }
        }
        gameOn = false;




    }
}
