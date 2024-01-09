using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdButton : MonoBehaviour
{
    public int id;
    public GameObject coolObj;
    public Text coolTimeText;
    public Button thisButton;

    public void PurchasedAd()
    {
        if (DataController.inst.IAPAD)
        {
            DataController.inst.C_Ad++;
            DataController.inst.C_AdFive++;
            if (id < 4)
            { AdMobController.inst.AddTime(id); AdMobController.inst.StartBuffOne(id); }
            else if(id < 5)
            { BattleController.inst.AdRevive(); StartCoroutine(InteractiveCor()); }
        }
        else
        { AdMobController.inst.ShowRewardAd(id); StartCoroutine(InteractiveCor()); }
    }

    IEnumerator InteractiveCor()
    {
        thisButton.interactable = false;
        yield return new WaitForSeconds(120f);
        thisButton.interactable = true;
    }

    public void SetCoolUI(int time)
    {
        coolTimeText.text = string.Format("{0}m", time.ToString());
    }
}
