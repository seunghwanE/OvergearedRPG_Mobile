using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public int id, at, hp, cri, avoid, gold, luck, numb, level, upgradeRate;
    public string itemKind, rank;

    public Image itemImage;
    public Text rankText, levelText;


    public void ButtonClick()
    {
        if (numb > -1)
        {
            ItemSlotController.inst.EquipItemPopUpdateUI(this);
        }
    }

    //public void SetAbilityAdd()
    //{
    //    PlayerController.inst.SetItemAbility(at, hp, cri, avoid);
    //}

    public void Upgrade()
    {
        int r = NumbToData.RankToInt(rank);
        if (r < 4)
        {
            if (level < 10)
            { UpgradeDetail(r); }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[119], 20); SoundController.inst.UISound(5); }
        }
        else if (r < 7)
        {
            if (level < 20)
            { UpgradeDetail(r); }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[119], 20); SoundController.inst.UISound(5); }
        }
        else if (r < 10)
        {
            if (level < 30)
            { UpgradeDetail(r); }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[119], 20); SoundController.inst.UISound(5); }
        }
        else
        {
            if (level < 40)
            { UpgradeDetail(r); }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[119], 20); SoundController.inst.UISound(5); }
        }
    }
    public void UpgradeDetail(int r)
    {
        int cost = (level + 1) * 100 * r * r;
        if (DataController.inst.gold >= cost)
        {
            DataController.inst.C_PaidGold++;
            MainSceneController.inst.UpdateGold(-cost);
            float random = Random.Range(0f, 100f);
            if (random <= upgradeRate)
            {
                MainSceneController.inst.UpgradeObjSet();
                SoundController.inst.UISound(11);
                DataController.inst.C_Upgrade++;
                level++;
                int tmpAt = at, tmpHp = hp, tmpCri = cri, tmpAvoid = avoid, tmpGold = gold, tmpLuck = luck;
                
                at = (int)(at * 1.1f);
                hp = (int)(hp * 1.1f);
                if (!cri.Equals(0))
                { cri = (int)(cri * 1.1f) + 1; }
                if (!avoid.Equals(0))
                { avoid = (int)(avoid * 1.1f) + 1; }
                if (!gold.Equals(0))
                { gold = (int)(gold * 1.1f) + 1; }
                if (!luck.Equals(0))
                { luck = (int)(luck * 1.1f) + 1; }
                upgradeRate -= 7;
                if (upgradeRate < 1)
                { upgradeRate = 1; }
                SimpleSign.inst.SimpleSignSet(Language.inst.strArray[120], 0);

                DataController.inst.SaveEquip(this);
                PlayerController.inst.SetItemAbility(at - tmpAt, hp - tmpHp, cri - tmpCri, avoid - tmpAvoid);
                
                UpdateAbility(r);
            }
            else
            {
                if (!upgradeRate.Equals(100))
                { upgradeRate += 1; DataController.inst.SaveEquip(this); }
                
                ItemSlotController.inst.rateText.text = string.Format("{0}%", upgradeRate.ToString());
                SimpleSign.inst.SimpleSignSet(Language.inst.strArray[121], 0);
            }
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); }
    }


    public void UpdateAbility(int r)
    {
        ItemSlotController.inst.rateText.text = string.Format("{0}%", upgradeRate.ToString());
        ItemSlotController.inst.costText.text = NumbToData.GetInt((level + 1) * 100 * r * r);

        NumbToData.GetEquipAbilityStr(this, ItemSlotController.inst.abilityText);
        levelText.text = string.Format("+{0}", level.ToString());
        ItemSlotController.inst.levelText.text = levelText.text;
    }

    public void UpdateUI()
    {
        if (level > 0)
        { levelText.text = string.Format("+{0}", level.ToString()); }
        else
        { levelText.text = string.Empty; }

        if (numb > -1)
        {
            rankText.color = NumbToData.RankColor(rank);
            rankText.text = rank;
            itemImage.color = Color.white;
            if (itemKind.Equals("weapon"))
            { itemImage.sprite = ImageController.inst.weapons[numb]; MainSceneController.inst.SetCharacterWeapon(numb); }
            else if (itemKind.Equals("shield"))
            { itemImage.sprite = ImageController.inst.shields[numb]; MainSceneController.inst.SetCharacterShield(numb); }
            else if (itemKind.Equals("helmet"))
            { itemImage.sprite = ImageController.inst.helmets[numb]; MainSceneController.inst.SetCharacterHelmet(numb); }
            else if (itemKind.Equals("amor"))
            { itemImage.sprite = ImageController.inst.amors[numb]; MainSceneController.inst.SetCharacterAmor(numb); }
            else if (itemKind.Equals("cape"))
            { itemImage.sprite = ImageController.inst.capes[numb]; MainSceneController.inst.SetCharacterCape(numb); }
            else if (itemKind.Equals("acc"))
            { itemImage.sprite = ImageController.inst.acc[numb]; }
            else if (itemKind.Equals("helper"))
            { itemImage.sprite = ImageController.inst.helpers[numb]; MainSceneController.inst.SetCharacterHelper(numb); }
            else if (itemKind.Equals("back"))
            { itemImage.sprite = ImageController.inst.backs[numb]; MainSceneController.inst.SetCharacterBack(numb); }
            else if (itemKind.Equals("stone"))
            { itemImage.sprite = ImageController.inst.stones[numb]; }
        }
        else
        {
            itemImage.color = Color.clear;
            rankText.text = string.Empty;
            if (itemKind.Equals("weapon"))
            { MainSceneController.inst.SetCharacterWeapon(numb); }
            else if (itemKind.Equals("shield"))
            { MainSceneController.inst.SetCharacterShield(numb); }
            else if (itemKind.Equals("helmet"))
            { MainSceneController.inst.SetCharacterHelmet(numb); }
            else if (itemKind.Equals("amor"))
            { MainSceneController.inst.SetCharacterAmor(numb); }
            else if (itemKind.Equals("cape"))
            { MainSceneController.inst.SetCharacterCape(numb); }
            else if (itemKind.Equals("helper"))
            { MainSceneController.inst.SetCharacterHelper(numb); }
            else if (itemKind.Equals("back"))
            { MainSceneController.inst.SetCharacterBack(numb); }
        }
    }
}
