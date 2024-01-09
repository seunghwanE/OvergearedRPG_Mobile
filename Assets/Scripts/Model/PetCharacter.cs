using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCharacter : MonoBehaviour
{
    public GameObject fireBall, explosion;

    public Transform startPos;

    public Animator anim;
    public Damage damage;
    public float invokeTime, at;
    public int finalAt;
    public Character character;

    private void OnEnable()
    {
        character = PlayerController.inst.character;
    }

    public void Attack()
    {
        if (character != null)
        {
            if (character.currentHp > 0)
            { StartCoroutine(MoveFireBall()); }
        }
    }

    public void SetDamage()
    {
        if (BattleController.inst != null)
        {
            if (BattleController.inst.battleEnemy != null)
            {
                if (BattleController.inst.battleEnemy.currentHp > 0)
                {
                    finalAt = (int)(PlayerController.inst.totalAt * at);
                    BattleController.inst.battleEnemy.HitPet(finalAt);
                    if (DataController.inst.S_Damage)
                    {
                        damage.gameObject.SetActive(true);
                        damage.damageText.text = NumbToData.GetIntGold(finalAt);
                    }
                }
            }
        }
        else
        {
            if (!BattleSceneController.inst.deadFlag)
            {
                finalAt = (int)(PlayerController.inst.totalAt * at);
                BattleSceneController.inst.HitPet(finalAt);
                if (DataController.inst.S_Damage)
                {
                    damage.gameObject.SetActive(true);
                    damage.damageText.text = NumbToData.GetIntGold(finalAt);
                }
            }
        }
    }

    IEnumerator MoveFireBall()
    {
        float duration = invokeTime;
        Vector2 newPos = Vector2.zero, startVec = startPos.position, endVec = explosion.transform.position;
        fireBall.SetActive(true);
        explosion.SetActive(false);

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            newPos = Vector3.Lerp(startVec, endVec, progress);
            fireBall.transform.position = newPos;
            yield return null;
        }
        fireBall.transform.position = newPos;
        fireBall.SetActive(false);
        explosion.SetActive(true);
        SetDamage();
    }
}
