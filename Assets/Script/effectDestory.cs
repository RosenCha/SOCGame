using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDestory : MonoBehaviour
{
    public float existTime;
    void Start()
    {
        StartCoroutine("Kill", existTime);
    }
    IEnumerator Kill(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
