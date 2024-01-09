using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelAward : MonoBehaviour
{
    public int id, maxValue, kind, count;
    public string rank;
    public bool isVip, isGet;
    public Text awardText, countText, emptyText;
    public Image awardImage;
    public GameObject checkObj, lockObj, pointObj;
    public Button getButton;
    public RectTransform _element;

    
    public void SetNow()
    {
        lockObj.SetActive(false);
        if (!isGet)
        { getButton.gameObject.SetActive(true); }
    }
    public void SetNext()
    {
        checkObj.SetActive(true);
        getButton.gameObject.SetActive(false);
        if (!id.Equals(29))
        { AwardController.inst.nowAward = AwardController.inst.awards[id + 1]; }
        AwardController.inst.vipAwards[AwardController.inst.nowAward.id].SetNow();
        AwardController.inst.nowAward.SetNow();
        if (id > 1 && id < 27)
        { AwardController.inst.StartPosMoveCor(id); }
    }

    public void SetVip()
    {
        checkObj.SetActive(true);
        getButton.gameObject.SetActive(false);
    }

    public void ClickButton()
    {
        if (!isGet)
        {
            if (PlayerController.inst.totalPower >= maxValue)
            {
                if (kind < 8)
                {
                    if (kind.Equals(0))
                    { GetItem("weapon"); }
                    else if (kind.Equals(1))
                    { GetItem("shield"); }
                    else if (kind.Equals(2))
                    { GetItem("helmet"); }
                    else if (kind.Equals(3))
                    { GetItem("amor"); }
                    else if (kind.Equals(4))
                    { GetItem("acc"); }
                    else if (kind.Equals(5))
                    { GetItem("helper"); }
                    else if (kind.Equals(6))
                    { GetItem("back"); }
                    else if (kind.Equals(7))
                    { GetItem("stone"); }
                }
                else
                { GetAward(); }

                DataController.inst.SaveAward(this);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[77], 30); SoundController.inst.UISound(5); }
        }
    }

    public void GetAward()
    {
        SoundController.inst.UISound(6);
        if (kind.Equals(8))
        { MainSceneController.inst.UpdateGold(count); }
        else if (kind.Equals(9))
        { MainSceneController.inst.UpdateIron(count); }
        else if (kind.Equals(10))
        { MainSceneController.inst.UpdateRuby(count); }
        
        SimpleSign.inst.SimpleSignSet(Language.inst.strArray[76], 0);
        UpdateLoginButton(true);
    }

    public void GetItem(string _kind)
    {
        if (DataController.inst.invenEmptyCount > 0)
        {
            SoundController.inst.UISound(6);
            NumbToData.FreeGetItem(AwardController.inst.levelAwardGetItemPop, _kind, rank);
            AwardController.inst.levelAwardGetItemPop.gameObject.SetActive(true);
            UpdateLoginButton(true);
        }
        else
        { UpdateLoginButton(false); }
    }
    public void UpdateLoginButton(bool get)
    {
        if (get)
        {
            isGet = true;
            DataController.inst.SaveAward(this);
            UpdateUI();
            if(isVip)
            { SetVip(); }
            else
            { SetNext(); }
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[53], 30); SoundController.inst.UISound(5); }
    }

    public void UpdateUI()
    {
        if (kind < 8)
        {
            if (kind.Equals(0))
            { awardImage.sprite = AwardController.inst.weaponSprite; }
            else if (kind.Equals(1))
            { awardImage.sprite = AwardController.inst.shieldSprite; }
            else if (kind.Equals(2))
            { awardImage.sprite = AwardController.inst.helmetSprite; }
            else if (kind.Equals(3))
            { awardImage.sprite = AwardController.inst.amorSprite; }
            else if (kind.Equals(4))
            { awardImage.sprite = AwardController.inst.accSprite; }
            else if (kind.Equals(5))
            { awardImage.sprite = AwardController.inst.helperSprite; }
            else if (kind.Equals(6))
            { awardImage.sprite = AwardController.inst.backSprite; }
            else if (kind.Equals(7))
            { awardImage.sprite = AwardController.inst.stoneSprite; }
            awardText.text = string.Format("{0}", rank);
            awardText.color = NumbToData.RankColor(rank);
        }
        else
        {
            if (kind.Equals(8))
            { awardImage.sprite = AwardController.inst.goldSprite; }
            else if (kind.Equals(9))
            { awardImage.sprite = AwardController.inst.ironSprite; }
            else if (kind.Equals(10))
            { awardImage.sprite = AwardController.inst.rubySprite; }
            else if (kind.Equals(11))
            { awardImage.sprite = AwardController.inst.statSprite; }
            awardText.text = string.Format("+{0}", NumbToData.GetInt(count));
        }

        if (!isVip)
        {
            countText.text = maxValue.ToString();

            if (isGet)
            { checkObj.SetActive(true); getButton.gameObject.SetActive(false); }
            else
            { lockObj.SetActive(true); }
        }
        else
        {
            if (!id.Equals(0))
            {
                if (AwardController.inst.awards[id - 1].isGet)
                {
                    lockObj.SetActive(false);
                    if (!isGet)
                    { getButton.gameObject.SetActive(true); }
                    else
                    { getButton.gameObject.SetActive(false); }
                }
                else
                { lockObj.SetActive(true); }
            }
        }
    }
}
