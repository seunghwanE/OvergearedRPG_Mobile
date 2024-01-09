using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Text damageText;
    protected ObjectPooling Pool;

    public void SetOff()
    {
        Push();
    }
    

    public virtual void Create(ObjectPooling pool)
    {
        Pool = pool;
        transform.SetParent(Pool.transform);
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }

    public virtual void Push()
    {
        if (Pool != null)
        { Pool.PushObject(this); }
        else
        { gameObject.SetActive(false); }
    }
}
