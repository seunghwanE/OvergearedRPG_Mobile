using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    public int id, count, maxCount, cost;
    public bool isGet;
    public Text getText, titleText, contentsText, numbText;
    public Image getImage, getButtonImage;
    public Slider slider;

    //레벨업5
    //스미스10
    //미션5
    //광고5
    //상자5
    //기도10
    //스킬10
    //스탯10
    //현질

    //광고
    //전투
    //레벨업
    //골드소비
    //루비소비
    //10분
    //강화
    //킬
    //킬보스

    public void StartSet()
    {
        titleText.text = Language.inst.missionTitle[id];
        if (id.Equals(0))
        { cost = 5; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+10"; }
        else if (id.Equals(1))
        { cost = 10; getImage.sprite = AwardController.inst.ironSprite; getText.text = "+10"; }
        else if (id.Equals(2))
        { cost = 5; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+5"; }
        else if (id.Equals(3))
        { cost = 5; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+20"; }
        else if (id.Equals(4))
        { cost = 5; getImage.sprite = AwardController.inst.ironSprite; getText.text = "+15"; }
        else if (id.Equals(5))
        { cost = 30; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+10"; }
        else if (id.Equals(6))
        { cost = 10; getImage.sprite = AwardController.inst.ironSprite; getText.text = "+15"; }
        else if (id.Equals(7))
        { cost = 10; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+15"; }
        else if (id.Equals(8))
        { cost = 1; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+25"; }

        else if (id.Equals(9))
        { maxCount = 1; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+3"; }
        else if (id.Equals(10))
        { maxCount = 1; getImage.sprite = AwardController.inst.goldSprite; getText.text = "+1000"; }
        else if (id.Equals(11))
        { maxCount = 1; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+2"; }
        else if (id.Equals(12))
        { maxCount = 10; getImage.sprite = AwardController.inst.ironSprite; getText.text = "+5"; }
        else if (id.Equals(13))
        { maxCount = 2; getImage.sprite = AwardController.inst.rubySprite; getText.text = "+2"; }
        else if (id.Equals(14))
        { maxCount = 10; getImage.sprite = AwardController.inst.ironSprite; getText.text = "+5"; }
        else if (id.Equals(15))
        { maxCount = 1; getImage.sprite = AwardController.inst.goldSprite; getText.text = "+2000"; }
        else if (id.Equals(16))
        { maxCount = 20; getImage.sprite = AwardController.inst.goldSprite; getText.text = "+1000"; }
        else if (id.Equals(17))
        { maxCount = 1; getImage.sprite = AwardController.inst.goldSprite; getText.text = "+1500"; }
        SetValue();
        contentsText.text = Language.inst.MissionContentsStr(id, maxCount);
    }

    public void Click()
    {
        if (count >= maxCount)
        {
            SoundController.inst.UISound(6);
            DataController.inst.C_MissionFive++;
            if (id.Equals(0))
            { MainSceneController.inst.UpdateRuby(10); }
            else if (id.Equals(1))
            { MainSceneController.inst.UpdateIron(10); }
            else if (id.Equals(2))
            { MainSceneController.inst.UpdateRuby(5); }
            else if (id.Equals(3))
            { MainSceneController.inst.UpdateRuby(20); }
            else if (id.Equals(4))
            { MainSceneController.inst.UpdateIron(15); }
            else if (id.Equals(5))
            { MainSceneController.inst.UpdateRuby(10); }
            else if (id.Equals(6))
            { MainSceneController.inst.UpdateIron(15); }
            else if (id.Equals(7))
            { MainSceneController.inst.UpdateRuby(15); }
            else if (id.Equals(8))
            { MainSceneController.inst.UpdateRuby(25); }
            else if (id.Equals(9))
            { isGet = true; MainSceneController.inst.UpdateRuby(3); }
            else if (id.Equals(10))
            { isGet = true; MainSceneController.inst.UpdateGold(1000); }
            else if (id.Equals(11))
            { isGet = true; MainSceneController.inst.UpdateRuby(2); }
            else if (id.Equals(12))
            { isGet = true; MainSceneController.inst.UpdateIron(5); }
            else if (id.Equals(13))
            { isGet = true; MainSceneController.inst.UpdateRuby(2); }
            else if (id.Equals(14))
            { isGet = true; MainSceneController.inst.UpdateIron(5); }
            else if (id.Equals(15))
            { isGet = true; MainSceneController.inst.UpdateGold(2000);  }
            else if (id.Equals(16))
            { isGet = true; MainSceneController.inst.UpdateGold(1000); }
            else if (id.Equals(17))
            { isGet = true; MainSceneController.inst.UpdateGold(1500); }

            if (id < 9)
            { maxCount += cost; }
            
            SetValue();
            contentsText.text = Language.inst.MissionContentsStr(id, maxCount);
            DataController.inst.SaveMission(this);
        }
    }

    public void SetValue()
    {
        if (id.Equals(0))
        { count = DataController.inst.C_LevelUpFive; }
        else if (id.Equals(1))
        { count = DataController.inst.C_SmithTen; }
        else if (id.Equals(2))
        { count = DataController.inst.C_MissionFive; }
        else if (id.Equals(3))
        { count = DataController.inst.C_AdFive; }
        else if (id.Equals(4))
        { count = DataController.inst.C_ChestFive; }
        else if (id.Equals(5))
        { count = DataController.inst.C_BlessTen; }
        else if (id.Equals(6))
        { count = DataController.inst.C_GetSkillTen; }
        else if (id.Equals(7))
        { count = DataController.inst.C_GetStatTen; }
        else if (id.Equals(8))
        { count = DataController.inst.C_IAP; }
        else
        {
            if (isGet)
            { gameObject.SetActive(false); }
            else
            {
                gameObject.SetActive(true);
                if (id.Equals(9))
                { count = DataController.inst.C_Ad; }
                else if (id.Equals(10))
                { count = DataController.inst.C_Battle; }
                else if (id.Equals(11))
                { count = DataController.inst.C_LevelUp; }
                else if (id.Equals(12))
                { count = DataController.inst.C_PaidGold; }
                else if (id.Equals(13))
                { count = DataController.inst.C_PaidRuby; }
                else if (id.Equals(14))
                { count = DataController.inst.C_TenMin; }
                else if (id.Equals(15))
                { count = DataController.inst.C_Upgrade; }
                else if (id.Equals(16))
                { count = DataController.inst.C_Kill; }
                else if (id.Equals(17))
                { count = DataController.inst.C_KillBoss; }
            }
        }

        if (maxCount.Equals(0))
        { maxCount = cost; }

        if (count >= maxCount)
        {
            getButtonImage.color = Color.white;
            //ChallengeController.inst.CheckRed();
        }
        else
        { getButtonImage.color = Color.gray; }
        numbText.text = string.Format("{0} {1}/{2}", Language.inst.strArray[147], count.ToString(), maxCount.ToString());
        slider.maxValue = maxCount;
        slider.value = count;
    }

}
