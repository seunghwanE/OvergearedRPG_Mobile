using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public int id, numb, level, count, maxLevel;
    public bool isPurchased;
    public Image skillImage;
    public GameObject lockObj, selectObj;
    public Text levelText, countText;
    public Slider countSlider;

    public void ClickSkill()
    {
        if(isPurchased)
        {
            SkillController.inst.SetSkill(this);
        }
        else
        {
            SkillController.inst.SetLock(this);
        }
    }

    public void PassiveSkillAdd()
    {
        if (numb > 19)
        {
            if (numb.Equals(20))
            { PlayerController.inst.skillAtRate = (level * 0.1f) + 1f; MainSceneController.inst.UpdateCharacterInfo(); }
            else if (numb.Equals(21))
            { PlayerController.inst.skillAt = level * 100; MainSceneController.inst.UpdateCharacterInfo(); }
            else if (numb.Equals(22))
            { PlayerController.inst.skillHp = level * 200; MainSceneController.inst.UpdateCharacterInfo(); }
            else if (numb.Equals(23))
            { PlayerController.inst.skillHpRate = (level * 0.1f) + 1f; MainSceneController.inst.UpdateCharacterInfo(); }
            else if (numb.Equals(24))
            { PlayerController.inst.skillCri = level * 2; }
            else if (numb.Equals(25))
            { PlayerController.inst.skillAvoid = level * 2; }
            else if (numb.Equals(26))
            { PlayerController.inst.skillSpeed = 1f + (level * 0.05f); PlayerController.inst.SetSpeed(); }
            else if (numb.Equals(27))
            { PlayerController.inst.skillCombo = level; PlayerController.inst.SetCombo(); }
            else if (numb.Equals(28))
            { PlayerController.inst.skillGold = (level * 0.05f) + 1f; }
            else if (numb.Equals(29))
            { PlayerController.inst.skillExp = (level * 0.05f) + 1f; }
            PlayerController.inst.SetAbility();
        }
    }

    public void GetMaxLevel()
    {
        if (numb.Equals(27) ||numb.Equals(24) || numb.Equals(25) || numb.Equals(13) || numb.Equals(8))
        { maxLevel = 11; }
        else if (numb.Equals(21) || numb.Equals(22))
        { maxLevel = 31; }
        else
        { maxLevel = 21; }
    }

    public void UpdateUI()
    {
        if(isPurchased)
        {
            GetMaxLevel();

            lockObj.SetActive(false);
            if (numb < 0)
            {
                skillImage.color = Color.clear;
                countSlider.gameObject.SetActive(false);
                levelText.text = string.Empty;
            }
            else
            {
                skillImage.color = Color.white;
                skillImage.sprite = ImageController.inst.skill[numb];
                countSlider.gameObject.SetActive(true);
                countText.text = string.Format("{0} / {1}",count, level * 2);
                countSlider.maxValue = level * 2;
                countSlider.value = count;

                if (level > 1)
                {
                    if (level.Equals(maxLevel))
                    { levelText.text = "Max"; }
                    else
                    { levelText.text = string.Format("+{0}", level - 1); }
                }
                else
                { levelText.text = string.Empty; }
            }
        }
        else
        {
            lockObj.SetActive(true);
            skillImage.color = Color.clear;
            countSlider.gameObject.SetActive(false);
        }
    }
}
