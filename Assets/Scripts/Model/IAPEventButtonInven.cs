using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPEventButtonInven : MonoBehaviour
{

    private void OnEnable()
    {
        if(DataController.inst.IAPEventPop || DataController.inst.IAPEvent)
        { gameObject.SetActive(false); }
    }

    public void EvenPopClosed()
    { DataController.inst.IAPEventPop = true; DataController.inst.es3.Sync(); }
}
