using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurTimer : MonoBehaviour
{
    public float shakePower;
    public Material material;

    private void OnEnable()
    {
        StartCoroutine(BlurStartCoroutine());
    }

    public void EndBlur()
    {
        StartCoroutine(BlurEndCoroutine());
    }

    protected IEnumerator BlurStartCoroutine()
    {
        float t = 0;
        while (t < 2.5f)
        {
            t += Time.deltaTime * 4f;
            material.SetFloat("Size", t);

            yield return null;
        }
    }

    protected IEnumerator BlurEndCoroutine()
    {
        float t = 0;
        while (t > 0.1f)
        {
            t -= Time.deltaTime * 4f;
            material.SetFloat("Size", t);

            yield return null;
        }
    }

    //IEnumerator HitEffectCoroutine(Color color)
    //{
    //    for (int i = 0; i < sr.Length; i++)
    //    {
    //        sr[i].material.SetColor(ColorEffectName, color);
    //    }
    //    float t = 0f;
    //    while (t < 0.5f)
    //    {
    //        t += Time.deltaTime * 4f;
    //        for (int i = 0; i < sr.Length; i++)
    //        {
    //            sr[i].material.SetFloat(WhiteEffectAmountName, t);
    //        }
    //        yield return null;
    //    }
    //    while (t > 0f)
    //    {
    //        t -= Time.deltaTime * 4f;
    //        for (int i = 0; i < sr.Length; i++)
    //        {
    //            sr[i].material.SetFloat(WhiteEffectAmountName, t);
    //        }
    //        yield return null;
    //    }
    //    for (int i = 0; i < sr.Length; i++)
    //    {
    //        sr[i].material.SetColor(ColorEffectName, Color.white);
    //        sr[i].material.SetFloat(WhiteEffectAmountName, 0f);
    //    }
    //}
}

