using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodInvenController : MonoBehaviour{
    public static GodInvenController inst;

    public InvenSlot[] invenSlots;
    public GodInven[] godSlots;
    public GetItem getItem;
    public List<GodInven> godList;

    public GameObject effect, getResult;
    public Text countText;

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        godList = new List<GodInven>();
        for (int i = 0; i < 25; i++)
        { godSlots[i].thisInven = invenSlots[i]; }
    }

    private void OnEnable()
    {
        for(int i =0; i < 25; i++)
        { godSlots[i].SetGodInven(); }
    }

    private void OnDisable()
    {
        godList.Clear();
    }

    public void MakeClick()
    {
        if(godList.Count.Equals(5))
        {
            effect.SetActive(true);
            Invoke("SetMake", 3f);

            for (int i = 0; i < 5; i++)
            { godList[i].thisInven.Change(ItemSlotController.inst.equipSlots[0], true); }
            ItemSlotController.inst.FindEmptySlot();
            NumbToData.GetGodItem(getItem);
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[216], 50); }
    }

    public void SetMake()
    {
        gameObject.SetActive(false);
        getResult.SetActive(true);
    }

    public void ListSet(bool flag, GodInven god)
    {
        if(flag)
        {
            if (godList.Count < 5)
            {
                godList.Add(god);
                god.checkObj.SetActive(true);
            }
            else
            { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[215], 50); }
        }
        else
        {
            godList.Remove(god);
            god.checkObj.SetActive(false);
        }
        countText.text = string.Format("{0}/5", godList.Count);
    }
}
