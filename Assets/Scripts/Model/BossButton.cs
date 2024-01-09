using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossButton : MonoBehaviour
{

    public void SetOff()
    {
        gameObject.SetActive(false);
    }
    public void NextMonster()
    {
        BattleController.inst.stageCount = 0;
        BattleController.inst.countText.text = "0 / 10";
        BattleController.inst.deadFlag = true;
        DataController.inst.field++;
        gameObject.SetActive(false);
    }

    public void ClickButton()
    {
        BattleController.inst.countText.text = "0 / 10";
        //BattleController.inst.deadFlag = true;
        BattleController.inst.BossButtonClick();
    }
}
