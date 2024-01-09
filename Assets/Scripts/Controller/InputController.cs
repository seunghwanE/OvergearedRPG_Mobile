using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputController : MonoBehaviour
{
    public static InputController inst;
    public List<Contents> PopUpList;
    public Contents exitPopUp;

    public Animator menuBack;
    public float beforeTime;

    public string url = "www.google.com";

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        PopUpList = new List<Contents>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.multiTouchEnabled = false;
    }

    public void StartGetTimeCor()
    { StartCoroutine(WebCheck()); }

    IEnumerator WebCheck()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        
        yield return request.SendWebRequest();
        if (!request.isNetworkError)
        {
            string date = request.GetResponseHeader("date");
            DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
            DataController.inst.GetLoginAward(dateTime.Day);
        }
        else
        { AwardController.inst.FindNowLogin(); }
        //}
    }


    //private void OnApplicationQuit()
    //{
    //    DataController.inst.es3.Sync();


    //    GPGSManager.inst.LogOut();
    //    AndroidJavaClass ajc = new AndroidJavaClass("com.lancekun.quit_helper.AN_QuitHelper");
    //    AndroidJavaObject UnityInstance = ajc.CallStatic<AndroidJavaObject>("Instance");
    //    UnityInstance.Call("AN_Exit");
    //}

    private void OnApplicationQuit()
    {
        DataController.inst.es3.Sync();
    }

    public void Quit()
    {
        DataController.inst.es3.Sync();
        GPGSManager.inst.LogOut();

        AndroidJavaClass ajc = new AndroidJavaClass("com.lancekun.quit_helper.AN_QuitHelper");
        AndroidJavaObject UnityInstance = ajc.CallStatic<AndroidJavaObject>("Instance");
        UnityInstance.Call("AN_Exit");

        //AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        //activity.Call<bool>("moveTaskToBack", true);

        Application.Quit();

        //Application.Quit();
    }

    public void OtherContentsTurnOff(bool isTrue = true)
    {
        //Time.timeScale = 1f;
        if (isTrue)
        {
            if (PopUpList.Count < 2)
            { menuBack.SetTrigger("ExitMenuBack_t"); }
        }

        for (int i = PopUpList.Count - 1; i >= 0; i--)
        {
            if (PopUpList[i].gameObject.activeInHierarchy.Equals(true))
            {
                if (PopUpList[i].anim != null)
                { PopUpList[i].anim.SetTrigger("ContentsExit_t"); }
                else
                { PopUpList[i].SetOff(); }

                break;
            }
        }
        if (MainSceneController.inst != null)
        { PlayerController.inst.GetTotaPower(); }
    }
    public void OtherContentsTurnOffNow(bool isTrue = true)
    {
        //Time.timeScale = 1f;
        if (isTrue)
        { menuBack.SetTrigger("ExitMenuBack_t"); }

        for (int i = PopUpList.Count - 1; i >= 0; i--)
        {
            if (PopUpList[i].gameObject.activeInHierarchy.Equals(true))
            {
                PopUpList[i].SetOff();
                if (PopUpList.Count.Equals(1))
                { menuBack.SetTrigger("ExitMenuBack_t"); }
                break;
            }
        }
        if (MainSceneController.inst != null)
        { PlayerController.inst.GetTotaPower(); }
    }

    public void MoneyContentsOpen()
    {
        for (int i = 0; i < PopUpList.Count; i++)
        {
            if (PopUpList[i].gameObject.activeInHierarchy.Equals(true))
            {
                PopUpList[i].anim.SetTrigger("ContentsExit_t");
                break;
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundController.inst.UISound(9);
            if (!PopUpList.Count.Equals(0))
            {
                for (int i = PopUpList.Count - 1; i >= 0; i--)
                {
                    if (PopUpList[i].gameObject.activeInHierarchy.Equals(true))
                    {
                        if (PopUpList[i].anim != null)
                        {
                            PopUpList[i].anim.SetTrigger("ContentsExit_t");
                            if (PopUpList.Count.Equals(1))
                            { menuBack.SetTrigger("ExitMenuBack_t"); }
                        }
                        else
                        { PopUpList[i].SetOff(); }
                        break;
                    }
                }
            }
            else
            {
                if (exitPopUp.gameObject.activeInHierarchy.Equals(true))
                {
                    exitPopUp.anim.SetTrigger("ContentsExit_t");
                    menuBack.SetTrigger("ExitMenuBack_t");
                }
                else
                {
                    menuBack.gameObject.SetActive(true);
                    exitPopUp.gameObject.SetActive(true);
                }
            }
        }
    }

    public void GoFacebook()
    {
        Application.OpenURL("https://www.facebook.com/BigBuger-186905565512223");
    }
    public void AnotherGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=BIGBUGER");
    }
    public void GoReview()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.GoblinInc.GoblinM");
    }
    public void GoKaTalk()
    {
        Application.OpenURL("https://open.kakao.com/o/gk6oInRb");
    }


    //private static string TAG = "COUPON_API";
    // 발급받은 API KEY 입니다.
    public static string COUPON_API_KEY = "45d0fbf0e04815ad";

    public void CouponCheck(string couponStr)
    {
        string url = "http://couponsystems.kr/api/UseCoupon.php?key=" + COUPON_API_KEY + "&coupon=" + couponStr + "&id=" + DataController.inst.uid;
        //string url = "http://couponsystems.kr/api/UseCoupon.php?key=" + COUPON_API_KEY + "&coupon=" + couponStr + "&id=" + "testtestIDID6";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse reponse = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(reponse.GetResponseStream());
        string json = reader.ReadToEnd();

        CouponData couponData = new CouponData(JsonUtility.FromJson<CouponData>(json));
        provideItem(couponData);
        Debug.LogWarning(json);
        Debug.LogWarning(couponData.error_code);
        Debug.LogWarning(couponData.result);
    }

    private void provideItem(CouponData data)
    {
        if (data.result)
        {
            if (data != null && data.items != null)
            {
                for (int j = 0; j < data.items.Count; j++)
                {
                    if (data.items[j].item_code.Equals("ruby"))
                    {
                        Debug.LogWarning("루비100 : " + data.items[j].item_count + "번 지급");
                        for (int i = 0; i < data.items[j].item_count; i++)
                        { MainSceneController.inst.UpdateRuby(100); }
                    }
                    else if (data.items[j].item_code.Equals("pack"))
                    {
                        Debug.LogWarning("루비100 + 재료100 + 1만골드 : " + data.items[j].item_count + "번 지급");
                        for (int i = 0; i < data.items[j].item_count; i++)
                        {
                            MainSceneController.inst.UpdateGold(10000);
                            MainSceneController.inst.UpdateIron(100);
                            MainSceneController.inst.UpdateRuby(100);
                        }
                    }
                    //else if (data.items[j].item_code.Equals("test_item"))
                    //{
                    //    Debug.LogWarning("루비1000 : " + data.items[j].item_count + "번 지급");
                    //    for (int i = 0; i < data.items[j].item_count; i++)
                    //    { MainSceneController.inst.UpdateRuby(1000); }
                    //}
                }
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[189], 0);
                DataController.inst.es3.Sync();
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[193], 50); }
        }
        else
        {
            if(data.error_code.Equals(-201))
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[190], 50); }
            else if (data.error_code.Equals(-302))
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[191], 50); }
            else if (data.error_code.Equals(-303))
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[192], 50); }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[193], 50); }
        }
    }
}


[Serializable]
public class CouponItem {
    public string item_code;
    public int item_count;
    public CouponItem(string _itemCode, int _itemCount)
    {
        item_code = _itemCode;
        item_count = _itemCount;
    }
}
[Serializable]
public class CouponData{
    public bool result;
    public int error_code;
    public string error_msg;
    public string benefit;
    public List<CouponItem> items;

    public CouponData(bool _result, int _errorCode, string _errorMsg, string _benefit, List<CouponItem> _item)
    {
        result = _result;
        error_code = _errorCode;
        error_msg = _errorMsg;
        benefit = _benefit;
        items = _item;
    }
    public CouponData(CouponData data)
    {
        result = data.result;
        error_code = data.error_code;
        error_msg = data.error_msg;
        benefit = data.benefit;
        items = data.items;
    }
}

