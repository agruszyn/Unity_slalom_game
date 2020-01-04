using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Flash_Text : MonoBehaviour
{
    private GameObject score_Keeper;
    private Score score;
    private Text self;
    private int previous_round;
    private bool start_flashing;
    private float start_time;
    private float flash_start_time;
    private bool text_active;

    void Start()
    {
        previous_round = 0;
        score_Keeper = GameObject.Find("Score");
        score = score_Keeper.GetComponent<Score>();
        self = this.GetComponent<Text>();
        text_active = false;
        self.enabled = text_active;
        start_flashing = false;
    }

    void Update()
    {
        if (previous_round != score.rounds_In_Game && (score.rounds_In_Game < 8 || gameObject.name == "SPEED++")) 
        {
            previous_round = score.rounds_In_Game;
            start_flashing = true;
            text_active = true;
            self.enabled = text_active;
            start_time = Time.time;
            flash_start_time = start_time;
            Debug.Log(text_active);
            Debug.Log(Time.time - start_time);
        }

        if (start_flashing == true)
        {
            if (Time.time - flash_start_time > 0.5f)
            {
                text_active = !text_active;
                self.enabled = text_active;
                flash_start_time = Time.time;
            }
            if (Time.time - start_time > 6.0f)
            {
                start_flashing = false;
                text_active = false;
                self.enabled = text_active;
            }
        }
    }


}
