using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTime : MonoBehaviour {

    GameObject player;
	void Start () {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 10.0f); 
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = player.transform.position;

    }
}
