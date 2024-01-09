using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj : MonoBehaviour
{
    public int at, kind, activePercent, id, timer;
    public GameObject obj;
    private WaitForSeconds fiveSec;
    public Coroutine skillCor;
    public SkillEquip thisEquipSlot;
    public AudioClip[] sound;

    private void Awake()
    {
        fiveSec = new WaitForSeconds(5f);
    }

    public void StartCor()
    { skillCor = StartCoroutine(Timer()); }
    public void StopCor()
    { StopCoroutine(skillCor); }

    IEnumerator Timer()
    {
        int r = 0;
        while (true)
        {
            //yield return onesec;
            //time++;
            //if (time.Equals(timer))
            //{
            //    time = 0;
            //    r = Random.Range(0, 100);
            //    if (r < activePercent)
            //    {
            //        thisEquipSlot.secondGroup.gameObject.SetActive(true);
            //        obj.SetActive(true);
            //        if (id.Equals(10) || id.Equals(11) || id.Equals(14))
            //        { time = -5; }
            //    }
            //}

            for (float _timer = timer; _timer > 0; _timer -= Time.deltaTime)
            {
                thisEquipSlot.fillImage.fillAmount = _timer / timer;
                thisEquipSlot.numbText.text = string.Format("{0}s", _timer.ToString("N1"));
                yield return null;   
            }
            r = Random.Range(0, 100);
            thisEquipSlot.fillImage.fillAmount = 0f;
            thisEquipSlot.numbText.text = string.Empty;
            if (r < activePercent)
            {
                StartSound();
                thisEquipSlot.secondGroup.gameObject.SetActive(true);
                obj.SetActive(true);
                if (id.Equals(10) || id.Equals(11) || id.Equals(14))
                { yield return fiveSec; }
            }
        }
    }

    public void StartSound()
    { StartCoroutine(SoundCor()); }

    IEnumerator SoundCor()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            SoundController.inst.SkillSound(sound[i]);
            yield return DataController.inst.halfSec;
        }
    }

    public void AttackActive()
    {
        int r = Random.Range(0, 100);
        if (r < activePercent)
        {
            StartSound();
            thisEquipSlot.secondGroup.gameObject.SetActive(true);
            obj.SetActive(true);
        }
        else
        {
            if (kind.Equals(4))
            { PlayerController.inst.character.DeadSign(); }
        }
    }
    public void HitActive()
    {
        int r = Random.Range(0, 100);
        if (r < activePercent)
        {
            StartSound();
            thisEquipSlot.secondGroup.gameObject.SetActive(true);
            obj.SetActive(true);
        }
    }

    public void SetSkillAbility(int damage = 0, int level = 0)
    {
        timer = 0;
        if (id.Equals(0))
        {
            timer = 10;
            activePercent = 100;
            at = (int)(damage * level * 0.1f);
        }
        else if (id.Equals(1))
        {
            activePercent = 20;
            at = (int)(damage * level * 0.05f);
        }
        else if (id.Equals(2))
        {
            timer = 15;
            activePercent = 30;
            at = (int)(damage * level * 0.75f);
        }
        else if (id.Equals(3))
        {
            activePercent = 20;
            at = (int)(damage * level * 0.1f);
        }
        else if (id.Equals(4))
        {
            timer = 12;
            activePercent = 30;
            at = (int)(damage * level * 0.04f);
        }
        else if (id.Equals(5))
        {
            activePercent = level * 8;
            at = damage;
        }
        else if (id.Equals(6))
        {
            timer = 9;
            activePercent = 30;
            at = (int)(damage * level * 0.07f);
        }
        else if (id.Equals(7))
        {
            activePercent = 10;
            at = (int)(damage * level * 0.2f);
        }
        else if (id.Equals(8))
        {
            activePercent = level * 10;
            at = damage;
        }
        else if (id.Equals(9))
        {
            timer = 8;
            activePercent = 25;
            at = (int)(damage * level * 0.05f);
        }
        else if (id.Equals(10))
        {    
            timer = 10;
            activePercent = 30;
            at = level * 5;
        }
        else if (id.Equals(11))
        {
            timer = 13;
            activePercent = 30;
            at = level * 5;
        }
        else if (id.Equals(12))
        {
            activePercent = 15;
            at = (int)(damage * level * 0.05f);
        }
        else if (id.Equals(13))
        {
            activePercent = level * 10;
            at = damage;
        }
        else if (id.Equals(14))
        {
            timer = 11;
            activePercent = level * 2;
        }
        else if (id.Equals(15))
        {
            activePercent = 10;
            at = (int)(damage * level * 0.03f);
        }
        else if (id.Equals(16))
        {
            timer = 14;
            activePercent = 17;
            at = damage * ( 1 + level);
        }
        else if (id.Equals(17))
        {
            timer = 10;
            activePercent = 14;
            at = (int)(damage * ( 70 + (level * 0.2f)));
        }
        else if (id.Equals(18))
        {
            timer = 9;
            activePercent = 10;
            at = damage * level;
        }
        else if (id.Equals(19))
        {
            activePercent = level * 3;
            at = damage;
        }

        if(!timer.Equals(0))
        { StartCor(); }
    }

    public void Attack()
    {
        if (kind.Equals(0))
        {
            if (BattleController.inst != null)
            { BattleController.inst.battleEnemy.Hit(at); }
            else
            { BattleSceneController.inst.Hit(at); }
        }
        else if (kind.Equals(1))
        {
            if (BattleController.inst != null)
            { BattleController.inst.damagePool.PopObject(PlayerController.inst.character.UIPos, "+" + NumbToData.GetInt(at)); }
            else
            { BattleSceneController.inst.damagePool.PopObject(PlayerController.inst.character.UIPos, "+" + NumbToData.GetInt(at)); }
            PlayerController.inst.character.currentHp += at;
            if(PlayerController.inst.character.hp < PlayerController.inst.character.currentHp)
            { PlayerController.inst.character.currentHp = PlayerController.inst.character.hp; }
            PlayerController.inst.character.HpBarUpdateUI();
        }
        else if (kind.Equals(2))
        {//공격속도
            PlayerController.inst.character.SpeedUp(at);
        }
        else if (kind.Equals(3))
        {//회피
            PlayerController.inst.character.AvoidUp(at);
        }
        else if (kind.Equals(4))
        {//부활
            PlayerController.inst.character.Revive();
        }
        else if (kind.Equals(5))
        {//무적
            PlayerController.inst.character.Immotal();
        }

    }
    public void SetOff()
    { gameObject.SetActive(false); }
}
