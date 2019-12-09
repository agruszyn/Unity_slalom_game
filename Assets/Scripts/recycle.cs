using UnityEngine;
using System.Collections;

public class recycle : MonoBehaviour {

    GameObject player;
    private GameManager manager;
    GameObject world;
    private Vector3 playerPos;
    private Vector3 playerRot;
    public Vector2 myPlace;
    Color myColor;
    Renderer rend;

    public Vector2 myLastPlace;
    public bool changePlaces;
    public int priority;
    public float estimaterot;
    

    // Use this for initialization
    void Start()
    {
        myColor = rend.material.color;
        rend = GetComponent<Renderer>();
        player = GameObject.Find("PlayerCharacter");
        world = GameObject.Find("GAME MANAGER");
        //world = GetComponent<GameManager>();
        manager = world.GetComponent<GameManager>();
        playerPos = player.transform.position;
        playerRot = world.transform.rotation.eulerAngles;
        myPlace = new Vector2(Mathf.Round((transform.position.x - playerPos.x) / 60) + 3, Mathf.Round((transform.position.z - playerPos.z) / 120) + 1);
        //myPlace = new Vector2(Mathf.Round((transform.position.x - playerPos.x) / 60) + 3, Mathf.Round((transform.rotation.x - playerRot.x) / 7) + 1);
        if (myPlace.x < 0)
        {
            myPlace.x = 0;
        }

        if (myPlace.y < 0)
        {
            myPlace.y = 0;
        }
        myLastPlace = myPlace;
        
    }

    void ChangeColor()
    {
        if (myColor.r == 255 && myColor.b == 255 && myColor.g > 0)
        {
            LessGreen();
        }
        else if(myColor.r > 0 && myColor.b == 255 && myColor.g == 00)
        {
            LessRed();
        }
        else if (myColor.r == 0 && myColor.b == 255 && myColor.g < 255)
        {
            MoreGreen();
        }
        else if (myColor.r == 255 && myColor.b > 0 && myColor.g == 255)
        {
            LessBlue();
        }
        else if (myColor.r < 255 && myColor.b == 0 && myColor.g == 255)
        {
            MoreRed();
        }
        else if (myColor.r == 255 && myColor.b == 0 && myColor.g > 0)
        {
            LessGreen();
        }
        else if (myColor.r == 255 && myColor.b < 255 && myColor.g == 0)
        {
            MoreBlue();
        }

        rend.material.color = myColor;
    }

    void LessRed()
    {
        myColor.r = myColor.r - 1;
    }

    void MoreRed()
    {
        myColor.r = myColor.r + 1;
    }

    void LessBlue()
    {
        myColor.b = myColor.b - 1;
    }

    void MoreBlue()
    {
        myColor.b = myColor.b + 1;
    }

    void LessGreen()
    {
        myColor.g = myColor.g - 1;
    }

    void MoreGreen()
    {
        myColor.g = myColor.g + 1;
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            ChangeColor();
            playerRot = player.transform.rotation.eulerAngles;
            playerPos = world.transform.position;
            estimaterot = Mathf.Round(transform.rotation.eulerAngles.x - playerRot.x);
            //myPlace = new Vector2(Mathf.Round((transform.position.x - playerPos.x) / 60) + 3, Mathf.Round((transform.position.z - playerPos.z) / 120) + 1);
            myPlace = new Vector2(Mathf.Round((transform.position.x - playerPos.x) / 60) + 3, Mathf.Round((float)(transform.rotation.eulerAngles.x - playerRot.x) * 2 / 7) + 1);

            if (myPlace.x < 0)
            {
                myPlace.x = 0;
            }

            if (myPlace.y < 0)
            {
                myPlace.y = 0;
            }

            //if (manager.tiles[(int)myPlace.x, (int)myPlace.y] == false)
            //{
            //    manager.tiles[(int)myPlace.x, (int)myPlace.y] = true;
            //}

            if (myPlace != myLastPlace)
            {
                manager.tiles[(int)myLastPlace.x, (int)myLastPlace.y] = false;
                if (manager.itiles[(int)myLastPlace.x, (int)myLastPlace.y] == priority)
                {
                    manager.itiles[(int)myLastPlace.x, (int)myLastPlace.y] = 100;
                }
                myLastPlace = myPlace;
            }

            if (manager.tiles[(int)myPlace.x, (int)myPlace.y] == false)
            {
                if (manager.itiles[(int)myPlace.x, (int)myPlace.y] != priority)
                {
                    manager.itiles[(int)myPlace.x, (int)myPlace.y] = priority;
                }
                manager.tiles[(int)myPlace.x, (int)myPlace.y] = true;
                // myLastPlace = myPlace;
            }

            if (myPlace.x >= 6 || myPlace.x <= 0 || myPlace.y >= 6 || myPlace.y <= 0)
            {
                manager.slotRequest = true;
                changePlaces = true;
                if (manager.requester > priority)
                {
                    manager.requester = priority;
                }
                if (manager.slotAvailable == true && manager.requester == priority)
                {
                    manager.tiles[(int)myPlace.x, (int)myPlace.y] = false;
                    if (manager.itiles[(int)myLastPlace.x, (int)myLastPlace.y] == priority)
                    {
                        manager.itiles[(int)myLastPlace.x, (int)myLastPlace.y] = 100;
                    }
                    transform.position = new Vector3(manager.slotPosition.x, manager.slotPosition.y, manager.slotPosition.z);
                    transform.eulerAngles = new Vector3(world.transform.eulerAngles.x + (7 / 2 * myPlace.y), 0, 0);
                    changePlaces = false;
                    manager.slotAvailable = false;
                    manager.slotRequest = false;
                    manager.requester = 100;
                }


            }

        }
    }
}
