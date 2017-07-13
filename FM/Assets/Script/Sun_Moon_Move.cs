using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_Moon_Move : MonoBehaviour {

    GameObject player;
    PlayerMove playerMove;
   
    float angle;
    public float sunRadius;

    bool isSun = true;
    bool isMoon = false;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        if ((StaticGlobal.gameTime.Hour >= 7 && StaticGlobal.gameTime.Hour <= 9) || (StaticGlobal.gameTime.Hour >= 16 && StaticGlobal.gameTime.Hour <= 18))
        {
            angle = (StaticGlobal.gameTime.Hour * 60 + StaticGlobal.gameTime.Min) / 1440.0f * 360 - 100;
            gameObject.transform.position = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * sunRadius + player.transform.position.x,
                                                        Mathf.Sin(Mathf.Deg2Rad * angle) * sunRadius +  player.transform.position.y);
        }
        else gameObject.transform.position = new Vector2(0, -1000);

    }
}
