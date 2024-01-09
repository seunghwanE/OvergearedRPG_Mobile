using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobController : MonoBehaviour {
    public static AdMobController inst;

    private Coroutine loadCor;
    private WaitForSeconds fiveSec;

    //dummy
    //private readonly string popUpId = "ca-app-pub-3940256099942544/8691691433";
    //private readonly string rewardId = "ca-app-pub-3940256099942544/5224354917";

    //real
    //private readonly string popUpId = "ca-app-pub-";
    private readonly string rewardId = "ca-app-pub-4781035585852380/2371887672";
    private bool check = true;
    private RewardBasedVideoAd rewardAD;
    //private InterstitialAd popUpAd;

    private WaitForSeconds oneMin;
    public int buffAuto, buffCri, buffGold, buffAt;
    private int ID;
    //public int buffTimeAuto, buffTimeCri, buffTimeGold, buffTimeAt;

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
        buffAuto = 0;
        buffCri = 1;
        buffGold = 0;
        buffAt = 0;

        fiveSec = new WaitForSeconds(30f);
        oneMin = new WaitForSeconds(60f);

        rewardAD = RewardBasedVideoAd.Instance;

        if (check)
        {
            //MobileAds.Initialize(rewardId);
            MobileAds.Initialize(
                (InitStatus) =>
                {
                    RewardAdLoad();
                });

            rewardAD.OnAdRewarded += AdRewarded;
            check = false;
        }
    }

    public void StartSet()
    {
        if (DataController.inst.buff_auto > 0)
        { buffAuto = 1; StartBuffOne(0); }
        else
        { buffAuto = 0; }

        if (DataController.inst.buff_cri > 0)
        { buffCri = 2; StartBuffOne(1); }
        else
        { buffCri = 1; }

        if (DataController.inst.buff_gold > 0)
        { buffGold = 1; StartBuffOne(2); }
        else
        { buffGold = 0; }

        if (DataController.inst.buff_dam > 0)
        { buffAt = 1; StartBuffOne(3); }
        else
        { buffAt = 0; }
    }

    public void RewardAdLoad()
    {
        if (!rewardAD.IsLoaded())
        {
            AdRequest request = new AdRequest.Builder().Build();
            rewardAD.LoadAd(request, rewardId);
        }
    }

    public void ShowRewardAd(int id)
    {
        ID = id;

        if (rewardAD.IsLoaded())
        { rewardAD.Show(); }
        else
        {
            SoundController.inst.UISound(5);
            CanvasController.inst.CanvasLongShake();
            RewardAdLoad();
            SimpleSign.inst.SimpleSignSet(Language.inst.strArray[144], 100);
            DataController.inst.Vibrate(200);
        }
    }

    public void AdRewarded(object sender, EventArgs args)
    {
        DataController.inst.C_Ad++;
        DataController.inst.C_AdFive++;
        if (ID < 4)
        { AddTime(ID); StartBuffOne(ID); }
        else if (ID < 5)
        { BattleController.inst.AdRevive(); }
        
        DataController.inst.es3.Sync();
        RewardAdLoad();
    }

    public void StartBuffOne(int id)
    {
        StartCoroutine(BuffOne(id));
        //BuffController.inst.BuffClick();
    }

    public void AddTime(int id)
    {
        if (id.Equals(0))
        { DataController.inst.buff_auto = 20; }
        else if (id.Equals(1))
        { DataController.inst.buff_cri = 10; }
        else if (id.Equals(2))
        { DataController.inst.buff_gold = 10; }
        else if (id.Equals(3))
        { DataController.inst.buff_dam = 10; }
    }

    public void AutoAttack()
    {
        StartCoroutine(BuffAttack());
    }

    //public void BuffSet(int id)
    //{
    //    if (id.Equals(0))
    //    {
    //        if (DataController.inst.buff_auto > 0)
    //        {
    //            SetBuffUI(0, DataController.inst.buff_auto);
    //            buffAuto = 1;
    //            AutoAttack();
    //        }
    //        else
    //        { buffAuto = 0; }
    //    }
    //    else if(id.Equals(1))
    //    {
    //        if (DataController.inst.buff_cri > 0)
    //        {
    //            buffCri = 2;
    //            PlayerController.inst.SetAbility();
    //            SetBuffUI(id, DataController.inst.buff_cri);
    //        }
    //        else
    //        {
    //            buffCri = 1;
    //            PlayerController.inst.SetAbility();
    //        }
    //    }
    //    else if(id.Equals(2))
    //    {
    //        if (DataController.inst.buff_gold > 0)
    //        {
    //            buffGold = 1;
    //            SetBuffUI(id, DataController.inst.buff_gold);
    //        }
    //        else
    //        { buffGold = 0; }
    //    }
    //    else if(id.Equals(3))
    //    {
    //        if (DataController.inst.buff_dam > 0)
    //        {
    //            buffAt = 1;
    //            PlayerController.inst.SetAbility();
    //            SetBuffUI(id, DataController.inst.buff_dam);
    //        }
    //        else
    //        {
    //            buffAt = 0;
    //            PlayerController.inst.SetAbility();
    //        }
    //    }
    //}

    private IEnumerator BuffAttack()
    {
        WaitForSeconds oneSec = new WaitForSeconds(1f);
        while(buffAuto.Equals(1))
        {
            if(BattleController.inst != null)
            { PlayerController.inst.character.AttackClick(); }
            yield return oneSec;
        }
    }

    private void SetBuffUI(int id, int count)
    {
        if (BattleController.inst != null)
        {
            BattleController.inst.adButtons[id].SetCoolUI(count);
            BattleController.inst.buffs[id].SetTimeUI(count);
        }
    }

    public IEnumerator BuffOne(int id)
    {
        if(BattleController.inst != null)
        { BattleController.inst.buffs[id].BuffStart(); }

        if (id.Equals(0))
        {
            buffAuto = 1;
            AutoAttack();
            while (DataController.inst.buff_auto > 0)
            {
                DataController.inst.buff_auto--;
                SetBuffUI(id, DataController.inst.buff_auto);
                yield return oneMin;
            }
            buffAuto = 0;
        }
        else if (id.Equals(1))
        {
            buffCri = 2;
            PlayerController.inst.SetAbility();
            while (DataController.inst.buff_cri > 0)
            {
                DataController.inst.buff_cri--;
                SetBuffUI(id, DataController.inst.buff_cri);
                yield return oneMin;
            }
            buffCri = 1;
            PlayerController.inst.SetAbility();
        }
        else if (id.Equals(2))
        {
            buffGold = 1;
            while (DataController.inst.buff_gold > 0)
            {
                DataController.inst.buff_gold--;
                SetBuffUI(id, DataController.inst.buff_gold);
                yield return oneMin;
            }
            buffGold = 0;
        }
        else if (id.Equals(3))
        {
            buffAt = 1;
            PlayerController.inst.SetAbility();
            while(DataController.inst.buff_dam > 0)
            { 
                DataController.inst.buff_dam--;
                SetBuffUI(id, DataController.inst.buff_dam);
                yield return oneMin;
            }
            buffAt = 0;
            PlayerController.inst.SetAbility();
        }

        if (BattleController.inst != null)
        { BattleController.inst.buffs[id].TimeOut(); }
    }

    public void ADChest()
    {
        //if (!ButtonController.inst.r.Equals(0))
        //{
        //    if (ButtonController.inst.r2.Equals(4))
        //    { MainSceneController.inst.FreeGen(4); }
        //    else
        //    { MainSceneController.inst.FreeGen(5); }
        //}
        //else
        //{
        //    if (ButtonController.inst.r2.Equals(4))
        //    {
        //        DataController.inst.RR(30);
        //        SimpleSign.inst.SimpleSignSet("+30 획득!", 1);
        //    }
        //    else
        //    {
        //        DataController.inst.RR(40);
        //        SimpleSign.inst.SimpleSignSet("+40 획득!", 1);
        //    }
        //}
        //ChallengeController.inst.ChallengeCount();
        //ButtonController.inst.chestObj.SetActive(false);
    }
}
