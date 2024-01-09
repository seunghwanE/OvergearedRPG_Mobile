using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoDelete : MonoBehaviour
{
    public Toggle toggle;
    public GameObject lockObj, godItem;
    public void Start()
    {
        StartSet();
    }

    private void OnEnable()
    {
        if (PlayerController.inst.level > 19)
        { lockObj.SetActive(false); }

        if(DataController.inst.highestField > 48)
        { godItem.SetActive(true); }
    }

    public void Click()
    {
        if (PlayerController.inst.level > 19)
        {
            if (toggle.isOn)
            { toggle.isOn = false; }
            else
            { toggle.isOn = true; }
            SetToggle();
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[162], 20); }
    }

    public void SetToggle()
    {
        if (toggle.isOn)
        { DataController.inst.S_AutoDelete = true; }
        else
        { DataController.inst.S_AutoDelete = false; }
    }
    public void StartSet()
    {
        if (DataController.inst.S_AutoDelete)
        { toggle.isOn = true; }
        else
        { toggle.isOn = false; }
    }
}
