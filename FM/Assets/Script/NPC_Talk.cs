using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour {

    string npc_dialog; //텍스트에 출력될 문자열
    Canvas myDialogBox;
    Text npc_Text;     //텍스트 컴포넌트
    int stringCount = 0;

    SpriteRenderer npcSprite;

    void Start()
    {
        npcSprite = gameObject.GetComponent<SpriteRenderer>();
        myDialogBox = gameObject.GetComponentInChildren<Canvas>();
        myDialogBox.enabled = false;
        npc_Text = gameObject.GetComponentInChildren<Text>();
        npc_Text.text = "";
        npc_dialog = "안녕하 새오 ? \n머하 새오 ?";
    }

    IEnumerator TalkStart()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                //대화 스킵입니다
                npc_Text.text = npc_dialog;
                stringCount = 0;
                StartCoroutine("TalkEnd");
                yield break; //코루틴 종료
            }
            else if (npc_dialog.Length <= npc_Text.text.Length)
            {
                stringCount = 0;
                StartCoroutine("TalkEnd");
                yield break; //코루틴 종료
            }
            else
            {
                npc_Text.text += npc_dialog[stringCount];
                stringCount++;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator TalkEnd()
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                myDialogBox.enabled = false;
                npc_Text.text = "";
                break;
            }
            yield return null;
        }
        yield break;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //플레이어만 상호작용
            if (Input.GetKeyDown(KeyCode.E) && myDialogBox.enabled == false) //E버튼
            {
                if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
                {
                    npcSprite.flipX = true;
                }
                else
                    npcSprite.flipX = false;
                //npc_Text.text = npc_dialog; //텍스트 넣기
                myDialogBox.enabled = true;//
                StartCoroutine("TalkStart");       //코루틴 시작
            }
    }
}
