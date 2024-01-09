using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSceneController : MonoBehaviour
{
    void Start()
    {
        DataController.inst.LoadBattleScene();
    }

}
