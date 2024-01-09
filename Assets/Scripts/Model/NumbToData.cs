using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumbToData : MonoBehaviour
{
    private static readonly string[] format = {"K", "M", "B", "T",
                                               "aa", "ab", "ac", "ad", "ae",
                                               "af", "ag", "ah", "ai", "aj",
                                               "ak", "al", "am", "an", "ao",
                                               "ap", "aq", "ar", "as", "at",
                                               "au", "av", "aw", "ax", "ay", "az",
                                               "ba", "bb", "bc", "bd", "be",
                                               "bf", "bg", "bh", "bi", "bj",
                                               "bk", "bl", "bm", "bn", "bo",
                                               "bp", "bq", "br", "bs", "bt",
                                               "bu", "bv", "bw", "bx", "by", "bz",
                                               "ca", "cb", "cc", "cd", "ce",
                                               "cf", "cg", "ch", "ci", "cj",
                                               "ck", "cl", "cm", "cn", "co",
                                               "cp", "cq", "cr", "cs", "ct",
                                               "cu", "cv", "cw", "cx", "cy", "cz"};

    public static int RankToInt(string rank)
    {
        if (rank.Equals("F"))
        { return 1; }
        else if (rank.Equals("E"))
        { return 2; }
        else if (rank.Equals("D"))
        { return 3; }
        else if (rank.Equals("C"))
        { return 4; }
        else if (rank.Equals("B"))
        { return 5; }
        else if (rank.Equals("A"))
        { return 6; }
        else if (rank.Equals("S"))
        { return 7; }
        else if (rank.Equals("SS"))
        { return 8; }
        else if (rank.Equals("SSS"))
        { return 9; }
        else if (rank.Equals("Ex"))
        { return 10; }
        else if(rank.Equals("God"))
        { return 15; }
        else
        { return 1; }
    }
    public static string NumbToRankString(float rank)
    {
        string tmpStr = string.Empty;
        if (rank.Equals(10))
        { tmpStr = "F"; }
        else if (rank.Equals(9))
        { tmpStr = "E"; }
        else if (rank.Equals(8))
        { tmpStr = "D"; }
        else if (rank.Equals(7))
        { tmpStr = "C"; }
        else if (rank.Equals(6))
        { tmpStr = "B"; }
        else if (rank.Equals(5))
        { tmpStr = "A"; }
        else if (rank.Equals(4))
        { tmpStr = "S"; }
        else if (rank.Equals(3))
        { tmpStr = "SS"; }
        else if (rank.Equals(2))
        { tmpStr = "SSS"; }
        else if (rank.Equals(1))
        { tmpStr = "Ex"; }
        else
        { tmpStr = "F"; }
        return tmpStr;
    }

    public static string GetInt(int numb)
    {
        string tmpString;

        string data = Mathf.FloorToInt(numb).ToString("N0");
        string[] splited = data.Split(',');
        if (splited.Length == 1)
        { return tmpString = string.Format("<b>{0}</b>", splited[0]); }
        else
        {
            char[] last = splited[1].ToCharArray();
            return tmpString = string.Format("<b>{0}</b><size=30>.{1}</size><b>{2}</b>", splited[0], last[0], format[splited.Length - 2]);
        }
    }
    public static string GetIntGold(int numb)
    {
        if (numb > 9999999)
        {
            string tmpString;

            string data = numb.ToString("N0");
            string[] splited = data.Split(',');
            if (splited.Length == 1)
            { return tmpString = string.Format("<b>{0}</b>", splited[0]); }
            else
            {
                char[] last = splited[1].ToCharArray();
                return tmpString = string.Format("<b>{0}</b><size=30>.{1}{2}</size><b>{3}</b>", splited[0], last[0], last[1], format[splited.Length - 2]);
            }
        }
        else
        { string data = numb.ToString("N0"); return data; }
    }

    //oneWeapon 0~129   twoWeapon 0~69  shield 0~36    helmet 0~ 98     amor 0~159      cape 0~14       acc 0~10

    //public static void GetSkillInfo(int level , int kind, Text skillNameText, Text skillContentsText)
    //{

    //    if (kind < 10)
    //    { skillNameText.text = string.Format("<b>{0}</b> <size=15>lv.{1}\nPassive Skill</size>", Language.inst.skillArr[kind], level); }
    //    else
    //    { skillNameText.text = string.Format("<b>{0}</b> <size=15>lv.{1}\nActive Skill</size>", Language.inst.skillArr[kind], level); }
    //    skillContentsText.text = Language.inst.SetSkillContentsLanguae(level, kind);
    //}

    public static void GetAbilityStr(InvenSlot item, Text abilityText)
    {
        string tmpStr = string.Empty;
        if (!item.at.Equals(0))
        { tmpStr += string.Format("{0} +{1}\n", Language.inst.strArray[97], item.at); }
        if (!item.hp.Equals(0))
        { tmpStr += string.Format("{0} +{1}\n", Language.inst.strArray[98], item.hp); }
        if (!item.cri.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[99], item.cri); }
        if (!item.avoid.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[100], item.avoid); }
        if (!item.gold.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[118], item.gold); }
        if (!item.luck.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[22], item.luck); }
        abilityText.text = tmpStr;
    }
    public static void GetEquipAbilityStr(EquipSlot item, Text abilityText)
    {
        string tmpStr = string.Empty;
        if (!item.at.Equals(0))
        { tmpStr += string.Format("{0} +{1}\n", Language.inst.strArray[97], item.at); }
        if (!item.hp.Equals(0))
        { tmpStr += string.Format("{0} +{1}\n", Language.inst.strArray[98], item.hp); }
        if (!item.cri.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[99], item.cri); }
        if (!item.avoid.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[100], item.avoid); }
        if (!item.gold.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[118], item.gold); }
        if (!item.luck.Equals(0))
        { tmpStr += string.Format("{0} +{1}%\n", Language.inst.strArray[22], item.luck); }
        abilityText.text = tmpStr;
    }
    public static void GetAbility(InvenSlot item, float rank)
    {
        string kind = item.itemKind;
        int rankNumb = (int)((rank - 11) * -1);
        rankNumb *= rankNumb;
        item.at = 0;
        item.hp = 0;
        item.cri = 0;
        item.avoid = 0;
        item.luck = 0;
        item.gold = 0;

        if (kind.Equals("weapon"))
        { item.at = (int)(18 * rankNumb * Random.Range(0.85f, 1.15f)); }
        else if (kind.Equals("amor"))
        { item.hp = (int)(60 * rankNumb * Random.Range(0.85f, 1.15f)); }
        else if (kind.Equals("back"))
        {
            item.at = (int)(8 * rankNumb * Random.Range(0.85f, 1.15f));
            item.cri = (int)(2 * rankNumb * Random.Range(0.85f, 1.15f));
        }
        else if (kind.Equals("cape"))
        {
            item.at = (int)(5 * rankNumb * Random.Range(0.85f, 1.15f));
            item.avoid = (int)(1 * rankNumb * Random.Range(0.85f, 1.15f));
        }
        else if (kind.Equals("stone"))
        {
            item.at = (int)(8 * rankNumb * Random.Range(0.85f, 1.15f));
            item.hp = (int)(15 * rankNumb * Random.Range(0.85f, 1.15f));
            item.cri = (int)(1 * rankNumb * Random.Range(0.85f, 1.15f));
        }
        else if (kind.Equals("shield"))
        {
            item.avoid = (int)(2 * rankNumb * Random.Range(0.85f, 1.15f));
            item.hp = (int)(20 * rankNumb * Random.Range(0.85f, 1.15f));
        }
        else if (kind.Equals("helmet"))
        {
            item.at = (int)(5 * rankNumb * Random.Range(0.85f, 1.15f));
            item.hp = (int)(12 * rankNumb * Random.Range(0.85f, 1.15f));
        }
        else if (kind.Equals("helper"))
        {
            int r = Random.Range(0, 2);
            if (r.Equals(1))
            { item.at = (int)(6 * rankNumb * Random.Range(0.85f, 1.15f)); }
            else
            { item.hp = (int)(20 * rankNumb * Random.Range(0.85f, 1.15f)); }
        }
        else if (kind.Equals("acc"))
        {
            int r = Random.Range(0, 2);
            if (r.Equals(1))
            { item.at = (int)(4 * rankNumb * Random.Range(0.85f, 1.15f)); }
            else
            { item.cri = (int)(1 * rankNumb * Random.Range(0.85f, 1.15f)); }
        }
        GetAbilityStr(item, SmithController.inst.abilityText);
    }
    public static Color RankColor(string rank)
    {
        Color color = Color.white;
        if(rank.Equals("F"))
        { color = Color.gray ; }
        else if (rank.Equals("E"))
        { color = Color.gray ; }
        else if (rank.Equals("D"))
        { color = Color.white ; }
        else if (rank.Equals("C"))
        { color = Color.green ; }
        else if (rank.Equals("B"))
        { color = Color.blue ; }
        else if (rank.Equals("A"))
        { color = Color.yellow ; }
        else if (rank.Equals("S"))
        { color = Color.red ; }
        else if (rank.Equals("SS"))
        { color = Color.cyan ; }
        else if (rank.Equals("SSS"))
        { color = Color.Lerp(Color.cyan, Color.magenta, 0.5f) ; }
        else if (rank.Equals("Ex"))
        { color = Color.magenta ; }
        else if (rank.Equals("God"))
        { color = Color.yellow ; }
        return color;
    }
    public static void GetItem(string kind, GetItem pop, int id, bool autoDelete)
    {
        InvenSlot slot = ItemSlotController.inst.emptySlot;
        slot.itemKind = kind;

        float[] floatArr = DataController.inst.RateSet();
        string nextRank = string.Empty;
        float before = 0f, after = 0f;


        if (floatArr[1].Equals(10))
        {
            before = DataController.inst.rateF;
            after = DataController.inst.rateE;
        }
        else if (floatArr[1].Equals(9))
        {
            before = DataController.inst.rateE;
            after = DataController.inst.rateD;
        }
        else if (floatArr[1].Equals(8))
        {
            before = DataController.inst.rateD;
            after = DataController.inst.rateC;
        }
        else if (floatArr[1].Equals(7))
        {
            before = DataController.inst.rateC;
            after = DataController.inst.rateB;
        }
        else if (floatArr[1].Equals(6))
        {
            before = DataController.inst.rateB;
            after = DataController.inst.rateA;
        }
        else if (floatArr[1].Equals(5))
        {
            before = DataController.inst.rateA;
            after = DataController.inst.rateS;
        }
        else if (floatArr[1].Equals(4))
        {
            before = DataController.inst.rateS;
            after = DataController.inst.rateSS;
        }
        else if (floatArr[1].Equals(3))
        {
            before = DataController.inst.rateSS;
            after = DataController.inst.rateSSS;
        }
        else if (floatArr[1].Equals(2))
        {
            before = DataController.inst.rateSSS;
            after = DataController.inst.rateEx;
        }
        slot.rank = NumbToRankString(floatArr[1]);
        nextRank = NumbToRankString(floatArr[1] - 1);

        if (!floatArr[1].Equals(1))
        {
            SmithController.inst.beforeRateText.text = string.Format("{0} : {1} -{2} (%)", slot.rank, before + floatArr[0], floatArr[0]);
            SmithController.inst.afterRateText.text = string.Format("{0} : {1} +{2} (%)", nextRank, after - floatArr[0], floatArr[0]);
        }
        else
        {
            SmithController.inst.beforeRateText.text = string.Empty;
            SmithController.inst.afterRateText.text = string.Empty;
        }


        slot.numb = GetItemNumb(slot.rank, kind);
        GetAbility(slot, floatArr[1]);
        slot.UpdateUI();
        DataController.inst.SaveInven(slot);
        GetItemUpdate(pop, slot);
        ItemSlotController.inst.FindEmptySlot();


        string myRank = ItemSlotController.inst.equipSlots[id].rank;
        int myRankNumb = (RankToInt(myRank) - 11) * -1;
        if (floatArr[1] >= myRankNumb && !floatArr[1].Equals(1))
        {
            if (autoDelete)
            {
                ItemSlotController.inst.selectInvenSlot = slot;
                GetItemUpdate(pop, slot);
                ItemSlotController.inst.AutoDelete(slot.rank);
            }
        }
        else if( floatArr[1] < myRankNumb)
        { GetItemUpdate(pop, slot, true); }
    }

    public static void FreeGetItem(GetItem getItem, string kind, string rank)
    {
        InvenSlot slot = ItemSlotController.inst.emptySlot;
        slot.itemKind = kind;
        slot.rank = rank;
        slot.numb = GetItemNumb(rank, kind);

        float rankInt = 11;
        rankInt = (rankInt - 11) * -1;
        GetAbility(slot, rankInt);

        slot.UpdateUI();
        DataController.inst.SaveInven(slot);

        GetItemUpdate(getItem, slot);

        DataController.inst.invenEmptyCount--;
        ItemSlotController.inst.FindEmptySlot();
    }

    public static void GetItemUpdate(GetItem pop, InvenSlot slot, bool flag = false)
    {
		if (pop.particle != null)
		{
			if (flag)
			{ pop.particle.SetActive(true); }
			else
			{ pop.particle.SetActive(false); }
		}
        slot.rankText.text = slot.rank;
        slot.rankText.color = RankColor(slot.rank);
        pop.rankText.text = slot.rank;
        pop.rankText.color = slot.rankText.color;
        pop.itemImage.sprite = slot.itemImage.sprite;
    }
    

    
    public static int SetNumb(string rank, int F, int E, int D, int C, int B, int A, int S, int SS, int SSS, int Ex)
    {
        if (rank.Equals("F"))
        { return F; }
        else if (rank.Equals("E"))
        { return E; }
        else if (rank.Equals("D"))
        { return D; }
        else if (rank.Equals("C"))
        { return C; }
        else if (rank.Equals("B"))
        { return B; }
        else if (rank.Equals("A"))
        { return A; }
        else if (rank.Equals("S"))
        { return S; }
        else if (rank.Equals("SS"))
        { return SS; }
        else if (rank.Equals("SSS"))
        { return SSS; }
        else if (rank.Equals("Ex"))
        { return Ex; }
        else
        { return 0; }
    }

    public static void GetGodItem(GetItem getItem)
    {
        InvenSlot slot = ItemSlotController.inst.emptySlot;
        int r = Random.Range(0, 4);
        if(r.Equals(0))
        {
            slot.itemKind = "weapon";
            slot.numb = 130;
            slot.numb = 131;
            slot.numb = 132;
        }
        else if (r.Equals(1))
        {
            slot.itemKind = "amor";
            slot.numb = 160;
            slot.numb = 161;
            slot.numb = 162;
        }
        else if (r.Equals(2))
        {
            slot.itemKind = "shield";
            slot.numb = 37;
        }
        else
        {
            slot.itemKind = "helmet";
            slot.numb = 99;
            slot.numb = 100;
        }
        slot.rank = "God";
        //slot.numb = GetItemNumb(rank, kind);

        GetAbility(slot, -4);
        slot.UpdateUI();
        DataController.inst.SaveInven(slot);

        GetItemUpdate(getItem, slot);

        DataController.inst.invenEmptyCount--;
        ItemSlotController.inst.FindEmptySlot();
    }

    public static int GetItemNumb(string rank, string kind)
    {
        int numb = 0;

        if (kind.Equals("weapon"))
        {                                       //F                 E                   D                   C                   B
            numb = SetNumb(rank, Random.Range(0, 20), Random.Range(20, 50), Random.Range(50, 70), Random.Range(70, 84), Random.Range(84, 99),
                            Random.Range(99, 119), Random.Range(119, 124), Random.Range(124, 127), Random.Range(127, 129), 129); // A  S    SS      SSS     EX
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[23], (numb - 129) * -1);
        }
        else if (kind.Equals("shield"))
        {
            numb = SetNumb(rank, Random.Range(0, 5), Random.Range(5, 10), Random.Range(10, 15), Random.Range(15, 20), Random.Range(20, 25),
                            Random.Range(25, 30), Random.Range(30, 32), Random.Range(32, 34), Random.Range(34, 36), 36);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[24], (numb - 36) * -1);
        }
        else if (kind.Equals("helmet"))
        {
            numb = SetNumb(rank, Random.Range(0, 10), Random.Range(10, 30), Random.Range(30, 50), Random.Range(50, 65), Random.Range(65, 80),
                            Random.Range(80, 89), Random.Range(89, 93), Random.Range(93, 96), Random.Range(96, 98), 98);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[25], (numb - 98) * -1);
        }
        else if (kind.Equals("amor"))
        {
            numb = SetNumb(rank, Random.Range(0, 20), Random.Range(20, 70), Random.Range(70, 99), Random.Range(99, 119), Random.Range(119, 139),
                            Random.Range(139, 149), Random.Range(149, 154), Random.Range(154, 157), Random.Range(157, 159), 159);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[26], (numb - 159) * -1);
        }
        else if (kind.Equals("cape"))
        {
            numb = SetNumb(rank, Random.Range(0, 2), Random.Range(2, 4), Random.Range(4, 6), Random.Range(6, 8), Random.Range(8, 10), 10, 11, 12, 13, 14);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[27], (numb - 14) * -1);
        }
        else if (kind.Equals("acc"))
        {
            numb = SetNumb(rank, Random.Range(0, 2), 2, 3, 4, 5, 6, 7, 8, 9, 10);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[28], (numb - 10) * -1);
        }
        else if (kind.Equals("back"))
        {
            numb = SetNumb(rank, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[115], (numb - 8) * -1);
        }
        else if (kind.Equals("helper"))
        {
            numb = SetNumb(rank, Random.Range(0, 2), Random.Range(2, 4), Random.Range(4, 6), Random.Range(6, 8), Random.Range(8, 10), 10, 11, 12, 13, 14);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[116], (numb - 14) * -1);
        }
        else if (kind.Equals("stone"))
        {
            numb = SetNumb(rank, Random.Range(0, 2), Random.Range(2, 4), Random.Range(4, 6), Random.Range(6, 8), Random.Range(8, 10), 10, 11, 12, 13, 14);
            SmithController.inst.kindNumbText.text = string.Format("{0} No.{1}", Language.inst.strArray[117], (numb - 14) * -1);
        }
        return numb;
    }

    public static float[] EnemyPower(int field)
    {
        float[] athp = new float[2];

        //if (field < 10)
        //{ athp = new int[] { 2 * (field + 1), 50 * (field + 1) }; }
        //else if (field < 20)
        //{ athp = new int[] { 2 * (field + 1), 200 * (field + 1) }; }
        //else if (field < 30)
        //{ athp = new int[] { 2 * (field + 1), 50 * (field + 1) }; }
        //else if (field < 40)
        //{ athp = new int[] { 2 * (field + 1), 50 * (field + 1) }; }
        //else
        //{ athp = new int[] { 2 * (field + 1), 50 * (field + 1) }; }

        if (field.Equals(0))
        { athp = new float[] { 2, 50 }; }
        else if (field.Equals(1))
        { athp = new float[] { 4, 200 }; }
        else if (field.Equals(2))
        { athp = new float[] { 6, 400 }; }
        else if (field.Equals(3))
        { athp = new float[] { 8, 1000 }; }
        else if (field.Equals(4)) // boss
        { athp = new float[] { 10, 2000 }; }
        else if (field.Equals(5))
        { athp = new float[] { 15, 4000 }; }
        else if (field.Equals(6))
        { athp = new float[] { 20, 7000 }; }
        else if (field.Equals(7))
        { athp = new float[] { 25, 11000 }; }
        else if (field.Equals(8))
        { athp = new float[] { 30, 17000 }; }
        else if (field.Equals(9)) // boss
        { athp = new float[] { 40, 25000 }; }
        else if (field.Equals(10))
        { athp = new float[] { 50, 40000 }; }
        else if (field.Equals(11))
        { athp = new float[] { 60, 55000 }; }
        else if (field.Equals(12))
        { athp = new float[] { 100, 75000 }; }
        else if (field.Equals(13))
        { athp = new float[] { 150, 95000 }; }
        else if (field.Equals(14)) // boss
        { athp = new float[] { 200, 200000 }; }
        else if (field.Equals(15))
        { athp = new float[] { 300, 270000 }; }
        else if (field.Equals(16))
        { athp = new float[] { 500, 340000 }; }
        else if (field.Equals(17))
        { athp = new float[] { 900, 400000 }; }
        else if (field.Equals(18))
        { athp = new float[] { 2000, 500000 }; }
        else if (field.Equals(19)) // boss
        { athp = new float[] { 3000, 1000000 }; }
        else if (field.Equals(20))
        { athp = new float[] { 5000, 1200000 }; }
        else if (field.Equals(21))
        { athp = new float[] { 9000, 1400000 }; }
        else if (field.Equals(22))
        { athp = new float[] { 14000, 1700000 }; }
        else if (field.Equals(23))
        { athp = new float[] { 20000, 2000000 }; }
        else if (field.Equals(24)) // boss
        { athp = new float[] { 27000, 4000000 }; }
        else if (field.Equals(25))
        { athp = new float[] { 35000, 5000000 }; }
        else if (field.Equals(26))
        { athp = new float[] { 44000, 6000000 }; }
        else if (field.Equals(27))
        { athp = new float[] { 54000, 7000000 }; }
        else if (field.Equals(28))
        { athp = new float[] { 65000, 10000000 }; }
        else if (field.Equals(29)) // boss
        { athp = new float[] { 80000, 20000000 }; }
        else if (field.Equals(30))
        { athp = new float[] { 100000, 30000000 }; }
        else if (field.Equals(31))
        { athp = new float[] { 120000, 40000000 }; }
        else if (field.Equals(32))
        { athp = new float[] { 140000, 50000000 }; }
        else if (field.Equals(33))
        { athp = new float[] { 160000, 60000000 }; }
        else if (field.Equals(34)) // boss
        { athp = new float[] { 180000, 100000000 }; }
        else if (field.Equals(35))
        { athp = new float[] { 200000, 200000000 }; }
        else if (field.Equals(36))
        { athp = new float[] { 230000, 300000000 }; }
        else if (field.Equals(37))
        { athp = new float[] { 260000, 400000000 }; }
        else if (field.Equals(38))
        { athp = new float[] { 280000, 500000000 }; }
        else if (field.Equals(39)) // boss
        { athp = new float[] { 300000, 600000000 }; }
        else
        { athp = new float[] { 300000 + (field - 39) * 50000, 600000000 + (field - 39) * 100000000 }; }

        return athp;
    }
}
