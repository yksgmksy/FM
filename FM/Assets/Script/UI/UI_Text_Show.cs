using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Show : MonoBehaviour {

    public bool showTime;
    public bool showMoney;
    Text text;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if(showTime)
            text.text = "Time : " + StaticGlobal.gameTime.Day + " Day  " + StaticGlobal.gameTime.Hour + ":" + StaticGlobal.gameTime.Min/10 + "0";
        if (showMoney)
            text.text = " "+StaticGlobal.money;
    }
}
