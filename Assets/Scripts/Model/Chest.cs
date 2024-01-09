using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public int hp, currenthp, r;
    public GameObject effect;
    public Slider hpbar;

    public void SetChest()
    {
        effect.SetActive(false);
        currenthp = 10;
        hpbar.value = 10;
        gameObject.SetActive(true);
    }

    public void Click()
    {
        effect.gameObject.SetActive(false);
        effect.gameObject.SetActive(true);
        CanvasController.inst.CanvasShortShake();
        currenthp--;
        SoundController.inst.CharacterAttackSound();
        hpbar.value = currenthp;

        if (currenthp.Equals(0))
        {
            DataController.inst.C_ChestFive++;
            SoundController.inst.UISound(11);
            for (int i = 0; i < 10; i++)
            { BattleController.inst.moneyPool.PopMoney(transform, 0); }
            MainSceneController.inst.UpdateGold(PlayerController.inst.level * 1000);
            r = Random.Range(0, 3);
            if (r.Equals(0))
            {
                for (int i = 0; i < 3; i++)
                { BattleController.inst.moneyPool.PopMoney(transform, 2); }
                MainSceneController.inst.UpdateRuby((int)(PlayerController.inst.level * 0.5f) + 1);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                { BattleController.inst.moneyPool.PopMoney(transform, 1); }
                MainSceneController.inst.UpdateIron(PlayerController.inst.level);
            }
            gameObject.SetActive(false);
        }
    }
}
