using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPop : MonoBehaviour
{
    public GameObject[] objs;
    public int count;

    private void OnEnable()
    {
        count = 0;
        objs[count].SetActive(true);
    }

    public void Click()
    {
        objs[count].SetActive(false);
        count++;
        if (count < objs.Length)
        { objs[count].SetActive(true); }
        else
        { gameObject.SetActive(false); }
    }
}
