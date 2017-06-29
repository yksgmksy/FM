using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float movePower;
    public float jumpPower;

    Rigidbody2D r;

    Vector3 movement;
    bool isJumping;

    void Start () {

        
        movement = Vector3.zero;
        isJumping = false;

        r = gameObject.GetComponent<Rigidbody2D>();
    }
	
	void Update () {
		if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
	}

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Jump()
    {
        if (!isJumping)
            return;
        r.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        r.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
}






