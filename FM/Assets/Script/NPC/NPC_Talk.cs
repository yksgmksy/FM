using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MessageProtocol;

public class NPC_Talk : NPC_Manager {

    public GameObject subtitle;
    Text[] subtitle_texts;

    Canvas myDialogBox;
    Text npc_Text;     //텍스트 컴포넌트

    public bool subtitleMode;
    public string data_name;
    public int dialog_state;

    int stringCount = 0; //한글자씩 출력
    bool isDialog = false;

    GameObject dialogTarget;

    no_mean_dialogs nomean_dialog;
    main_dialogs main_dialog;
    quest_dialogs quest_dialog;
    SpriteRenderer npcSprite;
    List<sendmessage> send_list;

    playermessage playerMessage;

    void Start()
    {
        subtitle_texts = subtitle.GetComponentsInChildren<Text>();
        subtitle_texts[0].text = string.Empty;
        subtitle_texts[1].text = string.Empty;
        subtitle.SetActive(false);

        nomean_dialog.npc_dialog = new List<string>();
        main_dialog.npc_dialog = new List<string>();
        quest_dialog.npc_dialog = new List<string>();
        send_list = new List<sendmessage>();

        npcSprite = gameObject.GetComponent<SpriteRenderer>(); //좌우반전
        myDialogBox = gameObject.GetComponentInChildren<Canvas>(); //
        myDialogBox.enabled = false;
        npc_Text = gameObject.GetComponentInChildren<Text>();
        npc_Text.text = string.Empty;


        Parse(data_name, nomean_dialog, main_dialog, quest_dialog , send_list);
        nomean_dialog.max_dialog_count = nomean_dialog.npc_dialog.Count-1;
        main_dialog.max_dialog_count = main_dialog.npc_dialog.Count - 1;
        quest_dialog.max_dialog_count = quest_dialog.npc_dialog.Count - 1;

        state_of_dialog = dialog_state;
    }

    public bool Get_IsDialog()
    {
        return isDialog;
    }

    void talkStart(GameObject ao)
    {
        dialogTarget = ao;
        if (!subtitleMode)
            myDialogBox.enabled = true;
        StartCoroutine("TalkStart");
    }

    void init_and_next_Dialog(bool canmove)
    {
        myDialogBox.enabled = false;
        npc_Text.text = string.Empty;

        subtitle_texts[0].text = string.Empty;
        subtitle_texts[1].text = string.Empty;
        subtitle.SetActive(false);

        if (canmove)
            dialogTarget.SendMessage("Can_Move", SendMessageOptions.DontRequireReceiver);

        if ((state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN && nomean_dialog.max_dialog_count > GetDialogNum(state_of_dialog)) ||
            (state_of_dialog == (int)StaticGlobal.Dialog.MAIN && main_dialog.max_dialog_count > GetDialogNum(state_of_dialog)) ||
            (state_of_dialog == (int)StaticGlobal.Dialog.QUEST && quest_dialog.max_dialog_count > GetDialogNum(state_of_dialog)))
            NextDialog(state_of_dialog);
    }

    void feedback_to_player(playermessage pm)
    {
        playerMessage = pm;
       
        if (playerMessage.answer_num != 0)
        {
            StopCoroutine("TalkEnd");

            init_and_next_Dialog(true);

            dialogTarget.SendMessage("Can_Move", SendMessageOptions.DontRequireReceiver);
            
        }
        //Debug.Log(pm.player_answer);
    }

