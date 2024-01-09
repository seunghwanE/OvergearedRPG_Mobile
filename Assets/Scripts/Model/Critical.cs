using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critical : MonoBehaviour
{
    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-40f, 20f));
    }

    public void SetOff()
    {
        gameObject.SetActive(false);
    }
}
