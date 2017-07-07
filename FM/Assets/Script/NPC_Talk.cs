using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MessageProtocol;

public class NPC_Talk : NPC_Manager {

    Canvas myDialogBox;
    Text npc_Text;     //텍스트 컴포넌트

    public string data_name;

    int stringCount = 0; //한글자씩 출력

    GameObject dialogTarget;

    no_mean_dialogs nomean_dialog;
    main_dialogs main_dialog;
    quest_dialogs quest_dialog;
    SpriteRenderer npcSprite;
    List<sendmessage> send_list;

    void Start()
    {
        nomean_dialog.npc_dialog = new List<string>();
        main_dialog.npc_dialog = new List<string>();
        quest_dialog.npc_dialog = new List<string>();
        send_list = new List<sendmessage>();

        npcSprite = gameObject.GetComponent<SpriteRenderer>(); //좌우반전
        myDialogBox = gameObject.GetComponentInChildren<Canvas>(); //
        myDialogBox.enabled = false;
        npc_Text = gameObject.GetComponentInChildren<Text>();
        npc_Text.text = "";
        
        Parse(data_name, nomean_dialog, main_dialog, quest_dialog , send_list);
        nomean_dialog.max_dialog_count = nomean_dialog.npc_dialog.Count-1;
        main_dialog.max_dialog_count = main_dialog.npc_dialog.Count - 1;
        quest_dialog.max_dialog_count = quest_dialog.npc_dialog.Count - 1;

        state_of_dialog = 1;
    }

    IEnumerator TalkStart()
    {
        while (true)
        {
            if ( (state_of_dialog == (int)Dialog.NO_MEAN && nomean_dialog.npc_dialog[number_of_dialog_nomean].Length <= npc_Text.text.Length) ||
                (state_of_dialog == (int)Dialog.MAIN && main_dialog.npc_dialog[number_of_dialog_main].Length <= npc_Text.text.Length) ||
                (state_of_dialog == (int)Dialog.QUEST && quest_dialog.npc_dialog[number_of_dialog_quest].Length <= npc_Text.text.Length))
            {
                stringCount = 0;
                StartCoroutine("TalkEnd");

                //메세지는 주고 받기는 MAIN과 QUEST 상태에서만 진행
                if( state_of_dialog == (int)Dialog.MAIN)
                    dialogTarget.SendMessage("Answer_Question", send_list[number_of_dialog_main], SendMessageOptions.RequireReceiver);
                if (state_of_dialog == (int)Dialog.QUEST)
                    dialogTarget.SendMessage("Answer_Question", send_list[number_of_dialog_main+number_of_dialog_quest], SendMessageOptions.RequireReceiver);
                yield break; //코루틴 종료
            }


            if (Input.GetKey(KeyCode.R))
            {
                //대화 스킵입니다
                if (state_of_dialog == (int)Dialog.NO_MEAN) npc_Text.text = nomean_dialog.npc_dialog[number_of_dialog_nomean];
                else if (state_of_dialog == (int)Dialog.MAIN) npc_Text.text = main_dialog.npc_dialog[number_of_dialog_main];
                else if (state_of_dialog == (int)Dialog.QUEST) npc_Text.text = quest_dialog.npc_dialog[number_of_dialog_quest];
                stringCount = 0;
                StartCoroutine("TalkEnd");

                if (state_of_dialog == (int)Dialog.MAIN)
                    dialogTarget.SendMessage("Answer_Question", send_list[number_of_dialog_main], SendMessageOptions.RequireReceiver);
                if (state_of_dialog == (int)Dialog.QUEST)
                    dialogTarget.SendMessage("Answer_Question", send_list[number_of_dialog_main+number_of_dialog_quest], SendMessageOptions.RequireReceiver);
                yield break; //코루틴 종료
            }
            else
            {
                if (state_of_dialog == (int)Dialog.NO_MEAN) npc_Text.text += nomean_dialog.npc_dialog[number_of_dialog_nomean][stringCount];
                else if (state_of_dialog == (int)Dialog.MAIN) npc_Text.text += main_dialog.npc_dialog[number_of_dialog_main][stringCount];
                else if (state_of_dialog == (int)Dialog.QUEST) npc_Text.text += quest_dialog.npc_dialog[number_of_dialog_quest][stringCount];
                stringCount++;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator TalkEnd()
    {
        while(true)
        {
            //질문에대한 답을 해야함 두번째 인자는 대답 갯수
            //if (state_of_dialog == (int)Dialog.NO_MEAN) number_of_dialog_nomean;
            //else if (state_of_dialog == (int)Dialog.MAIN) number_of_dialog_main;
            //else if (state_of_dialog == (int)Dialog.QUEST) number_of_dialog_quest;
           
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                myDialogBox.enabled = false;
                npc_Text.text = "";

                if ( (state_of_dialog == (int)Dialog.NO_MEAN && nomean_dialog.max_dialog_count > GetDialogNum(state_of_dialog) ) ||
                    (state_of_dialog == (int)Dialog.MAIN && main_dialog.max_dialog_count > GetDialogNum(state_of_dialog) ) ||
                    (state_of_dialog == (int)Dialog.QUEST && quest_dialog.max_dialog_count > GetDialogNum(state_of_dialog)))
                    NextDialog(state_of_dialog);
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
                //좌우반전
                if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
                {
                    npcSprite.flipX = true;
                }
                else
                    npcSprite.flipX = false;

                dialogTarget = collision.gameObject;
                myDialogBox.enabled = true;//말풍선 띄우기
                StartCoroutine("TalkStart");       //코루틴 시작
            }
    }
}
