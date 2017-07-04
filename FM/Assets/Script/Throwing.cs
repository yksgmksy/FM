using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour {

    public GameObject Effect;
     GameObject moveTo;
    public float speed;

    Vector3 nomal;
	// Use this for initialization
	void Start () {
        moveTo = GameObject.FindWithTag("Player"); 
        nomal = moveTo.transform.position - gameObject.transform.position ;
        nomal.Normalize();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += nomal * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Debug.Log("충돌");
            Quaternion r = new Quaternion();
           r.x = Random.Range(0.0f, 360.0f);
                r.y = Random.Range(0.0f, 360.0f);
            Instantiate(Effect, other.gameObject.transform.position, r );
            DestroyObject(gameObject);
        }
    }
}
