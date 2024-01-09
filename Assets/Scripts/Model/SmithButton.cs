using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithButton : MonoBehaviour
{
    public GameObject lockImage, lockText, popObj;
    public int id, count, startCost;
    public string kind;


    public void Start()
    {
        DataController.inst.LoadSmithButton(this);
    }

    public void ClickButton()
    {
        if (DataController.inst.invenEmptyCount > 0)
        {
            InputController.inst.OtherContentsTurnOff(false);
            SmithController.inst.smithButton = this;
            SmithController.inst.CostUpdate();
            popObj.SetActive(true);
        }
        else
        {
            ErrorSign.inst.SimpleSignSet(Language.inst.strArray[53], 30);
        }
    }
    
}
