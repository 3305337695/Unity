using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionElasticDeformation : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.material.SetFloat("_ImpactTime", -100);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 获取碰撞点
        Vector2 impactPoint = collision.contacts[0].point;

        // 计算强度（基于相对速度）
        float strength = Mathf.Clamp01(collision.relativeVelocity.magnitude / 10f);

        // 传参到 Shader
        sr.material.SetVector("_ImpactPos", new Vector4(impactPoint.x, impactPoint.y, 0, 0));
        sr.material.SetFloat("_ImpactTime", Time.time);
        sr.material.SetFloat("_ImpactStr", strength);
    }
}
