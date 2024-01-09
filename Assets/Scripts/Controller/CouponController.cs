using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CouponController : MonoBehaviour {
    public static CouponController inst;
    public InputField couponButton;

    private string rewardText;
    private bool checkCoupon;


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


    public void UsedCoupon()
    {
        SoundController.inst.UISound(13);
        ErrorSign.inst.SimpleSignSet("이미 사용한 쿠폰입니다", 50);
        CanvasController.inst.CanvasShortShake();
    }
    public void CheckCoupon()
    {
        if (couponButton.text.Equals("191519Pass"))
        {
            checkCoupon = DataController.inst.es3.Load<bool>("191519Pass", false);
            if (checkCoupon.Equals(false))
            {
                GetCoupon(0);
                DataController.inst.es3.Save<bool>("191519Pass", true);
            }
            else
            { UsedCoupon(); }
        }
        else
        {
            InputController.inst.CouponCheck(couponButton.text);
        }
        couponButton.text = string.Empty;
    }

    private void GetCoupon(int numb)
    {
        SoundController.inst.UISound(5);
        if (numb.Equals(0))
        {
            DataController.inst.IAPAD = false;
            DataController.inst.GG(999999999);
            DataController.inst.RR(999999999);
            DataController.inst.II(999999999);
            rewardText = Language.inst.strArray[188];
        }
        
        SimpleSign.inst.SimpleSignSet(rewardText, 0);
    }
}
