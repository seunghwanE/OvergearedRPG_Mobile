using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public int id, level, numb;
    public Image skillImage;

    public void Click()
    {
        SoundController.inst.UISound(3);
        SkillController.inst.EquipCheck(this);
    }

    

    public void UpdateUI()
    {
        if(numb > -1)
        {
            skillImage.sprite = ImageController.inst.skill[numb];
            skillImage.color = Color.white;
        }
        else
        {
            skillImage.color = Color.black;
        }
    }
}
