using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageProtocol;

public class Dialog : MonoBehaviour
{
    sendmessage my_answer;
    bool showDialog = false;
    string answer = "";

    Canvas myDialogBox;

    void Start()
    {
        myDialogBox = gameObject.GetComponentInChildren<Canvas>();
        myDialogBox.enabled = false;
    }

    void Answer_Question(sendmessage sm)
    {
        my_answer.answer_num = sm.answer_num;
        my_answer.dialog_num = sm.dialog_num;
        my_answer.dialog_subject = sm.dialog_subject;
        my_answer.isAnswer = sm.isAnswer;
        StartCoroutine("ShowDialog");
    }

    //answer 에 값이 들어가기 전까지는 코루틴이 끝나지 않음
    IEnumerator ShowDialog()
    {
        answer = "";
        showDialog = true;
        do
        {
            yield return null;
        } while (answer == "");

        showDialog = false;
    }

    IEnumerator ActionA()
    {
        Debug.Log("Action A");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ActionB()
    {
        Debug.Log("Action B");
        yield return new WaitForSeconds(2f);
    }

    void OnGUI()
    {
        if (showDialog)
        {
            for (int i = 0; i < my_answer.answer_num; ++i)
            {
                if (GUI.Button(new Rect(10f, 10f + i*20f, 100f, 20f), i+"번째 대답"))
                {
                    answer = "Action" + i;
                }
            }
        }
    }

}