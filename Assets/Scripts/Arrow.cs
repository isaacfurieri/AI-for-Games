using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Arrow : MonoBehaviour
{
    public GameObject hitEffect;
    public float arrowTime = 1f;
    public float timer = 0.0f;

    public Action<bool, Arrow, GameObject> OnHit;

    //private void Start()
    //{
    //    GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

    //    Destroy(effect, 2f);
    //}
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > arrowTime)
        {
            OnHit?.Invoke(false, this, this.gameObject);
            Destroy(gameObject);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect,1.5f);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            OnHit?.Invoke(false, this, collision.gameObject);
            Destroy(gameObject);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        if (collision.gameObject.CompareTag("Target"))
        {
            OnHit?.Invoke(true, this, collision.gameObject);
            Destroy(gameObject);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
