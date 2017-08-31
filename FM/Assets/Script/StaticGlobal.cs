using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGlobal : MonoBehaviour {

    public enum Mental{
        Very_Instability = 0, //매우 불안
        Instability,
        Nomal,
        Stable,
        Very_Stable, //매우 안정
        Unknown, //알 수 업 ㅅ 서
        Psychopath, //싸이코패스
        Violent     //폭력적인
    }
    public enum Dialog
    {
        NO_MEAN = 0,
        MAIN,
        QUEST
    }
    public enum NPC_ID
    {
        little_girl = 0,
        jjindda,
        jjindda_door
    }

    public const int NPC_LITTLE_GIRL = 1;

    public struct gameTime
    {
        public static int Day;
        public static int Hour;
        public static int Min;
        public static string AmPm;
    }

    float startTime;
    public float TenMin_Time; //분당 시간
    public int DebugHour;
    public int progress;
    public static bool StopTime;
    public static int Intimacy; //친밀도 -100 ~ 100
    public static int mentalCondition; // 멘탈상태 state
    public static int money;
    public static int stage_progress;

    private void Awake()
    {
        Debug.Log(gameTime.Hour);
        Debug.Log(money);
        Debug.Log(stage_progress);
        Debug.Log(Intimacy);
    }
    private void Start()
    {
        startTime = Time.time;

        gameTime.Day = 0;
        gameTime.Hour = 6;
        gameTime.Min = 0;
        gameTime.AmPm = "AM";

        mentalCondition = (int)Mental.Nomal;
        StopTime = false;

        Intimacy = 0;
        money = 5000;
        stage_progress = progress;
    }

    private void Update()
    {
        gameTime.Hour = DebugHour;
        if ( StopTime == true ) //계속멈추면 계속 초기화 될 가능성 존재
        {
            startTime = Time.time;
        }
        else if( startTime + TenMin_Time <= Time.time ) //몇 초마다
        {
            gameTime.Min += 1; //1분씩 증가
            if (gameTime.Min >= 60)
            {
                gameTime.Min = 0;
                gameTime.Hour += 1;
                DebugHour += 1;
                if (gameTime.Hour >= 24 )
                {
                    gameTime.Hour = 0;
                    DebugHour = 0;
                    gameTime.Day += 1;
                }
            }
            startTime = Time.time;
        }
    }
}
