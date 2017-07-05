using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeColor_MeshRender : MonoBehaviour {
    enum ChangeWeather
    {
        Dawn = 0,
        SunUp,
        Sun,
        SunDown,
        Night,
        DeepNight
    }

    MeshRenderer mr;
    Material mat;
    Color myColor;
    public float changeTime;
    int changeState = (int)ChangeWeather.Dawn;
    // Use this for initialization
    void Start () {
        mr = gameObject.GetComponent<MeshRenderer>();
        mat = gameObject.GetComponent<MeshRenderer>().material;

        myColor.a = 1;
        myColor.r = 100;
        myColor.g = 100;
        myColor.b = 100;
    }
	
	// Update is called once per frame
	void Update () {
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
        tmp.a = 1;
        mat.SetColor("_mixColor", tmp);
    }
}
