using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{

    public LineRenderer Vex;
    int b = -20;
    int index = 0;
    // Use this for initialization
    void Start()
    {
        Vex = GetComponent<LineRenderer>();
        Vex.positionCount = 1000;
        for (int a = 10; a >= -10; a--)
        {
            Vex.SetPosition(index, new Vector3(b, 0, a * 2));
            b = -b;
            index++;
            Vex.SetPosition(index, new Vector3(b, 0, a * 2));
            index++;
        }

        for (int a = 10; a >= -10; a--)
        {
            Vex.SetPosition(index, new Vector3(a * 2, 0, b));
            b = -b;
            index++;
            Vex.SetPosition(index, new Vector3(a * 2, 0, b));
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
