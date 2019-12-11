using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hazards : MonoBehaviour {

    private string seed;
    public int randomFillPercent = 4;
    public int percentLayerTwo = 25;
    public int percentLayerThree = 25;
    public Quaternion qHazardRotation;
    Vector3 vStartHazards;
    public float xLocalh;
    float yLocalh;
    float zLocalh;
    int[,] aiObstacle;
    const float degrees = 1.2f;
    const float cubedegree = 0.1248f;
    GameObject hazard;
    GameObject world;
    GameObject canv;
    GameObject face;
    GameObject rotator;
    GameObject mahRotation;
    public int[,] hazardSectionOne;
    int[,] hazardSectionTwo;
    public int iteration = 0;
    int cycle = 0;
    public int qeue;
    float slope = 0.0f;
    float target = 0.0f;
    float location = 0.0f;
    float acceleration = 0.0f;
    int distance = 0;
    int targetdistance = 0;
    float sign = 0.00f;
    float startpos = 0.0f;
    bool firstcycle = false;

    const float tileWidth = 20.944f;
    const float tileLength = 41.88714f;
    private Game_Master manager;
    private Boss head;
    private Score level;
    private Falling oFall;
    private GameObject[] openHazards;
    private GameObject[] buffer;
    private GameObject[] enemies;
    public int MyEnemies;

    public int countHazards;
    public int newRow;
    public int instance = 0;
    private GameObject box;
    private float offset = 0;
    public int hazardlevel;
    public float change;
    public bool go = false;

    private float bottomY;
    private float bottomZ;
    private float middleY;
    private float middleZ;
    private float topY;
    private float topZ;
    float funnelsize = 40;

    // Use this for initialization
    void Start ()
    {
        
        newRow = 101;
        rotator = GameObject.Find("Hazrd Generator");
        mahRotation = GameObject.Find("Rotate Obstacles");
        face = GameObject.Find("full_face");
        canv = GameObject.Find("Score");
        world = GameObject.Find("GAME MANAGER");
        level = canv.GetComponent<Score>();
        manager = world.GetComponent<Game_Master>();
        head = face.GetComponent<Boss>();
        hazard = (GameObject)Resources.Load("cube");
        //aiObstacle = new int[101, 71];
        hazardSectionOne = new int[101, 71];
        hazardSectionTwo = new int[101, 71];

        bottomY = GetOrientedy((float)(69), 1);
        bottomZ = GetOrientedz((float)(69), 1);
        middleY = GetOrientedy((float)(69), 2);
        middleZ = GetOrientedz((float)(69), 2);
        topY = GetOrientedy((float)(69), 3);
        topZ = GetOrientedz((float)(69), 3);
        initializeHazard();
        qHazardRotation.eulerAngles = new Vector3((int)(69 * cubedegree), 0, 0);
        change = mahRotation.transform.rotation.eulerAngles.x;
        MapHazard();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = new Vector3(manager.fUserOffsetT, 0, 0);
	}

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enabled");
        MyEnemies = enemies.Length;

        //change = mahRotation.transform.rotation.x;
        if (cycle >= 70 && iteration == 0)
        {
            MapHazard();
            cycle = 0;
        }
        hazardlevel = level.level;
        if (head.starthover != true)
            {
            // RecycleHazard();
            //ReplaceHazard();
            if (hazardlevel < 1 )
            { PatternHazard(); }
            if (hazardlevel == 1 || hazardlevel == 2)
            { MakePath(); }
            }
        else if (head.starthover == true)
            {
            firstcycle = false;
            go = false;
            funnelsize = 40;
            slope = 0.0f;
            target = 0.0f;
            location = 0.0f;
            acceleration = 0.0f;
            distance = 0;
            targetdistance = 0;
            sign = 0.00f;
            startpos = 0.0f;
            firstcycle = false;

            SpitHazards(); 
            }
    }


    public float GetOrientedy(float local, int layer)
    {
        if (layer == 1)
        {
            return Mathf.Cos((float)(local * (cubedegree) * Mathf.PI) / 180) * 2002.5f;
        }
        if (layer == 2)
        {
            return Mathf.Cos((float)(local * (cubedegree) * Mathf.PI) / 180) * 2007.5f;
        }
        if (layer == 3)
        {
            return Mathf.Cos((float)(local * (cubedegree) * Mathf.PI) / 180) * 2012.5f;
        }
        else return 0.0f;
    }

    public float GetOrientedz(float local, int layer)
    {
        if (layer == 1)
        {
            return Mathf.Sin((float)(local * cubedegree * Mathf.PI) / 180) * 2002.5f;
        }
        if (layer == 2)
        {
            return Mathf.Sin((float)(local * cubedegree * Mathf.PI) / 180) * 2007.5f;
        }
        if (layer == 3)
        {
            return Mathf.Sin((float)(local * cubedegree * Mathf.PI) / 180) * 2012.5f;
        }
        else return 0.0f;
    }

    public void initializeHazard()
    {
        for (int a = 0; a < 349; a++)
        {
            var cuboid = Instantiate(hazard, new Vector3(0, 0, -10), qHazardRotation) as GameObject;
            cuboid.transform.parent = gameObject.transform;
        }
        seed = Time.time.ToString();
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        for (int x = 0; x <= 100; x++)
        {
            for (int z = 30; z <= 100; z++)
            {
                if (pseudoRandom.Next(0, 100) < randomFillPercent)
                {
                    //aiObstacle[x, z] = 1;
                    qHazardRotation.eulerAngles = new Vector3((int)(z * cubedegree), 0, 0);
                    //Find the tile locations in the world
                    xLocalh = (float)((x * 5) - 250);
                    //yLocalh = GetOrientedy((float)(((z * 5) + 60) / tileLength),1);
                    //zLocalh = GetOrientedz((float)(((z * 5) + 60) / tileLength),1);
                    yLocalh = GetOrientedy((float)(z), 1);
                    zLocalh = GetOrientedz((float)(z), 1);
                    vStartHazards = new Vector3(xLocalh, yLocalh, zLocalh);
                    var cuboid = Instantiate(hazard, vStartHazards, qHazardRotation) as GameObject;
                    cuboid.transform.parent = gameObject.transform;

                    if (pseudoRandom.Next(0, 100) < percentLayerTwo)
                    {
                        yLocalh = GetOrientedy((float)(z), 2);
                        zLocalh = GetOrientedz((float)(z), 2);
                        vStartHazards = new Vector3(xLocalh, yLocalh, zLocalh);
                        cuboid = Instantiate(hazard, new Vector3(vStartHazards.x, vStartHazards.y, vStartHazards.z), qHazardRotation) as GameObject;
                        cuboid.transform.parent = gameObject.transform;

                        if (pseudoRandom.Next(0, 100) < percentLayerThree)
                        {
                            yLocalh = GetOrientedy((float)(z), 3);
                            zLocalh = GetOrientedz((float)(z), 3);
                            vStartHazards = new Vector3(xLocalh, yLocalh, zLocalh);
                            cuboid = Instantiate(hazard, new Vector3(vStartHazards.x, vStartHazards.y, vStartHazards.z), qHazardRotation) as GameObject;
                            cuboid.transform.parent = gameObject.transform;
                        }
                    }
                }
                //else aiObstacle[x, z] = 0;
            }
        }
    }



    //public void RecycleHazard()
    //{

    //    openHazards = GameObject.FindGameObjectsWithTag("disabled");
    //    countHazards = openHazards.Length;
    //    if (countHazards > 30)
    //    {
    //        //newRow = (lastrow - (int)((lastposition - transform.rotation.eulerAngles.x) / cubedegree));
    //       // Debug.Log(newRow);
    //        seed = Time.time.ToString();
    //        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
    //        offset = -Mathf.Round(manager.fUserOffsetT);
    //        for (int x = 0; x <= 100; x++)
    //        {
    //            if (pseudoRandom.Next(0, 100) < randomFillPercent)
    //            {
    //                //aiObstacle[x, newRow] = 1;
    //                qHazardRotation.eulerAngles = new Vector3((int)(newRow * cubedegree) , 0, 0);

    //                //Find the hazard locations in the world
    //                xLocalh = ((x * 5) - 250 + offset);
    //                yLocalh = GetOrientedy((float)(newRow), 1);
    //                zLocalh = GetOrientedz((float)(newRow), 1);
    //                vStartHazards = new Vector3(xLocalh, yLocalh, zLocalh);


    //                box = GameObject.FindGameObjectWithTag("disabled");
    //                box.transform.localPosition = vStartHazards;
    //                box.transform.localRotation = qHazardRotation;
    //                box.tag = "enabled";

    //                    if (pseudoRandom.Next(0, 100) < percentLayerTwo)
    //                    {
    //                        xLocalh = ((x * 5) - 250 + offset);
    //                        yLocalh = GetOrientedy((float)(newRow), 2);
    //                        zLocalh = GetOrientedz((float)(newRow), 2);
    //                        vStartHazards = new Vector3(xLocalh, yLocalh, zLocalh);

    //                        box = GameObject.FindGameObjectWithTag("disabled");
    //                        box.transform.localPosition = vStartHazards;
    //                        box.transform.localRotation = qHazardRotation;
    //                        box.tag = "enabled";

    //                        if (pseudoRandom.Next(0, 100) < percentLayerThree)
    //                        {
    //                            xLocalh = ((x * 5) - 250 + offset);
    //                            yLocalh = GetOrientedy((float)(newRow), 3);
    //                            zLocalh = GetOrientedz((float)(newRow), 3);
    //                            vStartHazards = new Vector3(xLocalh, yLocalh, zLocalh);

    //                            box = GameObject.FindGameObjectWithTag("disabled");
    //                            box.transform.localPosition = vStartHazards;
    //                            box.transform.localRotation = qHazardRotation;
    //                            box.tag = "enabled";
    //                        }

    //                }
    //                }
    //            }
    //        newRow++;
    //    }       
    //  }

    public void ReplaceHazard()
    {
        //find blocks that have been removed from play
        openHazards = GameObject.FindGameObjectsWithTag("disabled");
        countHazards = openHazards.Length;

        //if there is enough available then recycle the box's back in
        if (countHazards > 30)
        {
            //randomize location of blocks
            seed = Time.time.ToString();
            System.Random pseudoRandom = new System.Random(seed.GetHashCode());

            //change the center of randomization based on player position on X-axis
           // offset = -Mathf.Round(manager.fUserOffsetT);
            for (int x = 0; x <= 100; x++)
            {
                if (pseudoRandom.Next(0, 100) < randomFillPercent)
                {

                    //Find the hazard locations in the world - layer 1
                    xLocalh = ((x * 5) - 250 + offset);
                    vStartHazards = new Vector3(xLocalh, bottomY, bottomZ);
                    box = GameObject.FindGameObjectWithTag("disabled");
                    box.transform.SetParent(world.transform);
                    box.transform.localPosition = vStartHazards;
                    box.transform.localRotation = qHazardRotation;
                    box.transform.SetParent(rotator.transform);
                    box.tag = "enabled";

                    //layer 2
                    if (pseudoRandom.Next(0, 100) < percentLayerTwo)
                    {
                        xLocalh = ((x * 5) - 250 + offset);
                        vStartHazards = new Vector3(xLocalh, middleY, middleZ);
                        box = GameObject.FindGameObjectWithTag("disabled");
                        box.transform.SetParent(world.transform);
                        box.transform.localPosition = vStartHazards;
                        box.transform.localRotation = qHazardRotation;
                        box.transform.SetParent(rotator.transform);
                        box.tag = "enabled";

                        //layer 3
                        if (pseudoRandom.Next(0, 100) < percentLayerThree)
                        {
                            xLocalh = ((x * 5) - 250 + offset);
                            vStartHazards = new Vector3(xLocalh, topY, topZ);
                            box = GameObject.FindGameObjectWithTag("disabled");
                            box.transform.SetParent(world.transform);
                            box.transform.localPosition = vStartHazards;
                            box.transform.localRotation = qHazardRotation;
                            box.transform.SetParent(rotator.transform);
                            box.tag = "enabled";
                        }

                    }
                }
            }
        }
    }

    public void MapHazard()
    {
        //randomize location of blocks
        seed = Time.time.ToString();
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int y = 0; y <= 70; y++)
        { 
        for (int x = 0; x <= 100; x++)
        {
            if (pseudoRandom.Next(0, 100) < randomFillPercent)
            {
                hazardSectionOne[x, y] = 1;
                //layer 2
                if (pseudoRandom.Next(0, 100) < percentLayerTwo)
                {
                    hazardSectionOne[x, y] = 2;
                    //layer 3
                    if (pseudoRandom.Next(0, 100) < percentLayerThree)
                    {
                        hazardSectionOne[x, y] = 3;
                    }
                }
            }
            else { hazardSectionOne[x, y] = 0; }
        }
    }
    }

    public void MapCircles(int radius)
    {
        int creep = 0;
        int gap = 10;     
        //randomize location of blocks
        seed = Time.time.ToString();
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        //clear array
        for (int y = 0; y <= 70; y++)
        {
            for (int x = 0; x <= 100; x++)
            {
                hazardSectionOne[x, y] = 0;
            }
        }

                // normal pass
                for (int y = radius; y <= (70 - 2 * radius - 10); y += 2*radius+1+creep)
        {
            creep = 0;
            for (int x = radius; x <= (100 - 2 * radius); x++)
            {
                if (pseudoRandom.Next(0, 100) < gap)
                {
                    gap = 10;
                    for (int q = 0; q <= 70; q++)
                    {
                        if (pseudoRandom.Next(0, 100) < gap)
                        {
                            hazardSectionOne[x, y + q] = -1;
                            x += (radius * 2) + 1;
                            gap = 10;
                            if ( q > creep) { creep = q; }
                            q = 71;
                        }
                        else { gap += 10; }
                    }
                }    
                else { gap += 10; }
            }


        }

        for (int y = 0; y <= 70; y++)
        {
            for (int x = 0; x <= 100; x++)
            {
                if (hazardSectionOne[x, y] == -1)
                {
                    for (int a = -radius + 1; a < radius; a++)
                    {
                       // for (int b = -radius; b <= radius; b += (radius * 2))
                       // {
                            hazardSectionOne[x + a, y - radius] = 1;
                       // }
                    }
                    for (int a = -radius; a <= radius; a += (radius * 2))
                    {
                        for (int b = -radius + 1; b < radius; b++)
                        {
                            hazardSectionOne[x + a, y + b] = 1;
                        }
                    }
                }
            }
        }
    }
    

    public void PatternHazard()
    {

        //find blocks that have been removed from play
        openHazards = GameObject.FindGameObjectsWithTag("disabled");
        countHazards = openHazards.Length;
        if (countHazards > 350)
        { go = true; }
        Debug.Log(mahRotation.transform.rotation.eulerAngles.x);
        //if there is enough available then recycle the box's back in
        if (go == true && Mathf.Abs(mahRotation.transform.rotation.eulerAngles.x - change) >= 0.2)
        {
            change = mahRotation.transform.rotation.eulerAngles.x;
            //Debug.Log(iteration);
            for (int x = 0; x <= 100; x++)
            {
                
                if (hazardSectionOne[x,iteration] >= 1)
                {
                    //Debug.Log("I'm trying here!");
                    //Find the hazard locations in the world - layer 1
                    xLocalh = ((x * 5) - 250 + offset);
                    vStartHazards = new Vector3(xLocalh, bottomY, bottomZ);
                    box = GameObject.FindGameObjectWithTag("disabled");
                    box.transform.SetParent(world.transform);
                    box.transform.localPosition = vStartHazards;
                    box.transform.localRotation = qHazardRotation;
                    box.transform.SetParent(rotator.transform);
                    box.tag = "enabled";

                    //layer 2
                    if (hazardSectionOne[x, iteration] >= 2)
                    {
                        xLocalh = ((x * 5) - 250 + offset);
                        vStartHazards = new Vector3(xLocalh, middleY, middleZ);
                        box = GameObject.FindGameObjectWithTag("disabled");
                        box.transform.SetParent(world.transform);
                        box.transform.localPosition = vStartHazards;
                        box.transform.localRotation = qHazardRotation;
                        box.transform.SetParent(rotator.transform);
                        box.tag = "enabled";

                        //layer 3
                        if (hazardSectionOne[x, iteration] >= 3)
                        {
                            xLocalh = ((x * 5) - 250 + offset);
                            vStartHazards = new Vector3(xLocalh, topY, topZ);
                            box = GameObject.FindGameObjectWithTag("disabled");
                            box.transform.SetParent(world.transform);
                            box.transform.localPosition = vStartHazards;
                            box.transform.localRotation = qHazardRotation;
                            box.transform.SetParent(rotator.transform);
                            box.tag = "enabled";
                        }

                    }
                }
            }
            iteration++;
            cycle = iteration;
            if (iteration > 70) { iteration = 0; }
        }
        
        
    }

    public float Funnel(float size)
    {
        for (int x = -1; x <= 1; x += 2)
        {
            //Find the hazard locations in the world
            xLocalh = (x * size * 5) + transform.position.x + startpos;
            vStartHazards = new Vector3(xLocalh, bottomY, bottomZ);
            box = GameObject.FindGameObjectWithTag("disabled");
            box.transform.SetParent(world.transform);
            box.transform.localPosition = vStartHazards;
            box.transform.localRotation = qHazardRotation;
            box.transform.SetParent(rotator.transform);
            box.tag = "enabled";
        }
        size --;




        return size;
    }

    public void MakePath()
    {
        int size = 2;
        openHazards = GameObject.FindGameObjectsWithTag("disabled");
        countHazards = openHazards.Length;
        if (firstcycle == false)
        { startpos = -transform.position.x; }

        if (countHazards > 350)
        { go = true;
        }

        if (go == true && Mathf.Abs(mahRotation.transform.rotation.eulerAngles.x - change) >= 0.2)
        {
            if (funnelsize > size)
            {
                change = mahRotation.transform.rotation.eulerAngles.x;
                funnelsize = Funnel(funnelsize);
            }
            else
            {
                change = mahRotation.transform.rotation.eulerAngles.x;

                seed = Time.time.ToString();
                System.Random pseudoRandom = new System.Random(seed.GetHashCode());
                if ((slope >= target && sign >= 0) || (slope <= target && sign <= 0))
                {
                    distance++;
                }
                if ((slope >= target && sign >= 0) || (slope <= target && sign <= 0) && distance > targetdistance)
                {
                    if (target <= 6.0f)
                    { target = (pseudoRandom.Next(70, 100) / 10); }
                    else { target = (pseudoRandom.Next(0, 30) / 10); }
                    sign = target - slope;
                    sign = sign / Mathf.Abs(sign);
                    acceleration = (pseudoRandom.Next(40, 70) / 50.00f) * sign;
                    distance = 0;
                    targetdistance = (pseudoRandom.Next(40, 100) / 10);
                }

                //offset = -Mathf.Round(manager.fUserOffsetT);
                for (int x = -1; x <= 1; x += 2)
                {
                    //Find the hazard locations in the world
                    xLocalh = ((x * size * 5) + transform.position.x + location + startpos);
                    vStartHazards = new Vector3(xLocalh, bottomY, bottomZ);
                    box = GameObject.FindGameObjectWithTag("disabled");
                    box.transform.SetParent(world.transform);
                    box.transform.localPosition = vStartHazards;
                    box.transform.localRotation = qHazardRotation;
                    box.transform.SetParent(rotator.transform);
                    box.tag = "enabled";

                }
                location += (slope - 5.0f) * 0.6f;
                if ((slope < target && sign >= 0) || (slope > target && sign <= 0))
                {
                    slope = Mathf.Pow(acceleration, 2.0f) * sign + slope;
                }
            }
        }
        firstcycle = true;
    }




    public void SpitHazards()
    {
        randomFillPercent = 5;
        percentLayerTwo = 25;
        percentLayerThree = 25;
        openHazards = GameObject.FindGameObjectsWithTag("disabled");
        countHazards = openHazards.Length;
        if (Mathf.Abs(mahRotation.transform.rotation.eulerAngles.x - change) >= 0.2)
        {
            Debug.Log("spitting");
            change = mahRotation.transform.rotation.eulerAngles.x;
            seed = Time.time.ToString();
            System.Random pseudoRandom = new System.Random(seed.GetHashCode());
            //offset = -Mathf.Round(manager.fUserOffsetT);
            for (int x = 0; x <= 100; x++)
            {
                if (pseudoRandom.Next(0, 100) < randomFillPercent)
                {
                    //Find the hazard locations in the world
                    xLocalh = ((x * 5) - 250 + offset);
                    vStartHazards = new Vector3(xLocalh, bottomY, bottomZ);
                    box = GameObject.FindGameObjectWithTag("disabled");
                    box.transform.parent = head.transform;
                    box.transform.localRotation = qHazardRotation;
                    box.transform.localPosition = new Vector3(1.33f, 0, 3.5f);
                    box.transform.parent = this.transform;
                    oFall = box.GetComponent<Falling>();
                    oFall.destin = transform.InverseTransformPoint(vStartHazards);
                    oFall.layer = 1;
                    oFall.SendMessage("MoveTo", transform.InverseTransformPoint(vStartHazards));
                    box.tag = "enabled";

                        if (pseudoRandom.Next(0, 100) < percentLayerTwo)
                        {
                            xLocalh = ((x * 5) - 250 + offset);
                            vStartHazards = new Vector3(xLocalh, middleY, middleZ);
                            box = GameObject.FindGameObjectWithTag("disabled");
                            box.transform.parent = head.transform;
                            box.transform.localPosition = new Vector3(1.33f, 0, 3.5f);
                            box.transform.localRotation = qHazardRotation;
                            box.transform.parent = this.transform;
                            oFall = box.GetComponent<Falling>();
                            oFall.destin = transform.InverseTransformPoint(vStartHazards);
                            oFall.SendMessage("MoveTo", transform.InverseTransformPoint(vStartHazards));
                            box.tag = "enabled";

                            if (pseudoRandom.Next(0, 100) < percentLayerThree)
                            {
                                xLocalh = ((x * 5) - 250 + offset);
                                vStartHazards = new Vector3(xLocalh, topY, topZ);
                                box = GameObject.FindGameObjectWithTag("disabled");
                                box.transform.parent = head.transform;
                                box.transform.localPosition = new Vector3(1.33f,0,3.5f);
                                box.transform.localRotation = qHazardRotation;
                                box.transform.parent = this.transform;       
                                oFall = box.GetComponent<Falling>();
                                oFall.destin = transform.InverseTransformPoint(vStartHazards);
                                oFall.SendMessage("MoveTo", transform.InverseTransformPoint(vStartHazards));
                                box.tag = "enabled";
                            }
                    }
                }
            }
        }
    }

    }
