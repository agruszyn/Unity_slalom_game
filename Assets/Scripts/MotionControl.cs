using UnityEngine;
using System.Collections;

public class MotionControl : MonoBehaviour {

    // Use this for initialization
    public Vector3 orientation;
    public float startingjetSpeed = -1.5f;
    public float jetSpeed;
    public float strafeScale;
    public float multiplierSpeed;
    public float strafeDebug;
    public Vector3 tumble;
    public Vector3 starting_turn_speed = new Vector3(0, 0, 1);
    public Vector3 turn_speed;
    public Vector3 base_skybox_rotation = new Vector3(0.1f, 0.02f, 0.0f);
    private Vector3 skybox_rotation = new Vector3(0.1f, 0.02f, 0.0f);
    public int max_turn_angle = 30;
    public float max_button_turn_angle = (30 / 5);
    private bool no_Gyro = false;
    //public Vector3 g = new Vector3(0, 0, 0);

    //private GameManager center;
    //private CardboardHead viewPort;
    private Game_Master world;
    private TranslateObstacles speed;
    private GameObject scoreboard;
    private GameObject skybox;
    private SkyboxCamera skybox_controller;
    private Score myScore;
    private GameObject hazard_rotator;
    private float angularDirection;
    public bool bWait;
    private float fStartTime;
    public bool vrMode;
    public bool left_button_pressed = false;
    public bool right_button_pressed = false;
    public Vector3 pos;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //no_Gyro = (PlayerPrefs.GetInt("UseGyro") == 0);
        max_button_turn_angle = (max_turn_angle / 5);
        no_Gyro = PlayerPrefs.GetInt("UseGyro") == 0;
        startingjetSpeed = -1.5f;
        jetSpeed = startingjetSpeed;
        PlayerPrefs.SetFloat("jetSpeed", jetSpeed);
        hazard_rotator = GameObject.Find("Rotate Obstacles");
        scoreboard = GameObject.Find("Score");
        skybox = GameObject.Find("Skybox");
        myScore = scoreboard.GetComponent<Score>();
        vrMode = false; //PlayerPrefs.GetInt("VRmode") == 1;
        //viewPort = GetComponentInChildren<CardboardHead>();
        orientation.y = 0;
        world = GetComponentInParent<Game_Master>();
        speed = hazard_rotator.GetComponent<TranslateObstacles>();
        skybox_controller = skybox.GetComponent<SkyboxCamera>();
        strafeScale = jetSpeed * 0.013f;
        Input.gyro.enabled = !no_Gyro;
    }
    //void Update()
    //   {
    //       OnCollisionEnter();
    //   }

    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            jetSpeed = myScore.multiplier * startingjetSpeed;
            if (PlayerPrefs.GetFloat("jetSpeed") != jetSpeed)
            {
                strafeScale = jetSpeed * 0.013f;
                turn_speed = starting_turn_speed * myScore.multiplier;
                skybox_rotation.x = base_skybox_rotation.x * myScore.multiplier;
                skybox_controller.SetSkyBoxRotation(skybox_rotation);
                PlayerPrefs.SetFloat("jetSpeed", jetSpeed);
            }
            if (this.tag != "tumbling" && this.tag != "dead")
            {
                RotationCalculator();
            }
            else
            {
                Tumble();
            }
            //transform.position = new Vector3(0, transform.position.y, transform.position.z);
            //world.fUserOffsetx = world.fUserOffsetx + angularDirection * strafeScale;
            //world.fUserOffsetT = world.fUserOffsetT + angularDirection * strafeScale; //gio
            if (Input.GetKey("right") || right_button_pressed)
            {
                go_Right(world);
                if (no_Gyro)
                    { pos = Turn_camera_right(pos); }
            }
            else if (Input.GetKey("left") || left_button_pressed)
            {
                    go_Left(world);
                if (no_Gyro)
                    { pos = Turn_camera_left(pos);}
            }
            else if (no_Gyro)
            {
                pos = Return_to_center(pos);
            }
            if (!no_Gyro && !(Input.GetKey("left") || left_button_pressed) && !(Input.GetKey("right") || right_button_pressed))
            { 
                world.fUserOffsetT = world.fUserOffsetT - angularDirection * strafeScale;
                world.fUserOffsetx = world.fUserOffsetx - angularDirection * strafeScale; //gio
            }
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
        //pos = transform.rotation.eulerAngles;
        if (!no_Gyro)
        {
            pos.z = Vector3.Dot(Input.gyro.gravity * 90, Vector3.left); //small sides -> bottom down and top up is + (this is the important one)
            pos.y = Vector3.Dot(Input.gyro.gravity * 90, Vector3.down); // long sides -> bottom left and top right is +
            pos.x = Vector3.Dot(Input.gyro.gravity * 90, Vector3.back); // faces -> face down back up is z
        }

        if (pos.z <= max_turn_angle && pos.z >= -max_turn_angle)
        {
            orientation.z = pos.z;
        }
        else if (pos.z >= max_turn_angle)
        {
            orientation.z = max_turn_angle;
        }
        else 
        {
            orientation.z = -max_turn_angle;
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


        if (orientation.z <= max_turn_angle && orientation.z >= -max_turn_angle)
        {
            transform.eulerAngles = orientation;
        }
        else if (transform.eulerAngles.z <= 180 && orientation.z >= max_turn_angle)
        {
            transform.eulerAngles = new Vector3(orientation.x, 0, max_turn_angle);
        }
        else if (transform.eulerAngles.z > -180 && orientation.z <= -max_turn_angle)
        {
            transform.eulerAngles = new Vector3(orientation.x, 0, -max_turn_angle);
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
        Debug.Log(other);
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
    public void go_Left(Game_Master universe)
    {
        if (universe == null)
        {
            universe = GetComponentInParent<Game_Master>();
        }
        universe.fUserOffsetx = universe.fUserOffsetx - strafeScale * max_turn_angle;
        universe.fUserOffsetT = universe.fUserOffsetT - strafeScale * max_turn_angle;
    }

    public void go_Right(Game_Master universe)
    {
        if (universe == null)
        {
            universe = GetComponentInParent<Game_Master>();
        }
        universe.fUserOffsetx = universe.fUserOffsetx + strafeScale * max_turn_angle;
        universe.fUserOffsetT = universe.fUserOffsetT + strafeScale * max_turn_angle;
    }

    public void left_button_down(MotionControl myself)
    {
        if (myself == null)
        {
            myself = GetComponent<MotionControl>();
        }
        myself.left_button_pressed = true;
    }

    public void left_button_up(MotionControl myself)
    {
        if (myself == null)
        {
            myself = GetComponent<MotionControl>();
        }
        myself.left_button_pressed = false;
    }

    public void right_button_down(MotionControl myself)
    {
        if (myself == null)
        {
            myself = GetComponent<MotionControl>();
        }
        myself.right_button_pressed = true;
    }

    public void right_button_up(MotionControl myself)
    {
        if (myself == null)
        {
            myself = GetComponent<MotionControl>();
        }
        myself.right_button_pressed = false;
    }

    public Vector3 Turn_camera_left(Vector3 current_rotation)
    {
        if (current_rotation.z < max_button_turn_angle)
        {
            current_rotation += (turn_speed);
        }
        return current_rotation;
    }
    public Vector3 Turn_camera_right(Vector3 current_rotation)
    {
        if (current_rotation.z > -max_button_turn_angle)
        {
            current_rotation -= (turn_speed);
        }
        return current_rotation;
    }
    public Vector3 Return_to_center(Vector3 current_rotation)
    {
        if (Mathf.Abs(current_rotation.z) < turn_speed.z)
        { return new Vector3(0, 0, 0); }
        if (current_rotation.z > 0)
        {
            current_rotation -= (turn_speed);
        }
        else if (current_rotation.z < 0)
        {
            current_rotation += (turn_speed);
        }
        return current_rotation;
    }
}
