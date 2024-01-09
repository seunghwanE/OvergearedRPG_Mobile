using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour{
    public static ImageController inst;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        DontDestroyOnLoad(this);
    }
    public Sprite noneSkill;
    public Sprite[] weapons, shields, helmets, amors, capes, acc, stones, backs, helpers, heads, bodies, hands1, arm0, arm1, leg0, leg1, skill, bows, boots, gloves, enemyFace, pets;
    public GameObject[] backPrefabs;
}
