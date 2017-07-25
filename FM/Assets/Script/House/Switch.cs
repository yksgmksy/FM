using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

public class Switch : MonoBehaviour {

    MeshRenderer[] ms;
    SpriteRenderer sr;
    public Sprite on;
    public Sprite off;

    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ms = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer i in ms)
        {
            i.enabled = false;
        }
        sr.sprite = off;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //플레이어만 상호작용
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (sr.sprite == off) sr.sprite = on;
                else sr.sprite = off;

                foreach(MeshRenderer i in ms)
                {
                    i.enabled = !i.enabled;
                }
            }
        }
    }
}
