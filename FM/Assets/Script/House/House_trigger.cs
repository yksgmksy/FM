using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//집으로 통하는 통로에 대한 스크립트
public class House_trigger : MonoBehaviour {

    public Transform movePosition;
    bool isInside = false;
    int nowfloor = 0;

    public bool GetisInside()
    {
        return isInside;
    }
    public void SetFloor(int floor)
    {
        nowfloor = floor;
    }
    public int GetFloor()
    {
        return nowfloor;
    }

    //어둡게만듬 메쉬포함
    void workInRendererComponent(TimeColor_MeshRender[] mr, TimeColor_SpriteRender[] sr , bool isInside)
    {
        if (isInside)
        {
            foreach (TimeColor_MeshRender i in mr)
                i.SendMessage("insideMode");
            foreach (TimeColor_SpriteRender i in sr)
                i.SendMessage("insideMode");
        }
        else
        {
            foreach (TimeColor_MeshRender i in mr)
                i.SendMessage("outsideMode");
            foreach (TimeColor_SpriteRender i in sr)
                i.SendMessage("outsideMode");
        }
    }

    //어둡게만듬
    void workInRendererComponent(TimeColor_SpriteRender[] sr,bool isInside)
    {
        if (isInside)
        {
            foreach (TimeColor_SpriteRender i in sr)
                i.SendMessage("insideModeNPC");
        }
        else
        {
            foreach (TimeColor_SpriteRender i in sr)
                i.SendMessage("outsideModeNPC");
        }
    }

    //태그로 해당 객체를 찾음
    void findRendererComponent(bool isInside)
    {
        GameObject npcs = GameObject.FindWithTag("npcs");
        TimeColor_SpriteRender[] npc_sr = npcs.GetComponentsInChildren<TimeColor_SpriteRender>();
        workInRendererComponent(npc_sr, isInside);

        GameObject bg = GameObject.FindWithTag("background");
        TimeColor_SpriteRender[] sr = bg.GetComponentsInChildren<TimeColor_SpriteRender>();
        TimeColor_MeshRender[] mr = bg.GetComponentsInChildren<TimeColor_MeshRender>();

        workInRendererComponent(mr, sr, isInside);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //플레이어만 상호작용
        {
            if (Input.GetKeyDown(KeyCode.E) )//&& !isInside) //E버튼
            {
                collision.gameObject.GetComponentInChildren<Fade_In_Out>().SendMessage("GoFadeIn"); //이동시 페이드 인효과를줌
                //transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 0;

                //현재 상태 저장( 플레이어 대신에 집의 입구를 가르키는 이 스크립트에서 현재 layer와 inside를 )
                isInside = true;
                nowfloor = 1;

                //control shader uniform by SendMessage
                transform.parent.GetComponentInChildren<House_InOut_Collider>().SendMessage("Enable_Trigger");

                collision.gameObject.transform.position = movePosition.position;
                //findRendererComponent(isInside);
            }
            /*else if (Input.GetKeyDown(KeyCode.E) && isInside) //E버튼
            {
                collision.gameObject.GetComponentInChildren<Fade_In_Out>().SendMessage("GoFadeIn"); 
                //transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 10;
                isInside = false;
                nowfloor = 0;
                transform.parent.GetComponentInChildren<House_InOut_Collider>().SendMessage("Enable_Trigger");
                //findRendererComponent(isInside);
            }*/
        }
    }
}
