using UnityEngine;
using System.Collections;

public class Tile_organizer_menu : MonoBehaviour {

    //GameObject player;
    private terrainGenerator_menu manager;
    GameObject world;
    //private Vector3 vPlayerPos;
    private Vector3 vPlayerRot;
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
    private float fUserOffsetx;
    public bool changeme;
    public bool change;
    public bool playerLevel;


    // Use this for initialization
    void Start()
    {
        //player = GameObject.Find("PlayerCharacter");
        world = GameObject.Find("Terrain Generator_menu");
        manager = world.GetComponent<terrainGenerator_menu>();
        iXold = iX;
        iYold = iY;
    }



    // Update is called once per frame

    void FixedUpdate()
    {
        //transform.position = new Vector3((iX * tileWidth), transform.position.y, transform.position.z) ;
        if (iY != iYold || iX != iXold)
        {
            manager.bSlotRequestSideways = true;
            transform.localPosition = new Vector3((float)((iX * tileWidth)), Mathf.Cos((float)(iY * degrees * Mathf.PI) / 180) * 2000, Mathf.Sin((float)(iY * degrees * Mathf.PI) / 180) * 2000);
            transform.eulerAngles = new Vector3((float)(iY * degrees), 0, 0);
            changeme = false;
            iYold = iY;
            iXold = iX;
        }
    }

    void Update()
    {
        ChangePosition();
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


