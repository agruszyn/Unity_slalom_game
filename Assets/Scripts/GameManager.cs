using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    // Use this for initialization
    public Vector3 plainPosition;
    public Vector3 startPlains;

    [Range(0, 100)]
    public float randomFillPercent;
    public int percentLayerTwo;
    public int percentLayerThree;
    public Vector3 slotPosition;
    public bool slotRequest = false;
    public bool slotAvailable = false;
    public float userOffsetx;
    public float userOffsetz;
    public int rank;
    public int requester = 100;
    public float startTime;
    public bool wait;
    public float positionestimate;

    public int updateDistance;
   // private Vector3 nullVector = new Vector3(0,0,0);
    private string seed;
    private Vector3 userPosition;
    private float lastObstaclePosition;
    private float angularDirection;
    private float retainSpeed;
    public bool[,] tiles;
    public int[,] itiles;
    GameObject grassyPlain;
    GameObject hazard;
    GameObject instance;
    GameObject userPlayer;


    void Start()
    {
        plainPosition = new Vector3(-180, 0, 480);
        LoadResources();
        InitializeTerrain();
        initializeHazard();
    }


    void Update()
    {
        GenerateHazard();
        //GenerateTerrain();
        FindSlots();
        userPosition = userPlayer.transform.position;
    }


    public void LoadResources()
    {
        rank = 0;
        userPlayer = GameObject.Find("PlayerCharacter");
        tiles = new bool[8,8];
        itiles = new int[8, 8];
        grassyPlain = (GameObject)Resources.Load("Grassy Plain");
        hazard = (GameObject)Resources.Load("cube");
    }

    public void InitializeTerrain()
    {
        for (int x = -120; x <= 120; x += 60)
        {
            for (int z = 0; z <= 480; z += 120)
            {
                rank += 1;
                startPlains = new Vector3(x, 990, z);
                tiles[(x / 60)+ 3, (z / 120) + 1] = true;
                instance = Instantiate(grassyPlain, startPlains, Quaternion.identity) as GameObject;
                itiles[(x / 60) + 3, (z / 120) + 1] = rank;
                instance.gameObject.GetComponent<recycle>().priority = rank;
                instance.gameObject.GetComponent<recycle>().transform.eulerAngles = new Vector3((z / 120)*7/2, 0,0);
            }
        }
       // lastUpdatePosition = 120;
    }

    //public void GenerateTerrain()
    //{
    //    if (userPosition.z + updateDistance > lastUpdatePosition)
    //    {
    //        for (int x = -120; x <= 120; x += 60)
    //        {
    //            plainPosition.x = userPosition.x + x;
    //            plainPosition.y = -2;
    //            plainPosition.z = lastUpdatePosition + updateDistance + 360;

    //            Instantiate(grassyPlain, plainPosition, Quaternion.identity);
    //        }
    //        lastUpdatePosition = lastUpdatePosition + updateDistance;
    //    }
    //}

    public void FindSlots()
    {
        if (slotRequest == true & !slotAvailable)
        {
            if (wait != true)
            {
                startTime = Time.time;
                wait = true;
            }
                if (Time.time - startTime > 0.02f)
            {
                wait = false;
 
            for (int x = 1; x <= 5; x++)
            {
                for (int z = 1; z <= 5; z++)
                {
                    //if (tiles[x, z] == false)
                    //{
                    //    userOffsetx = Mathf.Round(userPosition.x / 60);
                    //    userOffsetz = Mathf.Round(userPosition.z / 120);
                    //    slotPosition = new Vector2(((userOffsetx + x - 3) * 60), ((userOffsetz + z - 1) * 120));
                    //    slotAvailable = true;
                    //}
                    

                        if (itiles[x, z] == 100)
                        {
                            userOffsetx = Mathf.Round(userPosition.x / 60);
                            userOffsetz = Mathf.Round(Mathf.Sin((transform.eulerAngles.x * Mathf.PI) / 180)*990*z);
                            positionestimate = Mathf.Cos((transform.eulerAngles.x * Mathf.PI) / 180) * 990;
                            //slotPosition = new Vector3(((userOffsetx + x - 3) * 60), Mathf.Cos((transform.eulerAngles.x * Mathf.PI) / 180)*990, ((userOffsetz + z - 1) * 120));
                             slotPosition = new Vector3(((userOffsetx + x - 3) * 60), Mathf.Cos((transform.eulerAngles.x * Mathf.PI) / 180)*990, userOffsetz);
                            slotAvailable = true;
                        }

                    }


            }
        }
        }
    }

    public void GenerateHazard()
    {
        if (lastObstaclePosition < userPosition.z - 10)
        {
            seed = Time.time.ToString();
            System.Random pseudoRandom = new System.Random(seed.GetHashCode());
            for (int x = 0; x <= 48; x++)
            {
                for (int z = 0; z <= 2; z++)
                {
                    if (pseudoRandom.Next(0, 100) < randomFillPercent)
                    {
                        Instantiate(hazard, new Vector3((int)userPosition.x - 120 + x * 5, (float)1.5, (int)userPosition.z + 360 + z * 5), Quaternion.identity);
                        //instance.GetComponent<HazardMotion>().Initialize(2, nullVector, 0, 0, nullVector, 0, 0, nullVector);
                        
                        if (pseudoRandom.Next(0, 100) < percentLayerTwo)
                        {
                            Instantiate(hazard, new Vector3((int)userPosition.x - 120 + x * 5, (float)6.5, (int)userPosition.z + 360 + z * 5), Quaternion.identity);
                            if (pseudoRandom.Next(0, 100) < percentLayerThree)
                            {
                                Instantiate(hazard, new Vector3((int)userPosition.x - 120 + x * 5, (float)11.5, (int)userPosition.z + 360 + z * 5), Quaternion.identity);
                            }
                        }
                    }
                }
                lastObstaclePosition = userPosition.z;
            }

        }
    }
    public void initializeHazard()
    {
        seed = Time.time.ToString();
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        for (int x = 0; x <= 48; x++)
        {
            for (int z = 0; z <= 72; z++)
            {
                if (pseudoRandom.Next(0, 100) < randomFillPercent)
                {
                    Instantiate(hazard, new Vector3((x * 5) - 120, (float)1.5, (z * 5) + 60), Quaternion.identity);
                    if (pseudoRandom.Next(0, 100) < percentLayerTwo)
                    {
                        Instantiate(hazard, new Vector3((int)userPosition.x - 120 + x * 5, (float)6.5, (int)userPosition.z + 60 + z * 5), Quaternion.identity);
                        if (pseudoRandom.Next(0, 100) < percentLayerThree)
                        {
                            Instantiate(hazard, new Vector3((int)userPosition.x - 120 + x * 5, (float)11.5, (int)userPosition.z + 60 + z * 5), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }


}
