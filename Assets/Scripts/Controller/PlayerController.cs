using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public static PlayerController inst;

    public Character character;
    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        DontDestroyOnLoad(this);
    }

    public int statPower, statDex, statHealth, statLuck, level, statPoint, maxExp, currentExp, totalPower,
             statAt, statCri, statAvoid, statHp, statLuckRate, itemAt, itemHp, itemCri, itemAvoid, skillAt, skillHp, skillCri, skillAvoid, skillCombo, skinAt;
    public int skinId, weaponId, shieldId, helmetId, amorId, capeId, helpId, backId, accId, stoneId;
    //public string weapon1Kind, weapon2Kind;
    public float skillAtRate, skillHpRate, skillExp, skillGold, skillSpeed, skinAtRate;
    public int totalAt, totalHp, totalCri, totalAvoid;


    private void Start()
    {
        DataController.inst.LoadPlayer(this); 
        //UpdateAbility();
    }

    public void SetItemAbility(int at, int hp, int cri, int avoid)
    {
        itemAt += at;
        itemHp += hp;
        itemCri += cri;
        itemAvoid += avoid;

        //Debug.LogWarning("at : " + itemAt.ToString() + "\nhp : " + itemHp.ToString() + "\ncri : " + itemCri.ToString() + "\navoid : " + itemAvoid.ToString());
        if (character != null)
        { SetAbility(); }
        DataController.inst.SavePlayer(this);
    }

    public void GetExp(int getExp)
    {
        currentExp += getExp + (int)(getExp * skillExp);
        LevelUp();
        MainSceneController.inst.UpdateExp();
    }
    public void LevelUp()
    {
        if(currentExp >= maxExp)
        {
            SoundController.inst.UISound(12);
            DataController.inst.C_LevelUp++;
            DataController.inst.C_LevelUpFive++;

            currentExp -= maxExp;
            level++;
            GPGSManager.inst.ReportLevel(level);
            GetRandomStat();
            statPoint++;
            MainSceneController.inst.LevelUp(level);
            maxExp = Mathf.FloorToInt(10 * Mathf.Pow(1.2f, level));
            DataController.inst.SavePlayer(this);
        }
    }

    public void SetCharacter(Character _character)
    {
        character = _character;
        //character.anim.speed = 2f;            basic : 1     fast : 2      slow : 0.5
        SetAbility();
    }

    public void SetSpeed()
    { character.anim.speed = skillSpeed; }
    public void SetCombo()
    {
        character.combo = skillCombo; 
        character.anim.SetInteger("Attack_Combo", skillCombo);
    }

    public void SetAbility()
    {
        totalAt = statAt + itemAt + skillAt + skinAt;
        if (DataController.inst.IAPAt)
        { totalAt = (int)(totalAt * (skillAtRate + skinAtRate + 1 + AdMobController.inst.buffAt)); }
        else
        { totalAt = (int)(totalAt * (skillAtRate + skinAtRate + AdMobController.inst.buffAt)); }
        
        totalHp = statHp + itemHp + skillHp;
        if (DataController.inst.IAPHp)
        { totalHp = (int)(totalHp * (skillHpRate + 1)); }
        else
        { totalHp = (int)(totalHp * skillHpRate); }

        totalCri = (statCri + itemCri + skillCri) * AdMobController.inst.buffCri;
        
        totalAvoid = statAvoid + itemAvoid + skillAvoid;
        character.SetCharacterAbility(totalAt, totalHp, totalCri, totalAvoid, statLuckRate);
        
        SetCombo();
        SetSpeed();
    }

    public void SetCharacterImage()
    { character.SetCharacter(skinId, weaponId, shieldId, helmetId, amorId, capeId, helpId, backId); }

    public void GetTotaPower()
    {
        totalPower = (level * 10) + (totalAt * 2) + totalHp + ((totalCri + totalAvoid + statLuckRate) * 8);
        MainSceneController.inst.profilePowerText.text = string.Format("{0}", totalPower.ToString("N0"));
        GPGSManager.inst.ReportPower(totalPower);
    }

    public void UpdateAbility()
    {
        statAt = (statPower * 8) + 10;
        statCri = statDex;
        statAvoid = statDex;
        statHp = (statHealth * 15) + 50;
        statLuckRate = statLuck;
        if (character != null)
        { SetAbility(); }
        GetTotaPower();
        MainSceneController.inst.UpdateCharacterInfo();
    }

    public void GetRandomStat()
    {
        int r = Random.Range(0, 10);
        string str = string.Empty;
        if(r.Equals(0))
        { statPower++; str = string.Format("+{0}", Language.inst.strArray[32]); }
        else if (r.Equals(1))
        { statDex++; str = string.Format("+{0}", Language.inst.strArray[33]); }
        else if (r.Equals(2))
        { statHealth++; str = string.Format("+{0}", Language.inst.strArray[34]); }
        else if (r.Equals(3))
        { statLuck++; str = string.Format("+{0}", Language.inst.strArray[35]); }
        else if (r.Equals(4))
        { statPoint++; str = string.Format("+{0}", Language.inst.strArray[44]); }
        else if (r < 7)
        { itemAt++; str = "+1 Att"; }
        else
        { itemHp++; str = "+1 HP"; }
        
        UpdateAbility();
        DataController.inst.SavePlayer(this);
        ErrorSign.inst.SimpleSignSet(str, 0);
    }
    
    public void AddStat(int kind)
    {
        if (statPoint > 0)
        {
            statPoint--;
            SoundController.inst.UISound(10);
            if (kind.Equals(0))
            { statPower++; ErrorSign.inst.SimpleSignSet("+8 Att", 0); }
            else if (kind.Equals(1))
            { statDex++; ErrorSign.inst.SimpleSignSet("+1% Cri/Avoid", 0); }
            else if (kind.Equals(2))
            { statHealth++; ErrorSign.inst.SimpleSignSet("+15 Hp", 0); }
            else if (kind.Equals(3))
            { statLuck++; ErrorSign.inst.SimpleSignSet("+0.1% Luck", 0); }
            UpdateAbility();
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[43], 10); SoundController.inst.UISound(5); }
    }

    public void ResetStat()
    {
        int tmpNumb = 0;

        tmpNumb = statPower;
        statPower = 0;
        statPoint += tmpNumb;

        tmpNumb = statDex;
        statDex = 0;
        statPoint += tmpNumb;

        tmpNumb = statHealth;
        statHealth = 0;
        statPoint += tmpNumb;

        tmpNumb = statLuck;
        statLuck = 0;
        statPoint += tmpNumb;

        UpdateAbility();
        DataController.inst.SavePlayer(this);
    }

}
