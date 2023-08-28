using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHandler : MonoBehaviour
{
    public int health;
    // 타이머
    public float attackTimerMax;
    public float attackTimer;

    public BulletSpawner bulletSpawner;
    // 발사 딜레이
    public float shootDelay;
    
    // 한번 발사시 반복 횟수
    public int iterTime;
    
    // 발사 각도
    public List<Quaternion> quaternions;

    // Events
    public UnityEvent OnDestroyEvent;
    
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerBullet"))
        {
            Damaged(other.GetComponent<BulletHandler>().damage);
            Destroy(other.gameObject);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void Damaged(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }
    
    public void Shoot()
    {
        bulletSpawner.BulletSpawn(this.transform.position, quaternions, iterTime, shootDelay);
    }

    private void OnDestroy()
    {
        OnDestroyEvent.Invoke();
    }
}
