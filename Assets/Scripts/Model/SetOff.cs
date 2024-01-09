using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOff : MonoBehaviour {

    private void OffObj()
    { gameObject.SetActive(false); }


    public void TipUpdate()
    { DataController.inst.UpdateTipID(); }
}
