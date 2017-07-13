using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeColor_SpriteRender : MonoBehaviour {

    enum ChangeWeather{
        Dawn = 0,
        SunUp,
        Sun,
        SunDown,
        Night,
        DeepNight
    }

    SpriteRenderer sr;
    Material mat;
    Color myColor;

    public bool alphaMode = false;
    public float changeTime ;

    string tmpSortingLayer;
    bool isInside = false;
    int changeState = (int)ChangeWeather.Dawn;
    // Use this for initialization
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        mat = gameObject.GetComponent<SpriteRenderer>().material;

        myColor.a = 255;
        myColor.r = 100;
        myColor.g = 100;
        myColor.b = 100;
    }

    void insideModeNPC()
    {
        Color tmp = new Color();
        tmp.a = 0;
        isInside = true;
        mat.SetColor("_mixColor", tmp);

        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<NPC_ThrowObject>().enabled = false;
    }

    void insideMode()
    {
        Color tmp = new Color();
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (!alphaMode)
        {
            tmpSortingLayer = sr.sortingLayerName;
            tmp.r = 0;
            tmp.g = 0;
            tmp.b = 0;
            tmp.a = 1;
        }
        else
        {
            tmpSortingLayer = sr.sortingLayerName; 
            sr.sortingLayerName = "front_objects";
            tmp = mat.GetColor("_mixColor");
            tmp.a = 0.7f;
        }
        isInside = true;
        mat.SetColor("_mixColor", tmp);
    }

    void outsideMode()
    {
        Color tmp = new Color();
        tmp.a = 1;
        isInside = false;
        mat.SetColor("_mixColor", tmp);
        sr.sortingLayerName = tmpSortingLayer;
    }

    void outsideModeNPC()
    {
        Color tmp = new Color();
        tmp.a = 1;
        isInside = false;
        mat.SetColor("_mixColor", tmp);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        //gameObject.GetComponent<NPC_ThrowObject>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInside)
        {
            if (StaticGlobal.gameTime.Hour >= 5 && StaticGlobal.gameTime.Hour < 7) //새벽
            {
                myColor.r = Mathf.Lerp(myColor.r, 100, changeTime * Time.deltaTime);
                myColor.g = Mathf.Lerp(myColor.g, 100, changeTime * Time.deltaTime);
                myColor.b = Mathf.Lerp(myColor.b, 100, changeTime * Time.deltaTime);
            }
            else if (StaticGlobal.gameTime.Hour >= 7 && StaticGlobal.gameTime.Hour < 9)
            {
                myColor.r = Mathf.Lerp(myColor.r, 200, changeTime * Time.deltaTime);
                myColor.g = Mathf.Lerp(myColor.g, 200, changeTime * Time.deltaTime);
                myColor.b = Mathf.Lerp(myColor.b, 200, changeTime * Time.deltaTime);
            }
            else if (StaticGlobal.gameTime.Hour >= 9 && StaticGlobal.gameTime.Hour < 11)
            {
                myColor.r = Mathf.Lerp(myColor.r, 255, changeTime * Time.deltaTime);
                myColor.g = Mathf.Lerp(myColor.g, 255, changeTime * Time.deltaTime);
                myColor.b = Mathf.Lerp(myColor.b, 255, changeTime * Time.deltaTime);
            }
            else if (StaticGlobal.gameTime.Hour >= 17 && StaticGlobal.gameTime.Hour < 19) //노을
            {
                myColor.r = Mathf.Lerp(myColor.r, 255, changeTime * Time.deltaTime);
                myColor.g = Mathf.Lerp(myColor.g, 153, changeTime * Time.deltaTime);
                myColor.b = Mathf.Lerp(myColor.b, 0, changeTime * Time.deltaTime);
            }
            else if (StaticGlobal.gameTime.Hour >= 19 && StaticGlobal.gameTime.Hour < 21)
            {
                myColor.r = Mathf.Lerp(myColor.r, 85, changeTime * Time.deltaTime);
                myColor.g = Mathf.Lerp(myColor.g, 70, changeTime * Time.deltaTime);
                myColor.b = Mathf.Lerp(myColor.b, 50, changeTime * Time.deltaTime);
            }
            else if (StaticGlobal.gameTime.Hour >= 21 && StaticGlobal.gameTime.Hour < 23)
            {
                myColor.r = Mathf.Lerp(myColor.r, 65, changeTime * Time.deltaTime);
                myColor.g = Mathf.Lerp(myColor.g, 50, changeTime * Time.deltaTime);
                myColor.b = Mathf.Lerp(myColor.b, 30, changeTime * Time.deltaTime);
            }
            Color tmp = new Color();
            tmp.r = myColor.r / 255.0f;
            tmp.g = myColor.g / 255.0f;
            tmp.b = myColor.b / 255.0f;
            tmp.a = myColor.a / 255.0f;

            mat.SetColor("_mixColor", tmp);

        }
    }
}