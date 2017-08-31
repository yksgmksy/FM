using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageProtocol;

public class Player_Answer : Choice_Manager
{
    sendmessage my_answer;
    bool showDialog = false;
    string answer = string.Empty;
    string showText = string.Empty;

    Canvas myDialogBox;

    charactor_text_set littleGirl;
    charactor_text_set jjinDda;
    charactor_text_set jjinDda_door;
    List<playermessage> pm;
    GameObject targetObject;
    string targetName;
    void Start()
    {
        myDialogBox = gameObject.GetComponentInChildren<Canvas>();
        myDialogBox.enabled = false;

        //initTextSet(littleGirl);
        //initTextSet(jjinDda);
        littleGirl.main_answer.answer_id = new List<int>();
        littleGirl.main_answer.answer_num = new List<int>();
        littleGirl.main_answer.main_dialog = new List<string>();
        littleGirl.quest_answer.answer_id = new List<int>();
        littleGirl.quest_answer.answer_num = new List<int>();
        littleGirl.quest_answer.main_dialog = new List<string>();

        jjinDda.main_answer.answer_id = new List<int>();
        jjinDda.main_answer.answer_num = new List<int>();
        jjinDda.main_answer.main_dialog = new List<string>();
        jjinDda.quest_answer.answer_id = new List<int>();
        jjinDda.quest_answer.answer_num = new List<int>();
        jjinDda.quest_answer.main_dialog = new List<string>();

        jjinDda_door.main_answer.answer_id = new List<int>();
        jjinDda_door.main_answer.answer_num = new List<int>();
        jjinDda_door.main_answer.main_dialog = new List<string>();
        jjinDda_door.quest_answer.answer_id = new List<int>();
        jjinDda_door.quest_answer.answer_num = new List<int>();
        jjinDda_door.quest_answer.main_dialog = new List<string>();

        pm = new List<playermessage>(); //message struct
        Parse("choice_jjindda", jjinDda, pm);
        Parse("choice_jjindda_door", jjinDda_door, pm);
        Parse("choice_littlegirl", littleGirl, pm);
    }

    void initTextSet(charactor_text_set txt)
    {
        txt.main_answer.answer_id = new List<int>();
        txt.main_answer.answer_num = new List<int>();
        txt.main_answer.main_dialog = new List<string>();
        txt.quest_answer.answer_id = new List<int>();
        txt.quest_answer.answer_num = new List<int>();
        txt.quest_answer.main_dialog = new List<string>();
    }

    void Answer_Question(sendmessage sm)
    {
        my_answer.answer_num = sm.answer_num;
        my_answer.dialog_num = sm.dialog_num;
        my_answer.dialog_subject = sm.dialog_subject;
        my_answer.isAnswer = sm.isAnswer;
        my_answer.sendObject = sm.sendObject;
        my_answer.message = sm.message;
        targetObject = sm.sendObject;
        targetName = sm.sendName;

        StartCoroutine("ShowDialog"); //대화가 끝나면 시작되는 함수
    }

    //answer 에 값이 들어가기 전까지는 코루틴이 끝나지 않음
    IEnumerator ShowDialog()
    {
        answer = string.Empty;
        //Debug.Log("들어왓음");
        if(my_answer.answer_num > 0)
            showDialog = true;
        do
        {
            yield return null;
        } while (answer == string.Empty);

        //메세지 리스트의 어떤 메세지를 보낼 것인가?
        int messageList_Index = 0;
        //if(my_answer.dialog_subject == (int)StaticGlobal.Dialog.MAIN) 
        messageList_Index = my_answer.dialog_num;
       
        targetObject.SendMessage("feedback_to_player", pm[messageList_Index], SendMessageOptions.RequireReceiver); // pm[1]에 어떤걸보내야할까요
        showDialog = false;
    }

    public bool Get_showDialog()
    {
        return showDialog;
    }

    void OnGUI()
    {
        if (showDialog)
        {
            
            for (int i = 0; i < my_answer.answer_num; ++i)
            {
                if (my_answer.dialog_subject == (int)StaticGlobal.Dialog.MAIN)
                {
                    if(targetName == "little_girl") showText = littleGirl.main_answer.main_dialog[i];
                    else if (targetName == "jjindda") showText = jjinDda.main_answer.main_dialog[i];
                    else if (targetName == "jjindda_door") showText = jjinDda_door.main_answer.main_dialog[i];
                }
                else if (my_answer.dialog_subject == (int)StaticGlobal.Dialog.QUEST)
                {
                    if (targetName == "little_girl") showText = littleGirl.quest_answer.main_dialog[i];
                    else if (targetName == "jjindda") showText = jjinDda.quest_answer.main_dialog[i];
                    else if (targetName == "jjindda_door") showText = jjinDda_door.quest_answer.main_dialog[i];
                }
                if (GUI.Button(new Rect(50f, 50f + i*50f, 400f, 50f), showText))
                {
                    answer = "something";
                    //어느 메세지를 클릭했는가 정해짐
                    playermessage tmp = pm[my_answer.dialog_num];
                    tmp.answer_num = my_answer.answer_num;
                    tmp.dialog_num = my_answer.dialog_num;
                    tmp.dialog_subject = my_answer.dialog_subject;
                    tmp.message = my_answer.message;
                    tmp.player_answer = i;
                    pm[my_answer.dialog_num] = tmp;
                }
            }
        }
    }

}