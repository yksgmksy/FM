using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnOffButton : MonoBehaviour {

    GameObject onoff; //인벤토리창 껏다 키기
    bool isShow;
    void Start () {
        onoff = GameObject.FindWithTag("Inventory_OnOff");
        isShow = false;
    }
	
	public void OnOff()
    {
        isShow = !isShow;
        onoff.SetActive(isShow);
    }
}
