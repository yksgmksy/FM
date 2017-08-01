using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_InOut_Collider : MonoBehaviour {

    BoxCollider2D[] cols;
   
    bool my_switch = false;
	// Use this for initialization
	void Start () {
        cols = gameObject.GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D i in cols)
            i.enabled = true;
    }

    void Enable_Trigger()
    {
        foreach (BoxCollider2D i in cols)
            i.enabled = true;
    }
}
