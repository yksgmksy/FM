using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemProtocol;

public class UI_InventorySystem : MonoBehaviour {
    
    enum ItemType
    {
        JustHave = 0,
        HaveEffect
    }
    enum ItemEffect
    {
        Effect01 = 0,
        Effect02,
        Effect03,
        Effect04,
        Effect05
    }

    ItemInfo[] inventroy;
    
    GameObject onoff; //인벤토리창 껏다 키기
    bool isShow;

    GameObject inventroyObject;
    GameObject inventoryExplain;
    Image[] slotImages; //슬롯의 이미지 찾기
    void Start () {
        inventroyObject = GameObject.FindWithTag("Inventory");
        inventoryExplain = GameObject.FindWithTag("InventoryExplain");
        onoff = GameObject.FindWithTag("Inventory_OnOff");
        isShow = false;
        onoff.SetActive(isShow);

        slotImages = inventroyObject.GetComponentsInChildren<Image>();
        inventroy = new ItemInfo[18];
        for(int i = 0; i<inventroy.Length; ++i)
        {
            inventroy[i].item_name = string.Empty;
            inventroy[i].item_explain = string.Empty;
        }
    }
	
    public void OnOff()
    {
        Debug.Log("눌림");
        isShow = !isShow;
        onoff.SetActive(isShow);
    }

    public void Explain(int index)
    {
        inventoryExplain.GetComponentInChildren<Text>().text = inventroy[index].item_explain;
        Image[] tmp = inventoryExplain.GetComponentsInChildren<Image>();
        Color cl = tmp[1].color;
        if (inventroy[index].item_name != string.Empty)
        {
            tmp[1].sprite = inventroy[index].item_image;
            cl.a = 255;
            tmp[1].color = cl;
        }
        else
        {
            cl.a = 0;
            tmp[1].color = cl;
        }
    }

    void AddItem(ItemInfo it) //아이템 먹을 때 자동추가
    {
        
        //아이템 빈곳 찾으면
        for(int i = 0; i<inventroy.Length; ++i)
        {
            if(inventroy[i].item_image == null)
            {
                //정보 저장
                inventroy[i].item_image = it.item_image;
                inventroy[i].item_name = it.item_name;
                inventroy[i].item_explain = it.item_explain; 
                inventroy[i].item_ID = it.item_ID;
                inventroy[i].item_type = it.item_type;
                inventroy[i].item_effect = it.item_effect;
                break;
            }
        }
        ReplaceInventory();
    }

    void ReplaceInventory() //하위 객체에 그려줌
    {
        for(int i = 0; i < inventroy.Length; ++i)
        {
            if (inventroy[i].item_image != null) {
                slotImages[i+1].sprite = inventroy[i].item_image; 
            }
        }
    }
	// Update is called once per frame
	void Update () {
    }
}
