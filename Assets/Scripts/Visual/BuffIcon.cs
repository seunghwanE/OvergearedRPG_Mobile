using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
    public Text timeText;
    public int id;


    private void OnEnable()
    {
        if (id.Equals(0))
        {
            timeText.text = string.Format("{0}m", DataController.inst.buff_auto);
            BattleController.inst.adButtons[id].SetCoolUI(DataController.inst.buff_auto);
        }
        else if (id.Equals(1))
        {
            timeText.text = string.Format("{0}m", DataController.inst.buff_cri);
            BattleController.inst.adButtons[id].SetCoolUI(DataController.inst.buff_cri);
        }
        else if (id.Equals(2))
        {
            timeText.text = string.Format("{0}m", DataController.inst.buff_gold);
            BattleController.inst.adButtons[id].SetCoolUI(DataController.inst.buff_gold);
        }
        else if (id.Equals(3))
        {
            timeText.text = string.Format("{0}m", DataController.inst.buff_dam);
            BattleController.inst.adButtons[id].SetCoolUI(DataController.inst.buff_dam);
        }
        BattleController.inst.adButtons[id].thisButton.interactable = false;
        BattleController.inst.adButtons[id].coolObj.SetActive(true);
        
    }


    private void Start()
    {
        if (id.Equals(0))
        {
            if (AdMobController.inst.buffAuto.Equals(0))
            { TimeOut(); }
        }
        else if (id.Equals(1))
        {
            if (AdMobController.inst.buffCri.Equals(1))
            { TimeOut(); }
        }
        else if (id.Equals(2))
        {
            if (AdMobController.inst.buffGold.Equals(0))
            { TimeOut(); }
        }
        else if (id.Equals(3))
        {
            if (AdMobController.inst.buffAt.Equals(0))
            { TimeOut(); }
        }
    }
    public void BuffStart()
    {
        gameObject.SetActive(true);
        BattleController.inst.adButtons[id].thisButton.interactable = false;
        BattleController.inst.adButtons[id].coolObj.SetActive(true);
    }

    public void TimeOut()
    {
        gameObject.SetActive(false);
        BattleController.inst.adButtons[id].thisButton.interactable = true;
        BattleController.inst.adButtons[id].coolObj.SetActive(false);
    }

    public void SetTimeUI(int time)
    {
        timeText.text = string.Format("{0}m", time.ToString());
    }
}
