using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour{
    public static SkillController inst;

    public SkillEquip[] equipSkills;
    public SkillButton[] activeSkills, passiveSkills;
    public SkillSlot[] skillSlots;
    public SkillButton selectSkill;
    public GameObject lockPop, sellPop, equipPop;
    
    public Image skillImage;
    public Slider slider;
    public Text contentsText, levelText, titleText, costText, sliderText, goldText, ironText;
    public Button equipButton, upButton, sellButton;
    public int cost;

    public List<SkillButton> activePurchasedList, passivePurchasedList, activeEmptyList, passiveEmptyList;

    public Toggle OnOff;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        slider.minValue = 0;
        activePurchasedList = new List<SkillButton>();
        passivePurchasedList = new List<SkillButton>();
        activeEmptyList = new List<SkillButton>();
        passiveEmptyList = new List<SkillButton>();
    }

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            DataController.inst.LoadSkill(activeSkills[i]);
            if(activeSkills[i].isPurchased)
            {
                if(activeSkills[i].numb.Equals(-1))
                { activeEmptyList.Add(activeSkills[i]); }
                else
                { activePurchasedList.Add(activeSkills[i]); }
            }
            activeSkills[i].UpdateUI();
        }
        for(int i= 0; i < 10; i++)
        {
            DataController.inst.LoadSkill(passiveSkills[i]);
            if (passiveSkills[i].isPurchased)
            {
                if (passiveSkills[i].numb.Equals(-1))
                { passiveEmptyList.Add(passiveSkills[i]); }
                else
                { passivePurchasedList.Add(passiveSkills[i]); }
            }
            passiveSkills[i].PassiveSkillAdd();
            passiveSkills[i].UpdateUI();
        }
        for(int i = 0; i < 5; i++)
        {
            DataController.inst.LoadSkillSlot(skillSlots[i]);
            skillSlots[i].UpdateUI();
            equipSkills[i].Set();
        }
        StartSetToggle();
    }

    public void SetToggle()
    {
        if(OnOff.isOn)
        { DataController.inst.S_Combo = true; PlayerController.inst.character.combo = PlayerController.inst.skillCombo; }
        else
        { DataController.inst.S_Combo = false; PlayerController.inst.character.combo = 0; }
        DataController.inst.es3.Sync();
    }

    public void StartSetToggle()
    {
        if (PlayerController.inst.skillCombo > 0)
        {
            if (DataController.inst.S_Combo)
            { OnOff.isOn = true; PlayerController.inst.character.combo = PlayerController.inst.skillCombo; }
            else
            { OnOff.isOn = false; PlayerController.inst.character.combo = 0; }
        }
        else
        { OnOff.gameObject.SetActive(false); }
    }

    public void BuyLock()
    {
        if(DataController.inst.ruby >= cost)
        {
            SoundController.inst.UISound(2);
            DataController.inst.C_PaidRuby++;
            MainSceneController.inst.UpdateRuby(-cost);
            DataController.inst.buySkillLockCount++;
            if(selectSkill.id < 20)
            {
                for(int i = 10; i < 20; i++)
                {
                    if(!activeSkills[i].isPurchased)
                    {
                        selectSkill = activeSkills[i];
                        activeEmptyList.Add(selectSkill);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 5; i < 10; i++)
                {
                    if (!passiveSkills[i].isPurchased)
                    {
                        selectSkill = passiveSkills[i];
                        passiveEmptyList.Add(selectSkill);
                        break;
                    }
                }
            }
            selectSkill.isPurchased = true;
            DataController.inst.SaveSkill(selectSkill);
            selectSkill.lockObj.SetActive(false);
            lockPop.SetActive(false);
            SimpleSign.inst.SimpleSignSet(Language.inst.strArray[55], 0);
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); SoundController.inst.UISound(5); }
    }

    public void GetRandomSkill()
    {
        int r = Random.Range(0, 3);
        SoundController.inst.UISound(10);
        if (r.Equals(0))
        {
            if (passiveEmptyList.Count.Equals(0))
            {
                r = Random.Range(0, passivePurchasedList.Count);
                passivePurchasedList[r].count++;
                selectSkill = passivePurchasedList[r];
            }
            else
            {
                r = Random.Range(20, 30);
                bool check = true;
                for (int i = 0; i < passivePurchasedList.Count; i++)
                {
                    if (passivePurchasedList[i].numb.Equals(r))
                    {
                        passivePurchasedList[i].count++;
                        selectSkill = passivePurchasedList[i];
                        check = false;
                        break;
                    }
                }
                if(check)
                {
                    passiveEmptyList[0].numb = r;
                    selectSkill = passiveEmptyList[0];
                    passivePurchasedList.Add(selectSkill);
                    passiveEmptyList.Remove(selectSkill);
                }
            }
            DataController.inst.SaveSkill(selectSkill);
            selectSkill.PassiveSkillAdd();
            selectSkill.UpdateUI();
        }
        else
        {
            if (activeEmptyList.Count.Equals(0))
            {
                r = Random.Range(0, activePurchasedList.Count);
                activePurchasedList[r].count++;
                selectSkill = activePurchasedList[r];
            }
            else
            {
                r = Random.Range(0, 20);
                bool check = true;
                for (int i = 0; i < activePurchasedList.Count; i++)
                {
                    if (activePurchasedList[i].numb.Equals(r))
                    {
                        activePurchasedList[i].count++;
                        selectSkill = activePurchasedList[i];
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    activeEmptyList[0].numb = r;
                    selectSkill = activeEmptyList[0];
                    activePurchasedList.Add(selectSkill);
                    activeEmptyList.Remove(selectSkill);
                }
            }
            DataController.inst.SaveSkill(selectSkill);
            selectSkill.UpdateUI();
        }
    }

    public void SetLock(SkillButton skill)
    {
        selectSkill = skill;
        cost = (DataController.inst.buySkillLockCount * 50) + 100;
        costText.text = cost.ToString();
        lockPop.SetActive(true);
        SetSkill(selectSkill);
    }
    public void SellSkill()
    {
        if (selectSkill == null)
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[179], 20); SoundController.inst.UISound(5); }
        else if(selectSkill.numb.Equals(-1))
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[179], 20); SoundController.inst.UISound(5); }
        else
        {
            SoundController.inst.UISound(4);
            if (selectSkill.count > 0)
            {
                int iron = selectSkill.count;
                goldText.text = string.Format("+{0}", NumbToData.GetInt(200 * iron));
                
                MainSceneController.inst.UpdateGold(200 * iron);
                
                selectSkill.count = 0;
                selectSkill.UpdateUI();
                slider.value = 0;
                sliderText.text = selectSkill.countText.text;
            }
            else
            {
                if(selectSkill.id < 20)
                {
                    activeEmptyList.Add(selectSkill);
                    activePurchasedList.Remove(selectSkill);
                }
                else
                {
                    passiveEmptyList.Add(selectSkill);
                    passivePurchasedList.Remove(selectSkill);
                }

                int iron = selectSkill.level * selectSkill.level;
                goldText.text = string.Format("+{0}", NumbToData.GetInt(200 * iron));
                selectSkill.level = 0;
                selectSkill.PassiveSkillAdd();
                MainSceneController.inst.UpdateGold(200 * iron);
                selectSkill.numb = -1;
                selectSkill.count = 0;
                selectSkill.level = 1;
                selectSkill.selectObj.SetActive(false);
                selectSkill.UpdateUI();
                contentsText.text = "----";
                titleText.text = "---";
                levelText.text = string.Empty;
                skillImage.color = Color.clear;
                slider.maxValue = 1;
                slider.value = 0;
                sliderText.text = "0 / 0";
            }
            sellPop.SetActive(true);
            DataController.inst.SaveSkill(selectSkill);
        }
    }
    public void UpgradeSkill()
    {
        if (selectSkill == null)
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[179], 20); SoundController.inst.UISound(5); }
        else
        {
            int max = selectSkill.level * 2;
            int maxLevel = 0;
            if (selectSkill.numb.Equals(27) || selectSkill.numb.Equals(24) || selectSkill.numb.Equals(25) || selectSkill.numb.Equals(13) || selectSkill.numb.Equals(8))
            { maxLevel = 11; }
            else if (selectSkill.numb.Equals(21) || selectSkill.numb.Equals(22))
            { maxLevel = 31; }
            else
            { maxLevel = 21; }

            if (selectSkill.level < maxLevel)
            {
                if (selectSkill.count >= max)
                {
                    SoundController.inst.UISound(10);
                    DataController.inst.C_Upgrade++;
                    selectSkill.count -= max;
                    selectSkill.level++;
                    selectSkill.PassiveSkillAdd();
                    selectSkill.UpdateUI();
                    contentsText.text = Language.inst.SetSkillContentsLanguae(selectSkill.level, selectSkill.numb);
                    slider.maxValue = selectSkill.level * 2;
                    slider.value = selectSkill.count;
                    sliderText.text = selectSkill.countText.text;
                    if (selectSkill.level.Equals(selectSkill.maxLevel))
                    { levelText.text = "Max"; }
                    else
                    { levelText.text = string.Format("+{0}", selectSkill.level - 1); }
                    DataController.inst.SaveSkill(selectSkill);
                }
                else
                { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[123], 20); }
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[119], 20); SoundController.inst.UISound(5); }
        }
    }
    public void EquipSkill()
    {
        if (selectSkill == null)
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[179], 20); SoundController.inst.UISound(5); }
        else
        {
            if (selectSkill.numb < 20)
            { equipPop.SetActive(true); SoundController.inst.UISound(5); }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[125], 0); SoundController.inst.UISound(5); }
        }
    }

    public void EquipCheck(SkillSlot slot)
    {
        if (selectSkill.numb.Equals(-1))
        {
            slot.numb = -1;
            slot.level = 1;
            equipSkills[slot.id].Set();
            DataController.inst.SaveSkillSlot(slot);
            slot.UpdateUI();
        }
        else
        {
            bool checker = true;
            for (int i = 0; i < 5; i++)
            {
                if (selectSkill.numb.Equals(skillSlots[i].numb))
                {
                    ErrorSign.inst.SimpleSignSet(Language.inst.strArray[126], 50);
                    equipPop.SetActive(false);
                    checker = false;
                    break;
                }
            }
            if (checker)
            {
                slot.numb = selectSkill.numb;
                slot.level = selectSkill.level;
                DataController.inst.SaveSkillSlot(slot);
                equipSkills[slot.id].Set();
                slot.UpdateUI();
            }
        }
        equipPop.SetActive(false);
    }

    public void ButtonClick()
    {
        if (selectSkill != null)
        {
            selectSkill.selectObj.SetActive(false);
            selectSkill = null;
        }
        levelText.text = "";
        contentsText.text = "----";
        titleText.text = "---";
        skillImage.color = Color.clear;
        slider.maxValue = 1;
        slider.value = 0;
        sliderText.text = "0 / 0";
    }

    public void SetSkill(SkillButton skill)
    {
        for (int i = 0; i < 20; i++)
        {
            if (activeSkills[i].isPurchased)
            { activeSkills[i].selectObj.SetActive(false); }
        }
        for (int i = 0; i < 10; i++)
        {
            if (passiveSkills[i].isPurchased)
            { passiveSkills[i].selectObj.SetActive(false); }
        }

        selectSkill = skill;

        if (skill.numb > -1)
        {
            skill.selectObj.SetActive(true);
            contentsText.text = Language.inst.SetSkillContentsLanguae(skill.level, skill.numb);
            titleText.text = Language.inst.skillArr[skill.numb];
            skillImage.color = Color.white;
            skillImage.sprite = skill.skillImage.sprite;
            slider.maxValue = skill.level * 2;
            slider.value = skill.count;
            sliderText.text = skill.countText.text;
            if (skill.level > 1)
            {
                if (skill.level.Equals(skill.maxLevel))
                { levelText.text = "Max"; }
                else
                { levelText.text = string.Format("+{0}", skill.level - 1); }
            }
            else
            { levelText.text = string.Empty; }
        }
        else
        {
            levelText.text = "";
            contentsText.text = "----";
            titleText.text = "---";
            skillImage.color = Color.clear;
            slider.maxValue = 1;
            slider.value = 0;
            sliderText.text = "0 / 0";
        }
    }
}
