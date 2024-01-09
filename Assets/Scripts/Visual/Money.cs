using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Image gold, iron, ruby;
    public GameObject stat, skill;
    protected ObjectPooling Pool;
    private WaitForSeconds oneSec = new WaitForSeconds(1f);

    public void Open()
    {
        Vector2 force = new Vector2(Random.Range(-100, 100), Random.Range(300, 400));
        rb2d.AddForce(force);
    }
    public void SetOff()
    {
        Push();
    }

    public void StartPos(Vector3 target)
    { StartCoroutine(StepScale(target)); }

    IEnumerator StepScale(Vector3 target)
    {
        yield return oneSec;
        Vector2 pos = transform.position;
        float duration = 0.25f;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            gameObject.transform.position = Vector3.Lerp(pos, target, progress);
            yield return null;
        }
        Push();
    }


    public virtual void Create(ObjectPooling pool)
    {
        Pool = pool;
        transform.SetParent(Pool.transform);
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }

    public virtual void Push()
    {
        Pool.PushObject(this);
    }
}
