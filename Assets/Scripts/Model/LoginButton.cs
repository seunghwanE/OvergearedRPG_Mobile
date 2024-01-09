using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    public bool isGet;
    public int id, kind, count;
    public string rank;
    public GameObject checkMark, arrow;
    //public LoginPop getPop;

    public Image itemImage;
    public Text itemText;
    public Button thisButton;

    public void GetLogin()
    {
        if (!isGet)
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
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[103], 30); SoundController.inst.UISound(5); }
    }
    public void UpdateLoginButton(bool get)
    {
        if (get)
        {
            SoundController.inst.UISound(6);
            DataController.inst.loginCheck = false;
            isGet = true;
            DataController.inst.SaveLogin(this);
            UpdateUI();
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[53], 30); SoundController.inst.UISound(5); }
    }

    public void GetAward()
    {
        if (kind.Equals(8))
        { MainSceneController.inst.UpdateGold(count); }
        else if (kind.Equals(9))
        { MainSceneController.inst.UpdateIron(count); }
        else if (kind.Equals(10))
        { MainSceneController.inst.UpdateRuby(count); }
        else if (kind.Equals(11))
        { PlayerController.inst.statPoint += count; DataController.inst.SavePlayer(PlayerController.inst); }
        SimpleSign.inst.SimpleSignSet(Language.inst.strArray[76], 0);
        UpdateLoginButton(true);
    }

    public void GetItem(string _kind)
    {
        if (DataController.inst.invenEmptyCount > 0)
        {
            SoundController.inst.UISound(6);
            NumbToData.FreeGetItem(AwardController.inst.loginGetItemPop, _kind, rank);
            AwardController.inst.loginGetItemPop.gameObject.SetActive(true);
            UpdateLoginButton(true);
        }
        else
        { UpdateLoginButton(false); SoundController.inst.UISound(5); }
    }

    public void UpdateUI()
    {
        if (kind < 8)
        {
            if (kind.Equals(0))
            { itemImage.sprite = AwardController.inst.weaponSprite; }
            else if (kind.Equals(1))
            { itemImage.sprite = AwardController.inst.shieldSprite; }
            else if (kind.Equals(2))
            { itemImage.sprite = AwardController.inst.helmetSprite; }
            else if (kind.Equals(3))
            { itemImage.sprite = AwardController.inst.amorSprite; }
            else if (kind.Equals(4))
            { itemImage.sprite = AwardController.inst.accSprite; }
            else if (kind.Equals(5))
            { itemImage.sprite = AwardController.inst.helperSprite; }
            else if (kind.Equals(6))
            { itemImage.sprite = AwardController.inst.backSprite; }
            else if (kind.Equals(7))
            { itemImage.sprite = AwardController.inst.stoneSprite; }
            itemText.text = string.Format("{0}", rank);
            itemText.color = NumbToData.RankColor(rank);
        }
        else
        {
            if (kind.Equals(8))
            { itemImage.sprite = AwardController.inst.goldSprite; }
            else if (kind.Equals(9))
            { itemImage.sprite = AwardController.inst.ironSprite; }
            else if (kind.Equals(10))
            { itemImage.sprite = AwardController.inst.rubySprite; }
            else if (kind.Equals(11))
            { itemImage.sprite = AwardController.inst.statSprite; }
            itemText.text = string.Format("+{0}", NumbToData.GetInt(count));
        }

        if (isGet)
        { checkMark.SetActive(true); }
    }
    public void TodayUpdateUI()
    {
        arrow.SetActive(true);
        itemImage.color = Color.white;
        thisButton.interactable = true;
    }

}
