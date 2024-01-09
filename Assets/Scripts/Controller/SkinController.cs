using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinController : MonoBehaviour{
    public static SkinController inst;

    public SkinButton[] skins;
    public SkinButton selectSkin;
    public GameObject buyPop, goldIcon, ironIcon, rubyIcon;
    public Text costText, buyCountText;
    public int cost;
    public string moneyKind;

    public Slider slider;
    public Image[] icons;
    public Text[] texts;

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
        for(int i = 0; i < skins.Length; i++)
        {
            DataController.inst.LoadSkin(skins[i]);
            skins[i].UpdateUI();
        }
        slider.maxValue = 12;
        slider.value = DataController.inst.buySkinCount;
        SetSlider();
    }

    public void BuyPopUpdateUI(string money, int _cost)
    {
        moneyKind = money;
        if (money.Equals("gold"))
        { goldIcon.SetActive(true); rubyIcon.SetActive(false); ironIcon.SetActive(false); }
        else if (money.Equals("iron"))
        { goldIcon.SetActive(false); rubyIcon.SetActive(false); ironIcon.SetActive(true); }
        else if (money.Equals("ruby"))
        { goldIcon.SetActive(false); rubyIcon.SetActive(true); ironIcon.SetActive(false); }
        costText.text = NumbToData.GetInt(_cost);
        cost = _cost;

        buyPop.SetActive(true);
    }


    IEnumerator StepSlider(int numb)
    {
        for (float timer = 0; timer < 1f; timer += Time.deltaTime)
        {
            float progress = timer / 1f;
            slider.value = Mathf.Lerp(numb, numb+1, progress);
            yield return null;
        }
        slider.value = numb + 1;
        SetSlider();
    }

    public void SetSlider()
    {
        int numb = DataController.inst.buySkinCount;
        PlayerController.inst.skinAt = 0;
        PlayerController.inst.skinAtRate = 0f;
        if (numb > 2)
        {
            icons[0].color = Color.white;
            texts[0].color = Color.white;
            PlayerController.inst.skinAt = 500;
            if (numb > 5)
            {
                icons[1].color = Color.white;
                texts[1].color = Color.white;
                PlayerController.inst.skinAt = 1500;
                if (numb > 8)
                {
                    icons[2].color = Color.white;
                    texts[2].color = Color.white;
                    PlayerController.inst.skinAtRate = 0.3f;
                    if (numb > 11)
                    {
                        icons[3].color = Color.white;
                        texts[3].color = Color.white;
                        PlayerController.inst.skinAtRate = 1.3f;
                    }
                }
            }
        }
        PlayerController.inst.UpdateAbility();
    }

    public void BuySkin()
    {
        if (moneyKind.Equals("gold"))
        {
            if (DataController.inst.gold >= cost)
            { 
                DataController.inst.C_PaidGold++;
                MainSceneController.inst.UpdateGold(-cost);
                BuySuccess();
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); SoundController.inst.UISound(5); }
        }
        else if (moneyKind.Equals("iron"))
        {
            if (DataController.inst.iron >= cost)
            { 
                MainSceneController.inst.UpdateIron(-cost);
                BuySuccess();
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); SoundController.inst.UISound(5); }
        }
        else if (moneyKind.Equals("ruby"))
        {
            if (DataController.inst.ruby >= cost)
            {
                DataController.inst.C_PaidRuby++;
                MainSceneController.inst.UpdateRuby(-cost);
                BuySuccess();
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 20); SoundController.inst.UISound(5); }
        }
    }

    public void BuySuccess()
    {
        SoundController.inst.UISound(2);
        StartCoroutine(StepSlider(DataController.inst.buySkinCount));
        DataController.inst.buySkinCount++;
        selectSkin.isPurchased = true;
        selectSkin.LockImage.gameObject.SetActive(false);
        DataController.inst.SaveSkin(selectSkin);
        buyPop.SetActive(false);
        SimpleSign.inst.SimpleSignSet(Language.inst.strArray[55], 0);
    }
}
