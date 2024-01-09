using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour{

    public GameObject contents, menuBackObj, checkObj;

    public void ClickButton()
    {
        SoundController.inst.UISound(3);
        menuBackObj.SetActive(true);
        contents.SetActive(true);
    }

    public void ClickBattle()
    {
        menuBackObj.SetActive(true);
        contents.SetActive(true);
        Invoke("TimeSlow", 0.3f);
    }

    public void TimeSlow()
    {
        Time.timeScale = 0.1f;
    }
    
}
