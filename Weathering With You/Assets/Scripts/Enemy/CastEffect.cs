using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastEffect : Effect
{
    [Header("预制体")]
    public GameObject prefab;

    [Header("时间间隔")]
    public float waitDuration;

    [Header("偏移")]
    public Vector3 offset;

    protected override void LaunchEffect(Character target)
    {
        StartCoroutine(Cast(target));
    }

    IEnumerator Cast(Character target)
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(prefab, target.transform.position + offset, Quaternion.identity);

            yield return new WaitForSeconds(waitDuration);
        }
    }
}
