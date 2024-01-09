using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LoadingController : MonoBehaviour
{
    public static LoadingController inst;

    public Slider loadingSlider;
    public Image logoImage;
    public GameObject personalInfoObj, goHomeObj, startTextObj, loginSign, korPop;
    public Text loadText;

    public Button korBtn, engBtn;

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

    private void Start()
    {
        if (DataController.inst.languageStr.Equals("Korean"))
        { logoImage.sprite = Language.inst.koreanLogo; }
        else
        { logoImage.sprite = Language.inst.englishLogo; }
        
        PersnoalInfoAgree();
    }

    public void SetKor()
    {
        DataController.inst.languageStr = "Korean";
        logoImage.sprite = Language.inst.koreanLogo;
        DataController.inst.es3.Sync();
        Language.inst.LangaugeSet();
#if UNITY_EDITOR
        StartCor();
#elif UNITY_ANDROID
            GPGSManager.inst.LogIn();
#endif
    }
    public void SetEng()
    {
        DataController.inst.languageStr = "English";
        logoImage.sprite = Language.inst.englishLogo;
        DataController.inst.es3.Sync();
        Language.inst.LangaugeSet();
#if UNITY_EDITOR
        StartCor();
#elif UNITY_ANDROID
            GPGSManager.inst.LogIn();
#endif
    }

    public void PersnoalInfoAgree()
    {
        if (!DataController.inst.agree)
        { personalInfoObj.SetActive(true); }
        else
        {
#if UNITY_EDITOR
            StartCor();
#elif UNITY_ANDROID
            GPGSManager.inst.LogIn();
#endif
        }
    }

    public void ClickAgree()
    {
        SoundController.inst.UISound(9);
        korPop.SetActive(true);
        DataController.inst.agree = true;
        personalInfoObj.SetActive(false);
    }


    public void StartCor()
    { StartCoroutine(LoadingCor()); }

    IEnumerator LoadingCor()
    {
        loadText.text = "Load";
        yield return new WaitForSeconds(1f);
        WaitForSeconds zeroOnesec = new WaitForSeconds(0.05f);
        for (int i = 0; i < 20; i++)
        {
            if(i .Equals(5))
            { loadText.text = "Load."; }
            else if (i.Equals(10))
            { loadText.text = "Load.."; }
            else if (i.Equals(15))
            { loadText.text = "Load..."; }
            
            loadingSlider.value = i * 0.05f;
            yield return zeroOnesec; 
        }
        loadText.text = "Ready to Start";
        loadingSlider.value = 1;
        startTextObj.gameObject.SetActive(true);
    }

    public void GoHome()
    {
        SoundController.inst.UISound(9);
        goHomeObj.SetActive(true);
        Invoke("GoHomeScene", 1.8f);
    }

    public void GoHomeScene()
    { DataController.inst.LoadMainScene(); }

}
