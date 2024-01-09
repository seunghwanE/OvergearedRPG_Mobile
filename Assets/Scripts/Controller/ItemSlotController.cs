using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour
{
    public static ItemSlotController inst;

    public EquipSlot[] equipSlots;
    public InvenSlot lockSlot, emptySlot;
    public InvenSlot[] invenSlots;
    public GameObject itemPop, deletePop, lockPop;
    public Text rankText, abilityText, levelText, rateText, costText, kindText, equipButtonText, deleteIronText, deleteRubyText, lockCostText;
    public Image itemImage;
    public Button equipButton, upgradeButton, DeleteButton;

    [HideInInspector]
    public InvenSlot selectInvenSlot;
    [HideInInspector]
    public EquipSlot selectEquipSlot;

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
        for(int i =0; i < invenSlots.Length; i++)
        {
            DataController.inst.LoadInven(invenSlots[i]);
            invenSlots[i].UpdateUI();
        }

        for(int i =0; i < equipSlots.Length; i++)
        {
            DataController.inst.LoadEquip(equipSlots[i]);
            equipSlots[i].UpdateUI();
            //equipSlots[i].SetAbilityAdd();
        }

        FindEmptySlot();
        FindLockSlot();
    }


    public void DeletePop()
    {
        if (selectInvenSlot == null)
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[122], 20); SoundController.inst.UISound(5); }
        else
        { ItemDelete(); SoundController.inst.UISound(7); }
    }

    public void AllDelete()
    {
        int tmpIron = 0, tmpRuby = 0, rank = 0;

        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (!invenSlots[i].isPurchased)
            { break; }
            if (!invenSlots[i].numb.Equals(-1))
            {
                selectInvenSlot = invenSlots[i];
                rank = NumbToData.RankToInt(selectInvenSlot.rank);
                tmpIron += rank;
                tmpRuby += (int)(rank * 0.5f);
                ItemDelete();
            }
        }
        deleteIronText.text = string.Format("+{0}", tmpIron);
        deleteRubyText.text = string.Format("+{0}", tmpRuby);
    }

    public void ItemDelete()
    {
        int rank = NumbToData.RankToInt(selectInvenSlot.rank);
        
        selectInvenSlot.Change(equipSlots[0], true);
        MainSceneController.inst.UpdateIron(rank);
        MainSceneController.inst.UpdateRuby((int)(rank * 0.5f));
        deleteIronText.text = string.Format("+{0}", rank);
        deleteRubyText.text = string.Format("+{0}", (int)(rank* 0.5f));
        itemPop.SetActive(false);
        deletePop.SetActive(true);
        FindEmptySlot();
    }
    public void AutoDelete(string _rank)
    {
        int ranktoNumb = NumbToData.RankToInt(_rank);
        selectInvenSlot.Change(equipSlots[0], true);
        MainSceneController.inst.UpdateIron(ranktoNumb);
        MainSceneController.inst.UpdateRuby((int)(ranktoNumb * 0.5f));
    }
    public void ItemPopUpdateUI(InvenSlot item)
    {
        SoundController.inst.UISound(9);
        selectEquipSlot = null;
        selectInvenSlot = item;
        int rank = NumbToData.RankToInt(item.rank);
        rankText.text = item.rank;
        rankText.color = item.rankText.color;
        rateText.text = string.Format("{0}%", item.upgradeRate.ToString());
        costText.text = NumbToData.GetInt((item.level + 1) * 100 * rank * rank);
        kindText.text = item.itemKind;
        itemImage.sprite = item.itemImage.sprite;

        NumbToData.GetAbilityStr(item, abilityText);
        levelText.text = item.levelText.text;
        equipButtonText.text = Language.inst.strArray[8];

        itemPop.SetActive(true);
    }
    public void EquipItemPopUpdateUI(EquipSlot item)
    {
        SoundController.inst.UISound(9);
        selectInvenSlot = null;
        selectEquipSlot = item;
        int rank = NumbToData.RankToInt(item.rank);
        rankText.text = item.rank;
        rankText.color = item.rankText.color;
        rateText.text = string.Format("{0}%", item.upgradeRate.ToString());
        costText.text = NumbToData.GetInt((item.level+ 1) * 100 * rank * rank);
        kindText.text = item.itemKind;
        itemImage.sprite = item.itemImage.sprite;

        NumbToData.GetEquipAbilityStr(item, abilityText);
        levelText.text = item.levelText.text;
        equipButtonText.text = Language.inst.strArray[49];

        itemPop.SetActive(true);
    }

    public void EquipButtonClick()
    {
        SoundController.inst.UISound(9);
        if (selectInvenSlot == null)
        { EquipOff(); }
        else
        { Equip(); }
    }
    public void Upgrade()
    {
        if (selectInvenSlot == null)
        { selectEquipSlot.Upgrade(); }
        else
        { selectInvenSlot.Upgrade(); }
    }

    public void Equip()
    {
        if (selectInvenSlot.itemKind.Equals("weapon"))
        { selectInvenSlot.Change(equipSlots[0]); PlayerController.inst.weaponId = equipSlots[0].numb; }
        else if (selectInvenSlot.itemKind.Equals("shield"))
        { selectInvenSlot.Change(equipSlots[1]); PlayerController.inst.shieldId = equipSlots[1].numb; }
        else if (selectInvenSlot.itemKind.Equals("helmet"))
        { selectInvenSlot.Change(equipSlots[2]); PlayerController.inst.helmetId = equipSlots[2].numb; }
        else if (selectInvenSlot.itemKind.Equals("amor"))
        { selectInvenSlot.Change(equipSlots[3]); PlayerController.inst.amorId = equipSlots[3].numb; }
        else if (selectInvenSlot.itemKind.Equals("cape"))
        { selectInvenSlot.Change(equipSlots[4]); PlayerController.inst.capeId = equipSlots[4].numb; }
        else if (selectInvenSlot.itemKind.Equals("acc"))
        { selectInvenSlot.Change(equipSlots[5]); }
        else if (selectInvenSlot.itemKind.Equals("helper"))
        { selectInvenSlot.Change(equipSlots[6]); PlayerController.inst.helpId = equipSlots[6].numb; }
        else if (selectInvenSlot.itemKind.Equals("back"))
        { selectInvenSlot.Change(equipSlots[7]); PlayerController.inst.backId = equipSlots[7].numb; }
        else if (selectInvenSlot.itemKind.Equals("stone"))
        { selectInvenSlot.Change(equipSlots[8]); }
        itemPop.SetActive(false);
        //else
    }

    public void EquipOff()
    {
        if (DataController.inst.invenEmptyCount > 0)
        { emptySlot.Change(selectEquipSlot, true, true); itemPop.SetActive(false); SoundController.inst.UISound(3); }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[53], 20); SoundController.inst.UISound(5); }
    }

    public void BuyLock()
    {
        int cost = DataController.inst.buyLockCount * 25 + 100;
        if (DataController.inst.ruby >= cost)
        {
            SoundController.inst.UISound(2);
            DataController.inst.C_PaidRuby++;
            MainSceneController.inst.UpdateRuby(-cost);
            LockOpen();
            lockPop.SetActive(false);
            SimpleSign.inst.SimpleSignSet(Language.inst.strArray[55], 0);
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); SoundController.inst.UISound(5); }
        FindEmptySlot();
    }

    public void LockOpen()
    {
        DataController.inst.invenEmptyCount++;
        lockSlot.isPurchased = true;
        DataController.inst.SaveInven(lockSlot);
        DataController.inst.buyLockCount++;
        lockSlot.lockImage.gameObject.SetActive(false);
        int id = lockSlot.id;
        if (id < 24)
        {
            lockSlot = invenSlots[id + 1];
            lockSlot.costText.text = (DataController.inst.buyLockCount * 25 + 100).ToString("N0");
            lockCostText.text = lockSlot.costText.text;
            lockSlot.costText.gameObject.SetActive(true);
        }
    }
    public void FindLockSlot()
    {
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (!invenSlots[i].isPurchased)
            {
                invenSlots[i].costText.text = (DataController.inst.buyLockCount * 25 + 100).ToString("N0");
                lockCostText.text = invenSlots[i].costText.text;
                invenSlots[i].costText.gameObject.SetActive(true);
                lockSlot = invenSlots[i];
                break;
            }
        }
    }

    public void FindEmptySlot()
    {
        bool checker = false;
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (invenSlots[i].isPurchased)
            {
                if (invenSlots[i].numb.Equals(-1))
                {
                    emptySlot = invenSlots[i];
                    checker = true;
                    break;
                }
            } 
        }
        if (!checker)
        {
            emptySlot = null;
            DataController.inst.invenEmptyCount = 0;
        }
    }

    //public void SetItemSlot(string kind, bool isPurchased, int numb, Sprite itemImage, ItemSlot itemSlot)
    //{
    //    itemSlot.kind = kind;
    //    itemSlot.isPurchased = isPurchased;
    //    itemSlot.rank = NumbToData.GetRank(kind, numb, itemSlot.rankText);
    //    if(numb > -1)
    //    {
    //        itemSlot.itemImage.gameObject.SetActive(true);
    //        GetAbility(itemSlot);
    //    }
    //    else
    //    { itemSlot.itemImage.gameObject.SetActive(false); }
    //    itemSlot.itemImage.sprite = itemImage;
    //    itemSlot.numb = numb;
    //    DataController.inst.SaveItemSlot(itemSlot);
    //}
    ////at

    //public void GetSkill(int level, ItemSlot item)
    //{
    //    item.skillLevel = level - 4;
    //    int r = Random.Range(0, 100);
    //    if(r > 65)
    //    {
    //        item.skillLevel += Random.Range(-1, 2);
    //        if(item.skillLevel < 1)
    //        { item.skillLevel = 1; }
    //    }
    //    item.skillKind = Random.Range(0, 30);
    //}

    //public int RankToInt(string rank)
    //{
    //    if(rank.Equals("F"))
    //    { return 1; }
    //    else if (rank.Equals("E"))
    //    { return 2; }
    //    else if (rank.Equals("D"))
    //    { return 3; }
    //    else if (rank.Equals("C"))
    //    { return 4; }
    //    else if (rank.Equals("B"))
    //    { return 5; }
    //    else if (rank.Equals("A"))
    //    { return 6; }
    //    else if (rank.Equals("S"))
    //    { return 7; }
    //    else if (rank.Equals("SS"))
    //    { return 8; }
    //    else if (rank.Equals("SSS"))
    //    { return 9; }
    //    else if (rank.Equals("Ex"))
    //    { return 10; }
    //    else
    //    { return 1; }
    //}
}
