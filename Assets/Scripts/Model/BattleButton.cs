using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour{

    public Image enemyImage, buttonImage, clearImage;
    public GameObject lockObj;
    public Text stageText, hpText;

    public int id;
    public bool isClear;



    public void Click()
    {
        if (DataController.inst.ticket < 1)
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[152], 20); }
        else
        {
            if (id.Equals(0))
            { BattleStart(); }
            else
            {
                if (MainSceneController.inst.battles[id - 1].isClear)
                { BattleStart(); }
                else
                { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[153], 20); }
            }
        }
    }

    public void BattleStart()
    {
        MainSceneController.inst.GoBattle();
        DataController.inst.battleButtonNumb = id;
        DataController.inst.es3.Sync();
    }

    public void HpTextUpdateUI()
    {
        int maxHp = 0, twob =0;
        if (id < 4)
        { maxHp = (id + 1) * 20000; twob = 0; }
        else if (id < 8)
        { maxHp = (id + 1) * 500000; twob = 0; }
        else if (id < 12)
        { maxHp = (id + 1) * 8000000; twob = 0; }
        else if (id < 16)
        { maxHp = (id + 1) * 100000000; twob = 0; }
        else
        { maxHp = 2000000000; twob = id - 16; }

        if(twob.Equals(0))
        { hpText.text = string.Format("HP : {0}", NumbToData.GetInt(maxHp)); }
        else
        { hpText.text = string.Format("HP : {0} x{1}", NumbToData.GetInt(maxHp), (twob+1).ToString()); }
    }

    public void UpdateUI()
    {
        //enemyImage.sprite = ImageController.inst.enemyFace[id];
        HpTextUpdateUI();
        hpText.color = Color.red;
        stageText.text = string.Format("{0}", id + 1);
        if (isClear)
        {
            stageText.gameObject.SetActive(true);
            buttonImage.color = Color.white;
            enemyImage.color = Color.white;
            clearImage.gameObject.SetActive(true);
            lockObj.SetActive(false);
        }
        else
        {
            clearImage.gameObject.SetActive(false);
            if (id.Equals(0))
            {
                stageText.gameObject.SetActive(true);
                buttonImage.color = Color.white;
                enemyImage.color = Color.white;
                lockObj.SetActive(false);
            }
            else
            {
                if(MainSceneController.inst.battles[id -1].isClear)
                {
                    stageText.gameObject.SetActive(true);
                    buttonImage.color = Color.white;
                    lockObj.SetActive(false);
                    enemyImage.color = Color.white;
                }
                else
                {
                    stageText.gameObject.SetActive(false);
                    buttonImage.color = Color.gray;
                    lockObj.SetActive(true);
                    enemyImage.color = Color.black;
                    hpText.color = Color.gray;
                }
            }
        }
    }
    
}
