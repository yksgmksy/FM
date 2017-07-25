using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_IntimacyStick : MonoBehaviour {
    int intimacy;
    RectTransform rtf;
    void Start()
    {
        rtf = GetComponent<RectTransform>();
    }
    void Update()
    {
        
        rtf.localPosition = new Vector2(intimacy, 0);
        intimacy = StaticGlobal.Intimacy;     
    }
}
