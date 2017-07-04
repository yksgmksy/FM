using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeColor_MeshRender : MonoBehaviour {

    MeshRenderer mr;
    Material mat;
    Color myColor;
    // Use this for initialization
    void Start () {
        mr = gameObject.GetComponent<MeshRenderer>();
        mat = gameObject.GetComponent<MeshRenderer>().material;
       
    }
	
	// Update is called once per frame
	void Update () {
        if (myColor.r > 255) myColor.r = 0;
        if(myColor.g > 255) myColor.g = 0;
        if(myColor.b > 255) myColor.b = 0;
        myColor.r = Random.Range(0.001f,1);
        myColor.g = Random.Range(0.001f, 1);
        myColor.b = Random.Range(0.001f, 1);
        myColor.a = 1;
        mat.SetColor("_mixColor", myColor);

    }
}
