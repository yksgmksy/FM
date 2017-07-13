using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using System;
using MessageProtocol;

public class NPC_Manager : MonoBehaviour {

    protected struct no_mean_dialogs
    {
        public List<string> npc_dialog; //대화저장
        public int max_dialog_count; //최대 대화 수
    }
    protected struct main_dialogs
    {
        public List<string> npc_dialog; //대화저장
        public int max_dialog_count; //최대 대화 수
        public int now_dialog_index; //현재 인덱스
    }
    protected struct quest_dialogs
    {
        public List<string> npc_dialog; //대화저장
        public int max_dialog_count; //최대 대화 수
        public int now_dialog_index; //현재 인덱스
    }
    
    protected int number_of_dialog_nomean; //다이얼로그 카운트 (몇번째 리스트의 대화인가?)
    protected int number_of_dialog_main;
    protected int number_of_dialog_quest;
    protected int state_of_dialog;  //대화 상태 (메인,,의미없는..퀘스트

	// Use this for initialization
	void Start () {
        
        state_of_dialog = (int)StaticGlobal.Dialog.NO_MEAN;
        number_of_dialog_nomean = 0;
        number_of_dialog_main = 0;
        number_of_dialog_quest = 0;
    }
	
    protected void NextDialog(int state_of_dialog)
    {
        if(state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN) number_of_dialog_nomean++;
        else if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN) number_of_dialog_main++;
        else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST) number_of_dialog_quest++;
    }
    protected int GetDialogNum(int state_of_dialog) {
        if (state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN) return number_of_dialog_nomean;
        else if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN) return number_of_dialog_main;
        else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST) return number_of_dialog_quest;
        return -1;
    }

    protected void SetDialogNum(int count , int state_of_dialog)
    {
        if (state_of_dialog == (int)StaticGlobal.Dialog.NO_MEAN) number_of_dialog_nomean = count;
        else if (state_of_dialog == (int)StaticGlobal.Dialog.MAIN) number_of_dialog_main = count;
        else if (state_of_dialog == (int)StaticGlobal.Dialog.QUEST) number_of_dialog_quest = count;
    }

    protected void ChangeDialogState(int state)
    {
        state_of_dialog = state;
    }

    string m_strPath = "Assets/Resources/";

    public void WriteData(string strData)
    {
        FileStream f = new FileStream(m_strPath + "Data.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
    }

    //반드시 UTF-8인가 인코딩으로 저장해야합니다 (파일을요)
    protected void Parse(string TextName , no_mean_dialogs dialogBuf_no_mean, 
        main_dialogs dialogBuf_main , quest_dialogs dialogBuf_quest , List<sendmessage> sm_list)
    {
        TextAsset ta = Resources.Load(TextName) as TextAsset;
        StringReader sr = new StringReader(ta.text);

        string source = sr.ReadLine();
        string[] tmp; //속성값을 받기위한 str

        sendmessage tmp_sm;

        int count =0 ;
        int text_state = 0;
        while (source != null)
        {
            //대화의 속성 정의
            if (source == "[NO_MEAN]")
            {
                count = 0;
                text_state = (int)StaticGlobal.Dialog.NO_MEAN;
                source = sr.ReadLine();
            }
            else if (source == "[MAIN]")
            {
                count = 0;
                text_state = (int)StaticGlobal.Dialog.MAIN;
                source = sr.ReadLine();
            }
            else if (source == "[QUEST]")
            {
                count = 0;
                text_state = (int)StaticGlobal.Dialog.QUEST;
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
                tmp_sm.sendObject = this.gameObject;
                sm_list.Add(tmp_sm);
                source = sr.ReadLine();
            }
            else if (tmp[0] == "YES")
            {
                tmp_sm.answer_num = int.Parse(tmp[1]);
                tmp_sm.dialog_num = int.Parse(tmp[2]);
                tmp_sm.dialog_subject = text_state;
                tmp_sm.isAnswer = true;
                tmp_sm.sendObject = this.gameObject;
                sm_list.Add(tmp_sm);
                source = sr.ReadLine();
            }

            // \ 는 한 문장에 띄어쓰기를 한다는 뜻이야 (말풍선에 엔터를 치는 것과 같아)
            source = source.Replace("\\", Environment.NewLine);

            //리스트에 해당 텍스트 추가
            if (text_state == (int)StaticGlobal.Dialog.NO_MEAN) dialogBuf_no_mean.npc_dialog.Add(source);
            else if (text_state == (int)StaticGlobal.Dialog.MAIN)
            {
                dialogBuf_main.npc_dialog.Add(source);
            }
            else if (text_state == (int)StaticGlobal.Dialog.QUEST)
            {
                dialogBuf_quest.npc_dialog.Add(source);
            }

            count++;
            source = sr.ReadLine();    // 한줄 읽는다.
        }
        sr.Close();
    }
}
