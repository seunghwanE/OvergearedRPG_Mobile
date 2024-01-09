using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorSign : MonoBehaviour
{
    public static ErrorSign inst;

    public Text titleText;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        gameObject.SetActive(false);
    }

    public void SimpleSignSet(string signStr, long vibeTime)
    {
        DataController.inst.Vibrate(vibeTime);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        titleText.text = string.Format("{0}", signStr);
        titleText.gameObject.SetActive(true);
    }

    public void SetOff()
    {
        gameObject.SetActive(false);
    }
}
