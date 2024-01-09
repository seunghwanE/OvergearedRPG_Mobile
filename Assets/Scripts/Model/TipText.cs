using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipText : MonoBehaviour
{
    public Text tip;

    private void OnEnable()
    { tip.text = string.Format("Tip. {0}", Language.inst.tips[DataController.inst.tipID]); }
}
