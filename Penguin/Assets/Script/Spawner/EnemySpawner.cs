using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{    
    public EnemyContainer _EnemyContainer;
    public EnemyContainer _bossContainer;
    public BulletSpawner _BulletSpawner;
    public BulletSpawner _bossBulletSpawner;
    public int maxCount;
    int count = 0;
    public UnityEvent _onGameClear;

    [SerializeField] private AudioClip _monsterDeadSFX;
    [SerializeField] private AudioClip _monsterSpawnSFX;
    [SerializeField] private AudioClip _gameClearBGM;

    public PlayerManager playerManager;
    // Item Prefab
    public GameObject item;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool stopTrigger = false;
    
    IEnumerator CreateEnemyRoutine()
    {
        while (!stopTrigger)
        {
            if (count < maxCount)
            {
                EnemySpawn();
            }
            yield return new WaitForSeconds(3.0f);
        }
        
        BossSpawn();
    }

    public void EnemySpawn()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 0.95f, 0));
        pos.z = 0.0f;

        EnemyData enemyData = _EnemyContainer.GetRandomEnemy();
        var spawnObj = Instantiate(enemyData.Enemy, pos, Quaternion.identity);
        
        // 생성 사운드 출력
        SFXManager.Instance.PlayOneShot(_monsterSpawnSFX);
        
        // 이벤트 설정
        EnemyHandler handler = spawnObj.GetComponent<EnemyHandler>();
        handler.bulletSpawner = _BulletSpawner;
        handler.OnDestroyEvent.AddListener(() =>
        {
            // 현재 생성된 적의 수를 1 줄임
            count--;
            
            // 플레이어의 점수를 갱신
            playerManager.point += enemyData.point;
        });
        handler.OnDestroyEvent.AddListener(() =>
        {
            float randomNum = UnityEngine.Random.Range(0, 1f);
            Debug.Log("random num:" + randomNum);
            if (randomNum <= 0.1f)
            {
                // spawn item
                Instantiate(item, spawnObj.transform.position, Quaternion.identity);
            }
        });
        handler.OnDestroyEvent.AddListener(() => SFXManager.Instance.PlayOneShot(_monsterDeadSFX));
        count++;
    }

    public void BossSpawn()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 0.95f, 0));
        pos.z = 0.0f;

        EnemyData bossData = _bossContainer.GetRandomEnemy();
        var spawnObj = Instantiate(bossData.Enemy, pos, Quaternion.identity);
        
        // 생성 사운드 출력
        SFXManager.Instance.PlayLoop(bossData._spawnSFX);
        
        BossHandler handler = spawnObj.GetComponent<BossHandler>();
        handler.bulletSpawner = _bossBulletSpawner;
        handler.OnDestroyEvent.AddListener(() =>
        {
            // 플레이어의 점수를 갱신
            playerManager.point += bossData.point;
        });
        handler.OnDestroyEvent.AddListener(() =>
        {
            // 보스 처치
            _onGameClear.Invoke();
            Time.timeScale = 0f;
            SFXManager.Instance.PlayLoop(_gameClearBGM);
        });
    }
}
