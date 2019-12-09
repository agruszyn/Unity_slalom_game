using UnityEngine;
using System.Collections;

public class terrainGenerator : MonoBehaviour {


    public Vector3 vPlainPosition;
    public Vector3 vStartPlains;
    public float fUserOffsetT;

    [Range(0, 100)]
    public float fTandomFillPercent;
    public int iPercentLayerTwo;
    public int iPercentLayerThree;
    public Vector3 vSlotPosition;
    public bool bSlotRequestSideways = false;
    public bool bSlotRequestForward = false;
    public bool bSlotAvailable = false;
    public float fUserOffsetx;
    public float fUserOffsetz;
    public bool sR;
    public bool sL;
    public int iRank;
    public float fStartTime;
    public bool bWait;
    public float fPositionEstimate;
    public Quaternion qPlainRotation;
    public int iWorldLocation;
    public int LocalStatus;
    public int iFutureLocations;
    const float tileWidth = 20.944f;
    const float tileLength = 41.88714f;
    const float degrees = 1.2f;
    public Vector2 openSlot;
    public bool hello;
    public int iPickyou;
    private GameObject moveableTiles;
    public int openTiles;
    public int arraySize;
    public int times;
    private bool bFlip;
    public float tiledeg = 0.833333f;

    public int Xpos;
    public int zpos;
    public int iX;
    public int iPreviousWorldLocation;
    public bool iY;

    float xLocal;
    float yLocal;
    float zLocal;
    private int imaxLength = 5;
    private int imaxWidth = 7;

    public bool[,] aiTiles;
    GameObject grassyPlain;
    GameObject instance;
    private Game_Master manager;
    GameObject world;

    // Use this for initialization
    void Start ()
    {
        world = GameObject.Find("GAME MANAGER");
        manager = world.GetComponent<Game_Master>();
        iRank = 1;
        //userPlayer = GameObject.Find("PlayerCharacter");
        //stUser = GetComponent<MotionControl>();
        aiTiles = new bool[20, 305];
        grassyPlain = (GameObject)Resources.Load("Grassy Plain");
        InitializeTerrain();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       transform.position = new Vector3(manager.fUserOffsetx,0,0);
	}

    void Update()
    {
        //RecycleLand();
    }


    public float GetOrientedy(int local)
    {
            return Mathf.Cos((float)(local * tiledeg) * (Mathf.PI / 180)) * 2000;
    }

    public float GetOrientedz(int local)
    {
        
            return Mathf.Sin((float)(local * tiledeg) * (Mathf.PI / 180)) * 2000;

    }

    public void InitializeTerrain()
    {


        for (int x = -imaxWidth; x <= imaxWidth; x += 1)
        {
            for (int z = 0; z <= imaxLength; z++)
            {
                if (Mathf.Abs(x) <= z + 2)
                {
                    //Write the tile's locations into the array
                    aiTiles[x + imaxWidth, z] = true;

                    //Set the rotation of the tiles
                    
                    qPlainRotation.eulerAngles = new Vector3(((float)z * tiledeg), 0, 0);

                    //Find the tile locations in the world
                    xLocal = (float)(x * tileWidth);
                    yLocal = GetOrientedy(z);
                    zLocal = GetOrientedz(z);
                    vStartPlains = new Vector3(xLocal, yLocal, zLocal);

                    // Instantiate the tile gameobject
                    //Debug.Log(tiledeg);
                    instance = Instantiate(grassyPlain, vStartPlains, qPlainRotation) as GameObject;
                    instance.transform.parent = gameObject.transform;
                    instance.gameObject.GetComponent<Tile_organizer>().iX = x;
                    instance.gameObject.GetComponent<Tile_organizer>().iY = z;
                }
            }

        }
    }


    public void RecycleLand()
    {
        iWorldLocation = manager.WorldLocation();

        for (int z = iWorldLocation; z <= iWorldLocation + imaxLength; z++)
        {
            if (z == 305)
            {
                z = 0;
                bFlip = true;
            }

            for (int x = -(z - iWorldLocation) - 2; x <= (z - iWorldLocation + 2); x++)
            {
                // If tile is open and within the triangle
                if (aiTiles[x + imaxWidth, z] == false)
                {
                    moveableTiles = GameObject.FindGameObjectWithTag("Request");
                    if (moveableTiles != null)
                    {
                        aiTiles[moveableTiles.GetComponent<Tile_organizer>().iX + imaxWidth, moveableTiles.GetComponent<Tile_organizer>().iY] = false;
                        moveableTiles.GetComponent<Tile_organizer>().iX = x;
                        moveableTiles.GetComponent<Tile_organizer>().iY = z;
                        moveableTiles.GetComponent<Tile_organizer>().tag = "Default";
                        aiTiles[x + imaxWidth, z] = true;
                        moveableTiles = null;
                    }
                }
            }
            if (z + 305 == iWorldLocation + imaxLength && bFlip == true)
            {
                z = iWorldLocation + imaxLength;
                bFlip = false;
            }
        }
    }





}
