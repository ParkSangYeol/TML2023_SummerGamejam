using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{    
    public EnemyContainer _EnemyContainer;
    public BulletSpawner _BulletSpawner;
    public int maxCount;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool stopTrigger = true;
    
    IEnumerator CreateEnemyRoutine()
    {
        while (stopTrigger)
        {
            if (count < maxCount)
            {
                EnemySpawn();
            }
            yield return new WaitForSeconds(3.0f);
        }
    }

    public void EnemySpawn()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 0.95f, 0));
        pos.z = 0.0f;
        var spawnObj = Instantiate(_EnemyContainer.GetRandomEnemy(), pos, Quaternion.identity);
        EnemyHandler handler = spawnObj.GetComponent<EnemyHandler>();
        handler.bulletSpawner = _BulletSpawner;
        handler.OnDestroyEvent.AddListener(() =>
        {
            count--;
        });
        count++;
    }
}
