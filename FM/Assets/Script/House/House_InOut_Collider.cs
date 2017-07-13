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
            i.enabled = false;
    }

    void Enable_Trigger()
    {
        if (!my_switch)
        {
            my_switch = true;
            foreach (BoxCollider2D i in cols)
                i.enabled = true;
        }
        else
        {
            my_switch = false;
            foreach (BoxCollider2D i in cols)
                i.enabled = false;
        }
    }
}
