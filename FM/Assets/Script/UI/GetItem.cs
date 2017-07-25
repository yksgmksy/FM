using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemProtocol;

public class GetItem : MonoBehaviour {

    public Sprite image;
    public string name;
    public string explain;

    GameObject ui;
    ItemInfo item;
	// Use this for initialization
	void Start () {
        item.item_image = image;
        item.item_name = name;
        item.item_explain = explain;
        item.item_ID = 0;
        item.item_type = 0;
        item.item_effect = 0;

        ui = GameObject.FindWithTag("InventorySystem");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ui.GetComponent<UI_InventorySystem>().SendMessage("AddItem", item, SendMessageOptions.RequireReceiver);
            DestroyObject(gameObject);
        }
        
    }
}
