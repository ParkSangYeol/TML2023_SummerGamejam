using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public int health;
    // 타이머
    public float attackTimerMax;
    public float attackTimer;

    public BulletSpawner bulletSpawner;
    // 한번 발사시 반복 횟수
    public int iterTime;
    
    // 발사 각도
    public List<Quaternion> quaternions;

    private void Start()
    {
        attackTimer = attackTimerMax;
    }

    private void Update()
    {
        if (attackTimer < 0)
        {
            attackTimer = attackTimerMax;
            Shoot();
        }
        attackTimer -= Time.deltaTime;
    }

    public void DestroyEnemy()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        bulletSpawner.BulletSpawn(this.transform.position, quaternions, iterTime);
    }
}
