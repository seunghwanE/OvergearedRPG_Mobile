using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTimer : MonoBehaviour
{
    public Slider timerSlider;
    public Text timeText;
    private WaitForSeconds zeroOneSec,  twoSec;
    public Coroutine timer;
    public float time;

    private float sec;
    private void Awake()
    {
        zeroOneSec = new WaitForSeconds(0.01f);
        twoSec = new WaitForSeconds(2f);
    }

    private void OnEnable()
    { timer = StartCoroutine(Timer()); }
    private void OnDisable()
    { StopCoroutine(timer); }

    IEnumerator Timer()
    {
        sec = time;
        timerSlider.maxValue = time;
        timerSlider.value = timerSlider.maxValue;

        string secStr;
        string[] splited;

        bool flag = true;
        timeText.text = "15:00";
        yield return twoSec;

        while (flag)
        {
            sec -= Time.deltaTime;
            if (sec < 10)
            { secStr = string.Format("0{0:N2}", sec); }
            else
            { secStr = string.Format("{0:N2}", sec); }
            splited = secStr.Split('.');
            if(splited[1].Length.Equals(1))
            { splited[1] += "0"; }

            timeText.text = string.Format("{0} : {1}", splited[0], splited[1]);
            timerSlider.value = sec;
            yield return zeroOneSec;
            if (sec < 0)
            { timeText.text = "00 : 00"; flag = false; break; }
        }
        SoundController.inst.UISound(8);
        BattleController.inst.deadSign.SetActive(true);
        gameObject.SetActive(false);
    }
}
