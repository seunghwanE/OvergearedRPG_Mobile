using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contents : MonoBehaviour
{
    public Animator anim;
    //public GameObject backObj;

    private void OnEnable()
    {
        InputController.inst.PopUpList.Add(this);
    }

    private void OnDisable()
    {
        if (InputController.inst.PopUpList.Contains(this))
        {
            InputController.inst.PopUpList.Remove(this);
        }
    }
    public void SetOff()
    {
        gameObject.SetActive(false);
    }
}
