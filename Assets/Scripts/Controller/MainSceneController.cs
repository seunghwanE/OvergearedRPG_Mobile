using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Play.Review;

public class MainSceneController : MonoBehaviour{
    public static MainSceneController inst;

    public Text goldText, ironText, rubyText, profileLevelText, profilePowerText, expText, statText, atText, hpText, criText, rateText, powerText, dexText, healthText, luckText, levelUpLevelText,
        criDamageText, avoidText, smithCountText, killCountText, playTimeCountText, prayCountTime;
    public Slider expSlider, powerSlider;
    public GameObject sign1, sign2, cloudObj, levelUpSign, cloudObj2, loadPopImage, tutorial, loadPopObj, powerObj, powerObj2, upgradeObj;
    public Text storeStatCostText, loadPopText, uidText;
    public Character[] characters;

    public Mission[] missions;
    public BattleButton[] battles;
    public ScrollRect battleScroll;
    public BattleButton nowBattle;

    public int count;
    public bool powerFlag;

    public Coroutine cor;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        sign1.SetActive(true);
        sign2.SetActive(true);
        cloudObj2.SetActive(true);
        count = 0;
        powerFlag = false;
    }

    public void UpgradeObjSet()
    {
        upgradeObj.SetActive(false);
        upgradeObj.SetActive(true);
    }

    private void Start()
    {
        for (int i = 0; i < missions.Length; i++)
        { DataController.inst.LoadMission(missions[i]); missions[i].StartSet(); }

        bool flag = true;

        for(int i = 0; i < battles.Length; i++)
        {
            DataController.inst.LoadBattleButton(battles[i]);
            battles[i].UpdateUI();

            if (flag)
            {
                if (!battles[i].isClear)
                {
                    if (i.Equals(0))
                    { nowBattle = battles[i]; flag = false; }
                    else
                    {
                        if (battles[i - 1].isClear)
                        { nowBattle = battles[i]; flag = false; }
                    }
                }
            }
        }
        uidText.text = string.Format("{0}", DataController.inst.uid);

        goldText.text = NumbToData.GetIntGold(DataController.inst.gold);
        ironText.text = NumbToData.GetInt(DataController.inst.iron);
        rubyText.text = NumbToData.GetInt(DataController.inst.ruby);
        
        storeStatCostText.text = NumbToData.GetIntGold(DataController.inst.storeStatCost);
        PlayerController.inst.UpdateAbility();
        characters[0].SetSkin(PlayerController.inst.skinId);
        characters[1].SetSkin(PlayerController.inst.skinId);

        AdMobController.inst.StartSet();

        if (!DataController.inst.viewTutorial)
		{
			tutorial.SetActive(true);
			DataController.inst.viewTutorial = true;
			DataController.inst.es3.Sync();
		}
		else if (!DataController.inst.review && DataController.inst.KillCount > 100)
		{
#if UNITY_EDITOR
#elif UNITY_ANDROID
            StartReview();
#endif
            DataController.inst.review = true;
			DataController.inst.es3.Sync();
		}
    }

	private ReviewManager _reviewManager;
	private PlayReviewInfo _playReviewInfo;

    public void ResetStat()
    {
        if(DataController.inst.ruby > 1000)
        {
            SoundController.inst.UISound(2);
            DataController.inst.C_PaidRuby++;
            UpdateRuby(-1000);
            PlayerController.inst.ResetStat();
            UpdateCharacterInfo();
            SimpleSign.inst.SimpleSignSet(Language.inst.strArray[55], 0);
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); SoundController.inst.UISound(5); }
    }

	public void StartReview()
	{ StartCoroutine(StartReviewCor()); }

	IEnumerator StartReviewCor()
	{
        yield return new WaitForSeconds(10f);
        DataController.inst.review = true;
        _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
		yield return requestFlowOperation;
		if (requestFlowOperation.Error != ReviewErrorCode.NoError)
		{
			// Log error. For example, using requestFlowOperation.Error.ToString().
			yield break;
		}
		_playReviewInfo = requestFlowOperation.GetResult();

		var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
		yield return launchFlowOperation;
		_playReviewInfo = null; // Reset the object
		if (launchFlowOperation.Error != ReviewErrorCode.NoError)
		{
			// Log error. For example, using requestFlowOperation.Error.ToString().
			yield break;
		}
	}

    public void UpdatePowerSlider()
    {
        if(count.Equals(300))
        { StartCoroutine(PowerCor()); }
        else
        { count++; }
        powerSlider.value = count;
    }

    IEnumerator PowerCor()
    {
        powerObj.SetActive(true);
        powerObj2.SetActive(true);
        powerFlag = true;
        for (float timer = 0; timer < 15f; timer += Time.deltaTime)
        {
            float progress = timer / 15f;
            count = (int)Mathf.Lerp(300, 0, progress);
            powerSlider.value = count;
            yield return null;
        }
        powerObj.SetActive(false);
        powerObj2.SetActive(false);
        powerFlag = false;
        count = 0;
        powerSlider.value = count;
    }

	public void Save()
    { GPGSManager.inst.SaveCloud(); }
    public void Load()
    {
        loadPopImage.SetActive(true);
        loadPopObj.SetActive(true);
        loadPopText.text = Language.inst.strArray[171];
        GPGSManager.inst.LoadCloud();
    }

    public void LevelUp(int level)
    {
        levelUpLevelText.text = level.ToString();
        levelUpSign.SetActive(true);
        UpdateIron(5);
        UpdateRuby(5);
    }

    public void ShowLeaderboard()
    { GPGSManager.inst.ShowLeaderboardUI(); }

    public void SetCharacterSkin(int numb)
    { characters[0].SetSkin(numb); characters[1].SetSkin(numb); }
    public void SetCharacterWeapon(int numb)
    { characters[0].SetWeapon(numb); characters[1].SetWeapon(numb); }
    public void SetCharacterShield(int numb)
    { characters[0].SetShield(numb); characters[1].SetShield(numb); }
    public void SetCharacterHelmet(int numb)
    { characters[0].SetHelmet(numb); characters[1].SetHelmet(numb); }
    public void SetCharacterAmor(int numb)
    { characters[0].SetAmor(numb); characters[1].SetAmor(numb); }
    public void SetCharacterCape(int numb)
    { characters[0].SetCape(numb); characters[1].SetCape(numb); }
    public void SetCharacterBack(int numb)
    {
        characters[0].SetBack(numb); characters[1].SetBack(numb);
        if (numb > -1)
        {
            for (int i = 0; i < characters[1].backObj.wingSprites.Length; i++)
            { characters[1].backObj.wingSprites[i].sortingLayerName = "ContentsUI"; }
        }
    }
    public void SetCharacterHelper(int numb)
    { characters[0].SetHelper(numb); characters[1].SetHelper(numb); }

    public void UpdateCharacterInfo()
    {
        profileLevelText.text = string.Format("lv. {0}", PlayerController.inst.level);
        profilePowerText.text = string.Format("{0}", PlayerController.inst.totalPower);

        

        statText.text = string.Format("{0} : {1}",Language.inst.strArray[44], PlayerController.inst.statPoint);

        int _at = PlayerController.inst.statAt + PlayerController.inst.itemAt + PlayerController.inst.skillAt + PlayerController.inst.skinAt;
        if (DataController.inst.IAPAt)
        { atText.text = string.Format("{0} : {1}", Language.inst.strArray[45], (int)(_at * (PlayerController.inst.skillAtRate + PlayerController.inst.skinAtRate + 1))); }
        else
        { atText.text = string.Format("{0} : {1}", Language.inst.strArray[45], (int)(_at * PlayerController.inst.skillAtRate + PlayerController.inst.skinAtRate)); }

        int _hp = PlayerController.inst.statHp + PlayerController.inst.itemHp + PlayerController.inst.skillHp;
        if (DataController.inst.IAPHp)
        { hpText.text = string.Format("{0} : {1}", Language.inst.strArray[46], (int)(_hp * (PlayerController.inst.skillHpRate + 1))); }
        else
        { hpText.text = string.Format("{0} : {1}", Language.inst.strArray[46], (int)(_hp * PlayerController.inst.skillHpRate)); }

        float luckRate = PlayerController.inst.statLuck * 0.1f;
        rateText.text = string.Format("{0} : {1}%", Language.inst.strArray[48], (luckRate * 0.1f).ToString());
        int cri = PlayerController.inst.statDex + PlayerController.inst.itemCri;
        int avoid = PlayerController.inst.statDex + PlayerController.inst.itemAvoid;
        int avoidSub = 0;
        if(avoid > 50)
        {
            avoidText.text = string.Format("{0} : {1}%", Language.inst.strArray[74], 50);
            avoidSub = avoid - 50;
        }
        else
        { avoidText.text = string.Format("{0} : {1}%", Language.inst.strArray[74], avoid); }

        if (cri > 50)
        {
            criText.text = string.Format("{0} : {1}%", Language.inst.strArray[47], 50);
            cri -= 50;
            if (DataController.inst.IAPCri)
            { criDamageText.text = string.Format("{0} : {1}%", Language.inst.strArray[47], 100 + cri + avoidSub); }
            else
            { criDamageText.text = string.Format("{0} : {1}%", Language.inst.strArray[72], 50 + cri + avoidSub); }
        }
        else
        {
            criText.text = string.Format("{0} : {1}%", Language.inst.strArray[47], cri);
            if (DataController.inst.IAPCri)
            { criDamageText.text = string.Format("{0} : {1}%", Language.inst.strArray[47], 100 + avoidSub); }
            else
            { criDamageText.text = string.Format("{0} : {1}%", Language.inst.strArray[72], 50 + avoidSub); }
        }
        
        smithCountText.text = DataController.inst.smithTotalCount.ToString("N0");
        killCountText.text = DataController.inst.KillCount.ToString("N0");
        playTimeCountText.text = DataController.inst.PlayTimeCount.ToString("N0");
        prayCountTime.text = DataController.inst.PrayCount.ToString("N0");

        UpdateExp();

        powerText.text = PlayerController.inst.statPower.ToString();
        dexText.text = PlayerController.inst.statDex.ToString();
        healthText.text = PlayerController.inst.statHealth.ToString();
        luckText.text = PlayerController.inst.statLuck.ToString();
    }

    public void UpdateExp()
    {
        float max = PlayerController.inst.maxExp, current = PlayerController.inst.currentExp;
        expSlider.maxValue = max;
        expSlider.value = current;
        expText.text = string.Format("{0}%", Mathf.FloorToInt(current / max * 1000) * 0.1f);
    }

    public void UpdateGold(int numb)
    {
        if (DataController.inst.gold > 1999999999 && numb > 0)
        { goldText.text = "Max"; }
        else
        {
            if (!numb.Equals(0))
            {
                StartCoroutine(StepCountGold(numb, DataController.inst.gold, 0.5f, goldText));
                DataController.inst.GG(numb);
                if (DataController.inst.gold > 1999999999)
                { goldText.text = "Max"; DataController.inst.MaxG(); }
            }
        }
    }
    public void UpdateIron(int numb)
    {
        if (DataController.inst.iron > 9999999 && numb > 0)
        { ironText.text = "Max"; }
        else
        {
            if (!numb.Equals(0))
            {
                StartCoroutine(StepCount(numb, DataController.inst.iron, 0.5f, ironText));
                DataController.inst.II(numb);
                if (DataController.inst.iron > 9999999)
                { ironText.text = "Max"; DataController.inst.MaxI(); }
            }
        }
    }
    public void UpdateRuby(int numb)
    {
        if (DataController.inst.ruby > 999999 && numb > 0)
        { rubyText.text = "Max"; }
        else
        {
            if (!numb.Equals(0))
            {
                StartCoroutine(StepCount(numb, DataController.inst.ruby, 0.5f, rubyText));
                DataController.inst.RR(numb);
                if (DataController.inst.ruby > 999999)
                { rubyText.text = "Max"; DataController.inst.MaxR(); }
            }
        }
    }

    IEnumerator StepCount(int _AddSub, int _Start, float _Time, Text _Text)
    {
        int target = _AddSub + _Start;

        int start = _Start;
        int countGold = 0;

        float duration = _Time;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            countGold = (int)Mathf.Lerp(start, target, progress);
            _Text.text = NumbToData.GetInt(countGold);
            yield return null;
        }
        _Text.text = NumbToData.GetInt(target);
    }
    IEnumerator StepCountGold(int _AddSub, int _Start, float _Time, Text _Text)
    {
        int target = _AddSub + _Start;

        int start = _Start;
        int countGold = 0;

        float duration = _Time;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            countGold = (int)Mathf.Lerp(start, target, progress);
            _Text.text = NumbToData.GetIntGold(countGold);
            yield return null;
        }
        _Text.text = NumbToData.GetIntGold(target);
    }

    public void GoBattle()
    {
        cloudObj.SetActive(true);
        Invoke("BattleScene", 1.8f);
    }

    public void GoHell()
    {
        cloudObj.SetActive(true);
        Invoke("HellScene", 1.8f);
    }

    public void BattleScene()
    { DataController.inst.LoadBattleScene(); }
    public void HellScene()
    { DataController.inst.LoadHellScene(); }

    public void StartPosMoveCor()
    {
        if (battles[18].isClear)
        { battleScroll.horizontalScrollbar.value = (18 * 0.07f) - 0.15f; }
        else
        {
            if (nowBattle.id > 1 && nowBattle.id < 17)
            { battleScroll.horizontalScrollbar.value = (nowBattle.id * 0.07f) - 0.15f; }
        }
    }

    public void StatAdd(int numb)
	{ PlayerController.inst.AddStat(numb); }

    public void StartStatAdd(int numb)
    { cor = StartCoroutine(BuyStatCor(numb)); }
    public void StopStatAdd()
    { StopCoroutine(cor); DataController.inst.SavePlayer(PlayerController.inst); }

    IEnumerator BuyStatCor(int numb)
    {
        bool flag = true;
        int count = 0;
        while (flag)
        {
            if (PlayerController.inst.statPoint > 0)
            { StatAdd(numb); }
            else
            { flag = false; ErrorSign.inst.SimpleSignSet(Language.inst.strArray[43], 10); SoundController.inst.UISound(5); }

            if(count < 10)
            { yield return DataController.inst.quaterSec; }
            else if (count < 30)
            { yield return DataController.inst.zeroSec; }
            else
            { yield return DataController.inst.zeroQuaterSec; }
            count++;
        }
    }
}
