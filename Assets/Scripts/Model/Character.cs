using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour{
    public Animator anim;
    public Slider hpBar;
    public SpriteRenderer headSkin, bodySkin, hand0Skin, hand1Skin, arm00Skin, arm01Skin, arm10Skin, arm11Skin, leg00Skin, leg01Skin, leg10Skin, leg11Skin,
                            helmet, amor, back, weapon, shield, cape, helper;
    public Text hpText;
    public bool isAttack, revive;
    public Wing[] backPrefabs;
    public Wing backObj;
    public Transform backParents;

    public WaitForSeconds time, attacktimer = new WaitForSeconds(0.2f);

    public Transform UIPos;
    private Coroutine attackCor;
    public int at, hp, currentHp, cri, avoid, luck, combo, comboCount;
    public bool immortality = false;
    public void AttackSkillCheck()
    {
        SkillEffectController.inst.AttackSkill();
    }

    public void Attack()
    {
        if (BattleController.inst != null)
        { BattleController.inst.battleEnemy.Hit(at); }
        else
        { BattleSceneController.inst.Hit(at); }
        CanvasController.inst.AttackShake();
    }
    public void AttackClick()
    {
        if(attackCor != null)
        { StopCoroutine(attackCor); }
        attackCor = StartCoroutine(AttackCor());
    }
    public void BattleClick()
    {
        BattleSceneController.inst.TouchHit((int)(at * 0.33f));
        AttackClick();
    }
    public void AttackFalse()
    {
        comboCount++;
        if (combo < comboCount)
        { anim.SetBool("Attack_b", false); }
    }

    public void ComboReset()
    { comboCount = 0; }
    IEnumerator AttackCor()
    {
        
        if (combo < comboCount)
        { anim.SetBool("Attack_b", false); }
        else
        { anim.SetBool("Attack_b", true); }
        yield return attacktimer;

        anim.SetBool("Attack_b", false);
    }

    public void SpeedUp(int speed)
    { anim.speed += speed * 0.01f; Invoke("ReSetAvoid", 5f); }
    public void AvoidUp(int _avoid)
    { avoid = (int)(avoid * _avoid * 0.01f); Invoke("ReSetAvoid", 5f); }
    public void Immotal()
    { immortality = true; Invoke("ReSetImmotal", 5f); }

    public void ReSetAvoid()
    { avoid = PlayerController.inst.statAvoid + PlayerController.inst.itemAvoid + PlayerController.inst.skillAvoid; }
    public void ReSetSpeed()
    { anim.speed = 1f; }
    public void ReSetImmotal()
    { immortality = false; }

    public void Revive()
    {
        if (revive)
        {
            revive = false;
            anim.SetBool("Dead_b", false);
            currentHp = hp;
            HpBarUpdateUI();
        }
        else
        { InvokeDead(); }
    }

    public void Hit(int damage)
    {
        if (currentHp > 0)
        {
            if (immortality)
            { BattleController.inst.damagePool.PopObject(UIPos, "0", 1); }
            else
            {
                if (avoid > 50)
                { avoid = 50; }
                if (Random.Range(0, 100) < avoid)
                { BattleController.inst.damagePool.PopObject(UIPos, "Miss", 1); }
                else
                {
                    SkillEffectController.inst.HitSkill();
                    if (DataController.inst.S_Damage)
                    { BattleController.inst.damagePool.PopObject(UIPos, damage.ToString("N0")); }
                    BattleController.inst.characterHitObj.SetActive(true);
                    currentHp -= damage;

                    if (currentHp < 1)
                    {
                        currentHp = 0;
                        anim.SetBool("Dead_b", true);
                        SkillEffectController.inst.DeadSkill();
                    }
                    HpBarUpdateUI();
                }
            }
        }
    }

    public void InvokeDead()
    { Invoke("DeadSign", 1f); }

    public void DeadSign()
    {
        if(currentHp < 1)
        {
            SoundController.inst.UISound(8);
            if(BattleController.inst != null)
            {
                BattleController.inst.bossBattleButton.gameObject.SetActive(true);
                BattleController.inst.Dead();
            }
        }
    }
    public void HpBarUpdateUI()
    {
        hpText.text = string.Format("{0} / {1}", NumbToData.GetInt(currentHp), NumbToData.GetInt(hp));
        hpBar.value = currentHp;
    }

    public void SetCharacterAbility(int _at, int _hp, int _cri, int _avoid, int _luck)
    {
        at = _at;
        hp = _hp;
        cri = _cri;
        avoid = _avoid;
        luck = _luck;

        currentHp = hp;
        hpBar.maxValue = hp;
        HpBarUpdateUI();
    }

    public void SetCharacter(int skinId, int weaponId, int shieldId, int helmetId, int amorId, int capeId, int helpId, int backId)
    {
        SetSkin(skinId);
        SetWeapon(weaponId);
        SetShield(shieldId);
        SetHelmet(helmetId);
        SetAmor(amorId);
        SetCape(capeId);
        SetBack(backId);
        SetHelper(helpId);
    }

    public void SetSkin(int _skinId)
    {
        SetSprite(headSkin, _skinId, ImageController.inst.heads);
        SetSprite(bodySkin, _skinId, ImageController.inst.bodies);
        SetSprite(hand0Skin, _skinId, ImageController.inst.hands1);
        SetSprite(hand1Skin, _skinId, ImageController.inst.hands1);
        SetSprite(arm00Skin, _skinId, ImageController.inst.arm0);
        SetSprite(arm01Skin, _skinId, ImageController.inst.arm1);
        SetSprite(arm10Skin, _skinId, ImageController.inst.arm0);
        SetSprite(arm11Skin, _skinId, ImageController.inst.arm1);
        SetSprite(leg00Skin, _skinId, ImageController.inst.leg0);
        SetSprite(leg01Skin, _skinId, ImageController.inst.leg1);
        SetSprite(leg10Skin, _skinId, ImageController.inst.leg0);
        SetSprite(leg11Skin, _skinId, ImageController.inst.leg1);
    }
    public void SetWeapon(int _weaponId)
    { SetSprite(weapon, _weaponId, ImageController.inst.weapons); }
    public void SetShield(int _shieldId)
    { SetSprite(shield, _shieldId, ImageController.inst.shields); }
    public void SetHelmet(int _helmetId)
    {SetSprite(helmet, _helmetId, ImageController.inst.helmets);}
    public void SetAmor(int _amorId)
    {SetSprite(amor, _amorId, ImageController.inst.amors);}
    public void SetCape(int _capeId)
    {SetSprite(cape, _capeId, ImageController.inst.capes);}

    public void SetBack(int _backId)
    {
        if(_backId.Equals(-1))
        {
            if(backObj != null)
            {
                Destroy(backObj.gameObject);
                backObj = null;
            }
        }
        else
        {
            if(backObj != null)
            {
                Destroy(backObj.gameObject);
                backObj = null;
            }
            backObj = Instantiate(backPrefabs[_backId], backParents);
        }
    }
    public void SetHelper(int _helperId)
    { SetSprite(helper, _helperId, ImageController.inst.helpers); }

    public void SetSprite(SpriteRenderer sprite, int numb, Sprite[] imageControllerSprites)
    {
        if (numb > -1)
        { sprite.sprite = imageControllerSprites[numb]; }
        else
        { sprite.sprite = null; }
    }

}
