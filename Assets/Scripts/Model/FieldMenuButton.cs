using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMenuButton : MonoBehaviour
{
    public GameObject lockObj;
    public int id;

    private void OnEnable()
    {
        if(DataController.inst.highestField >= id)
        { lockObj.SetActive(false); }
        else
        { lockObj.SetActive(true); }
    }
}
