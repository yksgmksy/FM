using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//겜 진행사항을 제어 
public class Control_Stage : MonoBehaviour {

    //필요사항 전부선언 문제없음
    GameObject subtitle;
    Text[] subtitle_texts;

    GameObject player;
    PlayerMove pm;
    SpriteRenderer player_sr;

    GameObject npcs;
    White white;
    NPC_Talk whiteTalk;

    public GameObject mainCamera;
    Camera_Option co;

    public GameObject bed;
    SpriteRenderer bedsr;
    public Sprite bed_not;
    public Sprite bed_in;

    public int debug_progress;
    int progress;

	void Awake () {
        player = GameObject.FindWithTag("Player");
        npcs = GameObject.FindWithTag("npcs");
        //mainCamera = GameObject.FindWithTag("MainCamera");
        subtitle = GameObject.FindWithTag("Subtitle");

        pm = player.GetComponent<PlayerMove>();
        player_sr = player.GetComponent<SpriteRenderer>();
        white = npcs.GetComponentInChildren<White>();
        whiteTalk = white.gameObject.GetComponent<NPC_Talk>();
        co = mainCamera.GetComponent<Camera_Option>();
        bedsr = bed.GetComponent<SpriteRenderer>();
        subtitle_texts = subtitle.GetComponentsInChildren<Text>();
        
        
        progress = debug_progress;
    }

    void GameProgress()
    {
        if (StaticGlobal.stage_progress == 0)//1-1
        {
            //어떠한 조건에 따라 progress 증가
            switch (progress)
            {
                case -1:
                    {
                        //co.ChangeCamera_Nomal(player.transform.position); //플레이어
                        pm.SendMessage("Can_Move", SendMessageOptions.RequireReceiver); //키입력가능
                        bedsr.sprite = bed_not;
                        player_sr.enabled = true;
                        StaticGlobal.StopTime = false;
                        progress = 100;
                        break;
                    }
                case 0:
                    {
                        //입력 불가
                        pm.SendMessage("Cannot_Move",new Vector2(0,0), SendMessageOptions.RequireReceiver);
                        //침대에 누워 있기
                        bedsr.sprite = bed_in;
                        player_sr.enabled = false;
                          //플레이어를 없애고 침대그림을 바꾼다?

                        //6시 15분에 다음으로
                        if (StaticGlobal.gameTime.Hour == 6 && StaticGlobal.gameTime.Min >= 15)
                        {
                            progress++;
                        }
                        break;
                    }
                case 1:
                    {
                        //서서히 등장
                        white.SendMessage("slowAppear", SendMessageOptions.RequireReceiver);
                        //카메라 고정
                        Debug.Log(co);
                        Debug.Log(player.transform.position);
                        Debug.Log(white.gameObject.transform.position);
                        co.ChangeCamera_Center(player.transform.position, white.gameObject.transform.position);
                        //시간고정
                        StaticGlobal.StopTime = true;
                        //말하기

                        if (false == whiteTalk.Get_IsDialog())
                        {
                            whiteTalk.subtitleMode = true;
                            subtitle.SetActive(true);
                            subtitle_texts[0].text = "목소리";
                            white.SendMessage("talkStart", player, SendMessageOptions.RequireReceiver);
                            progress++;
                        }
                        break;
                    }
                case 2:
                    {
                        if (false == whiteTalk.Get_IsDialog())
                        {
                            subtitle.SetActive(true);
                            subtitle_texts[0].text = "목소리";
                            white.SendMessage("talkStart", player, SendMessageOptions.RequireReceiver);
                            progress++;
                        }
                        break;
                    }
                case 3:
                    {
                        //앉기
                        if (false == whiteTalk.Get_IsDialog())
                        {
                            co.ChangeCamera_Nomal(player.transform.position); //플레이어
                            pm.SendMessage("Can_Move", SendMessageOptions.RequireReceiver); //키입력가능
                            bedsr.sprite = bed_not;
                            player_sr.enabled = true;
                            StaticGlobal.StopTime = false;
                            
                            progress++;
                        }
                        break;
                    }
                case 4:
                    {
                        if (white.DisappearComplete())
                        {
                            progress++;
                        }
                        white.SendMessage("slowDisappear", SendMessageOptions.RequireReceiver);
                        break;
                    }
                default:
                    break;
            }
        }
    }

    void Update()
    {
        GameProgress();
    }
                
}
