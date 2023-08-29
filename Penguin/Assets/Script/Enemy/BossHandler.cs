using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHandler : MonoBehaviour
{
    public int health;
    // 타이머
    public float attackTimerMax;
    public float attackTimer;

    public BulletSpawner bulletSpawner;

    public List<ShootPattern> shootPatterns;

    // Events
    public UnityEvent OnDestroyEvent;
    
    // Move Pattern
    public List<MovePattern> Patterns;

    // Animator
    private Animator _animator;
    
    private void Start()
    {
        attackTimer = attackTimerMax;
        _animator = GetComponent<Animator>();
        StartCoroutine(MovePatternRoutine());
    }
    
    private void Update()
    {
        if (attackTimer < 0)
        {
            attackTimer = attackTimerMax;
            StartAttack();
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
    
    public void Shoot(int patternIndex)
    {
        ShootPattern pattern = shootPatterns[patternIndex];
        SFXManager.Instance.PlayOneShot(pattern._attackSFX);
        bulletSpawner.BulletSpawn(this.transform.position, pattern.quaternions, pattern.iterTime, pattern.shootDelay);
    }

    public void StartAttack()
    {
        ShootPattern pattern = shootPatterns[UnityEngine.Random.Range(0, shootPatterns.Count)];
        _animator.SetTrigger(pattern.triggerName);
    }
    private void OnDestroy()
    {
        OnDestroyEvent.Invoke();
    }

    IEnumerator MovePatternRoutine()
    {
        if (Patterns == null)
        {
            Debug.LogError("There is no patterns.");
            yield break;
        }
        while (this.gameObject != null)
        {
            foreach (var pattern in Patterns)
            {
                PatternCommand command = null;
                switch (pattern)
                {
                    case MovePattern.MoveHorizontal:
                    {
                        command = MoveService.Instance.GetMoveHorizontalPattern(this.transform.position);
                        break;
                    }
                    case MovePattern.MovePosition:
                    {
                        command = MoveService.Instance.GetMovePattern(this.transform.position);
                        break;
                    }
                    case MovePattern.Stop:
                    {
                        command = MoveService.Instance.GetStopPattern(this.transform.position);
                        break;
                    }
                    case MovePattern.MovePoint:
                    {
                        command = MoveService.Instance.GetMovePointPattern(this.transform.position);
                        break;
                    }
                    case MovePattern.StopExactly:
                    {
                        command = MoveService.Instance.GetStopExactlyPattern(this.transform.position, 5f);
                        break;
                    }
                }

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
            float interpolate = Time.deltaTime / timer;
            this.transform.position = Vector3.Lerp(transform.position, position, interpolate);
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
