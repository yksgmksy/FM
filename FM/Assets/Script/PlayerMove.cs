using System.Collections;
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

    //입력?
    bool canMove = true;
    bool isOnLadder = false;
    bool isLayHit = false;

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
        vps[0] = new Vector2(-0.1f, 0.4f);
        vps[1] = new Vector2(-0.1f, -0.4068f);
        vps[2] = new Vector2(0.1f,  -0.4068f);
        vps[3] = new Vector2(0.1f, 0.4f);
        
    }
	
	void Update () {
        if (!isOnLadder)
            if (Input.GetButtonDown("Jump") && canMove)
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            Jump();
        }
    }

    public void SetPastPosition(Vector2 tmp)
    {
        pastPosition = tmp;
    }
    //움직이는 힘 인자
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
        //현재 프레임 - 이전 프레임 위치 가 음/양 수에 따라 낙하/상승
        if (gameObject.transform.position.y - pastPosition.y < -0.01f) // 하강
        {
            anim.SetBool("isStay", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isJumpDown", true);
        }
        else if (gameObject.transform.position.y - pastPosition.y > 0.005f)// 상승 
        {
            anim.SetBool("isStay", false);
            anim.SetBool("isJumping", true);
            anim.SetBool("isJumpDown", false);
        }
        else {
            anim.SetBool("isStay", true);
            anim.SetBool("isJumping", false);
            anim.SetBool("isJumpDown", false);
        }
        
        pastPosition = gameObject.transform.position;
        //Vector2 orgin = this.gameObject.transform.position;
        //Vector2 dir = Vector2.down;
        //RaycastHit2D hit = Physics2D.Raycast(orgin, dir, 0.55f, 1 << LayerMask.NameToLayer("Tiles")  |  1<< LayerMask.NameToLayer("passPlatform") );

        //RaycastHit2D hitPlatForm = Physics2D.Raycast(orgin, dir, 0.5f, 1 << LayerMask.NameToLayer("passPlatform"));
        //Debug.DrawLine(orgin, new Vector3(orgin.x, orgin.y+dir.y+ 0.5f, 0));
        //if (hit.collider != null)
        //{
        //    anim.SetBool("isJumpDown", false);
        //}

        //if (hitPlatForm.collider != null && anim.GetBool("isJumpDown"))
        //{
        //    isLayHit = true;
        //    return;
        //}
        //isLayHit = false;
    }

    public bool GetLayHit()
    {
        return isLayHit;
    }
    void Jump()
    {
        CheckJump();
        //점프중이아니면
        if (!isJumping)
        {
            return;
        }

        r.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        r.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetFloat("hor", Mathf.Abs( Input.GetAxisRaw("Horizontal") ));
        anim.SetFloat("ver", Mathf.Abs(Input.GetAxisRaw("Vertical")));

        //사다리에 올라탓을때
        if (isOnLadder)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                moveVelocity = Vector3.down;
                
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                moveVelocity = Vector3.up;
                
            }
            transform.position += moveVelocity * 1 * Time.deltaTime;
        }
        else
        {
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
            if (Input.GetAxisRaw("Horizontal") == 0)
                anim.SetBool("isWalk", false);

            if (anim.GetBool("isFacedown"))
                transform.position += moveVelocity * (clawpPower) * Time.deltaTime;
            else
                transform.position += moveVelocity * (movePower) * Time.deltaTime;
            MovingPostion = transform.position - firstPosition;
        }
    }

    void MovePlayerY(float y)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + y);
    }
    //사다리 오른후 그래비티 적용
    void SetGravityOn()
    {
        isOnLadder = false;
        r.gravityScale = 1;
        if (col.isTrigger)
            col.isTrigger = false;
    }
    void SetGravityOff()
    {
        isOnLadder = true;
        r.gravityScale = 0;
    }
    void SetLadderMotionOn()
    {
        anim.SetBool("isLadder", true);
    }
    public bool GetisOnLadder()
    {
        return isOnLadder;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tiles") ||
            collision.gameObject.layer == LayerMask.NameToLayer("passPlatform"))
        {
            //모션초기화
            anim.SetBool("isJump", false);
            anim.SetBool("isJumpDown", false);
            //SetPastPosition(collision.gameObject.transform.position);

            if (anim.GetBool("isLadder") )
            {
                anim.SetBool("isLadder", false);
                SetGravityOn();
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder_top"))
        {
            if (anim.GetBool("isLadder") && Input.GetKey(KeyCode.W))
            {
                //올라간다
                anim.SetTrigger("ladderup_mosition");
                anim.SetBool("isLadder", false);

                transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                SetGravityOff();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //내려간다
                if(anim.GetBool("isFacedown"))
                    anim.SetBool("isFacedown", false);
                if (!isOnLadder)
                {
                    anim.SetTrigger("ladderdown_mosition");
                    transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                    SetGravityOff();
                    anim.SetBool("isLadder", true);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            //점프타기불가
            if(!anim.GetBool("isJumpDown") && !anim.GetBool("isJumping"))
            if (Input.GetKeyDown(KeyCode.W) )
            {
                //모션변경
                anim.SetBool("isFacedown", false);
                anim.SetBool("isLadder", true);
                anim.speed = 1;
                r.gravityScale = 0;
                //사다리위치로 이동
                transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                isOnLadder = true;
            }
        }
        
    }
}






