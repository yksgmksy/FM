using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{

    bool showDialog = false;
    string answer = "";

    Canvas myDialogBox;

    IEnumerator Start()
    {
        myDialogBox = gameObject.GetComponentInChildren<Canvas>();
        myDialogBox.enabled = false;
        yield return StartCoroutine("ShowDialog"); //다른 코루틴이 끝날 때까지 대기
        yield return StartCoroutine(answer);
    }
    //answer 에 값이 들어가기 전까지는 코루틴이 끝나지 않음
    IEnumerator ShowDialog()
    {
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
            if (GUI.Button(new Rect(10f, 10f, 100f, 20f), "A"))
            {
                answer = "ActionA";
            }
            else if (GUI.Button(new Rect(10f, 50f, 100f, 20f), "B"))
            {
                answer = "ActionB";
            }
        }
    }

}