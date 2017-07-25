﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float movePower;
    public float jumpPower;
    public float clawpPower;

    Rigidbody2D r;
    SpriteRenderer sr;
    Animator anim;
    Vector3 movement;
    bool isJumping;

    public Vector3 MovingPostion; //처음위치로부터 움직인 거리
    Vector3 firstPosition;
    Vector2 pastPosition;

    PolygonCollider2D col;
    Vector2[] standPath = new Vector2[4]; //처음콜라이더
    Vector2[] vps = new Vector2[4]; //엎드릴때 콜라이더

    bool canMove = true;

    void Start () {

        MovingPostion = Vector3.zero;
        firstPosition = gameObject.transform.position;
        movement = Vector3.zero;
        isJumping = false;

        r = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<PolygonCollider2D>();

        standPath = col.GetPath(0);
        vps[0] = new Vector2(-0.466f, 0.291f);
        vps[1] = new Vector2(-0.466f, 0.025f);
        vps[2] = new Vector2(0.496f,  0.025f);
        vps[3] = new Vector2(0.496f, 0.291f);
        
    }
	
	void Update () {
		if(Input.GetButtonDown("Jump") && canMove)
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
       // Debug.Log(anim.GetFloat("hor"));
	}

    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            Jump();
        }
    }

    void Cannot_Move(Vector2 dir)
    {
        canMove = false;
        r.AddForce(dir, ForceMode2D.Impulse);
    }

    void Can_Move()
    {
        canMove = true;
    }

    void CheckJump()
    {
        Vector2 orgin = this.gameObject.transform.position;
        Vector2 dir = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(orgin, dir, 0.5f, 1 << LayerMask.NameToLayer("Tiles"));

        Debug.DrawLine(orgin, new Vector3(orgin.x, orgin.y+dir.y+ 0.5f, 0));
        if (hit.collider != null)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isJumpDown", false);
        }
    }

    void Jump()
    {

        //현재 프레임 - 이전 프레임 위치 가 음/양 수에 따라 낙하/상승
        if (gameObject.transform.position.y - pastPosition.y <= 0)
        {
            anim.SetBool("isJumpDown", true);
            CheckJump();
        }
        pastPosition = gameObject.transform.position;
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

        if (Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetBool("isFacedown", true);
            col.SetPath(0, vps);
            //col = crawl;
        }
        else
        {
            anim.SetBool("isFacedown", false);
            col.SetPath(0, standPath);
        }
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

        if (anim.GetBool("isFacedown")) 
            transform.position += moveVelocity * (clawpPower) * Time.deltaTime;
        else
            transform.position += moveVelocity * (movePower) * Time.deltaTime;
        MovingPostion = transform.position - firstPosition ;
    }
    
}






