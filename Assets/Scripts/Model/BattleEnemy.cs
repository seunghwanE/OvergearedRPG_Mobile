using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemy : MonoBehaviour
{
    public SpriteRenderer enemySprite;
    public Animator anim;

    public void Hit()
    {
        anim.SetTrigger("Hit_t");
    }
}
