using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetController : MonoBehaviour{
    public static PetController inst;

    public GameObject menuContent, effect, result;

    public Image petImage;
    public Text gradeText, nameText, levelText, atText, rateText;
    public Pet[] pets;

    private void Awake()
    {
        if(inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            DataController.inst.LoadPet(pets[i]);
            pets[i].UpdatePet();
        }
        if (MainSceneController.inst != null)
        { rateText.text = string.Format("<color=orange>{0} : 5%</color>\n<color=magent>{1} : 30%</color>\n<color=silver>{2} : 65%</color>", Language.inst.strArray[200], Language.inst.strArray[201], Language.inst.strArray[202]); }
    }
    

    public void SetOffContents()
    {
        InputController.inst.menuBack.gameObject.SetActive(false);
        menuContent.SetActive(false);
        result.SetActive(true);
    }

    public void BuyPet()
    {
        if(DataController.inst.ruby > 300 && DataController.inst.iron > 500)
        {
            int r = Random.Range(0, 100);
            MainSceneController.inst.UpdateIron(-500);
            MainSceneController.inst.UpdateRuby(-300);
            effect.SetActive(true);
            Invoke("SetOffContents", 1f);

            if (r < 5)
            { BuyPet(0); }
            else if (r < 35)
            { BuyPet(1); }
            else
            { BuyPet(2); }
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 50); }
    }
    public void OneMore()
    {
        if (DataController.inst.ruby > 100 && DataController.inst.iron > 500)
        {
            MainSceneController.inst.UpdateIron(-500);
            MainSceneController.inst.UpdateRuby(-100);
            effect.SetActive(true);
            Invoke("BuyPetOneMore", 1f);
        }
        else
        { ErrorSign.inst.SimpleSignSet(Language.inst.strArray[21], 50); }
    }
    public void BuyPetOneMore()
    {
        int r = Random.Range(0, 1000);
        
        if (r < 50)
        { BuyPet(0); }
        else if (r < 350)
        { BuyPet(1); }
        else
        { BuyPet(2); }
    }
    public void BuyPet(int numb)
    {
        pets[numb].level++;
        DataController.inst.SavePet(pets[numb]);
        pets[numb].UpdatePet();
        UpdateResult(pets[numb]);
    }
   
    public void UpdateResult(Pet pet)
    {
        petImage.sprite = ImageController.inst.pets[pet.id];
        if (pet.id.Equals(0))
        {
            nameText.text = string.Format("<color=orange>{0}</color>", Language.inst.strArray[197]);
            gradeText.text = string.Format("<color=orange>{0}</color>", Language.inst.strArray[200]);
        }
        else if (pet.id.Equals(1))
        {
            nameText.text = string.Format("<color=magenta>{0}</color>", Language.inst.strArray[198]);
            gradeText.text = string.Format("<color=magenta>{0}</color>", Language.inst.strArray[201]);
        }
        else if (pet.id.Equals(2))
        {
            nameText.text = string.Format("<color=silver>{0}</color>", Language.inst.strArray[199]);
            gradeText.text = string.Format("<color=silver>{0}</color>", Language.inst.strArray[202]);
        }
        atText.text = string.Format("{0} : {1} {2}%", Language.inst.strArray[97], Language.inst.strArray[203], pet.totalAt * 100);
    }
}
