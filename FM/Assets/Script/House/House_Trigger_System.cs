using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Trigger_System : MonoBehaviour {

    public bool isLeft;
    public int connectedFloor;
    public int myFloor;

    House_trigger ht;
    SpriteRenderer sr;
    // Use this for initialization
    void Start () {
        ht = transform.parent.transform.parent.transform.parent.GetComponentInChildren<House_trigger>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && ht.GetisInside() )
            {
                //int형에 따라 순간이동
                //페이드 인아웃
                //플립에 따라 방향 보기/
                //housetrigger의 층 변경
                House_Trigger_System[] hts = transform.parent.GetComponentsInChildren<House_Trigger_System>();
                foreach (House_Trigger_System i in hts)
                {
                    if (connectedFloor == i.myFloor)
                    {
                        collision.gameObject.transform.position = i.transform.TransformDirection(i.transform.position.x, i.transform.position.y, 0);
                        break;
                    }
                }
                sr = collision.gameObject.GetComponent<SpriteRenderer>();
                if (isLeft) sr.flipX = true;
                else sr.flipX = false;
                
                collision.gameObject.GetComponentInChildren<Fade_In_Out>().SendMessage("GoFadeIn");

                ht.SetFloor(connectedFloor);
            } 
        }
    }
}
