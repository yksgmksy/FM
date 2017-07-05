using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour {

    public GameObject Effect;
    GameObject player;
    public float speed;
    Rigidbody2D r;
    Vector3 nomal;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        r = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
   
        Vector3 tmp = r.velocity;
        transform.position += tmp * speed * Time.deltaTime;

        Vector2 orgin = this.gameObject.transform.position;
        Vector2 dir = r.velocity;
        RaycastHit2D hit = Physics2D.Raycast( orgin, dir,0.1f, 1 << LayerMask.NameToLayer("Player"));
       
        if (hit.collider != null)
        {
            Quaternion q = new Quaternion();
            q.x = Random.Range(0.0f, 360.0f);
            q.y = Random.Range(0.0f, 360.0f);
            GameObject.Instantiate(Effect, hit.point, q).transform.parent = player.transform;
            DestroyObject(gameObject);
        }
    }
}
