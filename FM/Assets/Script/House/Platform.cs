using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    BoxCollider2D col;
    Animator ani;
    float startTime;//colcheck지연
    void Start () {
        col = gameObject.GetComponent<BoxCollider2D>();	
        ani = GameObject.FindWithTag("Player").GetComponent<Animator>();
        startTime = Time.time;

    }

    private void Update()
    {
        //Debug.Log(ani.GetBool("isJump"));
        //if (ani.GetBool("isJumpDown") && !col.enabled)
        //{
        //    col.enabled = true;
        //}
        //else if (ani.GetBool("isJump") )
        //{
        //    col.enabled = false;
        //}
      //  Debug.Log("상승 "+ani.GetBool("isJump"));
      //  Debug.Log("하강 " + ani.GetBool("isJumpDown"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (ani.GetBool("isJumpDown"))
            {
                col.isTrigger = false;
                ani.SetBool("isJump", false);
                ani.SetBool("isJumpDown", false);
                startTime = Time.time;
            }
        }
    }
    //콜리젼으로 바뀐뒤 어떤 조건을 줘야 탈출가능
    //
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Time.time > startTime + 0.07f)
                if (ani.GetBool("isJumpDown"))
                {
                    col.isTrigger = true;
                    startTime = Time.time;
                }
        }
    }
    //빠져나가야 트리거로바뀜
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("바껴버렷");
        col.isTrigger = true;
    }
}
