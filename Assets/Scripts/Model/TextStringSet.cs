using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextStringSet : MonoBehaviour
{
    public Text thisText;
    public int id;

    private void Start()
    {
        thisText.text = Language.inst.strArray[id];
    }
}
