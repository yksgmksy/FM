using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float movePower;
    public float jumpPower;

    Rigidbody2D r;
    SpriteRenderer sr;
    Animator anim;
    Vector3 movement;
    bool isJumping;

    public Vector3 MovingPostion; //처음위치로부터 움직인 거리
    Vector3 firstPosition;

    void Start () {

        MovingPostion = Vector3.zero;
        firstPosition = gameObject.transform.position;
        movement = Vector3.zero;
        isJumping = false;

        r = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }
	
	void Update () {
		if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
       // Debug.Log(anim.GetFloat("hor"));
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
        anim.SetFloat("hor", Mathf.Abs( Input.GetAxisRaw("Horizontal") ));
        

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            sr.flipX = true;
            anim.SetBool("isWalk", true);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            sr.flipX = false;
            anim.SetBool("isWalk", true);
        }
        if(Input.GetAxisRaw("Horizontal") ==0)
            anim.SetBool("isWalk", false);

        transform.position += moveVelocity * (movePower) * Time.deltaTime;
        MovingPostion = transform.position - firstPosition ;
    }
}






