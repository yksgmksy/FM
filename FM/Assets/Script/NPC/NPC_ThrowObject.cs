using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ThrowObject : MonoBehaviour {

    public GameObject throwObject;
    public float Dist;
    
    GameObject target;
    float startTime ;
    float attSpeed = 2.5f;
	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player");
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 player2d;
        player2d.x =  gameObject.transform.position.x ;
        player2d.y = gameObject.transform.position.y;
        Vector2 my2d;
        my2d.x = gameObject.transform.position.x;
        my2d.y = gameObject.transform.position.y;
        if (Vector2.Distance(player2d, my2d) <= Dist)
        {
            if (startTime + attSpeed < Time.time)
            {
                Quaternion q = new Quaternion();
                q.x = Random.Range(0.0f, 360.0f);
                q.y = Random.Range(0.0f, 360.0f);
                GameObject to = (GameObject)Instantiate(throwObject, gameObject.transform.position,q);
                Vector2 tmp = target.transform.position - gameObject.transform.position;
                tmp.Normalize();
                to.GetComponent<Rigidbody2D>().velocity = tmp;
             
                startTime = Time.time;
            }
        }
            
	}
}
