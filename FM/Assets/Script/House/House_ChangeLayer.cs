using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//층을 구별해서 그것만 그려주게 하는 함수
public class House_ChangeLayer : MonoBehaviour {

    House_trigger ht;
    SpriteRenderer sr;
    int nowFloor = 0;
    int firstSortingOrder = 0;
    public int floor;
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ht = transform.parent.GetComponentInChildren<House_trigger>();
        firstSortingOrder = sr.sortingOrder;
    }
	
	void Update () {
        nowFloor = ht.GetFloor();
        if (floor != nowFloor)
        {
            sr.sortingOrder = -1;
        }
        else if (floor == nowFloor)
            sr.sortingOrder = firstSortingOrder;
    }
}
