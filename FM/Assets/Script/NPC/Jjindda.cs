using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jjindda : MonoBehaviour {

    Animator anim;
    // Use this for initialization
    WaitForSeconds delay;
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine("randomMosion");
        delay = new WaitForSeconds(1.0f);
    }

    IEnumerator randomMosion()
    {
        int r = (int)Random.Range(0, 10);
        if (r == 0) anim.SetBool("isIdle", true);
        else anim.SetBool("isIdle", false);
        yield return delay;
        StartCoroutine("randomMosion");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
