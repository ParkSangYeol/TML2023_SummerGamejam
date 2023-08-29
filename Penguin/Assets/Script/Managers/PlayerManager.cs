using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    // 내부 변수
    private int NUM_OF_BULLETS;
    public int numOfBullets
    {
        set
        {
            if (NUM_OF_BULLETS != value)
            {
                NUM_OF_BULLETS = value;
                _onBulletsChangeEvent.Invoke(NUM_OF_BULLETS);
            }
        }
        get
        {
            return NUM_OF_BULLETS;
        }
    }
    private int HP;
    public int maxHP;
    public int hp
    {
        set
        {
            if (HP != value)
            {
                if (value <= maxHP)
                {
                    HP = value;
                    _onHpChangeEvent.Invoke(HP);
                }

                if (value <= 0)
                {
                    _onDeadEvent.Invoke();
                }
            }
        }
        get
        {
            return HP;
        }
    }

    private int POINT;

    public int point
    {
        set
        {
            if (POINT != value)
            {
                POINT = value;
                _onChangePoint.Invoke(POINT);
            }

            if (value > 8)
            {
                GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().stopTrigger = true;
            }
        }
        get
        {
            return POINT;
        }
    }
    
    public float noHitTime;
    
    //Events
    public UnityEvent<PlayerState> _stateChageEvent;
    public UnityEvent<int> _onHpChangeEvent;
    public UnityEvent _onDeadEvent;
    public UnityEvent _onHitEvent;
    public UnityEvent<int> _onBulletsChangeEvent;
    public UnityEvent<int> _onChangePoint;
    public UnityEvent _onAttack;

    // Scriptable Object
    public PlayerStateContainer _PlayerStateContainer;
    private PlayerState CURRENT_STATE;

    // State Delay Timer
    public float stateChangeDelayMax;
    private float stateChangeDelay;
    
    //Shot Delay Timer
    public float shotDelayMax;
    private float shootDelay;
    
    // Player Bullet Sapwner
    public BulletSpawner _BulletSpawner;
    
    // Player 발사 각도
    public List<Quaternion> Quaternions;
    
    // Player 발사 반복 횟수
    public int iterTime;
    
    // AudioClips
    [SerializeField] 
    private AudioClip _attackSFX;
    [SerializeField] 
    private AudioClip _hitSFX;
    [SerializeField] 
    private AudioClip _deadSFX;
    [SerializeField] 
    private AudioClip _stateChageSFX;
    [SerializeField] 
    private AudioClip _healSFX;
    [SerializeField] 
    private AudioClip _getSFX;

    // 최대 수비모드 시간
    public float maxDefenceTime;
    
    public PlayerState _currentState
    {
        set
        {
            if (CURRENT_STATE != value)
            {
                CURRENT_STATE = value;
                _stateChageEvent.Invoke(value);
            }
        }
        get
        {
            return CURRENT_STATE;
        }
    }

    #region UnityMethod

    private void Start()
    {
        _stateChageEvent.AddListener(state =>
        {
            Debug.Log("State Changed. current State: " + _currentState._stateName);
        });
        _onHitEvent.AddListener(() =>
        {
            // 상태 변경
            StartCoroutine(ChangeStateAfterSec(_currentState, noHitTime));
            _currentState = _PlayerStateContainer.PlayerStates["HitState"];
        });
        
        _onHitEvent.AddListener(() =>
        {
            // 애니메이션 처리
            StartCoroutine(HitAnimation(this.GetComponent<SpriteRenderer>()));
        });
        _onDeadEvent.AddListener(() =>
        {
            this.GetComponent<Animator>().SetTrigger("Die");
            this.GetComponent<SpriteRenderer>().color = Color.white;
        });
        _onAttack.AddListener(() =>
        {
            SFXManager.Instance.PlayOneShot(_attackSFX);
        });
        _onHitEvent.AddListener(() =>
        {
            SFXManager.Instance.PlayOneShot(_hitSFX);
        });
        _onDeadEvent.AddListener(() =>
        {
            SFXManager.Instance.PlayBGMOneShot(_deadSFX);
        });
        _stateChageEvent.AddListener( state =>
        {
            SFXManager.Instance.PlayOneShot(_stateChageSFX);
        });
        
        ReSet();
        
        Debug.Log("Game Ready");
    }

    public void ReSet()
    {
        // set default value
        numOfBullets = 10;
        hp = maxHP;
        this._BulletSpawner = GameObject.Find("PlayerBulletSpawner").GetComponent<BulletSpawner>();
        
        this.GetComponent<Animator>().SetBool("isDead", false);
        this.GetComponent<Animator>().SetTrigger("Reset");
        
        _currentState = _PlayerStateContainer.PlayerStates["DefenceState"];
        StartCoroutine(ChangeAttackDelay(maxDefenceTime));
    }
    
    private void Update()
    {
        // 상태 변경
        if (Input.GetKeyDown(KeyCode.LeftControl) && stateChangeDelay <= 0)
        {
            ChangeState();
        }

        // 탄환 발사
        if (_currentState._stateName.Equals("AttackState") && Input.GetKeyDown(KeyCode.Space) && shootDelay <= 0)
        {
            
            if (numOfBullets > 0)
            {
                --numOfBullets;
                Shoot();
            }
        }
        
        //Timers
        // Delay시간 설정되있을 경우, 감소
        // 플레이어의 상태 변화에 대한 타이머
        if (stateChangeDelay > 0)
        {
            stateChangeDelay -= Time.deltaTime;
        }
        // 플레이어의 공격 딜레이
        if (shootDelay > 0)
        {
            shootDelay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("EnemyBullet"))
        {
            BulletHandler handler = other.GetComponent<BulletHandler>();

            if (handler.damage < 0)
            {
                hp -= handler.damage;
                SFXManager.Instance.PlayOneShot(_healSFX);
            }
            else if (_currentState._isGetDamage)
            {
                // 데이미지를 받는 경우
                _onHitEvent.Invoke();
                hp -= handler.damage;
            }
            else
            {
                // 데미지를 받지 않는 경우
                if (_currentState._stateName.Equals("HitState"))
                {
                    // 무적 시간인 경우
                    
                }
                else if (_currentState._stateName.Equals("DefenceState"))
                {
                    if (handler.isPoison)
                    {
                        _onHitEvent.Invoke();
                        hp -= handler.damage;
                    }
                    else
                    {
                        // 방어 상태인 경우
                        SFXManager.Instance.PlayOneShot(_getSFX);
                        ++numOfBullets;
                        Debug.Log(numOfBullets);
                    }
                }
            }
            Destroy(other.gameObject);
        }
    }

    #endregion

    #region Custom Method
    
    private void Shoot()
    {
        _BulletSpawner.BulletSpawn(this.transform.position, Quaternions, iterTime, shootDelay);
        _onAttack.Invoke();
        shootDelay = shotDelayMax;
    }
    
    public void ChangeState()
    {
        Debug.Log("Call Change State");
        if (_currentState._stateName.Equals("HitState"))
        {
            Debug.Log("Current State is Hit. State not Changed");
            return;
        }
        
        if (_currentState._stateName.Equals("AttackState"))
        {
            Debug.Log("Set State Defence");
            _currentState = _PlayerStateContainer.PlayerStates["DefenceState"];
            StartCoroutine(ChangeAttackDelay(maxDefenceTime));
        }
        else if (_currentState._stateName.Equals("DefenceState"))
        {
            Debug.Log("Set State Attack");
            _currentState = _PlayerStateContainer.PlayerStates["AttackState"];
        }

        stateChangeDelay = stateChangeDelayMax;
    }

    IEnumerator ChangeStateAfterSec(PlayerState state, float timer)
    {
        yield return new WaitForSeconds(timer);
        _currentState = state;
    }

    IEnumerator ChangeAttackDelay(float timer)
    {
        yield return new WaitForSeconds(timer);
        if (_currentState._stateName.Equals("DefenceState"))
        {
            _currentState = _PlayerStateContainer.PlayerStates["AttackState"];
        }
        else if(_currentState._stateName.Equals("HitState"))
        {
            StartCoroutine(ChangeAttackDelay(noHitTime));
        }
    }
    IEnumerator HitAnimation(SpriteRenderer renderer)
    {
        Color baseColor = renderer.color;
        renderer.color = Color.red;

        yield return new WaitForSeconds(noHitTime);

        renderer.color = baseColor;
    }

    public void AfterDeadAnimation()
    {
        Time.timeScale = 0;
    }
    
    #endregion
}