    IEnumerator TalkStart()
    {
        isDialog = true;
        while (true)
        {
            if ( (state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN && nomean_dialog.npc_dialog[number_of_dialog_nomean].Length <= npc_Text.text.Length) ||
                (state_of_dialog == (int)StaticGlobal.Dialog.MAIN && main_dialog.npc_dialog[number_of_dialog_main].Length <= npc_Text.text.Length) ||
                (state_of_dialog == (int)StaticGlobal.Dialog.QUEST && quest_dialog.npc_dialog[number_of_dialog_quest].Length <= npc_Text.text.Length))
            {
                stringCount = 0;
                StartCoroutine("TalkEnd");
                
                //메세지는 주고 받기는 MAIN과 QUEST 상태에서만 진행
                if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN)
                {
                    dialogTarget.SendMessage("Answer_Question", send_list[number_of_dialog_main], SendMessageOptions.RequireReceiver);
                }
                else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST)
                {
                    dialogTarget.SendMessage("Answer_Question", send_list[main_dialog.max_dialog_count + number_of_dialog_quest+1], SendMessageOptions.RequireReceiver);
                }
                Debug.Log("종료1");
                yield break; //코루틴 종료
            }


            if (Input.GetKey(KeyCode.R))
            {
                Debug.Log("종료");
                //대화 스킵입니다
                if (state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN) npc_Text.text = nomean_dialog.npc_dialog[number_of_dialog_nomean];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN) npc_Text.text = main_dialog.npc_dialog[number_of_dialog_main];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST) npc_Text.text = quest_dialog.npc_dialog[number_of_dialog_quest];

                //자막모드
                if (state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN) subtitle_texts[1].text = nomean_dialog.npc_dialog[number_of_dialog_nomean];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN) subtitle_texts[1].text = main_dialog.npc_dialog[number_of_dialog_main];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST) subtitle_texts[1].text = quest_dialog.npc_dialog[number_of_dialog_quest];

                stringCount = 0;
                StartCoroutine("TalkEnd");

                if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN)
                {
                    dialogTarget.SendMessage("Answer_Question", send_list[number_of_dialog_main], SendMessageOptions.RequireReceiver);
                }
                else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST)
                {
                    dialogTarget.SendMessage("Answer_Question", send_list[main_dialog.max_dialog_count + number_of_dialog_quest +1], SendMessageOptions.RequireReceiver);
                }
                yield break; //코루틴 종료
            }
            else
            {
                //말풍선
                if (state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN) npc_Text.text += nomean_dialog.npc_dialog[number_of_dialog_nomean][stringCount];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN) npc_Text.text += main_dialog.npc_dialog[number_of_dialog_main][stringCount];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST) npc_Text.text += quest_dialog.npc_dialog[number_of_dialog_quest][stringCount];
                //자막
                if (state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN) subtitle_texts[1].text += nomean_dialog.npc_dialog[number_of_dialog_nomean][stringCount];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN) subtitle_texts[1].text += main_dialog.npc_dialog[number_of_dialog_main][stringCount];
                else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST) subtitle_texts[1].text += quest_dialog.npc_dialog[number_of_dialog_quest][stringCount];

                stringCount++;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //대화창이 뜰 때에는 스킵이 될 수 없어야함
    //그외엔 스킵 가능
    IEnumerator TalkEnd()
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.E) && false == dialogTarget.GetComponent<Player_Answer>().Get_showDialog() )
            {
                init_and_next_Dialog(true);
                break;
            }
            yield return null;
        }
        isDialog = false;
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
                if(!subtitleMode)
                    myDialogBox.enabled = true;//말풍선 띄우기

                //캐릭터 움직이기
                Vector2 tmp = new Vector2();
                SpriteRenderer csr = dialogTarget.GetComponent<SpriteRenderer>();
                if (dialogTarget.transform.position.x < gameObject.transform.position.x)
                {
                    tmp = new Vector2(-1, 1);
                    if (csr.flipX == true) csr.flipX = false;
                }
                else
                {
                    tmp = new Vector2(1, 1);
                    if (csr.flipX == false) csr.flipX = true;
                }

                dialogTarget.SendMessage("Cannot_Move", tmp, SendMessageOptions.DontRequireReceiver);
                dialogTarget.GetComponent<Animator>().SetBool("isWalk", false);

                StartCoroutine("TalkStart");       //코루틴 시작
            }
    }
}
