using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    public int id;
    public Damage poolObj;
    public Money money;
    public int allocateCount;
    private Stack<Damage> damageStack;
    private Stack<Money> moneyStack;

    void Awake()
    {
        if (id.Equals(0))
        { damageStack = new Stack<Damage>(); AllocateDamage(); }
        else if (id.Equals(1))
        { moneyStack = new Stack<Money>();  AllocateMoney(); }
    }

    public void AllocateDamage()
    {
        for (int i = 0; i < allocateCount; i++)
        {
            Damage tObj = Instantiate(poolObj);
            tObj.Create(this);
            damageStack.Push(tObj);
        }
    }
    public void AllocateMoney()
    {
        for (int i = 0; i < allocateCount; i++)
        {
            Money tObj = Instantiate(money);
            tObj.Create(this);
            moneyStack.Push(tObj);
        }
    }

    public GameObject PopObject(Transform pos, string numb, int kind = 0)
    {
        if (damageStack.Count <= 0)
        {
            AllocateDamage();
        }

        Damage obj = damageStack.Pop();

        if (kind.Equals(1)) // miss
        { obj.damageText.color = Color.gray; obj.damageText.transform.localScale = Vector3.one; }
        else if(kind.Equals(2)) //cri
        { obj.damageText.color = Color.red; obj.damageText.transform.localScale = Vector3.one * 1.2f;  }
        else
        { obj.damageText.color = Color.white; obj.damageText.transform.localScale = Vector3.one; }

        obj.damageText.text = numb;
        obj.transform.position = pos.position;
        obj.gameObject.SetActive(true);
        return obj.gameObject;
    }

    public GameObject PopMoney(Transform pos, int numb)
    {
        if (moneyStack.Count <= 0)
        { AllocateMoney(); }

        Money obj = moneyStack.Pop();
        obj.transform.position = pos.position;
        obj.gameObject.SetActive(true);
        PopMoneySet(obj, numb);
        obj.Open();
        obj.StartPos(BattleController.inst.moneyPos[numb].position);
        return obj.gameObject;
    }

    public void PopMoneySet(Money obj, int numb)
    {
        if (numb.Equals(0))
        { obj.gold.gameObject.SetActive(true); obj.iron.gameObject.SetActive(false); obj.ruby.gameObject.SetActive(false); obj.stat.SetActive(false); obj.skill.SetActive(false); }
        else if (numb.Equals(1))
        { obj.gold.gameObject.SetActive(false); obj.iron.gameObject.SetActive(true); obj.ruby.gameObject.SetActive(false); obj.stat.SetActive(false); obj.skill.SetActive(false); }
        else if (numb.Equals(2))
        { obj.gold.gameObject.SetActive(false); obj.iron.gameObject.SetActive(false); obj.ruby.gameObject.SetActive(true); obj.stat.SetActive(false); obj.skill.SetActive(false); }
        else if (numb.Equals(3))
        { obj.gold.gameObject.SetActive(false); obj.iron.gameObject.SetActive(false); obj.ruby.gameObject.SetActive(false); obj.stat.SetActive(true); obj.skill.SetActive(false); }
        else if (numb.Equals(4))
        { obj.gold.gameObject.SetActive(false); obj.iron.gameObject.SetActive(false); obj.ruby.gameObject.SetActive(false); obj.stat.SetActive(false); obj.skill.SetActive(true); }
    }

    public void PushObject(Damage obj)
    {
        obj.gameObject.SetActive(false);
        damageStack.Push(obj);
    }

    public void PushObject(Money obj)
    {
        obj.gameObject.SetActive(false);
        moneyStack.Push(obj);
    }

}
