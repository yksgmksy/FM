using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageProtocol;
using System.IO;
using System;

public class Choice_Manager : MonoBehaviour {

    public enum Dialog
    {
        NO_MEAN = 0,
        MAIN,
        QUEST
    }

    string m_strPath = "Assets/Resources/choice_dialog";
    
    //반드시 UTF-8 인코딩으로
    protected void Parse(string TextName)
    {
        TextAsset ta = Resources.Load(TextName) as TextAsset;
        StringReader sr = new StringReader(ta.text);

        string source = sr.ReadLine();
        string[] tmp; //속성값을 받기위한 str

        sendmessage tmp_sm;

        int count = 0;
        int text_state = 0;
        while (source != null)
        {
            //캐릭터의 정보 구별
            //대화의 속성 정의
            if (source == "[NO_MEAN]")
            {
                count = 0;
                text_state = (int)Dialog.NO_MEAN;
                source = sr.ReadLine();
            }

            //대화의 메세지를 보낼 기타 속성들을 정의하기위한것
            tmp = source.Split(',');
            if (tmp[0] == "NO")
            {
                tmp_sm.answer_num = int.Parse(tmp[1]);
                tmp_sm.dialog_num = int.Parse(tmp[2]);
                tmp_sm.dialog_subject = text_state;
                tmp_sm.isAnswer = false;
                //sm_list.Add(tmp_sm);
                source = sr.ReadLine();
            }

            // \ 는 한 문장에 띄어쓰기를 한다는 뜻이야 (말풍선에 엔터를 치는 것과 같아)
            source = source.Replace("\\", Environment.NewLine);

            //리스트에 해당 텍스트 추가
            //dialogBuf_no_mean.npc_dialog.Add(source);

            count++;
            source = sr.ReadLine();    // 한줄 읽는다.
        }
        sr.Close();
    }
}
