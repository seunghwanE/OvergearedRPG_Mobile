using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwardController : MonoBehaviour{
    public static AwardController inst;

    public levelAward[] awards, vipAwards;
    public LoginButton[] logins;
    public GameObject vipLockObj;
    public Sprite goldSprite, ironSprite, rubySprite, helmetSprite, amorSprite, weaponSprite, shieldSprite, backSprite, helperSprite, stoneSprite, statSprite, accSprite;
    public levelAward nowAward;
    public Transform UIPos;
    public ScrollRect scroll;
    public GetItem loginGetItemPop, levelAwardGetItemPop;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
    }

    public void Start()
    {
        bool checker = true;
        InputController.inst.StartGetTimeCor();
        for (int i = 0; i < awards.Length; i++)
        {
            if(DataController.inst.VipPass)
            { vipLockObj.SetActive(false); }

            DataController.inst.LoadLogin(logins[i]);
            logins[i].UpdateUI();

            DataController.inst.LoadAward(awards[i]);
            awards[i].UpdateUI();

            DataController.inst.LoadAward(vipAwards[i]);
            vipAwards[i].UpdateUI();

            if(!awards[i].isGet && checker)
            {
                nowAward = awards[i];
                nowAward.SetNow();
                checker = false;
                if(DataController.inst.VipPass)
                { vipAwards[i].SetNow(); }
            }
        }
        if(awards[29].isGet)
        { nowAward = awards[29]; nowAward.SetNow(); }
    }

    public void FindNow()
    {
        for (int i = 0; i < awards.Length; i++)
        {
            if (!awards[i].isGet)
            {
                nowAward = awards[i];
                nowAward.SetNow();
                break;
            }
        }
        if (awards[29].isGet)
        { nowAward = awards[29]; nowAward.SetNow(); }
    }
    public void FindNowLogin()
    {
        for (int i = 0; i < logins.Length; i++)
        {
            if (!logins[i].isGet)
            {
                if (DataController.inst.loginCheck)
                {
                    logins[i].TodayUpdateUI();
                    break;
                }
                else
                {
                    if (i > 0)
                    { logins[i - 1].TodayUpdateUI(); }
                    else
                    { logins[i].TodayUpdateUI(); }
                    break;
                }
            }
        }
    }
    public void BuyVip()
    {
        IAPController.inst.BuyProduct(14);
    }

    public void UpdateVip()
    {
        if(DataController.inst.VipPass)
        {
            vipLockObj.SetActive(false);
            for(int i =0; i < vipAwards.Length; i++)
            {
                if (awards[i].isGet || i.Equals(nowAward.id))
                { vipAwards[i].SetNow(); }
            }
        }
    }

    public void SetContentsPos()
    {
        if (nowAward.id > 1 && nowAward.id < 28)
        { StartPosMoveCor(nowAward.id); }
    }

    public void StartPosMoveCor(int numb)
    { StartCoroutine(PosMoveCor(numb)); }

    private IEnumerator PosMoveCor(int numb)
    {
        float scrollValue = scroll.horizontalScrollbar.value;
        float last = (awards[numb].id * 0.04f) - 0.04f;
        //Vector2 tmpStartPos = UIPos2.anchoredPosition;
        //Vector2 tmpLastPos = tmpStartPos;
        ////tmpLastPos.x = (numb * -4.3f) - 6.5f;
        //tmpLastPos.x = awards[numb].transform.position.x;

        for (float timer = 0f; timer < 0.5f; timer += Time.deltaTime)
        {
            float progress = timer / 0.5f;
            //UIPos2.anchoredPosition = Vector2.Lerp(tmpStartPos, tmpLastPos, progress);

            scroll.horizontalScrollbar.value = Mathf.Lerp(scrollValue,last, progress);

            yield return null;
        }
        //UIPos2.anchoredPosition = tmpLastPos;

        scroll.horizontalScrollbar.value = last;
    }

}
