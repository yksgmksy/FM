using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

public class White : MonoBehaviour {

    public float angleSpeed;
    public float rad;
    float angle;
    Vector2 firstPosition;

    SpriteRenderer sr;
    LightSprite ls;
    Color tmpColor;
    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ls = gameObject.GetComponentInChildren<LightSprite>();
        ls.gameObject.transform.localScale = new Vector3(0, 0, 1);
        tmpColor = sr.color;
        tmpColor.a = 0;
        sr.color = tmpColor;

        firstPosition = gameObject.transform.position;
        angle = 0;

        
    }
	
    //천천히 나타나기
    void slowAppear()
    {
        StartCoroutine("appear");
    }
    //천천히 나타나기
    void slowDisappear()
    {
        StartCoroutine("disappear");
    }

    public bool DisappearComplete()
    {
        if (sr.color.a >= 0.0001f)
        {
            tmpColor = sr.color;
            tmpColor.a = 0;
            sr.color = tmpColor;
            StopCoroutine("appear");
            StopCoroutine("disappear");
            return true;
        }
        return false;
    }
    IEnumerator appear()
    {
        while(true)
        {
            tmpColor = sr.color;
            tmpColor.a = Mathf.Lerp(tmpColor.a, 1.0f, Time.deltaTime);
            sr.color = tmpColor;

            ls.gameObject.transform.localScale = Vector3.Lerp(ls.gameObject.transform.localScale, new Vector3(2, 2, 1), Time.deltaTime);
            yield return null;  
        }
        yield break;
    }
    IEnumerator disappear()
    {
        while (true)
        {
            tmpColor = sr.color;
            tmpColor.a = Mathf.Lerp(tmpColor.a, 0.0f, Time.deltaTime);
            sr.color = tmpColor;

            ls.gameObject.transform.localScale = Vector3.Lerp(ls.gameObject.transform.localScale, new Vector3(0, 0, 1), Time.deltaTime);
            yield return null;
        }
        yield break;
    }
    void Update () {
        //if(Input.GetKeyDown(KeyCode.E))
        //    slowAppear(); 
        //올라갔다 내려갔다?
        //빙글빙글?
        angle += Time.deltaTime * angleSpeed;
        gameObject.transform.position = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * rad + firstPosition.x,
                                                    Mathf.Sin(Mathf.Deg2Rad * angle) * rad + firstPosition.y);
    }
}
