using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour{
    public static CanvasController inst;
    public Animator animMain, animContent, animCam;


    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
    }

    public void CanvasLongShake()
    {
        if (DataController.inst.S_Shake)
        {
            animMain.SetTrigger("Long");
            animContent.SetTrigger("Long");
            animCam.SetTrigger("Long");
        }
    }

    public void CanvasShortShake()
    {
        if (DataController.inst.S_Shake)
        {
            animCam.SetTrigger("Short");
            animMain.SetTrigger("Short");
            animContent.SetTrigger("Short");
        }
    }

    public void AttackShake()
    {
        if (DataController.inst.S_Shake)
        { animCam.SetTrigger("Short"); }
    }

    public void ErrorSignShake()
    {
        if (DataController.inst.S_Shake)
        {
            animMain.SetTrigger("Short");
            animContent.SetTrigger("Short");
        }
    }
}
