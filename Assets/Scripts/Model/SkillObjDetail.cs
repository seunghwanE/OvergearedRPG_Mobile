using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjDetail : MonoBehaviour
{
    public SkillObj obj;


    public void Attack()
    { obj.Attack(); }
    public void SetOff()
    { gameObject.SetActive(false); }
}
