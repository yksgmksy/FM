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

    charactor_text_set textset;
    List<playermessage> pm;
    GameObject targetObject;
    void Start()
    {
        myDialogBox = gameObject.GetComponentInChildren<Canvas>();
        myDialogBox.enabled = false;
        
        textset.main_answer.answer_id = new List<int>();
        textset.main_answer.answer_num = new List<int>();
        textset.main_answer.main_dialog = new List<string>();
        textset.quest_answer.answer_id = new List<int>();
        textset.quest_answer.answer_num = new List<int>();
        textset.quest_answer.main_dialog = new List<string>();
        pm = new List<playermessage>(); //message struct
        Parse("choice_dialog", textset, pm);

    }

    void Answer_Question(sendmessage sm)
    {
        my_answer.answer_num = sm.answer_num;
        my_answer.dialog_num = sm.dialog_num;
        my_answer.dialog_subject = sm.dialog_subject;
        my_answer.isAnswer = sm.isAnswer;
        my_answer.sendObject = sm.sendObject;
        targetObject = sm.sendObject;
        

        StartCoroutine("ShowDialog"); //대화가 끝나면 시작되는 함수
    }

    //answer 에 값이 들어가기 전까지는 코루틴이 끝나지 않음
    IEnumerator ShowDialog()
    {
        answer = string.Empty;
        Debug.Log("들어왓음");
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

    void OnGUI()
    {
        if (showDialog)
        {
            
            for (int i = 0; i < my_answer.answer_num; ++i)
            {
                if (my_answer.dialog_subject == (int)StaticGlobal.Dialog.MAIN)
                    showText = textset.main_answer.main_dialog[i];
                else if (my_answer.dialog_subject == (int)StaticGlobal.Dialog.QUEST)
                    showText = textset.quest_answer.main_dialog[i];
                if (GUI.Button(new Rect(10f, 10f + i*20f, 200f, 20f), showText))
                {
                    answer = "something";
                    //어느 메세지를 클릭했는가 정해짐
                    playermessage tmp = pm[my_answer.dialog_num];
                    tmp.player_answer = i;
                    pm[my_answer.dialog_num] = tmp;
                }
            }
        }
    }

}