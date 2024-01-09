using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public int id, cost;
    public bool isPurchased;
    public string money;
    public Image LockImage;

    public void Click()
    {
        SoundController.inst.UISound(3);
        if (isPurchased)
        {
            if(!PlayerController.inst.skinId.Equals(id))
            {
                MainSceneController.inst.SetCharacterSkin(id);
                DataController.inst.es3.Save("CS", id);
            }
        }
        else
        {
            SkinController.inst.selectSkin = this;
            SkinController.inst.BuyPopUpdateUI(money, cost);
        }
    }

    public void UpdateUI()
    {
        if(!isPurchased)
        { LockImage.gameObject.SetActive(true); }
        else
        { LockImage.gameObject.SetActive(false); }
    }
}
