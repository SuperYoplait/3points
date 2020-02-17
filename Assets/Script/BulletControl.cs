﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl : MonoBehaviour
{
    public float BulletSpeed;
    public int BulletDamage;

    public static int bDamage;

    private Transform transform;
    Vector3 moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        bDamage = BulletDamage;
        if (PlayerMove.Pleft)
        {
            moveAmount = BulletSpeed * Vector3.left * Time.deltaTime;
        }
        else if (!PlayerMove.Pleft)
        {
            moveAmount = BulletSpeed * Vector3.right * Time.deltaTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(moveAmount);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            Destroy(gameObject);
            EnemyMove.Hp -= bDamage;
        }
    }
}
