using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : Singleton<PlayerManager>
{
    // 내부 변수
    private int numOfBullets;
    public uint hp;
    public float noHitTime;
    public UnityEvent _stateChageEvent;

    // 오브젝트들
    // TODO 탄환 스포너
    
    // Scriptable Object
    public PlayerStateContainer _PlayerStateContainer;
    private PlayerState CURRENT_STATE;

    // State Delay Timer
    public float stateChangeDelayMax;
    private float stateChangeDelay;
    
    //Shot Delay Timer
    public float shotDelayMax;
    private float shotDelay;
    
    public PlayerState _currentState
    {
        set
        {
            if (CURRENT_STATE != value)
            {
                _stateChageEvent.Invoke();
                CURRENT_STATE = value;
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
        _currentState = _PlayerStateContainer.PlayerStates["DefenceState"];
        _stateChageEvent.AddListener(() =>
        {
            Debug.Log("State Changed. Before State: " + _currentState._stateName);
        });

        // set default value
        stateChangeDelay = 0f;
        shotDelay = 0f;
        numOfBullets = 0;
        
        
        Debug.Log("Game Ready");
    }

    private void Update()
    {
        // 상태 변경
        if (Input.GetKeyDown(KeyCode.LeftControl) && stateChangeDelay <= 0)
        {
            ChangeState();
        }

        // 탄환 발사
        if (Input.GetKeyDown(KeyCode.Space) && shotDelay <= 0)
        {
            
            if (numOfBullets > 0)
            {
                --numOfBullets;
                Shot();
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
        if (shotDelay > 0)
        {
            shotDelay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet"))
        {
            if (_currentState._isGetDamage)
            {
                // 데이미지를 받는 경우
                StartCoroutine(ChangeStateAfterSec(_currentState));
                _currentState = _PlayerStateContainer.PlayerStates["HitState"];
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
                    // 방어 상태인 경우
                    numOfBullets++;
                }
            }
            Destroy(other);
        }
    }

    #endregion

    #region Custom Method
    
    private void Shot()
    {
        // TODO 탄환 스포너에서 물체 발사
        Debug.Log("플레이어 총알 발사!");
        shotDelay = shotDelayMax;
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
        }
        else if (_currentState._stateName.Equals("DefenceState"))
        {
            Debug.Log("Set State Attack");
            _currentState = _PlayerStateContainer.PlayerStates["AttackState"];
        }

        stateChangeDelay = stateChangeDelayMax;
    }

    IEnumerator ChangeStateAfterSec(PlayerState state)
    {
        yield return new WaitForSeconds(noHitTime);
        _currentState = state;
    }

    #endregion
}
