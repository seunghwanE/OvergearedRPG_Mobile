using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodInven : MonoBehaviour
{
    public Image itemImage;
    public GameObject checkObj;
    public InvenSlot thisInven;

    public void Click()
    {
        if(checkObj.activeInHierarchy)
        {
            GodInvenController.inst.ListSet(false, this);
        }
        else
        {
            GodInvenController.inst.ListSet(true, this);
        }
    }

    public void SetGodInven()
    {
        checkObj.SetActive(false);
        if (thisInven.rank.Equals("Ex"))
        {
            itemImage.sprite = thisInven.itemImage.sprite;
            gameObject.SetActive(true);
        }
        else
        { gameObject.SetActive(false); }
    }
}
