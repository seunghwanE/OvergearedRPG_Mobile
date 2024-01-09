using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SmithController : MonoBehaviour{
    public static SmithController inst;

    public GameObject smithEffect, smithEffect2, itemOneResult, itemTenResult;
    public Animator sourceAnim;
    public int goldCost, ironCost, rubyCost, count;
    public SmithButton smithButton;
    public Text goldCostText, ironCostText, rubyCostText, beforeRateText, afterRateText, totalRateText, kindNumbText, abilityText;
    public Button makeButton;
    public GetItem itemOne;
    public GetItem[] itemTen;
    public string myRank;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
    }

    private void Start()
    {
        UpdateRate();
    }

    public void UpdateSmithButton(int count)
    {
        smithButton.count += count;
        DataController.inst.SaveSmithButton(smithButton);
    }

    public void CostUpdate()
    {
        if (smithButton.id < 6)
        {
            goldCost = smithButton.count * 500;
            ironCost = smithButton.count;
            rubyCost = 0;
        }
        else
        {
            goldCost = smithButton.count * 3000;
            ironCost = 10 + smithButton.count;
            rubyCost = 10;
        }
        goldCostText.text = goldCost.ToString("N0");
        rubyCostText.text = rubyCost.ToString("N0");
        ironCostText.text = ironCost.ToString("N0");
    }

    public void SmithMake()
    {
        if (DataController.inst.gold >= goldCost && DataController.inst.ruby >= rubyCost && DataController.inst.iron >= ironCost)
        {
            SoundController.inst.SmithSound();
            DataController.inst.smithTotalCount++;
            DataController.inst.C_SmithTen++;
            DataController.inst.C_PaidGold++;
            if(rubyCost > 0)
            { DataController.inst.C_PaidRuby++; }
            DataController.inst.invenEmptyCount--;
            Invoke("ItemOnePop", 2.85f);
            sourceAnim.SetTrigger("Make_t");
            UpdateSmithButton(1);
            MainSceneController.inst.UpdateGold(-goldCost);
            MainSceneController.inst.UpdateIron(-ironCost);
            MainSceneController.inst.UpdateRuby(-rubyCost);
            NumbToData.GetItem(smithButton.kind, itemOne, smithButton.id, DataController.inst.S_AutoDelete);
            //NumbToData.GetNumbToData(DataController.inst.RateSet(), smithButton.kind, beforeRateText, afterRateText, kindNumbText, itemOne);
            makeButton.interactable = false;
            smithEffect.SetActive(true);
            Invoke("UpdateRate", 5f);
            DataController.inst.SaveSmithButton(smithButton);
        }
        else
        {
            SoundController.inst.UISound(5);
            ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30);
        }
    }

    private void ItemOnePop()
    {
        makeButton.interactable = true;
        InputController.inst.OtherContentsTurnOff(false);
        itemOneResult.SetActive(true);
    }

    public void OneMore()
    {
        count = smithButton.count;
        if (smithButton.id < 6)
        {
            goldCost = count * 500;
            ironCost = count;
            rubyCost = 0;
        }
        else
        {
            goldCost = count * 3000;
            ironCost = count + 10;
            rubyCost = 10;
        }

        if (DataController.inst.gold >= goldCost && DataController.inst.ruby >= rubyCost && DataController.inst.iron >= ironCost)
        {
            if (DataController.inst.invenEmptyCount > 0)
            {
                DataController.inst.smithTotalCount++;
                DataController.inst.C_PaidGold++;
                DataController.inst.C_SmithTen++;
                DataController.inst.invenEmptyCount--;
                if (rubyCost > 0)
                { DataController.inst.C_PaidRuby++; }
                smithEffect2.SetActive(true);
                itemTenResult.SetActive(false);
                itemOneResult.SetActive(false);
                itemOneResult.SetActive(true);
                UpdateSmithButton(1);
                MainSceneController.inst.UpdateGold(-goldCost);
                MainSceneController.inst.UpdateIron(-ironCost);
                MainSceneController.inst.UpdateRuby(-rubyCost);
                NumbToData.GetItem(smithButton.kind, itemOne, smithButton.id, DataController.inst.S_AutoDelete);
                //NumbToData.GetNumbToData(DataController.inst.RateSet(), smithButton.kind, beforeRateText, afterRateText, kindNumbText, itemOne);
                DataController.inst.SaveSmithButton(smithButton);
                Invoke("UpdateRate", 5f);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[53], 30); SoundController.inst.UISound(5); }
        }
        else
        {
            SoundController.inst.UISound(5);
            ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30);
        }
    }
    public void TenMore()
    {
        if (DataController.inst.invenEmptyCount > 9)
        {
            count = smithButton.count;
            goldCost = 0;
            ironCost = 0;
            rubyCost = 0;

            for (int i = 0; i < 10; i++)
            {
                if (smithButton.id < 6)
                {
                    goldCost += count * 500;
                    ironCost += count;
                }
                else
                {
                    goldCost += count * 3000;
                    ironCost += count + 10;
                    rubyCost += 10;
                }
                count++;
            }

            if (DataController.inst.gold >= goldCost && DataController.inst.ruby >= rubyCost && DataController.inst.iron >= ironCost)
            {
                DataController.inst.smithTotalCount += 10;
                DataController.inst.C_SmithTen += 10;
                DataController.inst.C_PaidGold++;
                DataController.inst.invenEmptyCount -= 10;
                if (rubyCost > 0)
                { DataController.inst.C_PaidRuby++; }
                smithEffect2.SetActive(true);
                itemOneResult.SetActive(false);
                itemTenResult.SetActive(false);
                itemTenResult.SetActive(true);
                for (int i = 0; i < 10; i++)
                { NumbToData.GetItem(smithButton.kind, itemTen[i], smithButton.id, DataController.inst.S_AutoDelete); }
                //{ NumbToData.GetNumbToData(DataController.inst.RateSet(), smithButton.kind, beforeRateText, afterRateText, kindNumbText, itemTen[i]); }
                UpdateSmithButton(10);
                MainSceneController.inst.UpdateGold(-goldCost);
                MainSceneController.inst.UpdateIron(-ironCost);
                MainSceneController.inst.UpdateRuby(-rubyCost);
                DataController.inst.SaveSmithButton(smithButton);
                UpdateRate();
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 30); SoundController.inst.UISound(5); }
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[53], 30); SoundController.inst.UISound(5); }
    }

    public void UpdateRate()
    {
        totalRateText.text = string.Format("Ex\t:\t{0:0.000}%\t\tSSS\t:\t{1:0.000}%\nSS\t:\t{2:0.000}%\t\t\tS\t:\t{3:0.000}%\nA\t:\t{4:0.000}%\t\t\tB\t:\t{5:0.000}%" +
            "\nC\t:\t{6:0.000}%\t\t\t\tD\t:\t{7:0.000}%\nE\t:\t{8:0.000}%\t\t\t\tF\t:\t{9:0.000}%",
            DataController.inst.rateEx, DataController.inst.rateSSS, DataController.inst.rateSS, DataController.inst.rateS, DataController.inst.rateA, DataController.inst.rateB,
            DataController.inst.rateC, DataController.inst.rateD, DataController.inst.rateE, DataController.inst.rateF);
    }

}
