using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    public int id, level, at, hp, cri, avoid, gold, luck, numb, upgradeRate;
    public string itemKind, rank;

    public bool isPurchased;

    public Image itemImage, lockImage;
    public Text rankText, levelText, costText;


    public void ButtonClick()
    {
        if (!isPurchased)
        { ItemSlotController.inst.lockPop.SetActive(true); }
        else
        {
            if (numb > -1)
            { ItemSlotController.inst.ItemPopUpdateUI(this); }
        }
    }

    public void Change(EquipSlot equip, bool basic = false, bool putOff = false)
    {
        if (basic)
        {
            if (putOff)
            {
                DataController.inst.invenEmptyCount++;
                //PlayerController.inst.SetItemAbility(at, hp, cri, avoid);

                itemKind = equip.itemKind;
                rank = equip.rank;
                numb = equip.numb;

                at = equip.at;
                hp = equip.hp;
                cri = equip.cri;
                avoid = equip.avoid;
                luck = equip.luck;
                gold = equip.gold;
                level = equip.level;
                upgradeRate = equip.upgradeRate;

                equip.rank = "";
                equip.numb = -1;

                equip.at = 0;
                equip.hp = 0;
                equip.cri = 0;
                equip.avoid = 0;
                equip.luck = 0;
                equip.gold = 0;
                equip.level = 0;

                equip.upgradeRate = 100;

                PlayerController.inst.SetItemAbility(-at, -hp, -cri, -avoid);
                ItemSlotController.inst.FindEmptySlot();
                UpdateUI();
                equip.UpdateUI();
                MainSceneController.inst.UpdateCharacterInfo();
                DataController.inst.SaveInven(this);
                DataController.inst.SaveEquip(equip);
                DataController.inst.es3.Sync();
            }
            else
            {
                DataController.inst.invenEmptyCount++;
                //PlayerController.inst.SetItemAbility(at, hp, cri, avoid);
                itemKind = "";
                rank = "";
                numb = -1;

                at = 0;
                hp = 0;
                cri = 0;
                avoid = 0;
                luck = 0;
                gold = 0;
                level = 0;

                upgradeRate = 100;
                UpdateUI();
                DataController.inst.SaveInven(this);
            }
        }
        else
        {
            string tmpKind = itemKind, tmpRank = rank;
            int tmpNumb = numb, tmpAt = at, tmpHp = hp, tmpCri = cri, tmpAvoid = avoid, tmpGold = gold, tmpLuck = luck, tmpRate = upgradeRate, tmpLevel = level;

            if (!numb.Equals(-1) && equip.numb.Equals(-1))
            { DataController.inst.invenEmptyCount++; }
            else if (numb.Equals(-1) && !equip.numb.Equals(-1))
            { DataController.inst.invenEmptyCount--; }

            if (!numb.Equals(-1) && !equip.numb.Equals(-1))
            { itemKind = equip.itemKind; }
            rank = equip.rank;
            numb = equip.numb;
            level = equip.level;

            at = equip.at;
            hp = equip.hp;
            cri = equip.cri;
            avoid = equip.avoid;
            luck = equip.luck;
            gold = equip.gold;

            upgradeRate = equip.upgradeRate;

            equip.itemKind = tmpKind;
            equip.rank = tmpRank;
            equip.numb = tmpNumb;
            equip.level = tmpLevel;

            equip.at = tmpAt;
            equip.hp = tmpHp;
            equip.cri = tmpCri;
            equip.avoid = tmpAvoid;
            equip.luck = tmpLuck;
            equip.gold = tmpGold;

            equip.upgradeRate = tmpRate;

            PlayerController.inst.SetItemAbility(tmpAt - at, tmpHp - hp, tmpCri - cri, tmpAvoid - avoid);

            ItemSlotController.inst.FindEmptySlot();
            UpdateUI();
            equip.UpdateUI();
            MainSceneController.inst.UpdateCharacterInfo();
            DataController.inst.SaveInven(this);
            DataController.inst.SaveEquip(equip);
            DataController.inst.es3.Sync();
        }
    }
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
            if (rank.Equals("God"))
            {
                if (level < 100)
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

                if (rank.Equals("God"))
                { upgradeRate -= 20; }
                else
                { upgradeRate -= 7; }

                if (upgradeRate < 1)
                { upgradeRate = 1; }
                SimpleSign.inst.SimpleSignSet(Language.inst.strArray[120], 0);
                UpdateAbility(r);
                DataController.inst.SaveInven(this);
            }
            else
            {
                if (!upgradeRate.Equals(100))
                { upgradeRate += 1; }
                ItemSlotController.inst.rateText.text = string.Format("{0}%", upgradeRate.ToString());
                SimpleSign.inst.SimpleSignSet(Language.inst.strArray[121], 0);
                DataController.inst.SaveInven(this);
            }
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); }
    }


    public void UpdateAbility(int r)
    {
        ItemSlotController.inst.rateText.text = string.Format("{0}%", upgradeRate.ToString());
        ItemSlotController.inst.costText.text = NumbToData.GetInt((level + 1) * 100 * r * r);

        NumbToData.GetAbilityStr(this, ItemSlotController.inst.abilityText);
        levelText.text = string.Format("+{0}", level.ToString());
        ItemSlotController.inst.levelText.text = levelText.text;
    }

    public void UpdateUI()
    {
        if (isPurchased)
        {
            lockImage.gameObject.SetActive(false);
            rankText.color = NumbToData.RankColor(rank);
            rankText.text = rank;
            if (level > 0)
            { levelText.text = string.Format("+{0}", level.ToString()); }
            else
            { levelText.text = string.Empty; }

            if (numb > -1)
            {
                itemImage.color = Color.white;
                if (itemKind.Equals("weapon"))
                { itemImage.sprite = ImageController.inst.weapons[numb]; }
                else if (itemKind.Equals("shield"))
                { itemImage.sprite = ImageController.inst.shields[numb]; }
                else if (itemKind.Equals("helmet"))
                { itemImage.sprite = ImageController.inst.helmets[numb]; }
                else if (itemKind.Equals("amor"))
                { itemImage.sprite = ImageController.inst.amors[numb]; }
                else if (itemKind.Equals("cape"))
                { itemImage.sprite = ImageController.inst.capes[numb]; }
                else if (itemKind.Equals("acc"))
                { itemImage.sprite = ImageController.inst.acc[numb]; }
                else if (itemKind.Equals("helper"))
                { itemImage.sprite = ImageController.inst.helpers[numb]; }
                else if (itemKind.Equals("back"))
                { itemImage.sprite = ImageController.inst.backs[numb]; }
                else if (itemKind.Equals("stone"))
                { itemImage.sprite = ImageController.inst.stones[numb]; }
            }
            else
            { itemImage.color = Color.clear; }
        }
        else
        {
            rankText.text = string.Empty;
            levelText.text = string.Empty;
            itemImage.color = Color.clear;
            lockImage.gameObject.SetActive(true);
        }
    }
}
