using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public int[] _athp;
    public int battleKind, r, field, r2, reward;

    public float[] athp;
    public float currentHp;

    public Slider hpBar;
    public Transform UIPos;
    public Animator anim;
    public Coroutine StateCor;

    public SpriteRenderer[] sprites;

    public void Attack()
    {
        PlayerController.inst.character.Hit((int)athp[0]);
    }
    public void AttackFalse()
    {
        anim.SetBool("Attack_b", false);
        Invoke("AttackTrue", 1f);
    }
    public void AttackTrue()
    { anim.SetBool("Attack_b", true); }

    public void StageEnemyCheck()
    {
        //StartCoroutine(StepScale(gameObject.transform.localScale, Vector3.zero));
        hpBar.gameObject.SetActive(false);
        BattleController.inst.Kill();
    }
    public void Dead()
    { StageEnemyCheck(); }

    public void SetHpBar(Slider _hpbar)
    {
        hpBar = _hpbar;
        hpBar.transform.position = UIPos.position;
        hpBar.maxValue = athp[1];
        hpBar.value = currentHp;
    }

    IEnumerator StepScale(Vector3 start, Vector3 target)
    {
        float duration = 0.25f;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            gameObject.transform.localScale = Vector3.Lerp(start, target, progress);
            yield return null;
        }
        gameObject.transform.localScale = target;
        if (target.Equals(Vector3.zero))
        { gameObject.SetActive(false); hpBar.gameObject.SetActive(false); }
        else
        { hpBar.transform.position = UIPos.position; hpBar.gameObject.SetActive(true); }
    }
    public void HitPet(int damage)
    {
        if (currentHp > 0)
        {
            DataController.inst.Vibrate(10);
            if (field < 49)
            { currentHp -= damage; }
            else if (field < 98)
            { currentHp -= damage * 0.1f; }
            else
            { currentHp -= damage * 0.01f; }

            if (currentHp < 1f)
            {
                DataController.inst.KillCount++;
                GPGSManager.inst.ReportKill(DataController.inst.KillCount);
                BattleController.inst.DeadEffect();
                anim.SetBool("Dead_b", true);
                GetReward();
                currentHp = 0;
            }
            hpBar.value = currentHp;
        }
    }
    public void Hit(int damage)
    {
        if (currentHp > 0)
        {
            r = Random.Range(0, 100);
            int cri = PlayerController.inst.character.cri;
            int avoid = PlayerController.inst.character.avoid;
            float criDamage = 0f, avoidSub = 0f;
            if (cri > 50)
            {
                criDamage = (cri - 50) * 0.01f;
                cri = 50;
            }
            if (avoid > 50)
            { avoidSub = (avoid - 50) * 0.01f; }

            if (cri > r || MainSceneController.inst.powerFlag)
            {
                SoundController.inst.CriSound();
                DataController.inst.Vibrate(10);
                if (DataController.inst.IAPCri)
                { damage = (int)(damage * (2 + criDamage + avoidSub)); }
                else
                { damage = (int)(damage * (1.5f + criDamage + avoidSub)); }
                if (DataController.inst.S_Damage)
                {
                    BattleController.inst.damagePool.PopObject(UIPos, NumbToData.GetIntGold(damage), 2);
                    BattleController.inst.CriticalOn();
                }
            }
            else
            {
                DataController.inst.Vibrate(5);
                SoundController.inst.CharacterAttackSound();
                if (DataController.inst.S_Damage)
                { BattleController.inst.damagePool.PopObject(UIPos, NumbToData.GetIntGold(damage), 0); }
            }

            BattleController.inst.bloodEffect.SetActive(false);
            BattleController.inst.bloodEffect.SetActive(true);
            if (field < 49)
            { currentHp -= damage; }
            else if (field < 98)
            { currentHp -= damage * 0.1f; }
            else
            { currentHp -= damage * 0.01f; }

            if (currentHp < 1f)
            {
                DataController.inst.KillCount++;
                GPGSManager.inst.ReportKill(DataController.inst.KillCount);
                BattleController.inst.DeadEffect();
                anim.SetBool("Dead_b", true);
                GetReward();
                currentHp = 0;
            }
            hpBar.value = currentHp;
        }
    }

    public void GetReward()
    {
        r2 = Random.Range(0, 1000);
        int luck = (int)(PlayerController.inst.statLuck * 0.1f);
        if(luck > 90)
        { luck = 90; }
        if (r2 < 10 + luck)
        {
            BattleController.inst.biteObj.SetActive(true);
            r2 = Random.Range(0, 100);
            if (r2 < 70)
            { Invoke("GetStat", 1.5f); }
            else
            { Invoke("GetSkill", 1.5f); }
        }
        else if(r2 < 40 + luck)
        {
            if (DataController.inst.S_Damage)
            { BattleController.inst.moneyPool.PopMoney(UIPos, 2); }
            MainSceneController.inst.UpdateRuby(1 + (int)(field * 0.08f));
        }
        else if(r2 < 100 + luck)
        {
            if (DataController.inst.S_Damage)
            { BattleController.inst.moneyPool.PopMoney(UIPos, 1); }
            MainSceneController.inst.UpdateIron(1 + (int)(field * 0.18f));
        }
        else
        {
            if (DataController.inst.S_Damage)
            {
                for (int i = 0; i <= field; i++)
                {
                    BattleController.inst.moneyPool.PopMoney(UIPos, 0);
                    if (i > 8)
                    { break; }
                }
            }
            reward = 50 + (field * 15);
            reward = (int)(reward * (PlayerController.inst.skillGold + AdMobController.inst.buffGold));
            MainSceneController.inst.UpdateGold(reward);
        }
        reward = 1 + field;
        reward = (int)(reward * PlayerController.inst.skillExp);
        PlayerController.inst.GetExp(reward);
        r2 = Random.Range(0, 100);
        if(r2 < 2)
        { BattleController.inst.ChestOn(); }
    }
    public void GetStat()
    {
        DataController.inst.C_GetStatTen++;
        if (DataController.inst.S_Damage)
        { BattleController.inst.moneyPool.PopMoney(UIPos, 3); }
        PlayerController.inst.GetRandomStat();
    }
    public void GetSkill()
    {
        DataController.inst.C_GetSkillTen++;
        if (DataController.inst.S_Damage)
        { BattleController.inst.moneyPool.PopMoney(UIPos, 4); }
        SkillController.inst.GetRandomSkill();
    }

    public void SetEnemy(int _field, Slider hp)
    {
        anim.SetBool("Dead_b", false);
        athp = NumbToData.EnemyPower(_field);
        field = DataController.inst.field;
        currentHp = athp[1];
        if (BattleController.inst.battleEnemy != null)
        {
            if (BattleController.inst.battleEnemy.gameObject.activeInHierarchy)
            { BattleController.inst.battleEnemy.gameObject.SetActive(false); }
        }
        
        SetHpBar(hp);
        BattleController.inst.battleEnemy = this;
        gameObject.SetActive(true);
        StartCoroutine(StepScale(Vector3.zero, gameObject.transform.localScale));

        if (!battleKind.Equals(0))
        { Invoke("AttackTrue", 2f); }
    }
}
