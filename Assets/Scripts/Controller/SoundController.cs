using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public static SoundController inst;

    public AudioClip[] UIList, attackList, smithList;
    public AudioClip cri;
    public AudioSource BGM, UI, Effect;
    private WaitForSeconds oneSec;
    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        DontDestroyOnLoad(this);
        oneSec = new WaitForSeconds(1f);
    }

    void Start()
    {
        PrivateSet();
    }

    public void BGMStart()
    {
        BGM.Play();
    }

    public void CharacterAttackSound()
    {
        int r = Random.Range(0, attackList.Length);
        Effect.PlayOneShot(attackList[r]);
    }

    public void UISound(int id)
    {
        UI.PlayOneShot(UIList[id]);
    }

    public void AttackSound(AudioClip sound)
    { Effect.PlayOneShot(sound); }

    public void CriSound()
    { UI.PlayOneShot(cri); }

    public void SkillSound(AudioClip clip)
    { Effect.PlayOneShot(clip); }

    private void PrivateSet()
    {
        if (DataController.inst.S_BGM.Equals(true))
        {
            BGM.Play();
            BGM.volume = 0.7f;
        }
        else
        { BGM.volume = 0f; }
        
        if (DataController.inst.S_Effect.Equals(false))
        {
            UI.volume = 0f;
            Effect.volume = 0f;
        }
        else
        {
            UI.volume = 1f;
            Effect.volume = 1f;
        }
    }

    public void SmithSound()
    { StartCoroutine(Smith()); }

    IEnumerator Smith()
    {
        yield return oneSec;
        UI.PlayOneShot(smithList[Random.Range(0, 4)]);
        yield return oneSec;
        UI.PlayOneShot(smithList[Random.Range(0, 4)]);
        yield return oneSec;
        UI.PlayOneShot(smithList[Random.Range(0, 4)]);
    }
}
