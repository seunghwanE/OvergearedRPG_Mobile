using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    public int level, id;
    public GameObject uiObj;
    public float at, totalAt;
    public PetCharacter petCharacter;
    public Text levelText, atText;

    

    public void UpdatePet()
    {
        if(level > 0)
        {
            petCharacter.gameObject.SetActive(true);
            
            uiObj.SetActive(true);
            levelText.text = string.Format("Lv.{0}", level);
            totalAt = at + (at * (level - 1) * 0.5f);
            atText.text = string.Format("{0}%", totalAt * 100);
            petCharacter.at = totalAt;

            if (id.Equals(0))
            { petCharacter.anim.speed = 1f; petCharacter.invokeTime = 0.6f; }
            else if(id.Equals(1))
            { petCharacter.anim.speed = 1.15f; petCharacter.invokeTime = 0.4f; }
            else if (id.Equals(2))
            { petCharacter.anim.speed = 1.25f; petCharacter.invokeTime = 0.15f; }
        }
        else
        {
            petCharacter.gameObject.SetActive(false);
            uiObj.SetActive(false);
        }
    }
}
