using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RDG;

public class DataController : MonoBehaviour{
    public static DataController inst;
    public ES3File es3;
    public WaitForSeconds halfSec, quaterSec, zeroSec, zeroQuaterSec;
    public bool clearFlag, battleFlag;

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
        es3 = new ES3File("OverGeared.es3");
        es3.Sync();
    }

    private void Start()
    {
        clearFlag = false;
        battleFlag = false;
        halfSec = new WaitForSeconds(0.5f);
        zeroSec = new WaitForSeconds(0.1f);
        quaterSec = new WaitForSeconds(0.25f);
        zeroQuaterSec = new WaitForSeconds(0.025f);
        StartCoroutine(TimerCor());
    }

    public int difference, differenceI, differenceR, todayLoginButtonId, tipID;
    public Sprite loginItemSprite;
    public string loginContentStr;
    public bool isFirst = true, isLogin = false;
    public void GG(int numb)
    {
        GDad = UnityEngine.Random.Range(-100000000, 100000000);
        GMom = gold + numb - GDad;
        gold += numb;
    }
    public int GMom
    {
        get { return es3.Load("GMom", 0); }
        set { es3.Save<int>("GMom", value); }
    }
    public int GDad
    {
        get { return es3.Load("GDad", 0); }
        set { es3.Save<int>("GDad", value); }
    }
    public int gold
    {
        get { return es3.Load("gold", 500); }
        set
        {
            if ((GDad + GMom).Equals(value))
            { es3.Save<int>("gold", value); }
            else
            {
                difference = GDad + GMom - value;
                if (difference > -2 && difference < 2)
                { es3.Save<int>("gold", value); }
            }
        }
    }
    public void MaxG()
    {
        gold = 2000000000;
        GMom = 428902348;
        GDad = 1571097652;
    }
    public void MaxI()
    {
        iron = 10000000;
        IMom = 9098004;
        IDad = 901996;
    }
    public void MaxR()
    {
        ruby = 1000000;
        RMom = 273860;
        RDad = 726140;
    }
    public int ticket
    {
        get { return es3.Load("ticket", 3); }
        set { es3.Save<int>("ticket", value); }
    }

    public void II(int numb)
    {
        IDad = UnityEngine.Random.Range(-100000000, 100000000);
        IMom = iron + numb - IDad;
        iron += numb;
    }
    public int IMom
    {
        get { return es3.Load("IMom", 0); }
        set { es3.Save<int>("IMom", value); }
    }
    public int IDad
    {
        get { return es3.Load("IDad", 0); }
        set { es3.Save<int>("IDad", value); }
    }
    public int iron
    {
        get { return es3.Load("iron", 2); }
        set
        {
            if ((IDad + IMom).Equals(value))
            { es3.Save<int>("iron", value); }
            else
            {
                differenceI = IDad + IMom - value;
                if (difference > -2 && difference < 2)
                { es3.Save<int>("iron", value); }
            }
        }
    }

    public void RR(int numb)
    {
        RDad = UnityEngine.Random.Range(-100000000, 100000000);
        RMom = ruby + numb - RDad;
        ruby += numb;
    }
    public int RMom
    {
        get { return es3.Load("RMom", 0); }
        set { es3.Save<int>("RMom", value); }
    }
    public int RDad
    {
        get { return es3.Load("RDad", 0); }
        set { es3.Save<int>("RDad", value); }
    }
    public int ruby
    {
        get { return es3.Load("ruby", 0); }
        set
        {
            if ((RDad + RMom).Equals(value))
            { es3.Save<int>("ruby", value); }
            else
            {
                differenceR = RDad + RMom - value;
                if (difference > -2 && difference < 2)
                { es3.Save<int>("ruby", value); }
            }
        }
    }

    public string uid
    {
        get { return es3.Load("thisUserGoogleId", string.Empty); }
        set { es3.Save("thisUserGoogleId", value); }
    }

    public int highestPower
    {
        get { return es3.Load("highestPower", 0); }
        set
        {
            es3.Save("highestPower", value);
            GPGSManager.inst.ReportKill(value);
        }
    }

    public int buySkinCount
    {
        get { return es3.Load("buySkinCount", 1); }
        set { es3.Save("buySkinCount", value); }
    }
    public int buyLockCount
    {
        get { return es3.Load("buyLockCount", 0); }
        set { es3.Save("buyLockCount", value);}
    }
    public int buySkillLockCount
    {
        get { return es3.Load("buySkillLockCount", 0); }
        set { es3.Save("buySkillLockCount", value); }
    }

    public int field
    {
        get { return es3.Load("field", 0); }
        set { es3.Save("field", value); }
    }

    public int PrayCount
    {
        get { return es3.Load("PrayCount", 0); }
        set { es3.Save("PrayCount", value); }
    }
    public int KillCount
    {
        get { return es3.Load("KillCount", 0); }
        set { es3.Save("KillCount", value); }
    }
    public int PlayTimeCount
    {
        get { return es3.Load("PlayTimeCount", 1); }
        set { es3.Save("PlayTimeCount", value); }
    }


    public bool S_Combo
    {
        get { return es3.Load("S_Combo", true); }
        set { es3.Save<bool>("S_Combo", value); }
    }

    public bool agree
    {
        get { return es3.Load("agree", false); }
        set { es3.Save<bool>("agree", value); }
    }
    public bool S_AutoDelete
    {
        get { return es3.Load("S_AutoDelete", false); }
        set { es3.Save<bool>("S_AutoDelete", value); }
    }
    public bool S_Shake
    {
        get { return es3.Load("S_Shake", true); }
        set { es3.Save<bool>("S_Shake", value); }
    }
    public bool S_BGM
    {
        get { return es3.Load("S_BGM", true); }
        set { es3.Save<bool>("S_BGM", value); }
    }
    public bool S_Effect
    {
        get { return es3.Load("S_Effect", true); }
        set { es3.Save<bool>("S_Effect", value); }
    }
    public bool S_Damage
    {
        get { return es3.Load("S_Damage", true); }
        set { es3.Save<bool>("S_Damage", value); }
    }
    public bool S_Vibe
    {
        get { return es3.Load("S_Vibe", true); }
        set { es3.Save<bool>("S_Vibe", value); }
    }
    public bool S_Push
    {
        get { return es3.Load("S_Push", true); }
        set { es3.Save<bool>("S_Push", value); }
    }
    public bool S_JoyStick
    {
        get { return es3.Load("S_JoyStick", true); }
        set { es3.Save<bool>("S_JoyStick", value); }
    }

    public bool agreement
    {
        get { return es3.Load("agreement", false); }
        set { es3.Save<bool>("agreement", value); }
    }
    public bool viewTutorial
    {
        get { return es3.Load("viewTutorial", false); }
        set { es3.Save<bool>("viewTutorial", value); }
    }
    public bool review
    {
        get { return es3.Load("review", false); }
        set { es3.Save<bool>("review", value); }
    }
    //public bool adFlag
    //{
    //    get { return es3.Load("adFlag", true); }
    //    set { es3.Save<bool>("adFlag", value); }
    //}

    public string languageStr
    {
        get { return es3.Load("language", "Korean"); }
        set { es3.Save<string>("language", value); }
    }
    
    public void UpdateTipID()
    {
        tipID = UnityEngine.Random.Range(0, 11);
    }
    public int highestField
    {
        get { return es3.Load("highestField", 0); }
        set { es3.Save<int>("highestField", value); }
    }
    public int highestStage
    {
        get { return es3.Load("highestStage", 1); }
        set { es3.Save<int>("highestStage", value); }
    }
    public int highestLevel
    {
        get { return es3.Load("highestLevel", 1); }
        set { es3.Save<int>("highestLevel", value); }
    }
    public int highestScore
    {
        get { return es3.Load("highestScore", 1); }
        set { es3.Save<int>("highestScore", value); }
    }
    public int itemCount
    {
        get { return es3.Load("itemCount", 0); }
        set { es3.Save<int>("itemCount", value); }
    }

    public int storeStatCost
    {
        get { return es3.Load("storeStatCost", 1000); }
        set { es3.Save<int>("storeStatCost", value); }
    }

    public bool VipPass
    {
        get { return es3.Load("VipPass", false); }
        set { es3.Save<bool>("VipPass", value); }
    }
    

    //공,방,체,회피,치명,골드,경험치

    public int C_Ad
    {
        get { return es3.Load("C_Ad", 0); }
        set { es3.Save<int>("C_Ad", value);
            MainSceneController.inst.missions[9].SetValue();
        }
    }
    public int C_Upgrade
    {
        get { return es3.Load("C_Upgrade", 0); }
        set { es3.Save<int>("C_Upgrade", value);
            MainSceneController.inst.missions[15].SetValue();
        }
    }
    public int C_LevelUp
    {
        get { return es3.Load("C_LevelUp", 0); }
        set { es3.Save<int>("C_LevelUp", value);
            MainSceneController.inst.missions[11].SetValue();
        }
    }
    public int C_PaidGold
    {
        get { return es3.Load("C_PaidGold", 0); }
        set { es3.Save<int>("C_PaidGold", value);
            MainSceneController.inst.missions[12].SetValue();
        }
    }
    public int C_PaidRuby
    {
        get { return es3.Load("C_PaidRuby", 0); }
        set { es3.Save<int>("C_PaidRuby", value);
            MainSceneController.inst.missions[13].SetValue();
        }
    }
    public int C_Battle
    {
        get { return es3.Load("C_Play", 0); }
        set { es3.Save<int>("C_Play", value); }
    }
    public int C_TenMin
    {
        get { return es3.Load("C_TenMin", 0); }
        set
        {
            es3.Save<int>("C_TenMin", value);
            if (MainSceneController.inst != null)
            { MainSceneController.inst.missions[14].SetValue(); }
        }
    }

    public int buff_auto
    {
        get { return es3.Load("buff_auto", 0); }
        set { es3.Save("buff_auto", value); }
    }
    public int buff_dam
    {
        get { return es3.Load("buff_dam", 0); }
        set { es3.Save("buff_dam", value); }
    }
    public int buff_cri
    {
        get { return es3.Load("buff_cri", 0); }
        set { es3.Save("buff_cri", value); }
    }
    public int buff_gold
    {
        get { return es3.Load("buff_gold", 0); }
        set { es3.Save("buff_gold", value); }
    }

    public int C_Kill
    {
        get { return es3.Load("C_KillEnemy", 0); }
        set { es3.Save("C_KillEnemy", value); MainSceneController.inst.missions[16].SetValue(); }
    }
    public int C_KillBoss
    {
        get { return es3.Load("C_KillBoss", 0); }
        set { es3.Save("C_KillBoss", value); MainSceneController.inst.missions[17].SetValue(); }
    }

    public int C_LevelUpFive
    {
        get { return es3.Load("C_LevelUpFive", 0); }
        set { es3.Save("C_LevelUpFive", value); MainSceneController.inst.missions[0].SetValue(); }
    }
    public int C_SmithTen
    {
        get { return es3.Load("C_SmithTen", 0); }
        set { es3.Save("C_SmithTen", value); MainSceneController.inst.missions[1].SetValue(); }
    }
    public int C_MissionFive
    {
        get { return es3.Load("C_MissionFive", 0); }
        set { es3.Save("C_MissionFive", value); MainSceneController.inst.missions[2].SetValue(); }
    }
    public int C_AdFive
    {
        get { return es3.Load("C_AdFive", 0); }
        set { es3.Save("C_AdFive", value); MainSceneController.inst.missions[3].SetValue(); }
    }
    public int C_ChestFive
    {
        get { return es3.Load("C_ChestFive", 0); }
        set { es3.Save("C_ChestFive", value); MainSceneController.inst.missions[4].SetValue(); }
    }
    public int C_BlessTen
    {
        get { return es3.Load("C_BlessTen", 0); }
        set { es3.Save("C_BlessTen", value); MainSceneController.inst.missions[5].SetValue(); }
    }
    public int C_GetSkillTen
    {
        get { return es3.Load("C_GetSkillTen", 0); }
        set { es3.Save("C_GetSkillTen", value); MainSceneController.inst.missions[6].SetValue(); }
    }
    public int C_GetStatTen
    {
        get { return es3.Load("C_GetStatTen", 0); }
        set { es3.Save("C_GetStatTen", value); MainSceneController.inst.missions[7].SetValue(); }
    }
    public int C_IAP
    {
        get { return es3.Load("C_IAP", 0); }
        set { es3.Save("C_IAP", value); MainSceneController.inst.missions[8].SetValue(); }
    }



    public int smithTotalCount
    {
        get { return es3.Load("smithTotalCount", 0); }
        set { es3.Save<int>("smithTotalCount", value); }
    }
    //public int stageNumb
    //{
    //    get { return es3.Load("stageNumb", 0); }
    //    set { es3.Save<int>("stageNumb", value); }
    //}


    public int serverDate
    {
        get { return es3.Load("serverDate", 0); }
        set { es3.Save("serverDate", value); }
    }
    public int invenEmptyCount
    {
        get { return es3.Load("invenEmptyCount", 5); }
        set { es3.Save("invenEmptyCount", value); }
    }
    public double getServerTimestamp
    {
        get
        {
            string tmpStr = es3.Load("getServerTimestamp", "0");
            return Mathf.Floor(float.Parse(tmpStr));
        }
        set { es3.Save<string>("getServerTimestamp", value.ToString()); }
    }
    public bool loginCheck
    {
        get { return es3.Load("loginCheck", false); }
        set { es3.Save<bool>("loginCheck", value); }
    }

    public bool IAPEvent
    {
        get { return es3.Load("IAPEvent", false); }
        set { es3.Save("IAPEvent", value); }
    }
    public bool IAPEventPop
    {
        get { return es3.Load("IAPEventPop", false); }
        set { es3.Save("IAPEventPop", value); }
    }
    public bool IAPAt
    {
        get { return es3.Load("IAPAt", false); }
        set { es3.Save("IAPAt", value); }
    }
    public bool IAPHp
    {
        get { return es3.Load("IAPHp", false); }
        set { es3.Save("IAPHp", value); }
    }
    public bool IAPCri
    {
        get { return es3.Load("IAPCri", false); }
        set { es3.Save("IAPCri", value); }
    }
    public bool IAPAD
    {
        get { return es3.Load("IAPAD", false); }
        set { es3.Save("IAPAD", value); }
    }

    public float rateF
    {
        get { return es3.Load("rateF", 10f); }
        set { es3.Save<float>("rateF", value); }
    }
    public float rateE
    {
        get { return es3.Load("rateE", 35f); }
        set { es3.Save<float>("rateE", value); }
    }
    public float rateD
    {
        get { return es3.Load("rateD", 30f); }
        set { es3.Save<float>("rateD", value); }
    }
    public float rateC
    {
        get { return es3.Load("rateC", 15f); }
        set { es3.Save<float>("rateC", value); }
    }
    public float rateB
    {
        get { return es3.Load("rateB", 8f); }
        set { es3.Save<float>("rateB", value); }
    }
    public float rateA
    {
        get { return es3.Load("rateA", 1.38f); }
        set { es3.Save<float>("rateA", value); }
    }
    public float rateS
    {
        get { return es3.Load("rateS", 0.5f); }
        set { es3.Save<float>("rateS", value); }
    }
    public float rateSS
    {
        get { return es3.Load("rateSS", 0.1f); }
        set { es3.Save<float>("rateSS", value); }
    }
    public float rateSSS
    {
        get { return es3.Load("rateSSS", 0.019f); }
        set { es3.Save<float>("rateSSS", value); }
    }
    public float rateEx
    {
        get { return es3.Load("rateEx", 0.001f); }
        set { es3.Save<float>("rateEx", value); }
    }

    public float RateUpdate(int kind, float numb)
    {

        if (kind.Equals(0))
        {
            if (rateF >= numb)
            {
                rateF -= numb;
                rateE += numb;
                return numb;
            }
            else
            {
                float newnumb = rateF;
                rateE += newnumb;
                rateF = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(1))
        {
            if (rateE >= numb)
            {
                rateE -= numb;
                rateD += numb;
                return numb;
            }
            else
            {
                float newnumb = rateE;
                rateD += newnumb;
                rateE = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(2))
        {
            if (rateD >= numb)
            {
                rateD -= numb;
                rateC += numb;
                return numb;
            }
            else
            {
                float newnumb = rateC;
                rateD += newnumb;
                rateC = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(3))
        {
            if (rateC >= numb)
            {
                rateC -= numb;
                rateB += numb;
                return numb;
            }
            else
            {
                float newnumb = rateC;
                rateB += newnumb;
                rateC = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(4))
        {
            if (rateB >= numb)
            {
                rateB -= numb;
                rateA += numb;
                return numb;
            }
            else
            {
                float newnumb = rateB;
                rateA += newnumb;
                rateB = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(5))
        {
            if (rateA >= numb)
            {
                rateA -= numb;
                rateS += numb;
                return numb;
            }
            else
            {
                float newnumb = rateA;
                rateS += newnumb;
                rateA = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(6))
        {
            if (rateS >= numb)
            {
                rateS -= numb;
                rateSS += numb;
                return numb;
            }
            else
            {
                float newnumb = rateS;
                rateSS += newnumb;
                rateS = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(7))
        {
            if (rateSS >= numb)
            {
                rateSS -= numb;
                rateSSS += numb;
                return numb;
            }
            else
            {
                float newnumb = rateSS;
                rateSSS += newnumb;
                rateSS = 0f;
                return newnumb;
            }
        }
        else if (kind.Equals(8))
        {
            if (rateSSS >= numb)
            {
                rateSSS -= numb;
                rateEx += numb;
                return numb;
            }
            else
            {
                float newnumb = rateSSS;
                rateEx += newnumb;
                rateSSS = 0f;
                return newnumb;
            }
        }
        else
        { return numb; }
    }

    public float[] RateSet()
    {
        float r = UnityEngine.Random.Range(0f, 100f), numb = 0f;
        float[] rateArr = { rateF, rateE, rateD, rateC, rateB, rateA, rateS, rateSS, rateSSS, rateEx };
        for(int i = 0; i < 10; i++)
        {
            numb += rateArr[i];
            if(r <= numb)
            {
                if (i.Equals(0))
                { return new float[] { RateUpdate(0, 2f), 10 }; }
                else if (i.Equals(1))
                { return new float[] { RateUpdate(1, 1f), 9 }; }
                else if (i.Equals(2))
                { return new float[] { RateUpdate(2, 1f), 8 }; }
                else if (i.Equals(3))
                { return new float[] { RateUpdate(3, 1f), 7 }; }
                else if (i.Equals(4))
                { return new float[] { RateUpdate(4, 0.5f), 6 }; }
                else if (i.Equals(5))
                { return new float[] { RateUpdate(5, 0.5f), 5 }; }
                else if (i.Equals(6))
                { return new float[] { RateUpdate(6, 0.2f), 4 }; }
                else if (i.Equals(7))
                { return new float[] { RateUpdate(7, 0.1f), 3 };  }
                else if (i.Equals(8))
                { return new float[] { RateUpdate(8, 0.05f), 2 }; }
                else
                { return new float[] { -1, 1 }; }
            }
        }
        return new float[] { -1, 1 };
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int battleButtonNumb;

    public void SavePet(Pet pet)
    {
        es3.Save(pet.id + "pet_level", pet.level);
    }
    public void LoadPet(Pet pet)
    {
        pet.level = es3.Load(pet.id + "pet_level", 0);
    }

    public void SaveMission(Mission obj)
    {
        string key = obj.id.ToString();

        es3.Save(key + "mission_maxCount", obj.maxCount);
        es3.Save(key + "mission_count", obj.count);
        es3.Save(key + "mission_isGet", obj.isGet);
    }
    public void LoadMission(Mission obj)
    {
        string key = obj.id.ToString();

        obj.maxCount = es3.Load(key + "mission_maxCount", 0);
        obj.count = es3.Load(key + "mission_count", 0);
        obj.isGet = es3.Load(key + "mission_isGet", false);
    }

    public void SaveSkill(SkillButton skill)
    {
        es3.Save(skill.id.ToString() + "Skill_numb", skill.numb);
        es3.Save(skill.id.ToString() + "Skill_level", skill.level);
        es3.Save(skill.id.ToString() + "Skill_count", skill.count);
        es3.Save(skill.id.ToString() + "Skill_isPurchased", skill.isPurchased);
    }
    public void LoadSkill(SkillButton skill)
    {
        skill.numb = es3.Load(skill.id.ToString() + "Skill_numb", -1);
        skill.level = es3.Load(skill.id.ToString() + "Skill_level", 1);
        skill.count = es3.Load(skill.id.ToString() + "Skill_count", 0);

        if (skill.id < 10)
        { skill.isPurchased = es3.Load(skill.id.ToString() + "Skill_isPurchased", true); }
        else if (skill.id > 19 && skill.id < 25)
        { skill.isPurchased = es3.Load(skill.id.ToString() + "Skill_isPurchased", true); }
        else
        { skill.isPurchased = es3.Load(skill.id.ToString() + "Skill_isPurchased", false); }
    }
    public void SaveSkillSlot(SkillSlot skill)
    {
        es3.Save(skill.id.ToString() + "SkillSlot_level", skill.level);
        es3.Save(skill.id.ToString() + "SkillSlot_numb", skill.numb);
    }
    public void LoadSkillSlot(SkillSlot skill)
    {
        skill.level = es3.Load(skill.id.ToString() + "SkillSlot_level", 1);
        skill.numb = es3.Load(skill.id.ToString() + "SkillSlot_numb", -1);
    }
    public void SaveSmithButton(SmithButton smith)
    { es3.Save("SMB" + smith.id, smith.count); }
    public void LoadSmithButton(SmithButton smith)
    { smith.count = es3.Load("SMB" + smith.id, 1); }

    public void SaveEquip(EquipSlot item)
    {
        es3.Save(item.id.ToString() + "Equip_rate", item.upgradeRate);
        es3.Save(item.id.ToString() + "Equip_numb", item.numb);
        es3.Save(item.id.ToString() + "Equip_level", item.level);
        es3.Save(item.id.ToString() + "Equip_kind", item.itemKind);
        es3.Save(item.id.ToString() + "Equip_rank", item.rank);

        es3.Save(item.id.ToString() + "Equip_at", item.at);
        es3.Save(item.id.ToString() + "Equip_hp", item.hp);
        es3.Save(item.id.ToString() + "Equip_cri", item.cri);
        es3.Save(item.id.ToString() + "Equip_avoid", item.avoid);
        es3.Save(item.id.ToString() + "Equip_gold", item.gold);
        es3.Save(item.id.ToString() + "Equip_luck", item.luck);
    }
    public void LoadEquip(EquipSlot item)
    {
        item.upgradeRate = es3.Load(item.id.ToString() + "Equip_rate", 100);
        item.numb = es3.Load(item.id.ToString() + "Equip_numb", -1);
        item.level = es3.Load(item.id.ToString() + "Equip_level", 0);
        item.itemKind = es3.Load(item.id.ToString() + "Equip_kind", "");
        item.rank = es3.Load(item.id.ToString() + "Equip_rank", "");

        item.at = es3.Load(item.id.ToString() + "Equip_at", 0);
        item.hp = es3.Load(item.id.ToString() + "Equip_hp", 0);
        item.cri = es3.Load(item.id.ToString() + "Equip_cri", 0);
        item.avoid = es3.Load(item.id.ToString() + "Equip_avoid", 0);
        item.gold = es3.Load(item.id.ToString() + "Equip_gold", 0);
        item.luck = es3.Load(item.id.ToString() + "Equip_luck", 0);
    }

    public void SaveInven(InvenSlot item)
    {
        es3.Save(item.id.ToString() + "Inven_rate", item.upgradeRate);
        es3.Save(item.id.ToString() + "Inven_isPurchased", item.isPurchased);

        es3.Save(item.id.ToString() + "Inven_numb", item.numb);
        es3.Save(item.id.ToString() + "Inven_level", item.level);
        es3.Save(item.id.ToString() + "Inven_kind", item.itemKind);
        es3.Save(item.id.ToString() + "Inven_rank", item.rank);

        es3.Save(item.id.ToString() + "Inven_at", item.at);
        es3.Save(item.id.ToString() + "Inven_hp", item.hp);
        es3.Save(item.id.ToString() + "Inven_cri", item.cri);
        es3.Save(item.id.ToString() + "Inven_avoid", item.avoid);
        es3.Save(item.id.ToString() + "Inven_gold", item.gold);
        es3.Save(item.id.ToString() + "Inven_luck", item.luck);
    }
    public void LoadInven(InvenSlot item)
    {
        if (item.id < 5)
        { item.isPurchased = es3.Load(item.id.ToString() + "Inven_isPurchased", true); }
        else
        { item.isPurchased = es3.Load(item.id.ToString() + "Inven_isPurchased", false); }

        item.upgradeRate = es3.Load(item.id.ToString() + "Inven_rate", 100);
        item.numb = es3.Load(item.id.ToString() + "Inven_numb", -1);
        item.level = es3.Load(item.id.ToString() + "Inven_level", 0);
        item.itemKind = es3.Load(item.id.ToString() + "Inven_kind", "");
        item.rank = es3.Load(item.id.ToString() + "Inven_rank", "");

        item.at = es3.Load(item.id.ToString() + "Inven_at", 0);
        item.hp = es3.Load(item.id.ToString() + "Inven_hp", 0);
        item.cri = es3.Load(item.id.ToString() + "Inven_cri", 0);
        item.avoid = es3.Load(item.id.ToString() + "Inven_avoid", 0);
        item.gold = es3.Load(item.id.ToString() + "Inven_gold", 0);
        item.luck = es3.Load(item.id.ToString() + "Inven_luck", 0);
    }

    public void GetLoginAward(int today)
    {
        if (!today.Equals(serverDate))
        {
            //출석보상 지급
            PlayTimeCount++;
            loginCheck = true;
            IAPEventPop = false;
            serverDate = today;
            ticket = 3;
            C_Ad = 0;
            C_Battle = 0;
            C_LevelUp = 0;
            C_PaidGold = 0;
            C_PaidRuby = 0;
            C_TenMin = 0;
            C_Upgrade = 0;
            C_Kill = 0;
            C_KillBoss = 0;

            for (int i = 9; i < 18; i++)
            {
                es3.Save(i.ToString() + "mission_isGet", false);
                MainSceneController.inst.missions[i].isGet = false;
                MainSceneController.inst.missions[i].StartSet();
            }
        }
        AwardController.inst.FindNowLogin();
        es3.Sync();
    }

    public void LoadLoginButton(LoginButton login)
    { login.isGet = es3.Load(login.id.ToString() + "LoginGet", false); }
    public void SaveLoginButton(LoginButton login)
    { es3.Save(login.id.ToString() + "LoginGet", login.isGet); }

    public void LoadBattleButton(BattleButton battle)
    { battle.isClear = es3.Load(battle.id.ToString() + "battleClear", false); }
    public void SaveBattleButton(BattleButton battle)
    { es3.Save(battle.id.ToString() + "battleClear", battle.isClear); }

    public void LoadSkin(SkinButton button)
    {
        if (button.id.Equals(0))
        { button.isPurchased = es3.Load(button.id + "SkinBuy", true); }
        else
        { button.isPurchased = es3.Load(button.id + "SkinBuy", false); }
    }
    public void SaveSkin(SkinButton button)
    { es3.Save(button.id + "SkinBuy", button.isPurchased); }

    //public void GetServerIdleMoney(double newTime)
    //{
    //    if (getServerTimestamp > 0)
    //    {
    //        if (newTime > getServerTimestamp + 5)
    //        {
    //            double tmpTime = newTime - getServerTimestamp;
    //            if(tmpTime > 720)
    //            { tmpTime = 720; }
    //            //double newgold = storeGoldCount * tmpTime;
    //            //MainSceneController.inst.ServerTimeGoldGet(tmpTime, newgold);
    //            //GG(newgold);
    //            //MainSceneController.inst.UpdateGold();
    //            getServerTimestamp = newTime;
    //            es3.Sync();
    //        }
    //    }
    //}

    //public void LoadChallenge(ChallengeController key)
    //{
    //    key.id = es3.Load<int>("challengeId", 0);
    //    key.maxNumb = es3.Load<int>("challengeMaxNumb", 1);
    //}
    //public void SaveChallenge(ChallengeController key)
    //{
    //    es3.Save<int>("challengeId", key.id);
    //    es3.Save<int>("challengeMaxNumb", key.maxNumb);
    //}

    //public void LoadGiftButton(GiftButton gift)
    //{
    //    gift.getGift = es3.Load<bool>("gift_getGift" + gift.id.ToString(), false);
    //}
    //public void SaveGiftButton(GiftButton gift)
    //{ es3.Save<bool>("gift_getGift" + gift.id.ToString(), gift.getGift); }
    public void LoadAward(levelAward award)
    {
        if (award.isVip)
        { award.isGet = es3.Load("VIPLevelAwardGet" + award.id.ToString(), false); }
        else
        { award.isGet = es3.Load("levelAwardGet" + award.id.ToString(), false); }
    }
    public void SaveAward(levelAward award)
    {
        if (award.isVip)
        { es3.Save("VIPLevelAwardGet" + award.id.ToString(), award.isGet); }
        else
        { es3.Save("levelAwardGet" + award.id.ToString(), award.isGet); es3.Sync(); }
    }

    public void LoadPlayerCharacter(PlayerController player)
    {
        player.skinId = es3.Load("CS", 0);
        player.weaponId = es3.Load("CW1", -1);
        player.shieldId = es3.Load("CW2", -1);
        player.helmetId = es3.Load("CH", -1);
        player.amorId = es3.Load("CA", -1);
        player.backId = es3.Load("CB", -1);
        player.capeId = es3.Load("CC", -1);
        player.helpId = es3.Load("CH1", -1);
        player.accId = es3.Load("CA1", -1);
        player.stoneId = es3.Load("CStone", -1);
    }
    public void SavePlayerCharacter(PlayerController player)
    {
        es3.Save("CS", player.skinId);
        es3.Save("CW1", player.weaponId);
        es3.Save("CW2", player.shieldId);
        es3.Save("CH", player.helmetId);
        es3.Save("CA", player.amorId);
        es3.Save("CB", player.backId);
        es3.Save("CC", player.capeId);
        es3.Save("CH1", player.helpId);
        es3.Save("CA1", player.accId);
        es3.Save("CStone", player.stoneId);
        es3.Sync();
    }

    public void LoadPlayer(PlayerController player)
    {
        player.level = es3.Load("playerLevel", 1);
        player.statPoint = es3.Load("playerStat", 0);

        player.maxExp = es3.Load("maxExp", 10);
        player.currentExp = es3.Load("currentExp", 0);

        player.statPower = es3.Load("playerPower", 0);
        player.statDex = es3.Load("playerDex", 0);
        player.statHealth = es3.Load("playerHealth", 0);
        player.statLuck = es3.Load("playerWiz", 0);

        player.itemAt = es3.Load("playerItemAt", 0);
        player.itemHp = es3.Load("playerItemHp", 0);
        player.itemCri = es3.Load("playerItemCri", 0);
        player.itemAvoid = es3.Load("playerItemAvoid", 0);
        player.skillAt = es3.Load("playerSkillAt", 0);
        player.skillHp = es3.Load("playerSkillHp", 0);
        player.skillAtRate = es3.Load("playerSkillAtRate", 1f);
        player.skillHpRate = es3.Load("playerSkillHpRate", 1f);
        player.skillCri = es3.Load("playerSkillCri", 0);
        player.skillAvoid = es3.Load("playerSkillAvoid", 0);
        player.skillGold = es3.Load("playerSkillGold", 1f);
        player.skillCombo = es3.Load("playerSkillCombo", 0);
        player.skillSpeed = es3.Load("playerSkillSpeed", 1f);
        player.skillExp = es3.Load("playerSkillExp", 1f);
        player.skinAtRate = es3.Load("playerSkinAtRate", 0f);
        player.skinAt = es3.Load("playerSkinAt", 0);
        LoadPlayerCharacter(player);
    }
    public void SavePlayer(PlayerController player)
    {
        es3.Save("playerLevel", player.level);
        es3.Save("playerStat", player.statPoint);

        es3.Save("maxExp", player.maxExp);
        es3.Save("currentExp", player.currentExp);

        es3.Save("playerPower", player.statPower);
        es3.Save("playerDex", player.statDex);
        es3.Save("playerHealth", player.statHealth);
        es3.Save("playerWiz", player.statLuck);

        es3.Save("playerItemAt", player.itemAt);
        es3.Save("playerItemHp", player.itemHp);
        es3.Save("playerItemCri", player.itemCri);
        es3.Save("playerItemAvoid", player.itemAvoid);
        es3.Save("playerSkillAt", player.skillAt);
        es3.Save("playerSkillHp", player.skillHp);
        es3.Save("playerSkillAtRate", player.skillAtRate);
        es3.Save("playerSkillHpRate", player.skillHpRate);
        es3.Save("playerSkillCri", player.skillCri);
        es3.Save("playerSkillAvoid", player.skillAvoid);
        es3.Save("playerSkillGold", player.skillGold);
        es3.Save("playerSkillCombo", player.skillCombo);
        es3.Save("playerSkillSpeed", player.skillSpeed);
        es3.Save("playerSkillExp", player.skillExp);
        es3.Save("playerSkinAtRate", player.skinAtRate);
        es3.Save("playerSkinAt", player.skinAt);
        SavePlayerCharacter(player);

        es3.Sync();
    }

    public void SaveLogin(LoginButton login)
    {
        es3.Save<bool>(login.id.ToString() + "isGet", login.isGet);
        es3.Sync();
    }
    public void LoadLogin(LoginButton login)
    {
        login.isGet = es3.Load(login.id.ToString() + "isGet", false);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    //public void TotalAtSet(PlayerController player)
    //{
    //    //전투력 = (레벨X100) + (공격력 * 치명타율 * 0.5) + (방어력 * 회프율) + (체력/10)
    //    //int at = (int)(player.at * (book_at + bookHave_at + 100) * 0.01f);
    //    //int def = (int)(player.def * (book_def + bookHave_def + 100) * 0.01f);
    //    //int hp = (int)(player.maxHp * (book_hp + bookHave_hp + 100) * 0.01f);
    //    //int cri = player.cri + book_cri + bookHave_cri;
    //    //int avoid = player.avoid + book_avoid + bookHave_avoid;

    //    totalCharacterAt = (player.level * 100) + (int)(at * cri * 0.5f) + (def * avoid) + (int)(hp * 0.1f);
    //    //MainSceneController.inst.totalAtText.text = totalCharacterAt.ToString();
    //    SaveLeaderboardAt(player.level);
    //}

    public void Vibrate(long time)
    {
        if (S_Vibe.Equals(true))
        {
            if (time > 0)
            { Vibration.Vibrate(time); }
        }
    }

    IEnumerator TimerCor()
    {
        WaitForSeconds min = new WaitForSeconds(60f);
        while(true)
        {
            yield return min;
            es3.Sync();
            C_TenMin++;
        }
    }

    public void LoadMainScene()
    {
        es3.Sync();
        SceneManager.LoadScene(1);
        GC.Collect();
    }
    public void LoadBattleScene()
    {
        es3.Sync();
        SceneManager.LoadScene(2);
        GC.Collect();
    }
    public void LoadReScene()
    {
        es3.Sync();
        SceneManager.LoadScene(3);
        GC.Collect();
    }
    public void LoadHellScene()
    {
        es3.Sync();
        SceneManager.LoadScene(4);
        GC.Collect();
    }

}
