using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Controller : MonoBehaviour {

    public Image Stick;
    private Vector3 orignPos = Vector3.zero;


    private void Awake()
    {
        
    }

    private void Start()
    {
        orignPos = Stick.transform.position;  
    }

    public void OnDrag()
    {
        Touch touch = Input.GetTouch(0);
        if(Stick != null)
        {
            Stick.rectTransform.position = touch.position;
        }
        Vector3 dir = (orignPos - new Vector3(touch.position.x, touch.position.y, orignPos.z)).normalized;

        float touchAreaRadius = Vector3.Distance(orignPos, new Vector3(touch.position.x, touch.position.y, orignPos.z));
    }

    public void OnEndDrag()
    {
        if(Stick!=null)
        {
            Stick.rectTransform.position = orignPos;
        }
    }
}
