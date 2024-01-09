using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trail2D;

public class SettingController : MonoBehaviour{
    public static SettingController inst;

    public Toggle BGMToggle, EffectToggle, VibeToggle, ShakeToggle, damageToggle, pushToggle;

    public SpriteTrail[] spriteTrails;

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
    { StartSet(); }

    public void BGMToggleSet()
    {
        SoundController.inst.UISound(9);
        if (BGMToggle.isOn)
        {
            DataController.inst.S_BGM = true;
            SoundController.inst.BGM.volume = 0.7f;
        }
        else
        {
            DataController.inst.S_BGM = false;
            SoundController.inst.BGM.volume = 0f;
        }
        DataController.inst.es3.Sync();
    }

    public void SetLanguage(int numb)
    {
        if(numb.Equals(0))
        {
            DataController.inst.languageStr = "Korean";
            DataController.inst.es3.Sync();
        }
        else
        {
            DataController.inst.languageStr = "English";
            DataController.inst.es3.Sync();
        }
    }
    public void EffectToggleSet()
    {
        SoundController.inst.UISound(9);
        if (EffectToggle.isOn)
        {
            DataController.inst.S_Effect = true;
            SoundController.inst.UI.volume = 1f;
            SoundController.inst.Effect.volume = 1f;
        }
        else
        {
            DataController.inst.S_Effect = false;
            SoundController.inst.UI.volume = 0f;
            SoundController.inst.Effect.volume = 0f;
        }
        DataController.inst.es3.Sync();
    }

    public void VibeToggleSet()
    {
        SoundController.inst.UISound(9);
        if (VibeToggle.isOn)
        {
            DataController.inst.S_Vibe = true;
            DataController.inst.Vibrate(50);
        }
        else
        { DataController.inst.S_Vibe = false; }
        DataController.inst.es3.Sync();
    }
    public void ShakeToggleSet()
    {
        SoundController.inst.UISound(9);
        if (ShakeToggle.isOn)
        {
            DataController.inst.S_Shake = true;
            CanvasController.inst.CanvasShortShake();
        }
        else
        { DataController.inst.S_Shake = false; }
        DataController.inst.es3.Sync();
    }

    public void DamageToggleSet()
    {
        SoundController.inst.UISound(9);
        if (damageToggle.isOn)
        {
            DataController.inst.S_Damage = true;
            spriteTrails[0].properties.emitDistance = 0.15f;
            spriteTrails[1].properties.emitDistance = 0.15f;
            spriteTrails[2].properties.emitDistance = 0.1f;
            spriteTrails[0].properties.lifeTime = 0.2f;
            spriteTrails[1].properties.lifeTime = 0.2f;
            spriteTrails[2].properties.lifeTime = 0.4f;
        }
        else
        {
            DataController.inst.S_Damage = false;
            spriteTrails[0].properties.emitDistance = 15f;
            spriteTrails[1].properties.emitDistance = 15f;
            spriteTrails[2].properties.emitDistance = 10f;
            spriteTrails[0].properties.lifeTime = 0.5f;
            spriteTrails[1].properties.lifeTime = 0.5f;
            spriteTrails[2].properties.lifeTime = 1f;
        }
        DataController.inst.es3.Sync();
    }


    public void PushToggleSet()
    {
        SoundController.inst.UISound(9);
        if (pushToggle.isOn)
        { DataController.inst.S_Push = true; }
        else
        { DataController.inst.S_Push = false; }
        DataController.inst.es3.Sync();
    }

    public void StartSet()
    {
        if (DataController.inst.S_Vibe)
        { VibeToggle.isOn = true; }
        else
        { VibeToggle.isOn = false; }

        if (DataController.inst.S_BGM)
        { BGMToggle.isOn = true; }
        else
        { BGMToggle.isOn = false; }

        if (DataController.inst.S_Effect)
        { EffectToggle.isOn = true; }
        else
        { EffectToggle.isOn = false; }

        if (DataController.inst.S_Shake)
        { ShakeToggle.isOn = true; }
        else
        { ShakeToggle.isOn = false; }

        if (DataController.inst.S_Damage)
        {
            damageToggle.isOn = true;
            spriteTrails[0].properties.emitDistance = 0.15f;
            spriteTrails[1].properties.emitDistance = 0.15f;
            spriteTrails[2].properties.emitDistance = 0.1f;
            spriteTrails[0].properties.lifeTime = 0.2f;
            spriteTrails[1].properties.lifeTime = 0.2f;
            spriteTrails[2].properties.lifeTime = 0.4f;
        }
        else
        {
            spriteTrails[0].properties.emitDistance = 15f;
            spriteTrails[1].properties.emitDistance = 15f;
            spriteTrails[2].properties.emitDistance = 10f;
            spriteTrails[0].properties.lifeTime = 0.5f;
            spriteTrails[1].properties.lifeTime = 0.5f;
            spriteTrails[2].properties.lifeTime = 1f;
            damageToggle.isOn = false; }

        if (DataController.inst.S_Push)
        { pushToggle.isOn = true; }
        else
        { pushToggle.isOn = false; }
    }
}
