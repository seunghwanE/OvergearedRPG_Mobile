using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Snap a scroll rect to its children items. All self contained.
/// Note: Only supports 1 direction
/// </summary>
public class SnapScroll : UIBehaviour, IEndDragHandler, IBeginDragHandler
{
    public int itemCount; // how many items we have in our scroll rect

    public int count = 1;
    public GameObject content;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        StopCoroutine(GetCount());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(GetCount());
    }

    public void NextSnap()
    {
        if (count < 5)
        {
            count++;
            StartPosMoveCor(count, -1);
            //StartCoroutine(SnapRect(nextStartNormal));
        }
    }

    public void PreviousSnap()
    {
        if (count > 1)
        {
            count--;
            StartPosMoveCor(count, 1);
            //StartCoroutine(SnapRect(nextStartNormal));
        }
    }

    public void StartPosMoveCor(int numb, int isFlag)
    { StartCoroutine(PosMoveCor(numb, isFlag)); }

    private IEnumerator PosMoveCor(int numb, int next)
    {
        Vector3 tmpStartPos = content.transform.position;
        Vector3 tmpLastPos = tmpStartPos;
        tmpStartPos.z = 0f;
        tmpLastPos.z = 0f;

        float duration = 0.5f;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            content.transform.position = Vector3.Lerp(tmpStartPos, tmpLastPos, progress);
            yield return null;
        }
        content.transform.position = tmpLastPos;
    }

    private IEnumerator ScaleChange(GameObject upObj, GameObject downObj, GameObject downObj2)
    {
        Vector3 startScale0 = upObj.transform.localScale, startScale1 = downObj.transform.localScale, startScale2 = downObj2.transform.localScale;
        float duration = 0.2f;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            upObj.transform.localScale = Vector3.Lerp(startScale0, Vector3.one, progress);
            downObj.transform.localScale = Vector3.Lerp(startScale1, Vector3.one * 0.5f, progress);
            downObj2.transform.localScale = Vector3.Lerp(startScale2, Vector3.one * 0.5f, progress);
            yield return null;
        }
        upObj.transform.localScale = Vector3.one;
        downObj.transform.localScale = Vector3.one * 0.5f;
        downObj2.transform.localScale = Vector3.one * 0.5f;
    }

    private IEnumerator GetCount()
    {
        Vector3 tmpStartPos = content.transform.position;
        Vector3 tmpLastPos = tmpStartPos;
        tmpStartPos.z = 0f;
        tmpLastPos.z = 0f;
        float min = 10f, now = tmpLastPos.x;
        //float[] list = { 0f ,now + 5.85f, now + 9.85f, now + 13.85f, now + 17.85f };
        float[] list = { 0f, 5.85f, 9.85f, 13.85f, 17.85f };

        for (int i = 0; i < list.Length; i++)
        {
            if (Mathf.Abs(list[i] + now) < min) // 최소값 알고리즘
            {
                min = Mathf.Abs(list[i] + now); // 최소값 알고리
               
                count = i+1;
            }
        }

        float duration = 0.15f;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            content.transform.position = Vector3.Lerp(tmpStartPos, tmpLastPos, progress);
            yield return null;
        }
        content.transform.position = tmpLastPos;
    }

}
