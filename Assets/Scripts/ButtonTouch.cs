using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonTouch : MonoBehaviour
{

    //private RaycastHit hit;
    //private Vector3 end;
    GraphicRaycaster gr;
    PointerEventData ped;
    List<RaycastResult> results;
    public bool playerPreferences;
    public bool changeScene;
    public int prefValue;
    public string prefTag;
    public int loadScene;


    void Start()
    {
         gr = this.GetComponent<GraphicRaycaster>();
         ped = new PointerEventData(null);
        results = new List<RaycastResult>();
    }



    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if ((Input.GetTouch(i).phase == TouchPhase.Ended))
                {
                    Debug.Log(i);
                    ped.position = Input.GetTouch(i).position;
                    gr.Raycast(ped, results);
                    for (int q = 0; q <= results.Count - 1; q++)
                    {
                        if (results[q].gameObject.name == this.name && playerPreferences)
                        {
                            PlayerPrefs.SetInt(prefTag, prefValue);
                            results.Clear();
                        }
                        else if (results[q].gameObject.name
                            == this.name && changeScene)
                        {
                            SceneManager.LoadScene(loadScene);
                            results.Clear();
                        }
                    }
                }
            }
        }



    }
}