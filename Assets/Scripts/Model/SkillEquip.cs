using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEquip : MonoBehaviour
{
    public int id, level, numb;
    public Image skillImage, kindImage, skillImageSecond, secondGroup, fillImage;
    public SkillObj skillObj;
    public Text numbText;
    public Sprite[] kind;
    
    public void Set()
    {
        if (SkillController.inst != null)
        {
            numb = SkillController.inst.skillSlots[id].numb;
            level = SkillController.inst.skillSlots[id].level;
        }
        else
        {
            numb = BattleSceneController.inst.skillSlots[id].numb;
            level = BattleSceneController.inst.skillSlots[id].level;
        }
        if (skillObj != null)
        { skillObj.gameObject.SetActive(false); }
        if (numb > -1)
        {
            skillObj = SkillEffectController.inst.skills[numb];
            SkillEffectController.inst.skills[numb].thisEquipSlot = this;
            skillObj.gameObject.SetActive(true);
            SkillEffectController.inst.skills[numb].SetSkillAbility(PlayerController.inst.character.at, level);
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (numb > -1)
        {
            kindImage.gameObject.SetActive(true);
            skillImage.sprite = ImageController.inst.skill[numb];
            skillImageSecond.sprite = skillImage.sprite;
            skillImage.color = Color.white;
            skillImageSecond.color = Color.white;
            if (numb.Equals(1))
            { kindImage.sprite = kind[1]; numbText.text = string.Format("{0}%", level * 5); fillImage.fillAmount = 0f; }
            else if (numb.Equals(13))
            { kindImage.sprite = kind[3]; numbText.text = string.Format("{0}%", level * 10); fillImage.fillAmount = 0f; }
            else if (numb.Equals(0) || numb.Equals(2) || numb.Equals(4) || numb.Equals(6) || numb.Equals(9) || numb.Equals(10) || numb.Equals(11) || numb.Equals(14) || numb.Equals(16) ||
                numb.Equals(17) || numb.Equals(18))
            { kindImage.sprite = kind[2]; }
            else
            {
                if (numb.Equals(3))
                { numbText.text = "20%"; }
                else if (numb.Equals(5))
                { numbText.text = string.Format("{0}%", level * 8); }
                else if (numb.Equals(7))
                { numbText.text = "10%"; }
                else if (numb.Equals(8))
                { numbText.text = string.Format("{0}%", level * 10); }
                else if (numb.Equals(12))
                { numbText.text = "15%"; }
                else if (numb.Equals(15))
                { numbText.text = "10%"; }
                else if (numb.Equals(19))
                { numbText.text = string.Format("{0}%", level * 3); }
                kindImage.sprite = kind[0];
                fillImage.fillAmount = 0f;
            }
        }
        else
        {
            numbText.text = string.Empty;
            kindImage.gameObject.SetActive(false);
            fillImage.fillAmount = 0f;
            skillImage.color = Color.black;
            skillImageSecond.color = Color.black;
        }
    }
}
