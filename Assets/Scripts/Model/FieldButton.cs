using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldButton : MonoBehaviour
{
    public Button selectButton;
    public Text fieldText, storyText;
    public GameObject lockObj;
    public Image characterImage;
    public int id;


    public void Click()
    {
        if (id.Equals(DataController.inst.field))
        {
            InputController.inst.OtherContentsTurnOff();
        }
        else
        {
            MainSceneController.inst.cloudObj.SetActive(true);
            Invoke("SecondCloud", 1.9f);
        }
    }
    public void SecondCloud()
    {
        DataController.inst.field = id;
        //if (id.Equals(DataController.inst.highestField))
        //{ DataController.inst.stageNumb = 0; }
        //else
        //{ DataController.inst.stageNumb = 9; }

        BattleController.inst.ChangeField();
        InputController.inst.OtherContentsTurnOff();
        MainSceneController.inst.cloudObj2.SetActive(true);
    }


    public void UpdateUI()
    {
        if (id < 49)
        {
            fieldText.text = Language.inst.fieldArr[id];
            if (DataController.inst.highestField >= id)
            {
                lockObj.SetActive(false);
                selectButton.gameObject.SetActive(true);
                characterImage.color = Color.white;
            }
            else
            {
                lockObj.SetActive(true);
                selectButton.gameObject.SetActive(false);
                characterImage.color = Color.gray;
            }
        }
        else
        {
            if (id < 98)
            { fieldText.text = Language.inst.fieldArr[id - 49]; }
            else
            { fieldText.text = Language.inst.fieldArr[id - 98]; }

            if (DataController.inst.highestField >= id)
            {
                lockObj.SetActive(false);
                selectButton.gameObject.SetActive(true);
                characterImage.color = Color.gray;
            }
            else
            {
                lockObj.SetActive(true);
                selectButton.gameObject.SetActive(false);
                characterImage.color = Color.black;
            }
        }
    }
}
