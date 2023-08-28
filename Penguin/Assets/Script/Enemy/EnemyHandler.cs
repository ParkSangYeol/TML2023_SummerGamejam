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
    
    // Move Pattern
    public List<MovePattern> Patterns;
    
    private void Start()
    {
        attackTimer = attackTimerMax;
        StartCoroutine(MovePatternRoutine());
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

    IEnumerator MovePatternRoutine()
    {
        while (this.gameObject != null)
        {
            foreach (var pattern in Patterns)
            {
                PatternCommand command = null;
                switch (pattern)
                {
                    case MovePattern.MoveHorizontal:
                    {
                        command = MoveService.GetMoveHorizontalPattern(this.transform.position);
                        break;
                    }
                    case MovePattern.MovePosition:
                    {
                        command = MoveService.GetMovePattern(this.transform.position);
                        break;
                    }
                    case MovePattern.Stop:
                    {
                        command = MoveService.GetStopPattern(this.transform.position);
                        break;
                    }
                }
                Debug.Log("[Command] position:" + command.pos);

                StartCoroutine(MoveToPosition(command.pos, command.duration));
                yield return new WaitForSeconds(command.duration);
            }
        }
    }

    IEnumerator MoveToPosition(Vector3 position, float duration)
    {
        float timer = duration;
        while (timer > 0)
        {
            float interpolate = Time.deltaTime / duration;
            Debug.Log("[Interpolate] interpolate:" + interpolate);
            this.transform.position = Vector3.Lerp(transform.position, position, interpolate);
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
