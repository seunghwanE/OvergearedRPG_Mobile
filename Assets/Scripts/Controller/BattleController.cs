using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {
    public static BattleController inst;

    public FieldButton[] fieldButtons;
    public Enemy[] enemyPrefabs;
    public Enemy battleEnemy;
    public List<Enemy> enemyList;
    public List<GameObject> criticalList;
    public Slider[] hpbars;
    public Transform enemyPos;
    public Transform[] moneyPos;
    public GameObject WariningObj, deadSign, bloodEffect, characterHitObj, biteObj, deadObj, bossWinObj;
    public int at, hp, stageCount, difficultyNumb;
    public bool deadFlag, fieldChange, isBoss;
    public ObjectPooling damagePool, moneyPool;
    public AdButton[] adButtons;
    public Text fieldText, countText, ticketText;
    public BossTimer bossTimer;
    public BossButton bossBattleButton, nextMonsterButton;

    public GameObject angelRing;

    public SpriteRenderer[] bgs;
    public Sprite[] bgSprites;

    public BuffIcon[] buffs;
    public Chest chest;

    public Color halfWhite, halfGrat;
    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }

        isBoss = false;
        enemyList = new List<Enemy>();
        deadFlag = true;
        fieldChange = false;
        halfWhite = new Color(1f, 1f, 1f, 0.5f);
        halfGrat = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        ticketText.text = string.Format("{0}/3", DataController.inst.ticket);
    }


    public void CriticalOn()
    {
        for (int i = 0; i < criticalList.Count; i++)
        {
            if (!criticalList[i].gameObject.activeInHierarchy)
            {
                criticalList[i].SetActive(true);
                break;
            }
        }

    }

    public void ChestOn()
    {
        if (!chest.gameObject.activeInHierarchy)
        { chest.SetChest(); }
    }

    public void DeadEffect()
    {
        deadObj.SetActive(false);
        deadObj.SetActive(true);
        Invoke("SetOffDeadObj", 1.2f);
    }
    private void SetOffDeadObj()
    { deadObj.SetActive(false); }
    private void Start()
    {
        countText.text = "0 / 10";
        for (int i = 0; i < fieldButtons.Length; i++)
        {
            fieldButtons[i].UpdateUI();
        }

        stageCount = 0;
        PlayerController.inst.SetCharacter(MainSceneController.inst.characters[0]);
        SetFieldUI();
        SetEnemy(false);
        BGSet();
    }

    public void ChangeField()
    {
        if (DataController.inst.field.Equals(DataController.inst.highestField))
        {
            stageCount = 0;
            enemyList.Clear();
            bossBattleButton.gameObject.SetActive(false);
            battleEnemy.gameObject.SetActive(false);

            BGSet();
            Kill();
            SetFieldUI();
        }
        else
        {
            stageCount = 0;
            enemyList.Clear();
            bossBattleButton.gameObject.SetActive(true);
            battleEnemy.gameObject.SetActive(false);
            fieldChange = true;

            bossTimer.gameObject.SetActive(false);
            PlayerController.inst.character.Revive();

            BGSet();
            Kill();
            SetFieldUI();
        }
    }

    public void BGSet()
    {
        int numb = DataController.inst.field;
        bgs[0].gameObject.SetActive(false);
        bgs[1].gameObject.SetActive(false);

        if (numb < 49)
        {
            bgs[0].gameObject.SetActive(true);
            bgs[0].sprite = bgSprites[(int)(numb * 0.1f)];
        }
        else if(numb < 98)
        {
            bgs[0].gameObject.SetActive(true);
            bgs[0].sprite = bgSprites[14];
        }
        else
        {
            bgs[0].gameObject.SetActive(true);
            bgs[0].sprite = bgSprites[15];
        }
        //if (numb < 80)
        //{
        //    bgs[0].gameObject.SetActive(true);
        //    bgs[0].sprite = bgSprites[(int)(numb * 0.1f)];
        //}
        //else if (numb < 140)
        //{
        //    bgs[1].gameObject.SetActive(true);
        //    bgs[1].sprite = bgSprites[(int)(numb * 0.1f)];
        //}
        //else if (numb < 220)
        //{
        //    bgs[0].gameObject.SetActive(true);
        //    numb = (int)(numb * 0.1f) - 14;
        //    bgs[0].sprite = bgSprites[(int)(numb * 0.1f)];
        //}
        //else if (numb < 280)
        //{
        //    bgs[1].gameObject.SetActive(true);
        //    numb = (int)(numb * 0.1f) - 22;
        //    bgs[1].sprite = bgSprites[(int)(numb * 0.1f)];
        //}
        //else
        //{
        //    bgs[1].gameObject.SetActive(true);
        //    bgs[1].sprite = bgSprites[13];
        //}
    }


    public void Kill()
    {
        if (stageCount.Equals(10))
        { bossBattleButton.gameObject.SetActive(true); }
        else
        { stageCount++; }

        if (isBoss)
        { BossKill(); Invoke("KillBossNextEnemy", 2f); DataController.inst.C_KillBoss++; }
        else
        {
            if (!deadFlag)
            { SetEnemy(true); }
            else
            { SetEnemy(false); DataController.inst.C_Kill++; }
        }
        countText.text = string.Format("{0} / 10", stageCount.ToString());
    }
    public void SetFieldUI()
    {
        int numb = DataController.inst.field;
        if (numb < 49)
        { fieldText.text = string.Format("{0}", Language.inst.fieldArr[numb]); }
        else if( numb < 98)
        { fieldText.text = string.Format("{0}", Language.inst.fieldArr[numb-49]); }
        else
        { fieldText.text = string.Format("{0}", Language.inst.fieldArr[numb-98]); }
    }

    public void KillBossNextEnemy()
    { SetEnemy(false); }

    public void Dead()
    {
        if (!deadFlag)
        {
            SoundController.inst.UISound(8);
            deadFlag = true;
            deadSign.SetActive(true);
            if(bossTimer.gameObject.activeInHierarchy)
            { bossTimer.StopCoroutine(bossTimer.timer); }
        }
    }
    public void BossButtonClick()
    {
        if (fieldChange)
        {
            fieldChange = false;
            MainSceneController.inst.cloudObj.SetActive(true);
            stageCount = 0;
            Invoke("SecondCloud", 1.9f);
        }
        else
        {
            deadFlag = false;
            battleEnemy.anim.SetBool("Dead_b", true);
        }
    }

    public void SecondCloud()
    {
        DataController.inst.field = DataController.inst.highestField;
        ChangeField();
        InputController.inst.OtherContentsTurnOff();
        MainSceneController.inst.cloudObj2.SetActive(true);
        //battleEnemy.gameObject.SetActive(false);
        //Kill();
    }


    public void DeadSetBattle()
    {
        SoundController.inst.UISound(8);
        PlayerController.inst.character.revive = true;
        isBoss = false;
        battleEnemy.hpBar.gameObject.SetActive(false);
        bossTimer.gameObject.SetActive(false);
        SetEnemy(false);
        PlayerController.inst.character.Revive();
        InputController.inst.OtherContentsTurnOff(true);
    }
    public void AdRevive()
    {
        PlayerController.inst.character.revive = true;
        deadFlag = false;
        bossTimer.gameObject.SetActive(false);
        bossBattleButton.gameObject.SetActive(false);
        bossTimer.gameObject.SetActive(true);
        PlayerController.inst.character.Revive();
        InputController.inst.OtherContentsTurnOff(true);
    }
    public void BossKill()
    {
        SoundController.inst.UISound(1);
        enemyList.Clear();
        bossWinObj.SetActive(true);
        bossBattleButton.gameObject.SetActive(false);
        PlayerController.inst.character.currentHp = PlayerController.inst.character.hp;
        PlayerController.inst.character.HpBarUpdateUI();
        if (DataController.inst.field < 146)
        { DataController.inst.field++; }
        int i = DataController.inst.field;
        if (DataController.inst.highestField < i)
        { DataController.inst.highestField = i; }
        fieldButtons[i - 1].UpdateUI();
        fieldButtons[i].UpdateUI();
        stageCount = 0;
        isBoss = false;
        deadFlag = true;
        fieldText.text = Language.inst.fieldArr[i];
        battleEnemy.hpBar.gameObject.SetActive(false);
        bossTimer.gameObject.SetActive(false);
        BGSet();
    }
    public void SetEnemy(bool boss)
    {
        int field = DataController.inst.field;
        int numb = 0;
        if(field < 10)
        { numb = 1; }
        else if(field < 30)
        { numb = 2; }
        else
        { numb = 3; }

        if (boss)
        {
            PlayerController.inst.character.revive = true;
            WariningObj.SetActive(true);
            bossTimer.time = 15f;
            bossTimer.gameObject.SetActive(true);
            StartCoroutine(OneBossGen(field, numb));
        }
        else
        {
            isBoss = false;
            EnemyGen(field, 0);
        }
    }
    
    public void EnemyGen(int field, int barNumb)
    {
        bool flag = true;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].gameObject.activeInHierarchy)
            {
                if (enemyList[i].battleKind.Equals(0))
                {
                    flag = false;
                    enemyList[i].SetEnemy(field, hpbars[barNumb]);
                    GodEnemy(enemyList[i], field);
                    break;
                }
            }
        }

        if (flag)
        {
            int numb = DataController.inst.field;
            if (numb < 49)
            { }
            else if (numb < 98)
            { numb -= 49; }
            else
            { numb -= 98; }

            Enemy newone = Instantiate(enemyPrefabs[numb], enemyPos);
            newone.battleKind = 0;
            newone.transform.position = enemyPos.position;
            newone.SetEnemy(field, hpbars[barNumb]);
            HellEnemy(newone, field);
            GodEnemy(newone, field);
            enemyList.Add(newone);
        }
    }

    public void GodEnemy(Enemy enemy, int field)
    {
        if(field > 97)
        { angelRing.transform.position = enemy.UIPos.position; }
    }

    public void HellEnemy(Enemy enemy, int field)
    {
        if(field > 48)
        {
            if(field < 98)
            {
                for(int i=0; i < enemy.sprites.Length; i++)
                { enemy.sprites[i].color = Color.gray; }
            }
        }
    }

    IEnumerator OneBossGen(int field, int barNumb)
    {
        bool flag = true;
        SoundController.inst.UISound(0);
        yield return DataController.inst.halfSec;
        isBoss = true;
        yield return DataController.inst.halfSec;
        SoundController.inst.UISound(0);
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].gameObject.activeInHierarchy)
            {
                if (enemyList[i].battleKind.Equals(1))
                {
                    flag = false;
                    enemyList[i].SetEnemy(field+2, hpbars[barNumb]);
                    GodEnemy(enemyList[i], field);
                    break;
                }
            }
        }

        if (flag)
        {
            int numb = DataController.inst.field;
            if (numb < 49)
            { }
            else if (numb < 98)
            { numb -= 49; }
            else
            { numb -= 98; }

            Enemy newone = Instantiate(enemyPrefabs[numb], enemyPos);
            newone.transform.localScale *= 1.3f;
            newone.battleKind = 1;
            newone.transform.position = enemyPos.position;
            newone.SetEnemy(field+2, hpbars[barNumb]);
            HellEnemy(newone, field);
            GodEnemy(newone, field);
            enemyList.Add(newone);
        }
        yield return DataController.inst.halfSec;
        yield return DataController.inst.halfSec;
        SoundController.inst.UISound(0);
    }
}
