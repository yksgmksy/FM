using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_In_Out : MonoBehaviour
{
    void GoFadeIn()
    {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        Color color = new Vector4(0, 0, 0, 1);
        transform.GetComponent<SpriteRenderer>().color = color;
        StartCoroutine("FadeOut");
        yield break;
    }

    IEnumerator FadeOut()
    {
        for (float i = 1f; i >= 0; i -= 0.05f)
        {
            Color color = new Vector4(0, 0, 0, i);
            transform.GetComponent<SpriteRenderer>().color = color;
            yield return 0;
        }
        yield break;
    }
}
