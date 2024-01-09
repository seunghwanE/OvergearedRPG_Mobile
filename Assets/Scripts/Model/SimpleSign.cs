using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSign : MonoBehaviour
{
    public static SimpleSign inst;

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
        titleText.text = string.Format("{0}", signStr);
        titleText.gameObject.SetActive(true);
        gameObject.SetActive(true);
        if (vibeTime > 99)
        { CanvasController.inst.ErrorSignShake(); }
    }

    private void OnDisable()
    {
        titleText.gameObject.SetActive(false);
    }
}
