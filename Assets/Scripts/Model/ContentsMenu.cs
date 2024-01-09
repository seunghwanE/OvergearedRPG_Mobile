using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsMenu : MonoBehaviour
{
    public Image thisImage, selectImage;
    public GameObject contents;
    public ContentsMenu[] otherMenu;
    public Color onColor, offColor;

    public void SetOn()
    {
        thisImage.color = Color.white;
        contents.SetActive(true);
    }
    public void SetOff()
    {
        thisImage.color = Color.gray;
        contents.SetActive(false);
    }

    public void ClickMenu()
    {
        SoundController.inst.UISound(5);
        otherMenu[0].SetOff();
        otherMenu[1].SetOff();
        SetOn();
    }

    public void Off()
    {
        selectImage.gameObject.SetActive(false);
        thisImage.color = offColor;
        contents.SetActive(false);
    }
    public void Set()
    {
        selectImage.gameObject.SetActive(true);
        thisImage.color = onColor;
        contents.SetActive(true);
    }
    public void ClickStoreMenu()
    {
        for (int i = 0; i < otherMenu.Length; i++)
        {
            otherMenu[i].Off();
        }
        Set();
    }
}
