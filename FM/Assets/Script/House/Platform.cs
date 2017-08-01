using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    BoxCollider2D col;
    Animator ani;

    void Start () {
        col = gameObject.GetComponent<BoxCollider2D>();	
        ani = GameObject.FindWithTag("Player").GetComponent<Animator>();
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

            }
        }
    }
    //콜리젼으로 바뀐뒤 어떤 조건을 줘야 탈출가능
    //
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (ani.GetBool("isStay"))
            return;
        if (!ani.GetBool("isJumpDown"))
        {
            Debug.Log("이런도중에바껴버렷");
            col.isTrigger = true;
        }
    }
    //빠져나가야 트리거로바뀜
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("바껴버렷");
        col.isTrigger = true;
    }
}
