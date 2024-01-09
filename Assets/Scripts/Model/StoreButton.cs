using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public int id;
    public Coroutine cor;

    public void BuyIAP()
    {
        IAPController.inst.BuyProduct(id);
    }

    public void BuyStore()
    {
        if (id.Equals(4))
        {
            if (DataController.inst.gold >= DataController.inst.storeStatCost)
            {
                SoundController.inst.UISound(6);
                DataController.inst.PrayCount++;
                DataController.inst.C_BlessTen++;
                DataController.inst.C_PaidGold++;
                MainSceneController.inst.UpdateGold(-DataController.inst.storeStatCost);
                DataController.inst.storeStatCost = (int)(DataController.inst.storeStatCost * 1.1f);
                MainSceneController.inst.storeStatCostText.text = string.Format("{0}", NumbToData.GetIntGold(DataController.inst.storeStatCost));
                PlayerController.inst.GetRandomStat();
                DataController.inst.es3.Sync();
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }
        else if (id.Equals(5))
        {
            if (DataController.inst.ruby > 4)
            {
                SoundController.inst.UISound(2);
                DataController.inst.C_PaidRuby++;
                MainSceneController.inst.UpdateRuby(-5);
                MainSceneController.inst.UpdateGold(5000);
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 30);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }
        else if (id.Equals(6))
        {
            if (DataController.inst.ruby > 49)
            {
                SoundController.inst.UISound(2);
                DataController.inst.C_PaidRuby++;
                MainSceneController.inst.UpdateRuby(-50);
                MainSceneController.inst.UpdateGold(80000);
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 30);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }
        else if (id.Equals(7))
        {
            if (DataController.inst.ruby > 249)
            {
                SoundController.inst.UISound(2);
                DataController.inst.C_PaidRuby++;
                MainSceneController.inst.UpdateRuby(-250);
                MainSceneController.inst.UpdateGold(500000);
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 30);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }

        else if (id.Equals(8))
        {
            if (DataController.inst.ruby > 9)
            {
                SoundController.inst.UISound(2);
                DataController.inst.C_PaidRuby++;
                MainSceneController.inst.UpdateRuby(-10);
                MainSceneController.inst.UpdateIron(20);
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 30);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }
        else if (id.Equals(9))
        {
            if (DataController.inst.ruby > 99)
            {
                SoundController.inst.UISound(2);
                DataController.inst.C_PaidRuby++;
                MainSceneController.inst.UpdateRuby(-100);
                MainSceneController.inst.UpdateIron(300);
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 30);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }
        else if (id.Equals(10))
        {
            if (DataController.inst.ruby > 299)
            {
                SoundController.inst.UISound(2);
                DataController.inst.C_PaidRuby++;
                MainSceneController.inst.UpdateRuby(-300);
                MainSceneController.inst.UpdateIron(1200);
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 30);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }
        //else if (id.Equals(11))
        //{
        //    if (DataController.inst.ruby > 1)
        //    {
        //        SoundController.inst.UISound(6);
        //        DataController.inst.PrayCount++;
        //        DataController.inst.C_PaidRuby++;
        //        DataController.inst.C_BlessTen++;
        //        MainSceneController.inst.UpdateRuby(-2);
        //        SkillController.inst.GetRandomSkill();
        //        ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 30);
        //    }
        //    else
        //    { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 100); SoundController.inst.UISound(5); }
        //}
    }

    public void StartBuySkill()
    { cor = StartCoroutine(BuySkillCor()); }
    public void StopBuySkill()
    { StopCoroutine(cor); }

    IEnumerator BuySkillCor()
    {
        bool flag = true;
        int count = 0;
        while(flag)
        {
            if(DataController.inst.ruby > 1)
            {
                SoundController.inst.UISound(6);
                DataController.inst.PrayCount++;
                DataController.inst.C_PaidRuby++;
                DataController.inst.C_BlessTen++;
                MainSceneController.inst.UpdateRuby(-2);
                SkillController.inst.GetRandomSkill();
                if (count < 10)
                { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 0); yield return DataController.inst.quaterSec; }
                else if (count < 30)
                { yield return DataController.inst.zeroSec; }
                else
                { yield return DataController.inst.zeroQuaterSec; }
            }
            else
            { flag = false; ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
            count++;
        }
    }
}
