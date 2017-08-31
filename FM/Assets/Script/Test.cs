using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(StaticGlobal.gameTime.Day);
        Debug.Log(StaticGlobal.gameTime.Hour);
        Debug.Log(StaticGlobal.gameTime.Min);
        Debug.Log(StaticGlobal.stage_progress);
    }
}
