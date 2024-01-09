using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour {
    public static BattleSceneController inst;
    //14
    public GameObject cloudObj, cloudObj2, bossWarn, bloodEffect, touchEffect, failSign, winSign, bg0, bg1, petC0, petC1, errorSign;
    public Character[] character;
    public Character nowCharacter;

    public List<GameObject> criticalList;

    public BattleEnemy[] enemyPrefab;
    public BattleEnemy enemy;

    public SkillSlot[] skillSlots;
    public SkillEquip[] skillEquips;

    public Transform UIPos;
    public Transform[] attackPos;
    public Slider hpBar, timer;
    public ObjectPooling damagePool;

    public Text hpText, ironText, rubyText, statText, stageText;
    public Button clickButton;
    public Coroutine timerCor;

    public int currentHp, maxHp, r, stageNumb, r2, twob;
    public float sec;
    public bool deadFlag;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        deadFlag = false;
        errorSign.SetActive(true);
        cloudObj2.SetActive(true);
        bossWarn.SetActive(true);
    }

    public void Start()
    {
        DataController.inst.C_Battle++;
        DataController.inst.es3.Sync();
        SetBattle();
    }

    public void BattleClick()
    {
        clickButton.interactable = false;
        nowCharacter.BattleClick();
        Invoke("SetClickButton", 0.025f);
    }

    public void SetClickButton()
    {
        clickButton.interactable = true;
    }

    public void GoRe()
    {
        if (DataController.inst.ticket > 0)
        {
            cloudObj.SetActive(true);
            DataController.inst.es3.Sync();
            Invoke("ReScene", 1.8f);
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[152], 20); }
    }
    public void GoMain()
    {
        cloudObj.SetActive(true);
        Invoke("MainScene", 1.8f);
    }

    public void ReScene()
    { DataController.inst.LoadReScene(); }
    public void MainScene()
    { DataController.inst.LoadMainScene(); }

    public void SetBattle()
    {
        stageNumb = DataController.inst.battleButtonNumb;
        if (stageNumb < 14)
        { nowCharacter = character[0]; bg0.SetActive(true); character[0].gameObject.SetActive(true); petC0.SetActive(true); }
        else
        { nowCharacter = character[1]; bg1.SetActive(true); character[1].gameObject.SetActive(true); petC1.SetActive(true); }
        PlayerController.inst.SetCharacter(nowCharacter);
        PlayerController.inst.SetCharacterImage();

        for (int i = 0; i < skillSlots.Length; i++)
        { DataController.inst.LoadSkillSlot(skillSlots[i]); skillEquips[i].Set(); }

        enemy = enemyPrefab[stageNumb];
        enemy.gameObject.SetActive(true);

        if (stageNumb < 4)
        { maxHp = (stageNumb + 1) * 20000; twob = 0; }
        else if (stageNumb < 8)
        { maxHp = (stageNumb + 1) * 500000; twob = 0; }
        else if (stageNumb < 12)
        { maxHp = (stageNumb + 1) * 8000000; twob = 0; }
        else if (stageNumb < 16)
        { maxHp = (stageNumb + 1) * 100000000; twob = 0; }
        else
        { maxHp = 2000000000; twob = stageNumb - 16; }
        currentHp = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = maxHp;
        if (twob > 0)
        { hpText.text = string.Format("{0} / {1} x{2}", maxHp.ToString("N0"), maxHp.ToString("N0"), (twob+1).ToString()); }
        else
        { hpText.text = string.Format("{0} / {1}", maxHp.ToString("N0"), maxHp.ToString("N0")); }

        Invoke("StartTimer", 2f);
    }

    public void StartTimer()
    { timerCor = StartCoroutine(TimerCor()); clickButton.interactable = true; }
    IEnumerator TimerCor()
    {
        timer.maxValue = 20;
        sec = 20;
        bool flag = true;
        while (flag)
        {
            sec -= Time.deltaTime;
            timer.value = sec;
            yield return null;
            if (sec < 0)
            {
                deadFlag = true;
                FailSignSet();
                flag = false;
                break;
            }
        }
    }
    public void FailSignSet()
    { failSign.SetActive(true); }
    public void WinSignSet()
    {
        DataController.inst.es3.Save(stageNumb.ToString() + "battleClear", true);
        DataController.inst.ticket--;
        int reward = ((stageNumb + 1) * 5) + 15;
        ironText.text = string.Format("+{0}", reward.ToString());
        rubyText.text = string.Format("+{0}", (reward - 15).ToString());
        statText.text = string.Format("+{0}", (reward - 10).ToString());

        DataController.inst.II(reward);
        DataController.inst.RR(reward - 15);
        PlayerController.inst.statPoint += reward - 10;
        winSign.SetActive(true);
        if (!stageNumb.Equals(24))
        { stageNumb++; }
        DataController.inst.battleButtonNumb = stageNumb;
        stageText.text = stageNumb.ToString();
        DataController.inst.es3.Sync();
    }

    public void CriticalOn()
    {
        for(int i = 0; i < criticalList.Count; i++)
        {
            if(!criticalList[i].activeInHierarchy)
            {
                criticalList[i].SetActive(true);
                break;
            }
        }
    }

    public void HitPet(int damage)
    {
        if (!deadFlag)
        {
            if (currentHp > 0)
            {
                enemy.Hit();
                currentHp -= damage;
                if (currentHp < 1)
                {
                    if (twob > 0)
                    {
                        twob--;
                        currentHp = maxHp;
                    }
                    else
                    {
                        Invoke("WinSignSet", 1f);
                        deadFlag = true;
                        enemy.anim.SetBool("Dead_b", true);
                        currentHp = 0;
                        StopCoroutine(timerCor);
                    }
                }
                hpBar.value = currentHp;
                if (twob > 0)
                { hpText.text = string.Format("{0} / {1} x{2}", currentHp.ToString("N0"), maxHp.ToString("N0"),twob.ToString()); }
                else
                { hpText.text = string.Format("{0} / {1}", currentHp.ToString("N0"), maxHp.ToString("N0")); }
            }
        }
    }

    public void Hit(int damage)
    {
        if (!deadFlag)
        {
            r = Random.Range(0, 1000);
            if (PlayerController.inst.character.cri > r)
            {
                CriticalOn();
                if (DataController.inst.IAPCri)
                { damage = damage * 3; }
                else
                { damage = (int)(damage * 1.5f); }
                damagePool.PopObject(UIPos, NumbToData.GetIntGold(damage), 2);
            }
            else
            { damagePool.PopObject(UIPos, NumbToData.GetIntGold(damage), 0); }

            if (currentHp > 0)
            {
                enemy.Hit();
                bloodEffect.SetActive(false);
                bloodEffect.SetActive(true);
                currentHp -= damage;
                if (currentHp < 1)
                {
                    if (twob > 0)
                    {
                        twob--;
                        currentHp = maxHp;
                    }
                    else
                    {
                        Invoke("WinSignSet", 1f);
                        deadFlag = true;
                        enemy.anim.SetBool("Dead_b", true);
                        currentHp = 0;
                        StopCoroutine(timerCor);
                    }
                }
                hpBar.value = currentHp;
                if (twob.Equals(0))
                { hpText.text = string.Format("{0} / {1}", currentHp.ToString("N0"), maxHp.ToString("N0")); }
                else
                { hpText.text = string.Format("{0} / {1} x{2}", currentHp.ToString("N0"), maxHp.ToString("N0"), twob.ToString()); }
            }
        }
    }

    public void TouchHit(int damage)
    {
        if (!deadFlag)
        {
            r2 = Random.Range(0, attackPos.Length);

            touchEffect.SetActive(false);
            damagePool.PopObject(attackPos[r2], NumbToData.GetIntGold(damage), 0);
            touchEffect.transform.position = attackPos[r2].position;
            touchEffect.SetActive(true);

            if (currentHp > 0)
            {
                currentHp -= damage;
                if (currentHp < 1)
                {
                    if (twob > 0)
                    {
                        twob--;
                        currentHp = maxHp;
                    }
                    else
                    {
                        Invoke("WinSignSet", 1f);
                        deadFlag = true;
                        enemy.anim.SetBool("Dead_b", true);
                        currentHp = 0;
                        StopCoroutine(timerCor);
                    }
                }
                hpBar.value = currentHp;
                if (twob > 0)
                { hpText.text = string.Format("{0} / {1} x{2}", currentHp.ToString("N0"), maxHp.ToString("N0"), twob.ToString()); }
                else
                { hpText.text = string.Format("{0} / {1}", currentHp.ToString("N0"), maxHp.ToString("N0")); }
            }
        }
    }


    //public void SetCharacterSkin(int numb)
    //{ character.SetSkin(numb); }
    //public void SetCharacterWeapon(int numb)
    //{ character.SetWeapon(numb); }
    //public void SetCharacterShield(int numb)
    //{ character.SetShield(numb); }
    //public void SetCharacterHelmet(int numb)
    //{ character.SetHelmet(numb); }
    //public void SetCharacterAmor(int numb)
    //{ character.SetAmor(numb); }
    //public void SetCharacterCape(int numb)
    //{ character.SetCape(numb); }
    //public void SetCharacterBack(int numb)
    //{ character.SetBack(numb); }
    //public void SetCharacterHelper(int numb)
    //{ character.SetHelper(numb); }

}
