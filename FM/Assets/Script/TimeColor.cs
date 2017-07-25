using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeColor : MonoBehaviour {

    SpriteRenderer sr;
    Color myColor;
    // Use this for initialization
    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        myColor = sr.color;
        Debug.Log(myColor);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (StaticGlobal.gameTime.Hour >= 5 && StaticGlobal.gameTime.Hour < 7) //새벽
        {
            myColor.a = 110;
            myColor.b = 255;
        }
        else if (StaticGlobal.gameTime.Hour >= 7 && StaticGlobal.gameTime.Hour < 9)
        {
            myColor.a = 150;
            myColor.b = 255;
        }
        else if (StaticGlobal.gameTime.Hour >= 9 && StaticGlobal.gameTime.Hour < 11)
        {
            myColor.a = 190;
            myColor.b = 255;
        }
        else if (StaticGlobal.gameTime.Hour >= 17 && StaticGlobal.gameTime.Hour < 19) //노을
        {
            myColor.a = 150;
            myColor.b = 30;
        }
        else if (StaticGlobal.gameTime.Hour >= 19 && StaticGlobal.gameTime.Hour < 21)
        {
            myColor.a = 100;
            myColor.b = 255;
        }
        else if (StaticGlobal.gameTime.Hour >= 21 && StaticGlobal.gameTime.Hour < 23)
        {
            myColor.a = 80;
            myColor.b = 255;
        }
        Color tmp = new Color();
        tmp = myColor;
        tmp.b = myColor.b/255.0f;
        tmp.a = myColor.a / 255.0f;
        sr.color = tmp;
    }
}
