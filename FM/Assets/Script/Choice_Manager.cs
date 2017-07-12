using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageProtocol;
using System.IO;
using System;

public class Choice_Manager : MonoBehaviour {

    
    public struct player_answer
    {
        public List<string> main_dialog; //선택지임
        public List<int> answer_id; //몇번째 지문
        public List<int> answer_num; //몇번의? 대답
    }
    public struct charactor_text_set
    {
        public int npc_id; //npc고유 id (define정의)
        public player_answer main_answer;
        public player_answer quest_answer;
    }
        
    void Start()
    {
        
    }

    string m_strPath = "Assets/Resources/";

    public void WriteData(string strData)
    {
        FileStream f = new FileStream(m_strPath + "Data.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
    }

    //반드시 UTF-8 인코딩으로
    protected void Parse(string TextName , charactor_text_set textset , List<playermessage> pm_list)
    {
        TextAsset ta = Resources.Load(TextName) as TextAsset;
        StringReader sr = new StringReader(ta.text);

        string source = sr.ReadLine();
        playermessage tmp_pm; // message dumy
        int dm = 0;
        int an = 0;
        textset.npc_id = (int)StaticGlobal.NPC_ID.little_girl; //기본초기화
            
        int text_state = 0; //대화의 주제
        while (source != null)
        {
            //Debug.Log(source);
            //캐릭터의 정보 구별
            if (source == "[little_girl]")
            {
                textset.npc_id = (int)StaticGlobal.NPC_ID.little_girl;
                source = sr.ReadLine();
            }
            //대화의 속성 정의
            if (source == "[MAIN]")
            {
                text_state = (int)StaticGlobal.Dialog.MAIN;
                source = sr.ReadLine();
            }
            else if (source == "[QUEST]")
            {
                text_state = (int)StaticGlobal.Dialog.QUEST;
                source = sr.ReadLine();
            }

            //대화의 메세지를 보낼 기타 속성들을 정의하기위한것
            string[] tmp; //속성값을 받기위한 str
            tmp = source.Split(',');
            if (tmp[0] == "<ANSWER>") //질문이면
            {
                tmp_pm.dialog_num = int.Parse(tmp[1]);
                dm = int.Parse(tmp[1]);
                tmp_pm.answer_num = int.Parse(tmp[2]);
                an = int.Parse(tmp[2]);
                tmp_pm.player_answer = 0; //일단 0
                tmp_pm.dialog_subject = text_state;

                //Debug.Log(tmp_pm.dialog_num + " " + tmp_pm.answer_num);

                pm_list.Add(tmp_pm);
            }
            else if (tmp[0] != "<ANSWER>")
            {
                //리스트에 해당 텍스트 추가
                if (text_state == (int)StaticGlobal.Dialog.MAIN)
                {
                    textset.main_answer.answer_id.Add(dm);
                    textset.main_answer.answer_num.Add(an);
                    textset.main_answer.main_dialog.Add(source);
                }
                else if (text_state == (int)StaticGlobal.Dialog.QUEST)
                {
                    textset.quest_answer.answer_id.Add(dm);
                    textset.quest_answer.answer_num.Add(an);
                    textset.quest_answer.main_dialog.Add(source);
                }
            }
            source = sr.ReadLine();    // 한줄 읽는다.
        }
        sr.Close();
    }
}
