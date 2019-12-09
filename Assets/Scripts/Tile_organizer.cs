using UnityEngine;
using System.Collections;

public class Tile_organizer : MonoBehaviour {

    //GameObject player;
    private terrainGenerator manager;
    GameObject world;
    //private Vector3 vPlayerPos;
    public Vector2 vMyPlace;
    public Vector2 vMyLastPlace;
    public bool bChangePlaces;
    public int iPriority;
    public float fEstimateRot;
    const float tileWidth = 20.944f;
    const float tileLength = 41.88f;
    const float degrees = 1.2f;
    public int iX;
    public int iY;
    private int iXold;
    private int iYold;
    public bool changeme;
    public bool change;
    public bool playerLevel;
    public Color myColor;
    Renderer rend;


    // Use this for initialization
    void Start()
    {        
        
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Toon/Basic");
        myColor = rend.material.color;
        world = GameObject.Find("Terrain Generator");
        manager = world.GetComponent<terrainGenerator>();
        iXold = iX;
        iYold = iY;
    }

    void ChangeColor()
    {

        if (myColor.r == 1f && myColor.b == 1f && myColor.g > 0f)
        {
            LessGreen();  
        }
        else if (myColor.r > 0f && myColor.b > 0.99f && myColor.g < 0.1f)
        {
            LessRed();
        }
        else if (myColor.r < 0.1f && myColor.b > 0.9f && myColor.g < 1f)
        {
            MoreGreen();
        }
        else if (myColor.r < 0.1f && myColor.b > 0f && myColor.g > 0.9f)
        {
            LessBlue();
        }
        else if (myColor.r < 1f && myColor.b < 0.1f && myColor.g > 0.9f)
        {
            MoreRed();
        }
        else if (myColor.r > 0.9f && myColor.b < 0.1f && myColor.g > 0f)
        {
            LessGreen();
        }
        else if (myColor.r > 0.9f && myColor.b < 1f && myColor.g < 0.1f)
        {
            MoreBlue();
        }

        rend.material.SetColor("_Color", myColor);
    }

    void LessRed()
    {
        myColor.r = myColor.r - .01f;
    }

    void MoreRed()
    {
        myColor.r = myColor.r + .01f;
    }

    void LessBlue()
    {
        myColor.b = myColor.b - .01f;
    }

    void MoreBlue()
    {
        myColor.b = myColor.b + .01f;
    }

    void LessGreen()
    {
        myColor.g = myColor.g - .01f;
    }

    void MoreGreen()
    {
        myColor.g = myColor.g + .01f;
    }


    // Update is called once per frame

    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            if (iY != iYold || iX != iXold)
            {
                manager.bSlotRequestSideways = true;
                transform.localPosition = new Vector3((float)((iX * tileWidth)), Mathf.Cos((float)(iY * degrees * Mathf.PI) / 180) * 2000, Mathf.Sin((float)(iY * degrees * Mathf.PI) / 180) * 2000);
                changeme = false;
                iYold = iY;
                iXold = iX;
            }
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            ChangeColor();
            ChangePosition();
        }
    }


    public void ChangePosition()
    {
        

        if (Mathf.Abs(iX) > iY - manager.iWorldLocation + 2 || iY == manager.iWorldLocation - 1 && gameObject.tag != "Request")
        {
            changeme = true;
            gameObject.tag = "Request";
        }
        else if ((Mathf.Abs(iX) > iY - manager.iWorldLocation - 303 || iY == manager.iWorldLocation + 295 && gameObject.tag != "Request") && (manager.iWorldLocation < 90 && iY > 270))
        {
            changeme = true;
            gameObject.tag = "Request";
        }


    }
    }


