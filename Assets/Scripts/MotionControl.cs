using UnityEngine;
using System.Collections;

public class MotionControl : MonoBehaviour {

    // Use this for initialization
    public Vector3 orientation;
    public float jetSpeed;
    public float strafeScale;
    public float multiplierSpeed;
    public float strafeDebug;
    public Vector3 tumble;

    //private GameManager center;
    //private CardboardHead viewPort;
    private Game_Master world;
    private TranslatePlayer speed;
    private GameObject scoreboard;
    private Score myScore;
    private float angularDirection;
    public bool bWait;
    private float fStartTime;
    public bool vrMode;


    void Start()
    {
        scoreboard = GameObject.Find("Score");
        myScore = scoreboard.GetComponent<Score>();
        vrMode = PlayerPrefs.GetInt("VRmode") == 1;
        //viewPort = GetComponentInChildren<CardboardHead>();
        orientation.y = 0;
        world = GetComponentInParent<Game_Master>();
        speed = GetComponentInParent<TranslatePlayer>();
        strafeScale = 0.01f + speed.jetSpeed * 0.002f;
    }
    //void Update()
    //   {
    //       OnCollisionEnter();
    //   }

    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            vrMode = PlayerPrefs.GetInt("VRmode") == 1;

            if (this.tag != "tumbling" && this.tag != "dead")
            {
                RotationCalculator();
            }
            else
            {
                Tumble();
            }
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            world.fUserOffsetx = world.fUserOffsetx + angularDirection * strafeScale;
            if (Input.GetKey("right"))
            {
                world.fUserOffsetx = world.fUserOffsetx - 45 * strafeScale;
                world.fUserOffsetT = world.fUserOffsetT - 45 * strafeScale;
            }
            else if (Input.GetKey("left"))
            {
                world.fUserOffsetx = world.fUserOffsetx + 45 * strafeScale;
                world.fUserOffsetT = world.fUserOffsetT + 45 * strafeScale;
                // Debug.Log("LEFT!");
            }
            world.fUserOffsetT = world.fUserOffsetT + angularDirection * strafeScale;
        }
    }

    public void Tumble()
    {
        transform.eulerAngles = orientation;
        orientation.x = orientation.x + tumble.x;
        orientation.y = orientation.y + tumble.y;
        orientation.z = orientation.z + tumble.z;
        if (Time.time - fStartTime > 0.5f)
        {
            gameObject.tag = "dead";
        }
    }

    public void RotationCalculator()
    {
        Vector3 pos = transform.position;
        pos.z = Vector3.Dot(Input.gyro.gravity*90, Vector3.left); //small sides -> bottom down and top up is + (this is the important one)
        pos.y = Vector3.Dot(Input.gyro.gravity*90, Vector3.down); // long sides -> bottom left and top right is +
        pos.x = Vector3.Dot(Input.gyro.gravity*90, Vector3.back); // faces -> face down back up is +

        if (pos.z <= 45 || pos.z >= 315)
        {
            orientation.z = pos.z;
        }
        if (world.transform.eulerAngles.y == 0 || (transform.rotation.eulerAngles.x > 270 && (transform.rotation.eulerAngles.y < 1 || transform.rotation.eulerAngles.y > 359)))
        {
            if (vrMode == true)
            {
                orientation.x = pos.x + world.transform.eulerAngles.x;
            }
            else
            {
                orientation.x = world.transform.eulerAngles.x;
            }
        }
        else
        {
            if (vrMode == true)
            {
                orientation.x = pos.x + 180 - world.transform.eulerAngles.x;
            }
            else
            {
                orientation.x = 180 - world.transform.eulerAngles.x;
            }
        }


        if (orientation.z <= 45 || orientation.z >= 315)
        {
            transform.eulerAngles = orientation;
        }
        else if (transform.eulerAngles.z <= 180 && orientation.z >= 45)
        {
            transform.eulerAngles = new Vector3(orientation.x, 0, 45);
        }
        else if (transform.eulerAngles.z >= 180 && orientation.z <= 315)
        {
            transform.eulerAngles = new Vector3(orientation.x, 0, 315);
        }

        if (orientation.z >= 180)
        {
            angularDirection = orientation.z - 360.0f;
        }
        else
        {
            angularDirection = orientation.z;
        };
    }



    void OnTriggerEnter(Collider other)
    {
        
        myScore.SendMessage("HighScore");
            tumble = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
            fStartTime = Time.time;
            strafeScale = 0;
            if (gameObject.tag != "dead" && gameObject.tag != "tumbling")
            {
            Vibration.Vibrate(300);
            gameObject.tag = "tumbling";
            }
            strafeScale = 0;
    }
    

}
