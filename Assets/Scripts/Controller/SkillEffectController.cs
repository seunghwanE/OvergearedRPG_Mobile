using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectController : MonoBehaviour{
    public static SkillEffectController inst;

    public SkillObj[] skills, attackSkill, deadSkill, hitSkill, timerSkill;
    public int at;
    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }        //onesec = new WaitForSeconds(1f);
    }


    public void AttackSkill()
    {
        for(int i = 0; i < attackSkill.Length; i++)
        {
            if(attackSkill[i].gameObject.activeInHierarchy)
            { attackSkill[i].AttackActive(); }
        }
    }
    public void HitSkill()
    {
        if(skills[1].gameObject.activeInHierarchy)
        { skills[1].AttackActive(); }
    }
    public void DeadSkill()
    {
        if (skills[13].gameObject.activeInHierarchy)
        { skills[13].AttackActive(); }
    }

    //public void SetSkillAt()
    //{
    //    for(int i = 0; i < 20; i++)
    //    { skills[i].gameObject.SetActive(false); }

    //    int level = 0, numb = 0;
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (SkillController.inst.equipSkills[i].numb > -1)
    //        {
    //            skills[numb].gameObject.SetActive(true);
    //            level = SkillController.inst.equipSkills[i].level;
    //            numb = SkillController.inst.equipSkills[i].numb;
    //            skills[numb].SetSkillAbility(at, level);
    //        }
    //    }
    //}

}
