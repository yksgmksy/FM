using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform movePosition;
	

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //플레이어만 상호작용
        {
            if (Input.GetKeyDown(KeyCode.E) ) //E버튼
            {
                collision.gameObject.GetComponentInChildren<Fade_In_Out>().SendMessage("GoFadeIn"); //이동시 페이드 인효과를줌
                transform.parent.GetComponentInChildren<House_InOut_Collider>().SendMessage("Enable_Trigger");
                collision.gameObject.transform.position = movePosition.position;
                collision.gameObject.GetComponent<PlayerMove>().SetPastPosition(movePosition.position);
                //findRendererComponent(isInside);
            }
        }
    }
}
